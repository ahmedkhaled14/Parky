using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using static ParkyAPI.Models.Trail;

namespace ParkyAPI.Models.DTO
{
    public class TrailInsertDTO
    {
        
       
        [Required]
        public string Name { get; set; }
        [Required]
        public double Distance { get; set; }
       

        public DifficultyType Difficulty  { get; set; }
       
        public int NationalParkId { get; set; }
       




    }
}
