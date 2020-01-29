using System;
using System.ComponentModel.DataAnnotations;

namespace WareHouse.Data
{
    public class Accept
    {
        public int Id { get; set; }
        public DateTime AcceptDate { get; set; }
        [Required]
        public virtual Product Product { get; set; }
    }
}
