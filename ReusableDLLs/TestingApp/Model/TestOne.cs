using Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingApp.Model
{
    public class TestOne : DtoSideMapper<TEST_ONE, TestOne>
    {
        public int Id { get; set; }
        public string NameAndLastName { get; set; }
        public TestTwo TestTwoProp { get; set; }
        public List<TestTwo> TestTwoListProp { get; set; }

        public TestOne()
        {
            TestTwoListProp = new List<TestTwo>();
        }


    }
}
