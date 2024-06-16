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
    // TODO make a base Response

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

                List<ProductResponse> response = mapper.Map<List<ProductResponse>>(products);

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

                ProductResponse response = mapper.Map<ProductResponse>(product);

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

                await productRepository.DeleteProductAsync(input);

                result = Ok("Product is deleted");
            }
            catch (Exception ex)
            {
                result = BadRequest(ex.Message);
            }

            return result;
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProductAsync([FromBody] AddProduct input)
        {
            ObjectResult result;

            try
            {
                if (input.Stock < 1)
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
                    product = mapper.Map<Product>(productBo);

                    await actionRepository.ProductAddNewStockActionAsync(product, initialAction);

                    ProductResponse response = mapper.Map<ProductResponse>(productBo);

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
                    await productRepository.UpdateProductAsync(product);

                    ProductResponse response = mapper.Map<ProductResponse>(productBo);
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

        [HttpGet("GetMostPopularProducts")]
        public async Task<IActionResult> GetMostPopularProductsAsync()
        {
            ObjectResult result;

            try
            {
                List<Product> products = await productService.MostPopularProductsAsync(productRepository.QueryProducts(), 10);
                List<ProductResponse> response = mapper.Map<List<ProductResponse>>(products);

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
                if (input.StockAction.Action == Objects.Enums.Action.Reserved)
                {
                    throw new Exception("Not allowed to manually reserve products");
                }

                Product product = await productRepository.GetProductBySerialNumberAsync(input.ProductSerialNumber);

                StockAction stockAction = mapper.Map<StockAction>(input.StockAction);
                BO_Product productBo = mapper.Map<BO_Product>(product);

                productBo.StockActions.Add(stockAction);

                if (productBo.IsValid)
                {
                    product.Stock = productBo.Stock;
                    stockAction.Product = product;
                    await actionRepository.ProductAddNewStockActionAsync(product, stockAction);

                    ProductResponse response = mapper.Map<ProductResponse>(productBo);
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

        [HttpPut("RemoveStockActionFromProduct")]
        public async Task<IActionResult> RemoveStockActionFromProductAsync([FromBody] RemoveStockAction input)
        {
            ObjectResult result;

            try
            {
                Product product = await productRepository.GetProductBySerialNumberAsync(input.ProductSerialNumber);
                StockAction stockAction = product.StockActions.SingleOrDefault(sa => sa.Id == input.StockActionId)
                                                ?? throw new Exception("StockAction not found");

                product.StockActions.RemoveAll(sa => sa.Id == input.StockActionId);
                BO_Product productBo = mapper.Map<BO_Product>(product);

                if (productBo.IsValid)
                {
                    await actionRepository.DeleteStockActionAsync(stockAction);

                    product.Stock = productBo.Stock;
                    product.Reserved = productBo.Reserved;

                    await productRepository.UpdateProductAsync(product);

                    result = Ok(product);
                }
                else
                {
                    result = BadRequest("Does not create a valid product");
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