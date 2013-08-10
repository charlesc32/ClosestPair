using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
//using ClosestPair;
using System.Linq;

namespace ClosestPairTests
{
    [TestClass]
    public class ClosestPairsTests
    {
        private List<List<Tuple<int, int>>> exampleInput;
        [TestInitialize()]
        public void Initialize()
        {
            exampleInput = new List<List<Tuple<int, int>>>() {
                new List<Tuple<int, int>>() {
                    new Tuple<int, int>(0, 0), 
                    new Tuple<int, int>(10000, 10000), 
                    new Tuple<int, int>(20000, 20000)
                },
                new List<Tuple<int, int>>() {
                    new Tuple<int, int>(0, 2),
                    new Tuple<int, int>(6, 67),
                    new Tuple<int, int>(43, 71),
                    new Tuple<int, int>(39, 107),
                    new Tuple<int, int>(189, 140)
                }
            };
        }
        
        [TestMethod]
        public void TestParseInput()
        {
            var lines = new List<string>();
            lines.Add("3");
            lines.Add("0 0");
            lines.Add("10000 10000");
            lines.Add("20000 20000");
            lines.Add("5");
            lines.Add("0 2");
            lines.Add("6 67");
            lines.Add("43 71");
            lines.Add("39 107");
            lines.Add("189 140");
            lines.Add("0");

            var expected = exampleInput;
             
            PrivateType cpf = new PrivateType(typeof(ClosestPairFinder));
            var actual = (List<List<Tuple<int, int>>>)cpf.InvokeStatic("ParseInput", lines);
            CollectionAssert.AreEqual(expected[0], actual[0]);
        }

        [TestMethod]
        public void TestFindClosestPair()
        {
            PrivateType cpf = new PrivateType(typeof(ClosestPairFinder));
            var actual = cpf.InvokeStatic("FindClosestPairInSet", exampleInput[1]);
            Assert.AreEqual(actual, "36.2215");
        }

        [TestMethod]
        public void TestCalculate()
        {
            PrivateType cpf = new PrivateType(typeof(ClosestPairFinder));
            var example = new List<List<Tuple<int, int>>>();
            example.Add(exampleInput[1]);
            example.Add(exampleInput[1]);
            var actual = (ParallelQuery<string>)cpf.InvokeStatic("Calculate", example);
            
            foreach (var item in actual)
            {
                Assert.AreEqual(item, "36.2215");    
            }
            
        }
    }
}
