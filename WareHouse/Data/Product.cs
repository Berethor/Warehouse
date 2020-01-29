using System;
using System.ComponentModel.DataAnnotations;

namespace WareHouse.Data
{
    public class Product 
    {
        [Key]
        public int CodeId { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public int Price { get; set; }
    }
}
