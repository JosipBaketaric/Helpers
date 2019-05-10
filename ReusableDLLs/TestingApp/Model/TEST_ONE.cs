using Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingApp.Model
{
    public class TEST_ONE
    {
        public int ID { get; set; }
        public string NAME_AND_LAST_NAME { get; set; }
        public TEST_TWO TEST_TWO_PROP { get; set; }

        public List<TEST_TWO> TEST_TWO_LIST_PROP { get; set; }

        public TEST_ONE()
        {
            TEST_TWO_LIST_PROP = new List<TEST_TWO>();
        }

    }
}
