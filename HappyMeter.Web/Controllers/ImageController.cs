using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HappyMeter.Service;
using HappyMeter.Model;

namespace HappyMeter_Web.Controllers
{
    [Route("api/[controller]")]
    public class ImageController : Controller
    {
        IEmotionService _emotionService;

        public ImageController()
        {
            _emotionService = new EmotionService();
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<CategoryGridDTO>> GetImageCategories()
        {
            return await _emotionService.GetCategories();
        }
        [HttpGet("[action]")]
        public async Task<IEnumerable<CategoryGridDTO>> GetCategoriesChart()
        {
            return await _emotionService.GetCategoriesChart();
        }

        [HttpPost("[action]")]
        
        public async Task<IEnumerable<ImageGridDTO>> GetImagesPerCategory([FromBody]ImageGridRequest request)
        {
            return await _emotionService.GetEmotionsPerCategory(request.Option, request.Category);
        }

        [HttpPost("[action]")]
        public async Task<ImageDTO> GetImage([FromBody]ImageRequest request)
        {
            return await _emotionService.GetImage(request.Category, request.Id);
        }
    }

    public class ImageGridRequest
    {
        public string Option{ get; set; }
        public string Category { get; set; }
    }
    public class ImageRequest
    {
        public string Id { get; set; }
        public string Category { get; set; }
    }
    
}
