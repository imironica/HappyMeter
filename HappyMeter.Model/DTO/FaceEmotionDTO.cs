using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyMeter.Model
{
    public class FaceEmotionDTO
    {
        public FaceRectangleDTO FaceRectangle { get; set; }
        public ScoreDTO Scores { get; set; }
    }
 
}
