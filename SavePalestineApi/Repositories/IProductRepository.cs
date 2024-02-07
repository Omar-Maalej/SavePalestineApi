using SavePalestineApi.Models;

namespace SavePalestineApi.Repositories
{
    public interface IProductRepository
    {
        ICollection<Product> GetProducts();
        Product GetProduct(int id);
        Product AddProduct(Product product);
        Product UpdateProduct(Product product);
        Product DeleteProduct(Product product);

    }
}
