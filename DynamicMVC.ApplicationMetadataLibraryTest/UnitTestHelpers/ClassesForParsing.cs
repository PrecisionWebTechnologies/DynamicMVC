using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicMVC.Annotations;

namespace DynamicMVCTest.UnitTestHelpers
{
    public class Hello
    {

    }

    [DynamicEntity]
    public class World
    {
        [Required]
        public object Name { get; set; }
        public int Age { get; set; }

        [Required]
        public ICollection<Hello> Hellos { get; set; }

        [Required]
        public ICollection<World2> Worlds { get; set; }
    }

    [DynamicEntity]
    public class World2
    {

    }
}
