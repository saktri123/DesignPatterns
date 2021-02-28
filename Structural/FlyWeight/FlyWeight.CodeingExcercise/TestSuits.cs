using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlyWeight.CodeingExcercise
{
    [TestFixture]
    public class TestSuite
    {
        [Test]
        public void Test()
        {
            var s = new Sentence("alpha beta gamma");
            s[1].Capitalize = true;
            Assert.That(s.ToString(),
              Is.EqualTo("alpha BETA gamma"));
        }
    }

}
