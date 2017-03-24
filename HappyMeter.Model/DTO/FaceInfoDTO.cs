using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyMeter.Model
{
    public class FaceInfoDTO
    {
        public string FaceId { get; set; }
        public FaceRectangleDTO FaceRectangle { get; set; }
        public FaceLandmarksDTO FaceLandmarks { get; set; }
        public FaceattributesDTO FaceAttributes { get; set; }
    }

    public class FaceLandmarksDTO
    {
        public CoordinateDTO PupilLeft { get; set; }
        public CoordinateDTO PupilRight { get; set; }
        public CoordinateDTO NoseTip { get; set; }
        public CoordinateDTO MouthLeft { get; set; }
        public CoordinateDTO MouthRight { get; set; }
        public CoordinateDTO EyeBrowLeftOuter { get; set; }
        public CoordinateDTO EyeBrowLeftInner { get; set; }
        public CoordinateDTO EyeLeftOuter { get; set; }
        public CoordinateDTO EyeLeftTop { get; set; }
        public CoordinateDTO EyeLeftBottom { get; set; }
        public CoordinateDTO EyeLeftInner { get; set; }
        public CoordinateDTO EyebrowRightInner { get; set; }
        public CoordinateDTO EyebrowRightOuter { get; set; }
        public CoordinateDTO EyeRightInner { get; set; }
        public CoordinateDTO EyeRightTop { get; set; }
        public CoordinateDTO EyeRightBottom { get; set; }
        public CoordinateDTO EyeRightOuter { get; set; }
        public CoordinateDTO NoseRootLeft { get; set; }
        public CoordinateDTO NoseRootRight { get; set; }
        public CoordinateDTO NoseLeftAlarTop { get; set; }
        public CoordinateDTO NoseRightAlarTop { get; set; }
        public CoordinateDTO NoseLeftAlarOutTip { get; set; }
        public CoordinateDTO NoseRightAlarOutTip { get; set; }
        public CoordinateDTO UpperLipTop { get; set; }
        public CoordinateDTO UpperLipBottom { get; set; }
        public CoordinateDTO UnderLipTop { get; set; }
        public CoordinateDTO UnderLipBottom { get; set; }
    }

    public class CoordinateDTO
    {
        public float X { get; set; }
        public float Y { get; set; }
    }



    public class FaceattributesDTO
    {
        public float Smile { get; set; }

        public HeadposeDTO HeadPose { get; set; }

        public string Gender { get; set; }

        public float Age { get; set; }

        public FacialhairDTO FacialHair { get; set; }

        public string Glasses { get; set; }
    }

    public class HeadposeDTO
    {
        public float Pitch { get; set; }
        public float Roll { get; set; }
        public float Yaw { get; set; }
    }

    public class FacialhairDTO
    {
        public float Moustache { get; set; }
        public float Beard { get; set; }
        public float Sideburns { get; set; }
    }
}
