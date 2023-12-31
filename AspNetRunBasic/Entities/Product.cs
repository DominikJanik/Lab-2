﻿using System.ComponentModel.DataAnnotations;

namespace AspNetRunBasic.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Required, StringLength(80)]
        public string Name { get; set; }

        [Required, StringLength(255)]
        public string Description { get; set; }
       

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
