using DynamicMVC.ApplicationMetadataLibrary.Strategies.SimpleTypeParsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicMVC.ApplicationMetadataLibraryTest.UnitTests.Strategies
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var stp = new SimpleTypeBoolNullableParser();
            var result = stp.Parse("false");
            Assert.IsTrue(result is bool?);
        }
    }
}
