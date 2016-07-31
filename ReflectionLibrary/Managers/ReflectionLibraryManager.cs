using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using ReflectionLibrary.Interfaces;

namespace ReflectionLibrary.Managers
{
    public class ReflectionLibraryManager : IReflectedLibraryManager
    {
        private readonly IReflectedClassBuilder _reflectedClassBuilder;

        public ReflectionLibraryManager()
        {
            _reflectedClassBuilder = UnityConfig.GetConfiguredContainer().Resolve<IReflectedClassBuilder>();
        }

        public ReflectionLibraryManager(IReflectedClassBuilder reflectedClassBuilder)
        {
            _reflectedClassBuilder = reflectedClassBuilder;
        }

        public IReflectedClass GetReflectedClass(Type type)
        {
            var reflectedClassBuilder = _reflectedClassBuilder;
            var reflectedClass = reflectedClassBuilder.BuildReflectedClass(type);
            return reflectedClass;
        }

        public ICollection<IReflectedClass> GetReflectedClasses(IEnumerable<Type> types, params Func<Type, bool>[] filters)
        {
            types = types.Where(x => x != null).ToList();
            foreach (var filter in filters)
            {
                types = types.Where(x => filter(x)).ToList();
            }
            var reflectedClasses = new List<IReflectedClass>();
            foreach (var type in types)
            {
                var reflectedClass = GetReflectedClass(type);
                reflectedClasses.Add(reflectedClass);
            }
            return reflectedClasses;
        }
    }
}
