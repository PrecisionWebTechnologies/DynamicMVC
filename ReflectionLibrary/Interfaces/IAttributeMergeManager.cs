using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionLibrary.Interfaces
{
    public interface IAttributeMergeManager
    {
        void MergeAttributes(IReflectedClass source, IReflectedClass target);
    }
}
