using System.Diagnostics.CodeAnalysis;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalcEngine.Tests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class PersonalTests
    {
        [TestMethod]
        public void Personal_Constructor_SetsValues()
        {
            Personal classUnderTest = new Personal
            {
                AscentRate = 11,
                DescentRate = 20,
                GasSwitchTime = 2,
                MaxPpHeDeco = 1.6,
                MaxPpHeTransit = 1.4,
                MaxPpN2Deco = 1.6,
                MaxPpN2Transit = 1.4,
                MaxPpO2Deco = 1.6,
                MaxPpO2Transit = 1.4,
                SacRateBottom = 15,
                SacRateDeco = 11
            };

            Assert.AreEqual(11, classUnderTest.AscentRate, "Unexpected AscentRate");
            Assert.AreEqual(20, classUnderTest.DescentRate, "Unexpected DescentRate");
            Assert.AreEqual(2, classUnderTest.GasSwitchTime, "Unexpected GasSwitchTime");
            Assert.AreEqual(1.6, classUnderTest.MaxPpHeDeco, "Unexpected MaxPpHeDeco");
            Assert.AreEqual(1.4, classUnderTest.MaxPpHeTransit, "Unexpected MaxPpHeTransit");
            Assert.AreEqual(1.6, classUnderTest.MaxPpN2Deco, "Unexpected MaxPpN2Deco");
            Assert.AreEqual(1.4, classUnderTest.MaxPpN2Transit, "Unexpected MaxPpN2Transit");
            Assert.AreEqual(1.6, classUnderTest.MaxPpO2Deco, "Unexpected MaxPpO2Deco");
            Assert.AreEqual(1.4, classUnderTest.MaxPpO2Transit, "Unexpected MaxPpO2Transit");
            Assert.AreEqual(15, classUnderTest.SacRateBottom, "Unexpected SacRateBottom");
            Assert.AreEqual(11, classUnderTest.SacRateDeco, "Unexpected SacRateDeco");
        }
    }
}