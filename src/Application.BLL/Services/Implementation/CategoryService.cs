using Application.DAL.UnitOfWork;
using Application.Shared;
using AutoMapper;
using Application.DAL.Entities;
using Application.DAL;

namespace Application.BLL
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ReadCategoryDto>> AllCategories()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
            
            List<ReadCategoryDto>categoryDtos = new List<ReadCategoryDto>(); 
            
            foreach(var category in categories)
            {
                var Dto = new ReadCategoryDto()
                {
                    Id = category.CategoryId,
                    Name = category.Name,
                    Books = category.Books.Select(b => b.Title).ToList()

                };
                categoryDtos.Add(Dto);

            }
            return categoryDtos;
        }

        public async Task<ReadCategoryDto> GetCategoryById(Guid categoryId)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(categoryId);
            if (category == null)
                throw new ArgumentException("Category not found");

            var Dto = new ReadCategoryDto()
            {
                Id = category.CategoryId,
                Name = category.Name,
                Books = category.Books.Select(b => b.Title).ToList()

            };
            return Dto;
        }

        public async Task AddCategory(CreateCategoryDto createCategoryDto)
        {
            // Check if category already exists by name
            var existingCategory = await _unitOfWork.CategoryRepository.GetCategory(createCategoryDto.Name);

            if (existingCategory != null)
                throw new ArgumentException($"Category with the name '{createCategoryDto.Name}' already exists.");

            // Map and create new category
            var newCategory = new Category
            {
                Name = createCategoryDto.Name,
                IsDeleted = false,
                Books = new List<Book>()
            };

            // Add to database and save changes
            await _unitOfWork.CategoryRepository.AddAsync(newCategory);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            // Retrieve the category by ID
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(updateCategoryDto.CategoryId);
            if (category == null)
                throw new ArgumentException("Category not found");

            // Update category properties
            category.Name = updateCategoryDto.Name;
            category.IsDeleted = updateCategoryDto.IsDeleted;

            // Save changes to the database
            await _unitOfWork.CategoryRepository.UpdateAsync(category);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteCategory(Guid categoryId)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(categoryId);
            if (category == null)
                throw new ArgumentException("Category not found");

            // Mark category as deleted (soft delete) or remove it permanently
            category.IsDeleted = true;
            await _unitOfWork.CategoryRepository.UpdateAsync(category);
            await _unitOfWork.CompleteAsync();
        }
    }
}
