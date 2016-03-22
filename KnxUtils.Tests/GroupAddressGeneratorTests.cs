using NUnit.Framework;

namespace KnxUtils.Tests
{
    [TestFixture]
    public class GroupAddressGeneratorTests
    {
        private static GroupAddressGenerator _generator;

        [SetUp]
        public static void Setup()
        {
            _generator = new GroupAddressGenerator();
        }

        [Test]
        public void InitializeGroupAddress_From_SmallesPossible()
        {
            _generator.Reset();
            Assert.AreEqual(_generator.CurrentAddressString, "1/1/1");
        }

        [Test]
        public void GeneratesGroupAddress_In3Edge_Case()
        {
            _generator.Setup(1, 1, 254);
            Assert.AreEqual(_generator.CurrentAddressString, "1/1/254");

            _generator.NextFree();
            Assert.AreEqual(_generator.CurrentAddressString, "1/1/255");

            _generator.NextFree();
            Assert.AreEqual(_generator.CurrentAddressString, "1/2/1");
        }

        [Test]
        public void GeneratesGroupAddress_In2Edge_Case()
        {
            _generator.Setup(1, 255, 254);
            Assert.AreEqual(_generator.CurrentAddressString, "1/255/254");

            _generator.NextFree();
            Assert.AreEqual(_generator.CurrentAddressString, "1/255/255");

            _generator.NextFree();
            Assert.AreEqual(_generator.CurrentAddressString, "2/1/1");
        }

    }
}
