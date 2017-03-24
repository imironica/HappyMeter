using System.Collections.Generic;
using HappyMeter.Model;
using System.Threading.Tasks;

namespace HappyMeter.Service
{
    public interface IEmotionService
    {
        Task<List<ImageGridDTO>> GetEmotionsPerCategory(string option, string category);
        Task<ImageDTO> GetImage(string category, string id);
        Task<List<CategoryGridDTO>> GetCategories();
        Task<List<CategoryGridDTO>> GetCategoriesChart();
    }
}