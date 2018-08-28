using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingApp.Model;

namespace TestingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            TestOne testOneDto = new TestOne();
            TEST_ONE testOneDb = new TEST_ONE();
            TEST_TWO testTwoDb = new TEST_TWO();

            testTwoDb.ID = 21212;
            testTwoDb.NAME = "sadasdasd";

            testOneDb.ID = 14;
            testOneDb.NAME_AND_LAST_NAME = "Josip B";
            testOneDb.TEST_TWO_PROP = testTwoDb;


            for(int i = 0; i < 1000; i++)
            {
                TEST_TWO testTwoDbTemp = new TEST_TWO();
                testTwoDbTemp.ID = i;
                testTwoDbTemp.NAME = "d354dfs 6ds74f568das4f a";
                testOneDb.TEST_TWO_LIST_PROP.Add(testTwoDbTemp);
            }

            var result = testOneDb.ToDto();
            var rez2 = result.ToDatabase();

        }

        private void DataGenerator(TEST_ONE source)
        {
        }


    }
}
