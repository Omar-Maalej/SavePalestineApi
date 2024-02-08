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
        public Product UpdateProduct(Product product, IFormFile newImageFile)
        {
            if (newImageFile != null && product.ImageUrl != null)
            {
                var existingImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", Path.GetFileName(product.ImageUrl));
                if (System.IO.File.Exists(existingImagePath))
                {
                    System.IO.File.Delete(existingImagePath);
                }
            }

            if (newImageFile != null && newImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + newImageFile.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    newImageFile.CopyTo(stream);
                }

                product.ImageUrl = "https://localhost:7217/api/upload/" + uniqueFileName;
            }
            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();
            return product;

        }

        public Product DeleteProduct(Product product)
        {
            if (product.ImageUrl != null)
            {
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", Path.GetFileName(product.ImageUrl));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            _context.Remove(product);
            _context.SaveChanges();
            return product;
        }
    }
}
