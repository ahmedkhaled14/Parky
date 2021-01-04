using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ParkyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ParkyAPI.Repository.IRepository
{
  public  interface INationalParkRepository
    {
        IEnumerable<NationalPark> GetNationalParks();

        NationalPark GetNationalPark(int Id);
        NationalPark GetNationalPark(string Name);

        bool CheckExistNationalPark(int Id);
        bool CheckExistNationalPark(string Name);

        bool CreateNationalPark(NationalPark nationalPark);
        bool UpdateNationalPark(NationalPark nationalPark);
        bool DeleteNationalPark(NationalPark nationalPark);

        bool save();
    }
}
