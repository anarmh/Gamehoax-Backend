﻿namespace Gamehoax_backend.Models
{
    public class Blog:BaseEntity
    {
        public string Image { get; set; }
        public string Icon { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string BlogIcon { get; set; }
    }
}
