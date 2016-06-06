using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DynamicLinqExtensions;
using DynamicMVC.Data.Interfaces;

namespace DynamicMVC.Data
{
    public class DynamicRepository : IDynamicRepository
    {
        public DynamicRepository(ICreateDbContextManager createDbContextManager)
        {
            DataContext = createDbContextManager.CreateDbContextFunction();
        }

        public DbContext DataContext { get; set; }

        public void Dispose()
        {
            if (DataContext != null)
                DataContext.Dispose();
        }

        public dynamic GetItem(Type type, string keyName, dynamic id, params string[] includes)
        {
            var qry = DataContext.Set(type).AsQueryable();
            foreach (var include in includes)
            {
                qry = qry.Include(include);
            }
            var propertyInfo = type.GetProperties().Single(x => x.Name == keyName);

            foreach (var item in qry.DynamicWhere(propertyInfo, (object)id))
            {
                return item;
            }
            return null;
        }

        public IEnumerable<dynamic> GetItems(Type type, string propName, dynamic propValue, params string[] includes)
        {
            var results = new List<dynamic>();
            var qry = DataContext.Set(type).AsQueryable();
            foreach (var include in includes)
            {
                qry = qry.Include(include);
            }
            var propertyInfo = type.GetProperties().Single(x => x.Name == propName);

            foreach (var item in qry.DynamicWhere(propertyInfo, (object)propValue))
            {
                results.Add(item);
            }
            return results;
        }
        public IEnumerable GetItems(Type type)
        {
            var items = new List<object>();
            foreach (var item in DataContext.Set(type))
            {
                items.Add(item);
            }
            return items;
        }

        public int GetItemsCount(Type type)
        {
            return DataContext.Set(type).Count();
        }
        
        private IQueryable Filter(Type type, IEnumerable<Func<IQueryable,IQueryable>> filters, params string[] includes)
        {
            var qry = DataContext.Set(type).AsQueryable();
            foreach (var include in includes)
            {
                qry = qry.Include(include);
            }
            foreach (var filter in filters)
            {
                qry = filter(qry);
            }
            return qry;
        }

        public long GetRecordCount(Type type, IEnumerable<Func<IQueryable, IQueryable>> filters)
        {
            return Filter(type, filters).Count();
        }

        public IEnumerable GetItems(Type type, IEnumerable<Func<IQueryable,IQueryable>> filters, int page, int pageSize, string orderBy, params string[] includes)
        {
            var qry = Filter(type, filters, includes);
            int skip = page * pageSize - pageSize;
            qry = qry.OrderBy(orderBy);
            qry = qry.Skip(skip);
            qry = qry.Take(pageSize);
            var items = new List<object>();
            foreach (var item in qry)
            {
                items.Add(item);
            }
            return items;
        }

        //need to replace this method with three stages to page
        //first stage is to build iqueryable - built filter method
        //second stage is to get total record count - built GetRecordCount method
        //third stage is to return the desired page of records - built GetItems method
        //need to phase this method out with the other three methods
        public IEnumerable GetItems(Type type, IDictionary<string, object> querystringDictionary, out long recordCount, params string[] includes)
        {
            var qry = DataContext.Set(type).AsQueryable();
            foreach (var include in includes)
            {
                qry = qry.Include(include);
            }
            //need to find IDynamicFilters here
            foreach (var propertyInfo in type.GetProperties().Where(x => querystringDictionary.ContainsKey(x.Name)))
            {
                //added filter to IDynamicFilter
                qry = qry.DynamicWhere(propertyInfo, querystringDictionary[propertyInfo.Name]);
            }
            var orderBy = querystringDictionary["OrderBy"].ToString();
            var page = int.Parse(querystringDictionary["Page"].ToString());
            var pageSize = int.Parse(querystringDictionary["PageSize"].ToString());
            int skip = page * pageSize - pageSize;
            qry = qry.OrderBy(orderBy);
            recordCount = qry.Count();

            qry = qry.Skip(skip);
            qry = qry.Take(pageSize);
            var items = new List<object>();
            foreach (var item in qry)
            {
                items.Add(item);
            }
            return items;
        }

        public void CreateItem(Type type, object item)
        {
            DataContext.Set(type).Add(item);
            SaveChanges();
        }
        public int SaveChanges()
        {
            return DataContext.SaveChanges();
        }
        public void DeleteItem(Type type, string keyName, dynamic id)
        {
            var item = GetItem(type, keyName, id);
            DataContext.Set(type).Remove(item);
            SaveChanges();
        }

        public IEnumerable GetAutoCompleteItems(Type type, string idColumn, string searchColumn, string searchString, int limit)
        {
            List<object> items = new List<object>();
            int count = 0;
            IQueryable qry = DataContext.Set(type);
            qry = qry.DynamicWhereContains(searchColumn, searchString);
            qry = qry.Select("new(" + idColumn + " as id, " + searchColumn + " as text)");
            foreach (var item in qry)
            {
                items.Add(item);
                count += 1;
                if (count >= limit)
                    break;
            }
            return items;
        }
    }
}
