using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Thibeault.EindWerk.Api.Models.Input;
using Thibeault.EindWerk.Api.Models.Response;
using Thibeault.EindWerk.DataLayer.Interfaces;
using Thibeault.EindWerk.Objects.DataObjects;
using Thibeault.EindWerk.Services;
using Thibeault.EindWerk.Services.Rules.BusinessObjects;

namespace Thibeault.EindWerk.Api.Controllers
{
    [ApiController]
    [Route("API/[controller]")]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly IStockActionRepository actionRepository;
        private readonly ProductService productService;
        private readonly IMapper mapper;

        public ProductController(IProductRepository productRepository, IStockActionRepository actionRepository, ProductService productService, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.actionRepository = actionRepository;
            this.productService = productService;
            this.mapper = mapper;
        }

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> Index()
        {
            ObjectResult result;

            try
            {
                List<Product> products = await productRepository.GetProductsAsync();

                List<ProductDto> response = mapper.Map<List<ProductDto>>(products);

                result = Ok(response);
            }
            catch (Exception ex)
            {
                result = BadRequest(ex.Message);
            }

            return result;
        }

        [HttpPatch("GetProductBySerialNumber")]
        public async Task<IActionResult> GetProductBySerialNumberAsync([FromBody] int input)
        {
            ObjectResult result;

            try
            {
                Product product = await productRepository.GetProductBySerialNumberAsync(input);

                ProductDto response = mapper.Map<ProductDto>(product);

                result = Ok(response);
            }
            catch (Exception ex)
            {
                result = BadRequest(ex.Message);
            }

            return result;
        }

        [HttpDelete("DeleteProductBySerialNumber")]
        public async Task<IActionResult> DeleteProductBySerialNumberAsync([FromBody] int input)
        {
            ObjectResult result;

            try
            {
                Product product = await productRepository.GetProductBySerialNumberAsync(input);

                foreach (StockAction stockAction in product.StockActions)
                {
                    await actionRepository.DeleteStockActionAsync(stockAction);
                }

                await productRepository.DeleteProductAsync(input);

                result = Ok("Product is deleted");
            }
            catch (Exception ex)
            {
                result = BadRequest(ex.Message);
            }

            return result;
        }

        [HttpPost("addProduct")]
        public async Task<IActionResult> AddProductAsync([FromBody] AddProduct input)
        {
            ObjectResult result;

            try
            {
                if (input.Stock > 1)
                {
                    throw new Exception("To add a product the amount needs to be bigger than 1");
                }

                Product product = await productRepository.AddNewProductAsync();

                // get product ready for testing
                BO_Product productBo = mapper.Map<BO_Product>(input);

                productBo.Id = product.Id;
                // I don't allow the database to have a product without knowing who or when it was created
                productBo.CreatedBy = product.CreatedBy;
                productBo.CreatedOn = product.CreatedOn;

                StockAction initialAction = new(product, Objects.Enums.Action.Add, input.Stock);

                productBo.StockActions.Add(initialAction);

                if (productBo.IsValid)
                {
                    product = mapper.Map<Product>(productBo); ;

                    await productRepository.UpdateProduct(product);

                    ProductDto response = mapper.Map<ProductDto>(productBo);

                    result = Ok(response);
                }
                else
                {
                    ProductDto response = mapper.Map<ProductDto>(productBo);

                    result = BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                result = BadRequest(ex.Message);
            }

            return result;
        }

        [HttpPut("ChangeProductName")]
        public async Task<IActionResult> UpdateProductNameAsync([FromBody] ChangeProductName input)
        {
            ObjectResult result;

            try
            {
                Product product = await productRepository.GetProductBySerialNumberAsync(input.SerialNumber);

                // get that in the object for testing
                BO_Product productBo = mapper.Map<BO_Product>(product);

                productBo.Name = input.Name;

                if (productBo.IsValid)
                {
                    product = mapper.Map<Product>(productBo);

                    await productRepository.UpdateProduct(product);

                    ProductDto response = mapper.Map<ProductDto>(productBo);

                    result = Ok(response);
                }
                else
                {
                    ProductDto response = mapper.Map<ProductDto>(productBo);

                    result = BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                result = BadRequest(ex.Message);
            }

            return result;
        }

        [HttpGet("Get10MostPopularProducts")]
        private async Task<IActionResult> GetMostPopularProducts()
        {
            ObjectResult result;

            try
            {
                List<Product> products = await productService.MostPopularProducts(10);

                List<ProductDto> response = mapper.Map<List<ProductDto>>(products);

                result = Ok(response);
            }
            catch (Exception ex)
            {
                result = BadRequest(ex.Message);
            }

            return result;
        }

        [HttpPut("AddStockActionToProduct")]
        public async Task<IActionResult> AddStockActionToProductAsync([FromBody] AddStockAction input)
        {
            ObjectResult result;

            try
            {
                Product product = await productRepository.GetProductBySerialNumberAsync(input.ProductSerialNumber);

                BO_StockAction stockActionBo = mapper.Map<BO_StockAction>(input.StockAction);

                stockActionBo.Product = product;

                if (stockActionBo.IsValid)
                {
                    StockAction stockAction = mapper.Map<StockAction>(stockActionBo);

                    await actionRepository.AddNewStockActionAsync(stockAction);

                    product = await productRepository.GetProductBySerialNumberAsync(input.ProductSerialNumber);

                    // get that in the object for testing
                    BO_Product productBo = mapper.Map<BO_Product>(product);

                    if (productBo.IsValid)
                    {
                        product = mapper.Map<Product>(productBo);

                        await productRepository.UpdateProduct(product);

                        ProductDto response = mapper.Map<ProductDto>(productBo);

                        result = Ok(response);
                    }
                    else
                    {
                        ProductDto response = mapper.Map<ProductDto>(productBo);

                        result = BadRequest(response);
                    }
                }
                else
                {
                    StockActionResponse response = mapper.Map<StockActionResponse>(stockActionBo);

                    result = BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                result = BadRequest(ex.Message);
            }

            return result;
        }
    }
}