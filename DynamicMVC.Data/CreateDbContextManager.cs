using System;
using System.Data.Entity;
using DynamicMVC.Data.Interfaces;

namespace DynamicMVC.Data
{
    public class CreateDbContextManager : ICreateDbContextManager
    {
        public CreateDbContextManager(Func<DbContext> createDbContextFunction)
        {
            CreateDbContextFunction = createDbContextFunction;
        }

        public Func<DbContext> CreateDbContextFunction { get; set; }
    }
}
