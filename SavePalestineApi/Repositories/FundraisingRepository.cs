using Microsoft.EntityFrameworkCore;
using SavePalestineApi.Models;

namespace SavePalestineApi.Repositories
{
    public class FundraisingRepository : IFundraisingRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FundraisingRepository(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public ICollection<Fundraising> GetFundraisings()
        {
            return _context.Fundraisings.OrderBy(f => f.Id).ToList();
        }

        public Fundraising GetFundraising(int id)
        {
            return _context.Fundraisings.Where(f => f.Id == id).FirstOrDefault();
        }

        public Fundraising AddFundraising(Fundraising fundraising, IFormFile imageFile)
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

                fundraising.ImageUrl = "https://localhost:7217/api/upload/" + uniqueFileName;

            }

            _context.Fundraisings.Add(fundraising);
            _context.SaveChanges();
            return fundraising;
        }

        public Fundraising UpdateFundraising(Fundraising fundraising, IFormFile newImageFile = null)
        {
            if (newImageFile != null && fundraising.ImageUrl != null)
            {
                var existingImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", Path.GetFileName(fundraising.ImageUrl));
                if (File.Exists(existingImagePath))
                {
                    File.Delete(existingImagePath);
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

   
                fundraising.ImageUrl = "https://localhost:7217/api/upload/" + uniqueFileName;
            }

            _context.Entry(fundraising).State = EntityState.Modified;
            _context.SaveChanges();
            return fundraising;
        }

        public Fundraising DeleteFundraising(Fundraising fundraising)
        {
            if (fundraising.ImageUrl != null)
            {
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", Path.GetFileName(fundraising.ImageUrl));
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
            }

            _context.Remove(fundraising);
            _context.SaveChanges();
            return fundraising;
        }
    }
}
