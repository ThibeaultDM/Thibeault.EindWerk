using Microsoft.EntityFrameworkCore;
using Thibeault.EindWerk.DataLayer.Interfaces;
using Thibeault.EindWerk.Objects.DataObjects;

namespace Thibeault.EindWerk.Services
{
    public class ProductService
    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<List<Product>> MostPopularProducts(int howMany)
        {
            List<Product> mostPopularProducts = await productRepository.QueryProducts()
                                                             .Select(p => new
                                                             {
                                                                 Product = p,
                                                                 OrderdAmount = p.StockActions
                                                                 .Where(s => s.Action != Objects.Enums.Action.Add)
                                                                 .Sum(s => s.Amount)
                                                             })
                       .OrderByDescending(an => an.OrderdAmount).Take(howMany).Select(an => an.Product).ToListAsync();

            return mostPopularProducts;
        }

        public async Task<Product> AddActionToProduct(Product product, StockAction action)
        {
            // Validity of action has already been checked
            if (action.Action == Objects.Enums.Action.Add)
            {
                product.Stock += action.Amount;
            }
            else if (action.Action == Objects.Enums.Action.Remove)
            {
                product.Stock -= action.Amount;
            }
            else
            {
                product.Stock += action.Amount;
            }

            product.StockActions.Add(action);

            await productRepository.UpdateProduct(product);

            return product;
        }

    }
}