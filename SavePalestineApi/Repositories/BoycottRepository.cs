using Microsoft.EntityFrameworkCore;
using SavePalestineApi.Models;

namespace SavePalestineApi.Repositories
{
    public class BoycottRepository : IBoycottRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BoycottRepository(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public ICollection<Boycott> GetBoycotts()
        {
            return _context.Boycotts.OrderBy(f => f.Id).ToList();
        }

        public Boycott GetBoycott(int id)
        {
            return _context.Boycotts.Where(f => f.Id == id).FirstOrDefault();
        }

        public Boycott AddBoycott(Boycott boycott, IFormFile imageFile)
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

                boycott.ImageUrl = "https://localhost:7217/api/upload/" + uniqueFileName;

            }

            _context.Boycotts.Add(boycott);
            _context.SaveChanges();
            return boycott;
        }

        public Boycott UpdateBoycott(Boycott boycott, IFormFile newImageFile)
        {
            if (newImageFile != null && newImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + newImageFile.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    newImageFile.CopyTo(stream);
                }

                boycott.ImageUrl = "https://localhost:7217/api/upload/" + uniqueFileName;
            }
            _context.Entry(boycott).State = EntityState.Modified;
            _context.SaveChanges();
            return boycott;
        }
        public Boycott DeleteBoycott(Boycott boycott)
        {
            if (!string.IsNullOrEmpty(boycott.ImageUrl))
            {
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", Path.GetFileName(boycott.ImageUrl));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            _context.Remove(boycott);
            _context.SaveChanges();
            return boycott;
        }
    }
}
