using Microsoft.EntityFrameworkCore;
using SavePalestineApi.Models;

namespace SavePalestineApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public ICollection<Product> GetProducts()
        {
            return _context.Products.OrderBy(p => p.Id).ToList();
        }

        public Product GetProduct(int id)
        {
            return _context.Products.Where(p => p.Id == id).FirstOrDefault();
        }
        public Product AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }
        public Product UpdateProduct(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();
            return product;

        }

        public Product DeleteProduct(Product product)
        {
            _context.Remove(product);
            _context.SaveChanges();
            return product;
        }
    }
}
