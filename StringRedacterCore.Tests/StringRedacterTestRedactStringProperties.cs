using NUnit.Framework;
using StringRedacterProject;
using System.Collections.Generic;

namespace StringRedacterCore.Tests
{
    [TestFixture]
    public class StringRedacterTestRedactStringProperties : StringRedacter
    {
        private class TestClass
        {
            public string A0 => "Can't touch this";
            public string A { get; set; }
            public int B { get; set; }
            public object[] C { get; set; }

            public Dictionary<string, object> D { get; set; }

            public List<object> E { get; set; }
        }

        [Test]
        public void RedactStringProperties()
        {
            var test = new TestClass()
            {
                A = "abc",
                B = 500,
                C = new object[] { "abc", 700, "cde", -1, null },
                D = new Dictionary<string, object> { { "a", "abc" }, { "b", "cde" }, { "c", 700 } },
                E = new List<object> { "a", null, -5, "b", 7 }
            };

            base.Redact(test);

            Assert.AreEqual("Can't touch this", test.A0);

            Assert.AreEqual(this.DefaultText, test.A);

            Assert.AreEqual(test.B, 500);

            Assert.AreEqual(this.DefaultText, test.C[0]);
            Assert.AreEqual(700, test.C[1]);
            Assert.AreEqual(this.DefaultText, test.C[2]);
            Assert.AreEqual(-1, test.C[3]);
            Assert.AreEqual(null, test.C[4]);

            Assert.AreEqual(this.DefaultText, test.D["a"]);
            Assert.AreEqual(this.DefaultText, test.D["b"]);
            Assert.AreEqual(700, test.D["c"]);

            Assert.AreEqual(this.DefaultText, test.E[0]);
            Assert.AreEqual(null, test.E[1]);
            Assert.AreEqual(-5, test.E[2]);
            Assert.AreEqual(this.DefaultText, test.E[3]);
            Assert.AreEqual(7, test.E[4]);
        }
    }
}