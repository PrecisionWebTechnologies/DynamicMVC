using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DynamicMVC.Data.Interfaces
{
    public interface IDynamicRepository : IDisposable
    {
        dynamic GetItem(Type type, string keyName, dynamic id, params string[] includes);
        IEnumerable GetItems(Type type);
        IEnumerable GetItems(Type type, IDictionary<string, object> querystringDictionary, out long recordCount, params string[] includes);
        IEnumerable GetItems(Type type, IEnumerable<Func<IQueryable, IQueryable>> filters, int page, int pageSize, string orderBy, params string[] includes);
        IEnumerable<dynamic> GetItems(Type type, string propName, dynamic propValue, params string[] includes);
        long GetRecordCount(Type type, IEnumerable<Func<IQueryable, IQueryable>> filters);
        void CreateItem(Type type, object item);
        int SaveChanges();
        void DeleteItem(Type type, string keyName, dynamic id);
        IEnumerable GetAutoCompleteItems(Type type, string idColumn, string searchColumn, string searchString, int limit);
        int GetItemsCount(Type type);

    }
}
