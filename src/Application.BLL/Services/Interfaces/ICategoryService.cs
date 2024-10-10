using Application.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BLL
{
    public interface ICategoryService
    {
        Task<IEnumerable<ReadCategoryDto>> AllCategories();
        Task AddCategory(CreateCategoryDto createCategoryDto);
        Task<ReadCategoryDto> GetCategoryById(Guid CategoryId);
        Task UpdateCategory(UpdateCategoryDto updateCategoryDto);
        Task DeleteCategory(Guid CategoryId);
        Task<bool> IsCategoryExists(string name); // Check if Category deleted or not 
    }
}
