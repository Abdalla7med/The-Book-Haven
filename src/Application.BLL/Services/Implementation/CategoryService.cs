using Application.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BLL
{
    public class CategoryService : ICategoryService
    {
        public Task AddCategory(CreateCategoryDto createCategoryDto)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ReadCategoryDto>> AllCategories()
        {
            throw new NotImplementedException();
        }

        public Task DeleteCategory(Guid CategoryId)
        {
            throw new NotImplementedException();
        }

        public Task<ReadCategoryDto> GetCategoryById(Guid CategoryId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsCategoryExists(string name)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            throw new NotImplementedException();
        }
    }
}
