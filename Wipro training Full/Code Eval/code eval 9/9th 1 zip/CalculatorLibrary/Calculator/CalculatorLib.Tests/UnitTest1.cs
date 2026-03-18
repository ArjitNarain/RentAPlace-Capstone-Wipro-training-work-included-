using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculatorLibrary;
using System;

namespace CalculatorLibrary.Tests
{
    [TestClass]
    public class MathOperationsTests
    {
        private Calculator _mathTool;

        [TestInitialize]
        public void Init()
        {
            _mathTool = new Calculator();
        }

        [TestMethod]
        public void Add_TwoValues_ReturnsCorrectResult()
        {
            double output = _mathTool.Add(7, 8);
            Assert.AreEqual(15, output);
        }

        [TestMethod]
        public void Subtract_TwoValues_ReturnsCorrectResult()
        {
            double output = _mathTool.Subtract(20, 6);
            Assert.AreEqual(14, output);
        }

        [TestMethod]
        public void Multiply_TwoValues_ReturnsCorrectResult()
        {
            double output = _mathTool.Multiply(5, 4);
            Assert.AreEqual(20, output);
        }

        [TestMethod]
        public void Divide_TwoValues_ReturnsCorrectResult()
        {
            double output = _mathTool.Divide(18, 3);
            Assert.AreEqual(6, output);
        }

        [TestMethod]
        public void Divide_WithZero_ThrowsDivideByZeroException()
        {
            try
            {
                _mathTool.Divide(25, 0);
                Assert.Fail("Expected DivideByZeroException was not thrown.");
            }
            catch (DivideByZeroException)
            {
                // Test passed
            }
        }

        [TestMethod]
        public void Add_WithZero_ReturnsSameValue()
        {
            double output = _mathTool.Add(9, 0);
            Assert.AreEqual(9, output);
        }
    }
}