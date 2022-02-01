using NUnit.Framework;
using StringRedacterProject;
using System.Collections.Generic;

namespace StringRedacterCore.Tests
{
    [TestFixture]
    public class StringRedacterTestRedactAsDictionary : StringRedacter
    {
        [Test]
        public void RedactAsDictionary_StringArray_False()
        {
            var a = new[] {"a", "b", "c"};
            Assert.False(base.RedactAsDictionary(a));
        }

        [Test]
        public void RedactAsList_ObjectArray_True()
        {
            var a = new Dictionary<int, object> { { 1, "a" }, {2, "b" }, {900, 500 }, {4, "c" } };
            Assert.True(base.RedactAsDictionary(a));
            Assert.AreEqual(base.DefaultText, a[1]);
            Assert.AreEqual(base.DefaultText, a[2]);
            Assert.AreEqual(500, a[900]);
            Assert.AreEqual(base.DefaultText, a[4]);
        }

        [Test]
        public void RedactAsDictionary_AsValueType_False()
        {
            Assert.False(base.RedactAsList("b"));
            Assert.False(base.RedactAsList(null));
            Assert.False(base.RedactAsList(-1));
            Assert.False(base.RedactAsList(new object()));
        }
    }
}