
using ECommerce.Domain.Abstraction;
using ECommerce.Inferastructure.Data;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace ECommerce.Inferastructure.DataSeed;

public class DataSeed(AppDbContext _context) : IDataSeed
{
    void IDataSeed.DataSeed()
    {
        if (_context.Database.GetPendingMigrations().Any())
        {
            _context.Database.Migrate(); 
        }
        if (!_context.ProductTypes.Any())
        {
           var type = File.ReadAllText("..\\ECommerce.Inferastructure\\DataSeed\\JsonData\\types.json");

            var productypes = JsonSerializer.Deserialize<List<ProductType>>(type);
            if(productypes is not null  && productypes.Any())
            _context.ProductTypes.AddRange(productypes);
               
        }


        if (!_context.Brands.Any())
        {
            var brand = File.ReadAllText("..\\ECommerce.Inferastructure\\DataSeed\\JsonData\\brands.json");

            var producBrands = JsonSerializer.Deserialize<List<ProductBrand>>(brand);
            if (producBrands is not null && producBrands.Any())
                _context.Brands.AddRange(producBrands);
        }


        if (!_context.Products.Any())
        {
            var productsJson = File.ReadAllText("..\\ECommerce.Inferastructure\\DataSeed\\JsonData\\products.json");

            var products = JsonSerializer.Deserialize<List<Product>>(productsJson);

            if (products is not null && products.Any())
                _context.Products.AddRange(products);
        }

        _context.SaveChanges(); 
    }
}
