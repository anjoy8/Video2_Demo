using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Video2_Demo.Models
{
    public class GoodsEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime? CreateTime { get; set; }
        public bool IsDeleted { get; set; }
        public List<Items> Items { get; set; }
        public BrandsEntity Brands { get; set; }

    }
}
