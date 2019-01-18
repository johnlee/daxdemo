using System;

namespace daxdemo.Models
{
    public class WriteViewModel
    {
        public string Name { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Description { get; set; }
    }
}
