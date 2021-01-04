using ParkyAPI.Data;
using ParkyAPI.Models;
using ParkyAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ParkyAPI.Repository
{
    public class NationalParkRepository : INationalParkRepository
    {
        private readonly ApplicationDbContext db;

        public NationalParkRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public bool CheckExistNationalPark(int Id)
        {
            return db.NationalParks.Any(m => m.Id == Id);
        }

        public bool CheckExistNationalPark(string Name)
        {
            return db.NationalParks.Any(m => m.Name.ToLower() == Name.ToLower());

        }

        public bool CreateNationalPark(NationalPark nationalPark)
        {
            db.NationalParks.Add(nationalPark);
            return save();
        }


        public bool DeleteNationalPark(NationalPark nationalPark)
        {
            db.NationalParks.Remove(nationalPark);
            return save();
        }

        public NationalPark GetNationalPark(int Id)
        {
            return db.NationalParks.Find(Id);
        }

        public NationalPark GetNationalPark(string Name)
        {
            return db.NationalParks.FirstOrDefault(m => m.Name.ToLower().Equals(Name.ToLower()));
        }

        public IEnumerable<NationalPark> GetNationalParks()
        {
            return db.NationalParks.ToList();
        }

        public bool save()
        {
            return db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateNationalPark(NationalPark nationalPark)
        {
            db.NationalParks.Update(nationalPark);
            return save();
        }
    }
}
