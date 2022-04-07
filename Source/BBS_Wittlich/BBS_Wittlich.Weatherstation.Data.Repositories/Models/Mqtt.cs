using System;
using System.Collections.Generic;

#nullable disable

namespace BBS_Wittlich.Weatherstation.Data.Models
{
    public partial class Mqtt
    {
        public int Id { get; set; }
        public string Topic { get; set; }
        public double Payload { get; set; }
    }
}
