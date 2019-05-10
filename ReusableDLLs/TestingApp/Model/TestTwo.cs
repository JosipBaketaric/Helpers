using Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingApp.Model
{
    public class TestTwo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> NestedList { get; set; }

        public TestTwo()
        {
            NestedList = new List<int>();
        }
    }
}
