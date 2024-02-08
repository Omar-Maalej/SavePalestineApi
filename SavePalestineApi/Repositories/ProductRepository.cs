using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using SavePalestineApi.Models;
using static System.Net.WebRequestMethods;

namespace SavePalestineApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductRepository(ApplicationDbContext applicationDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _context = applicationDbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public ICollection<Product> GetProducts()
        {
            return _context.Products.OrderBy(p => p.Id).ToList();
        }

        public Product GetProduct(int id)
        {
            return _context.Products.Where(p => p.Id == id).FirstOrDefault();
        }
        public Product AddProduct(Product product, IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }
              
                product.ImageUrl = "https://localhost:7217/api/upload/" + uniqueFileName;
               
            }

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
