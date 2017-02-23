using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyMeter.Models
{
    public class InfoDTO
    {
        public FaceEmotion[] Emotions { get; set; }
        public string Image { get; set; }
        public string Category { get; set; }
    }
}
