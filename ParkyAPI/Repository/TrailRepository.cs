using Microsoft.EntityFrameworkCore;
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
    public class TrailRepository : ITrailRepository
    {
        private readonly ApplicationDbContext db;

        public TrailRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public bool CheckExistTrail(int Id)
        {
            return db.Trails.Any(m => m.Id == Id);
        }

        public bool CheckExistTrail(string Name)
        {
            return db.Trails.Any(m => m.Name.ToLower() == Name.ToLower());

        }

        public bool CreateTrail(Trail Trail)
        {
            db.Trails.Add(Trail);
            return save();
        }


        public bool DeleteTrail(Trail Trail)
        {
            db.Trails.Remove(Trail);
            return save();
        }

        public Trail GetTrail(int Id)
        {
            return db.Trails.Include(m => m.NationalPark).FirstOrDefault(m => m.Id == Id);
        }

        public Trail GetTrail(string Name)
        {
            return db.Trails.Include(m => m.NationalPark).FirstOrDefault(m => m.Name.ToLower().Equals(Name.ToLower()));
        }

        public IEnumerable<Trail> GetTrails()
        {
            return db.Trails.Include(m => m.NationalPark).ToList();
        }

        public IEnumerable<Trail> GetTrailsInNationalPark(int npId)
        {
            return db.Trails.Include(m => m.NationalPark).Where(m => m.NationalParkId == npId).ToList(); 
        }

        public bool save()
        {
            return db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateTrail(Trail Trail)
        {
            db.Trails.Update(Trail);
            return save();
        }
    }
}
