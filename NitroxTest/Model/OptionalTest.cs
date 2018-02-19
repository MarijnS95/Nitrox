using Microsoft.VisualStudio.TestTools.UnitTesting;
using NitroxModel.DataStructures;

namespace NitroxTest.Model
{
    [TestClass]
    public class OptionalTest
    {
        [TestMethod]
        public void OptionalGet()
        {
            Optional<string> op = Optional<string>.Of("test");
            Assert.AreEqual("test", op.Get());
        }

        [TestMethod]
        public void OptionalIsPresent()
        {
            Optional<string> op = Optional<string>.Of("test");
            Assert.AreEqual(true, op.IsPresent());
        }

        [TestMethod]
        public void OptionalIsNotPresent()
        {
            Optional<string> op = Optional<string>.Empty();
            Assert.AreEqual(false, op.IsPresent());
        }

        [TestMethod]
        public void OptionalOrElseValidValue()
        {
            Optional<string> op = Optional<string>.Of("test");
            Assert.AreEqual("test", op.OrElse("test2"));
        }

        [TestMethod]
        public void OptionalOrElseNoValue()
        {
            Optional<string> op = Optional<string>.Empty();
            Assert.AreEqual("test", op.OrElse("test"));
        }

        [TestMethod]
        public void OptionalEmpty()
        {
            Optional<string> op = Optional<string>.Empty();
            Assert.AreEqual(true, op.IsEmpty());
        }

        // Test functionality with value (non-nullable) types.

        [TestMethod]
        public void OptionalValueTypeGet()
        {
            ValueTypeOptional<int> op = ValueTypeOptional<int>.Of(1);
            Assert.AreEqual(1, op.Get());
        }

        [TestMethod]
        public void OptionalValueTypeIsPresent()
        {
            ValueTypeOptional<int> op = ValueTypeOptional<int>.Of(1);
            Assert.AreEqual(true, op.IsPresent());
        }

        [TestMethod]
        public void OptionalValueTypeIsNotPresent()
        {
            ValueTypeOptional<int> op = ValueTypeOptional<int>.Empty();
            Assert.AreEqual(false, op.IsPresent());
        }

        [TestMethod]
        public void OptionalValueTypeOrElseValidValue()
        {
            ValueTypeOptional<int> op = ValueTypeOptional<int>.Of(1);
            Assert.AreEqual(1, op.OrElse(2));
        }

        [TestMethod]
        public void OptionalValueTypeOrElseNoValue()
        {
            ValueTypeOptional<int> op = ValueTypeOptional<int>.Empty();
            Assert.AreEqual(1, op.OrElse(1));
        }

        [TestMethod]
        public void OptionalValueTypeEmpty()
        {
            ValueTypeOptional<int> op = ValueTypeOptional<int>.Empty();
            Assert.AreEqual(true, op.IsEmpty());
        }
    }
}
