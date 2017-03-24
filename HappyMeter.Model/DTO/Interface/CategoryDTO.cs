using System;
using System.Collections.Generic;
using System.Text;

namespace HappyMeter.Model
{
    public class CategoryGridDTO
    {
        public string Category { get; set; }
        public double HappinessPercent { get; set; }
        public double ContemptPercent { get; set; }
        public double AngerPercent { get; set; }
        public double DisgustPercent { get; set; }
        public double FearPercent { get; set; }
        public double NeutralPercent { get; set; }
        public double SadnessPercent { get; set; }
        public double SurprizePercent { get; set; }
    }
} 