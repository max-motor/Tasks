using NUnit.Framework;
using StringRedacterProject;
using System.Collections.Generic;

namespace StringRedacterCore.Tests
{
    [TestFixture]
    public class StringRedacterTestRedactAsList : StringRedacter
    {
        [Test]
        public void RedactAsList_StringArray_True()
        {
            var a = new[] {"a", "b", "c"};
            Assert.True(base.RedactAsList(a));
            Assert.AreEqual(base.DefaultText, a[0]);
            Assert.AreEqual(base.DefaultText, a[1]);
            Assert.AreEqual(base.DefaultText, a[2]);
        }

        [Test]
        public void RedactAsList_ObjectArray_True()
        {
            var a = new object[] { "a", 5, "c" };
            Assert.True(base.RedactAsList(a));
            Assert.AreEqual(base.DefaultText, a[0]);
            Assert.AreEqual(5, a[1]);
            Assert.AreEqual(base.DefaultText, a[2]);
        }

        [Test]
        public void RedactAsList_StringList_True()
        {
            var a = new List<string> { { "a" }, { "b" }, { "c" } };
            Assert.True(base.RedactAsList(a));
            Assert.AreEqual(base.DefaultText, a[0]);
            Assert.AreEqual(base.DefaultText, a[1]);
            Assert.AreEqual(base.DefaultText, a[2]);
        }

        [Test]
        public void RedactAsList_ObjectList_True()
        {
            var a = new List<object> { { "a" }, { 5 }, { "c" } };
            Assert.True(base.RedactAsList(a));
            Assert.AreEqual(base.DefaultText, a[0]);
            Assert.AreEqual(5, a[1]);
            Assert.AreEqual(base.DefaultText, a[2]);
        }

        [Test]
        public void AsStringListTrue()
        {
            var a = new[] { "a", "b", "c" };
            Assert.True(base.RedactAsList(a));
            Assert.AreEqual(base.DefaultText, a[0]);
            Assert.AreEqual(base.DefaultText, a[1]);
            Assert.AreEqual(base.DefaultText, a[2]);
        }

        [Test]
        public void RedactAsList_AsValueType_False()
        {
            Assert.False(base.RedactAsList("b"));
            Assert.False(base.RedactAsList(null));
            Assert.False(base.RedactAsList(-1));
            Assert.False(base.RedactAsList(new object()));
        }
    }
}