using Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingApp.Model
{
    public class TEST_TWO
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public List<int> NESTED_LIST { get;set;}

        public TEST_TWO()
        {
            NESTED_LIST = new List<int>();
        }
    }
}
