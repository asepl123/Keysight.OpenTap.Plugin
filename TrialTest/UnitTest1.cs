using NUnit.Framework;
using Trial;

namespace TrialTest
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            Assert.AreEqual(true, new BalancedParanthesis().myfunc("{[()]}"));
        }

        [Test]
        public void Test2()
        {
            Assert.AreEqual(false, new BalancedParanthesis().myfunc("{[(])}"));
        }

        [Test]
        public void Test3()
        {
            Assert.AreEqual(true, new BalancedParanthesis().myfunc("{{[[(())]]}}"));
        }
    }
}