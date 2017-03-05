using AutoMapper;
using System.Collections.Generic;

namespace HappyMeter.Models.Mapper
{
    public class EmotionMapper
    {
        private readonly MapperConfiguration _mapperConfiguration;
        private readonly IMapper _mapper;
        public MapperConfiguration Configuration { get { return _mapperConfiguration; } }
        public IMapper Mapper { get { return _mapper; } }
        public void CompileMappings() { _mapperConfiguration.CompileMappings(); }

        public EmotionMapper()
        {
            _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FaceEmotion, FaceEmotionDTO>()
                   .ForMember(x => x.FaceRectangle, y => y.MapFrom(c => c.FaceRectangle))
                  .ForMember(x => x.Scores, y => y.MapFrom(c => c.Scores)); ;
                cfg.CreateMap<Score, ScoreDTO>();
                cfg.CreateMap<FaceRectangle, FaceRectangleDTO>();
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
    }
}
