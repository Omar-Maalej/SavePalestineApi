using SavePalestineApi.Models;

namespace SavePalestineApi.Repositories
{
    public interface IProductRepository
    {
        ICollection<Product> GetProducts();
        Product GetProduct(int id);
        Product AddProduct(Product product, IFormFile formFile);
        Product UpdateProduct(Product product, IFormFile formFile);
        Product DeleteProduct(Product product);

    }
}
