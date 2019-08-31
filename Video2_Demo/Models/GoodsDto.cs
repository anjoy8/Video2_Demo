using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Video2_Demo.Models
{
    public class GoodsDto
    {
        public string GoodsName { get; set; }
        public decimal Price { get; set; }
        public string CreateTime { get; set; }
        public string Content { get; set; }
        public bool IsDeleted { get; set; }
        public List<Items> Items { get; set; }

        public string BrandName { get; set; }

    }
}
