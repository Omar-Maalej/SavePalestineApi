﻿using SavePalestineApi.Models;

namespace SavePalestineApi.Repositories
{
    public interface IFundraisingRepository
    {
        ICollection<Fundraising> GetFundraisings();
        Fundraising GetFundraising(int id);
        Fundraising AddFundraising(Fundraising fundraising, IFormFile formFile);
        Fundraising UpdateFundraising(Fundraising fundraising, IFormFile formFile=null);
        Fundraising DeleteFundraising(Fundraising fundraising);

    }
}
