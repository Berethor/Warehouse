using System;
using System.ComponentModel.DataAnnotations;

namespace WareHouse.Data
{
    public class Sale
    {
        public int Id { get; set; }
        public DateTime AcceptDate { get; set; }
        public DateTime InStorageDate { get; set; }
        public DateTime SaleDate { get; set; }

        [Required]
        public Product Product { get; set; }
        public Sale()
        { }
        public Sale(InStorage product, int id)
        {
            AcceptDate = product.AcceptDate;
            InStorageDate = product.InStorageDate;
            Id = id;
            SaleDate = DateTime.Now;
            Product = product.Product;
        }
    }
}
