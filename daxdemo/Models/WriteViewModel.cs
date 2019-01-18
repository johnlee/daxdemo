using System;
using System.ComponentModel.DataAnnotations;

namespace daxdemo.Models
{
    public class WriteViewModel
    {
        [Required]
        public string Name { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Description { get; set; }
    }
}
