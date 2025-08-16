namespace Silo.API.Helpers.Mapper;

public interface IMapperHelper
{
    TResult MapOne<TResult>(object source);
    IEnumerable<TResult> MapTo<TResult>(IEnumerable source);
    IQueryable<TResult> MapTo<TResult>(IQueryable source);
}
