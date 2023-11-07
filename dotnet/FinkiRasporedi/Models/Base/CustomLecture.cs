﻿using FinkiRasporedi.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace FinkiRasporedi.Models.Base
{
    public class CustomLecture
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public Day Day { get; set; }
        public TimeSpan TimeFrom { get; set; }
        public TimeSpan TimeTo { get; set; }

        public Professor Professor { get; set; }
        public Course Course { get; set; }
        public Room Room { get; set; }

        public Lecture Lecture { get; set; }
    }
}
