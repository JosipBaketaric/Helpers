﻿using Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingApp.Model
{
    public class TEST_TWO : DatabaseSideMapper<TEST_TWO, TestTwo>
    {
        public int ID { get; set; }
        public string NAME { get; set; }

    }
}