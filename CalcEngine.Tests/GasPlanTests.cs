using System;
using System.Diagnostics.CodeAnalysis;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalcEngine.Tests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class GasPlanTests
    {
        private const int TestLitres = 12 * 230;

        [TestMethod]
        public void GasPlanTests_ConstructorAndAddGasPlan_GasPlanHasBeenAdded()
        {
            // Arrange
            GasPlan classUnderTest = new GasPlan();

            // Act

            classUnderTest.Parts.Add(new GasPlan.GasPlanPart
                                         {
                                             Action = GasPlan.GasPlanPart.ActionTypeEnum.Descend,
                                             Depth = 30,
                                             Minutes = 10,
                                             BestGas = new Gas(TestLitres, 0.21)
                                         });

            classUnderTest.Parts.Add(new GasPlan.GasPlanPart
                                         {
                                             Action = GasPlan.GasPlanPart.ActionTypeEnum.Descend,
                                             Depth = 20,
                                             Minutes = 20,
                                             BestGas = new Gas(TestLitres, 0.21)
                                         });

            // Assert
            Assert.AreEqual(2, classUnderTest.Parts.Count, "Unexpected number of gas parts");
        }

        [TestMethod]
        public void GasPlanTests_ConstructorAndAddDivePlan_DivePlanHasBeenAddedWithTime()
        {
            // Arrange
            DivePlan classUnderTest = new DivePlan();

            // Act

            classUnderTest.Parts.Add(new DivePlan.DivePlanPart(10, 20));

            // Assert
            Assert.AreEqual(1, classUnderTest.Parts.Count, "Unexpected number of diveplanparts");
            Assert.AreEqual(10, classUnderTest.Parts[0].Depth, "Unexpected action");
            Assert.AreEqual(20, classUnderTest.Parts[0].Time, "Unexpected time");
        }

        [TestMethod]
        public void Gas_Constructor_VariousBadValues_ThrowsExceptions()
        {
            // Arrange

            // Act/Assert
            ExceptionAssert.Throws<ArgumentOutOfRangeException>(() => new Gas(0, 0), "litres must be >= 1\r\nParameter name: litres");

            ExceptionAssert.Throws<ArgumentOutOfRangeException>(() => new Gas(TestLitres, 0), "o2Percent must be in the range 0.01 to 1.00\r\nParameter name: o2Percent");
            ExceptionAssert.Throws<ArgumentOutOfRangeException>(() => new Gas(TestLitres, 1.01), "o2Percent must be in the range 0.01 to 1.00\r\nParameter name: o2Percent");

            ExceptionAssert.Throws<ArgumentOutOfRangeException>(() => new Gas(TestLitres, .21, 0), "hePercent must be in the range 0.01 to 1.00\r\nParameter name: hePercent");
            ExceptionAssert.Throws<ArgumentOutOfRangeException>(() => new Gas(TestLitres, .21, 1.01), "hePercent must be in the range 0.01 to 1.00\r\nParameter name: hePercent");

            ExceptionAssert.Throws<ArgumentException>(() => new Gas(TestLitres, 0.5, 0.6), "Mixes should add up to 1.0");
        }

        [TestMethod]
        public void Gas_Constructor_GoodValues_NoHelium_SetsValues()
        {
            // Arrange/Act
            Gas actual = new Gas(TestLitres, 0.21);

            // Assert
            Assert.AreEqual(TestLitres, actual.Litres, "Unexpected litres");
            Assert.AreEqual(0.21, actual.O2Percent, "Unexpected o2percent");
            Assert.AreEqual(0, actual.HePercent, "Unexpected hepercent");
            Assert.AreEqual(0.79, actual.N2Percent, "Unexpected n2percent");
        }

        [TestMethod]
        public void Gas_Constructor_GoodValues_WitghHelium_SetsValues()
        {
            // Arrange/Act
            Gas actual = new Gas(TestLitres, 0.21, 0.40);

            // Assert
            Assert.AreEqual(TestLitres, actual.Litres, "Unexpected litres");
            Assert.AreEqual(0.21, actual.O2Percent, "Unexpected o2percent");
            Assert.AreEqual(0.40, actual.HePercent, "Unexpected hepercent");
            Assert.AreEqual(0.39, actual.N2Percent, "Unexpected n2percent");
        }
    }
}