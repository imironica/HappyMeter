using AutoMapper;
using System.Collections.Generic;

namespace HappyMeter.Model.Mapper
{
    public class MLMapper
    {
        private readonly MapperConfiguration _mapperConfiguration;
        private readonly IMapper _mapper;
        public MapperConfiguration Configuration { get { return _mapperConfiguration; } }
        public IMapper Mapper { get { return _mapper; } }
        public void CompileMappings() { _mapperConfiguration.CompileMappings(); }

        public MLMapper()
        {
            _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FaceEmotion, FaceEmotionDTO>()
                  .ForMember(x => x.FaceRectangle, y => y.MapFrom(c => c.FaceRectangle))
                  .ForMember(x => x.Scores, y => y.MapFrom(c => c.Scores));
                cfg.CreateMap<Score, ScoreDTO>();
                cfg.CreateMap<FaceRectangle, FaceRectangleDTO>();

                cfg.CreateMap<FaceInfo, FaceInfoDTO>()
                  .ForMember(x => x.FaceRectangle, y => y.MapFrom(c => c.FaceRectangle))
                  .ForMember(x => x.FaceLandmarks, y => y.MapFrom(c => c.FaceLandmarks))
                  .ForMember(x => x.FaceAttributes, y => y.MapFrom(c => c.FaceAttributes));

                cfg.CreateMap<FaceLandmarks, FaceLandmarksDTO>();
                cfg.CreateMap<Faceattributes, FaceattributesDTO>();
                cfg.CreateMap<Pupilleft, CoordinateDTO>();


                cfg.CreateMap<Pupilleft, CoordinateDTO>();
                cfg.CreateMap<Eyeleftouter, CoordinateDTO>();
                cfg.CreateMap<Pupilright, CoordinateDTO>();
                cfg.CreateMap<Nosetip, CoordinateDTO>();
                cfg.CreateMap<Mouthleft, CoordinateDTO>();
                cfg.CreateMap<Mouthright, CoordinateDTO>();
                cfg.CreateMap<Eyebrowleftouter, CoordinateDTO>();
                cfg.CreateMap<Eyebrowleftinner, CoordinateDTO>();
                cfg.CreateMap<Eyelefttop, CoordinateDTO>();
                cfg.CreateMap<Eyeleftbottom, CoordinateDTO>();
                cfg.CreateMap<Eyeleftinner, CoordinateDTO>();
                cfg.CreateMap<Eyebrowrightinner, CoordinateDTO>();
                cfg.CreateMap<Eyebrowrightouter, CoordinateDTO>();
                cfg.CreateMap<Eyerightinner, CoordinateDTO>();
                cfg.CreateMap<Eyerighttop, CoordinateDTO>();
                cfg.CreateMap<Eyerightbottom, CoordinateDTO>();
                cfg.CreateMap<Eyerightouter, CoordinateDTO>();
                cfg.CreateMap<Noserootleft, CoordinateDTO>();
                cfg.CreateMap<Noserootright, CoordinateDTO>();
                cfg.CreateMap<Noseleftalartop, CoordinateDTO>();
                cfg.CreateMap<Noserightalartop, CoordinateDTO>();
                cfg.CreateMap<Noseleftalarouttip, CoordinateDTO>();
                cfg.CreateMap<Noserightalarouttip, CoordinateDTO>();
                cfg.CreateMap<Upperliptop, CoordinateDTO>();
                cfg.CreateMap<Upperlipbottom, CoordinateDTO>();
                cfg.CreateMap<Underliptop, CoordinateDTO>();
                cfg.CreateMap<Underlipbottom, CoordinateDTO>();


                cfg.CreateMap<Headpose, HeadposeDTO>();
                cfg.CreateMap<Facialhair, FacialhairDTO>();
                cfg.CreateMap<ObjectInfo, ObjectInfoDTO>();
                cfg.CreateMap<Category, CategoryDTO>();
                cfg.CreateMap<Adult, AdultDTO>();
                cfg.CreateMap<Description, DescriptionDTO>();
                cfg.CreateMap<Metadata, MetadataDTO>();
                cfg.CreateMap<Face, FaceDTO>();
                cfg.CreateMap<Color, ColorDTO>();
                cfg.CreateMap<Imagetype, ImagetypeDTO>();
                cfg.CreateMap<Detail, DetailDTO>();
                cfg.CreateMap<Caption, CaptionDTO>();
                cfg.CreateMap<Tag, TagDTO>();

            });
            _mapper = _mapperConfiguration.CreateMapper();
        }

        public IList<FaceEmotionDTO> GetEmotionDTOListFromEmotionList(IList<FaceEmotion> emotion)
        {
            var result = _mapper.Map<IList<FaceEmotion>, IList<FaceEmotionDTO>>(emotion);
            return result;
        }

        public FaceEmotionDTO GetEmotionDTOFromEmotion(FaceEmotion emotion)
        {
            return _mapper.Map<FaceEmotion, FaceEmotionDTO>(emotion);
        }

        public IList<FaceInfoDTO> GetFaceDTOListFromFaceInfoList(IList<FaceInfo> faces)
        {
            var result = _mapper.Map<IList<FaceInfo>, IList<FaceInfoDTO>>(faces);
            return result;
        }

        public FaceInfoDTO GetFaceInfoDTOFromFaceInfo(FaceInfo face)
        {
            return _mapper.Map<FaceInfo, FaceInfoDTO>(face);
        }


        public IList<ObjectInfoDTO> GetObjectDTOListFromObjectInfoList(IList<ObjectInfo> objects)
        {
            var result = _mapper.Map<IList<ObjectInfo>, IList<ObjectInfoDTO>>(objects);
            return result;
        }

        public ObjectInfoDTO GetObjectInfoDTOFromObjectInfo(ObjectInfo obj)
        {
            return _mapper.Map<ObjectInfo, ObjectInfoDTO>(obj);
        }
    }
}
