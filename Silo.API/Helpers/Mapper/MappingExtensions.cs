namespace Silo.API.Helpers.Mapper;

public static class MappingExtensions 
{
    public static readonly IMapper _mapper;

    // Database-friendly projection (doesn't load everything into memory)
    public static IQueryable<TResult> MapTo<TResult>(this IQueryable source)
    {
        return source.ProjectToType<TResult>();
    }

    // Mapping in-memory collections
    public static IEnumerable<TResult> MapTo<TResult>(this IEnumerable source)
    {
        return source.Adapt<IEnumerable<TResult>>();
    }

    // Single object mapping
    public static TResult MapOne<TResult>(this object source)
    {
        return _mapper.Map<TResult>(source);
    }
}