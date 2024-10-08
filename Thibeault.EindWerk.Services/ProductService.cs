﻿using Microsoft.EntityFrameworkCore;
using Thibeault.EindWerk.Objects.DataObjects;

namespace Thibeault.EindWerk.Services
{
    public class ProductService
    {
        public async Task<List<Product>> MostPopularProductsAsync(IQueryable<Product> query, int howMany)
        {
            List<Product> mostPopularProducts = await query.Select(p => new
            {
                Product = p,
                OrderdAmount = p.StockActions
                .Where(s => s.Action != Objects.Enums.Action.Add).Sum(s => s.Amount)
            })
           .OrderByDescending(an => an.OrderdAmount).Take(howMany).Select(an => an.Product).ToListAsync();

            return mostPopularProducts;
        }

        // became rule should be unnecessary
        //public async Task<Product> AddActionToProduct(Product product, StockAction action)
        //{
        //    // Validity of action has already been checked
        //    if (action.Action == Objects.Enums.Action.Add)
        //    {
        //        product.Stock += action.Amount;
        //    }
        //    else if (action.Action == Objects.Enums.Action.Remove)
        //    {
        //        product.Stock -= action.Amount;
        //    }
        //    else
        //    {
        //        product.Stock += action.Amount;
        //    }

        //    product.StockActions.Add(action);

        //    await productRepository.UpdateProductAsync(product);

        //    return product;
        //}
    }
}