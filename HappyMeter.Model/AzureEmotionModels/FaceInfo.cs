using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyMeter.Model
{
    public class FaceInfo
    {
        [JsonProperty(PropertyName = "faceId")]
        public string FaceId { get; set; }
        [JsonProperty(PropertyName = "faceRectangle")]
        public FaceRectangle FaceRectangle { get; set; }
        [JsonProperty(PropertyName = "faceLandmarks")]
        public FaceLandmarks FaceLandmarks { get; set; }
        [JsonProperty(PropertyName = "faceAttributes")]
        public Faceattributes FaceAttributes { get; set; }
    }

    [JsonObject(Title = "Facelandmarks")]
    public class FaceLandmarks
    {
        [JsonProperty(PropertyName = "pupilLeft")]
        public Pupilleft PupilLeft { get; set; }
        [JsonProperty(PropertyName = "pupilRight")]
        public Pupilright PupilRight { get; set; }


        [JsonProperty(PropertyName = "noseTip")]
        public Nosetip NoseTip { get; set; }
        [JsonProperty(PropertyName = "mouthLeft")]
        public Mouthleft MouthLeft { get; set; }
        [JsonProperty(PropertyName = "mouthRight")]
        public Mouthright MouthRight { get; set; }

        [JsonProperty(PropertyName = "eyebrowLeftOuter")]
        public Eyebrowleftouter EyeBrowLeftOuter { get; set; }
        [JsonProperty(PropertyName = "eyebrowLeftInner")]
        public Eyebrowleftinner EyeBrowLeftInner { get; set; }
        [JsonProperty(PropertyName = "eyeLeftOuter")]
        public Eyeleftouter EyeLeftOuter { get; set; }
        [JsonProperty(PropertyName = "eyeLeftTop")]
        public Eyelefttop EyeLeftTop { get; set; }
        [JsonProperty(PropertyName = "eyeLeftBottom")]
        public Eyeleftbottom EyeLeftBottom { get; set; }
        [JsonProperty(PropertyName = "eyeLeftInner")]
        public Eyeleftinner EyeLeftInner { get; set; }
        [JsonProperty(PropertyName = "eyebrowRightInner")]
        public Eyebrowrightinner EyebrowRightInner { get; set; }
        [JsonProperty(PropertyName = "eyebrowRightOuter")]
        public Eyebrowrightouter EyebrowRightOuter { get; set; }
        [JsonProperty(PropertyName = "eyeRightInner")]
        public Eyerightinner EyeRightInner { get; set; }
        [JsonProperty(PropertyName = "eyeRightTop")]
        public Eyerighttop EyeRightTop { get; set; }
        [JsonProperty(PropertyName = "eyeRightBottom")]
        public Eyerightbottom EyeRightBottom { get; set; }
        [JsonProperty(PropertyName = "eyeRightOuter")]
        public Eyerightouter EyeRightOuter { get; set; }
        [JsonProperty(PropertyName = "noseRootLeft")]
        public Noserootleft NoseRootLeft { get; set; }
        [JsonProperty(PropertyName = "noseRootRight")]
        public Noserootright NoseRootRight { get; set; }
        [JsonProperty(PropertyName = "noseLeftAlarTop")]
        public Noseleftalartop NoseLeftAlarTop { get; set; }
        [JsonProperty(PropertyName = "noseRightAlarTop")]
        public Noserightalartop NoseRightAlarTop { get; set; }
        [JsonProperty(PropertyName = "noseLeftAlarOutTip")]
        public Noseleftalarouttip NoseLeftAlarOutTip { get; set; }
        [JsonProperty(PropertyName = "noseRightAlarOutTip")]
        public Noserightalarouttip NoseRightAlarOutTip { get; set; }
        [JsonProperty(PropertyName = "upperLipTop")]
        public Upperliptop UpperLipTop { get; set; }
        [JsonProperty(PropertyName = "upperLipBottom")]
        public Upperlipbottom UpperLipBottom { get; set; }
        [JsonProperty(PropertyName = "underLipTop")]
        public Underliptop UnderLipTop { get; set; }
        [JsonProperty(PropertyName = "underLipBottom")]
        public Underlipbottom UnderLipBottom { get; set; }
    }

    public class Pupilleft
    {
        [JsonProperty(PropertyName = "x")]
        public float X { get; set; }
        [JsonProperty(PropertyName = "y")]
        public float Y { get; set; }
    }

    public class Pupilright
    {
        [JsonProperty(PropertyName = "x")]
        public float X { get; set; }
        [JsonProperty(PropertyName = "y")]
        public float Y { get; set; }
    }

    public class Nosetip
    {
        [JsonProperty(PropertyName = "x")]
        public float X { get; set; }
        [JsonProperty(PropertyName = "y")]
        public float Y { get; set; }
    }

    public class Mouthleft
    {
        [JsonProperty(PropertyName = "x")]
        public float X { get; set; }
        [JsonProperty(PropertyName = "y")]
        public float Y { get; set; }
    }

    public class Mouthright
    {
        [JsonProperty(PropertyName = "x")]
        public float X { get; set; }
        [JsonProperty(PropertyName = "y")]
        public float Y { get; set; }
    }

    public class Eyebrowleftouter
    {
        [JsonProperty(PropertyName = "x")]
        public float X { get; set; }
        [JsonProperty(PropertyName = "y")]
        public float Y { get; set; }
    }

    public class Eyebrowleftinner
    {
        [JsonProperty(PropertyName = "x")]
        public float X { get; set; }
        [JsonProperty(PropertyName = "y")]
        public float Y { get; set; }
    }

    public class Eyeleftouter
    {
        [JsonProperty(PropertyName = "x")]
        public float X { get; set; }
        [JsonProperty(PropertyName = "y")]
        public float Y { get; set; }
    }

    public class Eyelefttop
    {
        [JsonProperty(PropertyName = "x")]
        public float X { get; set; }
        [JsonProperty(PropertyName = "y")]
        public float Y { get; set; }
    }

    public class Eyeleftbottom
    {
        [JsonProperty(PropertyName = "x")]
        public float X { get; set; }
        [JsonProperty(PropertyName = "y")]
        public float Y { get; set; }
    }

    public class Eyeleftinner
    {
        [JsonProperty(PropertyName = "x")]
        public float X { get; set; }
        [JsonProperty(PropertyName = "y")]
        public float Y { get; set; }
    }

    public class Eyebrowrightinner
    {
        [JsonProperty(PropertyName = "x")]
        public float X { get; set; }
        [JsonProperty(PropertyName = "y")]
        public float Y { get; set; }
    }

    public class Eyebrowrightouter
    {
        [JsonProperty(PropertyName = "x")]
        public float X { get; set; }
        [JsonProperty(PropertyName = "y")]
        public float Y { get; set; }
    }

    public class Eyerightinner
    {
        [JsonProperty(PropertyName = "x")]
        public float X { get; set; }
        [JsonProperty(PropertyName = "y")]
        public float Y { get; set; }
    }

    public class Eyerighttop
    {
        [JsonProperty(PropertyName = "x")]
        public float X { get; set; }
        [JsonProperty(PropertyName = "y")]
        public float Y { get; set; }
    }

    public class Eyerightbottom
    {
        [JsonProperty(PropertyName = "x")]
        public float X { get; set; }
        [JsonProperty(PropertyName = "y")]
        public float Y { get; set; }
    }

    public class Eyerightouter
    {
        [JsonProperty(PropertyName = "x")]
        public float X { get; set; }
        [JsonProperty(PropertyName = "y")]
        public float Y { get; set; }
    }

    public class Noserootleft
    {
        [JsonProperty(PropertyName = "x")]
        public float X { get; set; }
        [JsonProperty(PropertyName = "y")]
        public float Y { get; set; }
    }

    public class Noserootright
    {
        [JsonProperty(PropertyName = "x")]
        public float X { get; set; }
        [JsonProperty(PropertyName = "y")]
        public float Y { get; set; }
    }

    public class Noseleftalartop
    {
        [JsonProperty(PropertyName = "x")]
        public float X { get; set; }
        [JsonProperty(PropertyName = "y")]
        public float Y { get; set; }
    }

    public class Noserightalartop
    {
        [JsonProperty(PropertyName = "x")]
        public float X { get; set; }
        [JsonProperty(PropertyName = "y")]
        public float Y { get; set; }
    }

    public class Noseleftalarouttip
    {
        [JsonProperty(PropertyName = "x")]
        public float X { get; set; }
        [JsonProperty(PropertyName = "y")]
        public float Y { get; set; }
    }

    public class Noserightalarouttip
    {
        [JsonProperty(PropertyName = "x")]
        public float X { get; set; }
        [JsonProperty(PropertyName = "y")]
        public float Y { get; set; }
    }

    public class Upperliptop
    {
        [JsonProperty(PropertyName = "x")]
        public float X { get; set; }
        [JsonProperty(PropertyName = "y")]
        public float Y { get; set; }
    }

    public class Upperlipbottom
    {
        [JsonProperty(PropertyName = "x")]
        public float X { get; set; }
        [JsonProperty(PropertyName = "y")]
        public float Y { get; set; }
    }

    public class Underliptop
    {
        [JsonProperty(PropertyName = "x")]
        public float X { get; set; }
        [JsonProperty(PropertyName = "y")]
        public float Y { get; set; }
    }

    public class Underlipbottom
    {
        [JsonProperty(PropertyName = "x")]
        public float X { get; set; }
        [JsonProperty(PropertyName = "y")]
        public float Y { get; set; }
    }

    public class Faceattributes
    {
        [JsonProperty(PropertyName = "smile")]
        public float Smile { get; set; }

        [JsonProperty(PropertyName = "headPose")]
        public Headpose HeadPose { get; set; }

        [JsonProperty(PropertyName = "gender")]
        public string Gender { get; set; }

        [JsonProperty(PropertyName = "age")]
        public float Age { get; set; }

        [JsonProperty(PropertyName = "facialHair")]
        public Facialhair FacialHair { get; set; }

        [JsonProperty(PropertyName = "glasses")]
        public string Glasses { get; set; }
    }

    public class Headpose
    {
        [JsonProperty(PropertyName = "pitch")]
        public float Pitch { get; set; }
        [JsonProperty(PropertyName = "roll")]
        public float Roll { get; set; }
        [JsonProperty(PropertyName = "yaw")]
        public float Yaw { get; set; }
    }

    public class Facialhair
    {
        [JsonProperty(PropertyName = "moustache")]
        public float Moustache { get; set; }
        [JsonProperty(PropertyName = "beard")]
        public float Beard { get; set; }
        [JsonProperty(PropertyName = "sideburns")]
        public float Sideburns { get; set; }
    }

}
