using System;
using System.Data.Entity;

namespace DynamicMVC.Data.Interfaces
{
    public interface ICreateDbContextManager
    {
        Func<DbContext> CreateDbContextFunction { get; set; }
    }
}
