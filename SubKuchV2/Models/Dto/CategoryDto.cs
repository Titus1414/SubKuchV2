using System;

namespace SubKuchV2.Controllers
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StId { get; set; }
        public DateTime? Date { get; set; }
        
        public bool IsActive { get; set; }

        public string StoreName { get; set; }
        public string Category { get; set; }
        public int? Sid { get; set; }
    }
}