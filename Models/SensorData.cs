using System;
using System.ComponentModel.DataAnnotations;

namespace RealTimeThingSpeak.Models
{
    public class Feed
    {
        [Key]
        public int Id { get; set; }  // Primary Key for the database
        public DateTime CreatedAt { get; set; } // Store DateTime
        public double Field1 { get; set; } // Store sensor data
    }
}
