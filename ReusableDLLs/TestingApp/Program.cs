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
            TEST_ONE testOneDb = new TEST_ONE();
            DataGenerator(testOneDb);

            var result = testOneDb.ToDto();
            var rez2 = result.ToDatabase();

        }

        private static void DataGenerator(TEST_ONE source)
        {
            TEST_TWO testTwoDb = new TEST_TWO();

            testTwoDb.ID = 21212;
            testTwoDb.NAME = "sadasdasd";

            source.ID = 14;
            source.NAME_AND_LAST_NAME = "Josip B";
            source.TEST_TWO_PROP = testTwoDb;

            var list = GenerateList();

            foreach(var item in list)
            {
                var innerList = GenerateIntList();
                item.NESTED_LIST.AddRange(innerList);
            }

            source.TEST_TWO_LIST_PROP.AddRange(list);

        }

        private static List<TEST_TWO> GenerateList()
        {
            List<TEST_TWO> result = new List<TEST_TWO>();
            for (int i = 0; i < 1000; i++)
            {
                TEST_TWO testTwoDbTemp = new TEST_TWO();
                testTwoDbTemp.ID = i;
                testTwoDbTemp.NAME = "d354dfs 6ds74f568das4f a: " + i.ToString();
                result.Add(testTwoDbTemp);
            }
            return result;
        }
        private static List<int> GenerateIntList()
        {
            List<int> result = new List<int>();
            for (int i = 0; i < 1000; i++)
            {
                result.Add(i);
            }
            return result;
        }


    }
}
