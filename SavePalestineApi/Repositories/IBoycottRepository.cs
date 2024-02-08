using SavePalestineApi.Models;

namespace SavePalestineApi.Repositories
{
    public interface IBoycottRepository
    {
        ICollection<Boycott> GetBoycotts();
        Boycott GetBoycott(int id);
        Boycott AddBoycott(Boycott boycott, IFormFile formFile);
        Boycott UpdateBoycott(Boycott boycott, IFormFile formFile);
        Boycott DeleteBoycott(Boycott boycott);

    }
}
