using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ParkyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ParkyAPI.Repository.IRepository
{
  public  interface ITrailRepository
    {
        IEnumerable<Trail> GetTrails();
        IEnumerable<Trail> GetTrailsInNationalPark(int npId);

        Trail GetTrail(int Id);
        Trail GetTrail(string Name);

        bool CheckExistTrail(int Id);
        bool CheckExistTrail(string Name);

        bool CreateTrail(Trail Trail);
        bool UpdateTrail(Trail Trail);
        bool DeleteTrail(Trail Trail);

        bool save();
    }
}
