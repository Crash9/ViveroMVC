using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ViveroMVC.Models
{
    public class Plant
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public Double Price { get; set; }
        public int Stock { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
    }
}
