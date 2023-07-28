﻿namespace Gamehoax_backend.Models
{
    public class Cart:BaseEntity
    {
        public int Count { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<CartProduct> CartProducts { get; set; }
    }
}