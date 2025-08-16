namespace Silo.API.Helpers.Mapper;

public class MapperHelper(IMapper mapper) : IMapperHelper
{
    public readonly IMapper _mapper = mapper;

    // Database-friendly projection (doesn't load everything into memory)
    public IQueryable<TResult> MapTo<TResult>(IQueryable source)
    {
        return source.ProjectToType<TResult>();
    }

    // Mapping in-memory collections
    public IEnumerable<TResult> MapTo<TResult>(IEnumerable source)
    {
        return source.Adapt<IEnumerable<TResult>>();
    }

    // Single object mapping
    public TResult MapOne<TResult>(object source)
    {
        return _mapper.Map<TResult>(source);
    }
}