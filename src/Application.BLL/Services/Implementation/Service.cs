using Application.BLL.Services;
using Application.DAL.Repositories;
using Application.DAL.UnitOfWork;
using AutoMapper;
namespace Application.BLL;
public class GenericService<TDto, TEntity> : IService<TDto>
    where TEntity : class, new() // TEntity must be a class
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly GenericRepository<TEntity> _repository; // the error must be non-abstract is been solved using generic constraints new() 
    private readonly IMapper _mapper; // Assuming you're using AutoMapper

    public GenericService(IUnitOfWork unitOfWork, GenericRepository<TEntity> repository, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<TDto>>(entities);
    }

    public async Task<TDto> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return _mapper.Map<TDto>(entity);
    }

    public async Task CreateAsync(TDto dto)
    {
        var entity = _mapper.Map<TEntity>(dto);
        await _repository.AddAsync(entity);
        await _unitOfWork.CompleteAsync();
    }

    public async Task UpdateAsync(TDto dto)
    {
        var entity = _mapper.Map<TEntity>(dto);

        _repository.UpdateAsync(entity);
        await _unitOfWork.CompleteAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsyncById(id);
        await _unitOfWork.CompleteAsync();
    }
}
