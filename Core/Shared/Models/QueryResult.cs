using System.Collections.Generic;

namespace Core.Shared;

public class QueryResult<T>
{
    public IEnumerable<T> Items { get; set; }
    public int AllCount { get; set; }

    public QueryResult()
    {
        Items = new List<T>();
    }
}