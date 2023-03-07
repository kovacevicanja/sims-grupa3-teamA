﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Model.Images
{
    internal class AccommodationImages
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public Accommodation Accommodation { get; set; }
        public AccommodationImages()
        {
            Accommodation= new Accommodation();
        }
    }
}
