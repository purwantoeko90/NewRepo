using NameSorter.BusinessServices;
using NUnit.Framework;
using System.IO;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_Name_Sorter()
        {
            NameSorterServices nss = new NameSorterServices();
            nss.Sort("C:\\users\\ekopur\\unit-testing\\NameSorter\\unsorted-names-list.txt");
            string[] result = File.ReadAllLines("C:\\users\\ekopur\\unit-testing\\NameSorter\\sorted-names-list.txt");
            string[] expected = File.ReadAllLines("C:\\users\\ekopur\\unit-testing\\NameSorter.Tests\\ExpectedResult.txt");
            Assert.AreEqual(expected, result);
        }
    }
}