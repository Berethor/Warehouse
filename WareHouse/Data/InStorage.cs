using System;
using System.ComponentModel.DataAnnotations;

namespace WareHouse.Data
{
    public class InStorage
    {
        public int Id { get; set; }
        public DateTime AcceptDate { get; set; }
        public DateTime InStorageDate { get; set; }
        [Required]
        public Product Product { get; set; }

        public InStorage()
        { }
        public InStorage(Accept product, int id)
        {
            AcceptDate = product.AcceptDate;
            InStorageDate = DateTime.Now;
            Id = id;
            Product = product.Product;
        }
    }
}
