using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventorySystem.Models
{
    public class Warehouse
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        [Display(Name ="Name")]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name ="State")]
        public bool State { get; set; }
    }
}
