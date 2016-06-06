using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionLibrary.Interfaces
{
    public interface IReflectedPropertyBuilder
    {
        void BuildReflectedProperties(IReflectedClass reflectedClass, Type type);
    }
}
