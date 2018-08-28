using Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingApp.Model
{
    public class TestTwo : DtoSideMapper<TEST_TWO, TestTwo>
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
