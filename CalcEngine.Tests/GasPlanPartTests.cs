using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalcEngine.Tests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class GasPlanPartTests
    {
        private const int TestLitres = 12 * 230;

        [TestMethod]
        public void GasPlanPart_Constructor_SetsValues()
        {
            // arrange
            List<Gas> expectedRemainingGas = new List<Gas>
                                                 {
                                                     new Gas(TestLitres, 0.21)
                                                 };

            // act
            GasPlan.GasPlanPart classUnderTest = new GasPlan.GasPlanPart
                                                     {
                                                         Action = GasPlan.GasPlanPart.ActionTypeEnum.Ascend,
                                                         Depth = 10,
                                                         BestGas = new Gas(TestLitres, 0.21),
                                                         Minutes = 20,
                                                         DecoCeiling = 1.6,
                                                         RemainingGas = expectedRemainingGas
            };
            
            // assert
            Assert.AreEqual(GasPlan.GasPlanPart.ActionTypeEnum.Ascend, classUnderTest.Action, "Unexpected action");
            Assert.AreEqual(10, classUnderTest.Depth, "Unexpected depth");
            Assert.AreEqual(20, classUnderTest.Minutes, "Unexpected minutes");
            Assert.AreEqual(1.6, classUnderTest.DecoCeiling, "Unexpected deco ceiling");
            Assert.AreEqual(0.21, classUnderTest.BestGas.O2Percent, "Unexpected o2percent");
            Assert.AreEqual(0.79, classUnderTest.BestGas.N2Percent, "Unexpected n2percent");
            Assert.AreEqual(0, classUnderTest.BestGas.HePercent, "Unexpected HePercent");
            CollectionAssert.AreEquivalent(expectedRemainingGas, classUnderTest.RemainingGas, "Unexpected remaining gas");
        }
    }
}