using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using CalcEngine.Buhlmann;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalcEngine.Tests.Buhlmann
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Zh_L16ATests
    {
        private Zh_L16A _classUnderTest;
        private const int TestLitres = 2760;

        [TestInitialize]
        public void Initialise()
        {
        }

        [TestMethod]
        public void Bulhmann_Zh_L16A_GenerateGasPlan_GeneratesGasPlan()
        {
            // Arrange
            List<Gas> availableGasses = new List<Gas>
                                            {
                                                new Gas(TestLitres, 0.21),
                                                new Gas(TestLitres, 0.5)
                                            };

            List<DivePlan.DivePlanPart> divePlan = new List<DivePlan.DivePlanPart>
                                                       {
                                                           new DivePlan.DivePlanPart(35, 10),
                                                           new DivePlan.DivePlanPart(25, 15),
                                                           new DivePlan.DivePlanPart(20, 15)
                                                       };
            Personal personal = new Personal
                                    {
                                        AscentRate = 11,
                                        DescentRate = 18,
                                        GasSwitchTime = 2,
                                        MaxPpHeDeco = 1.6,
                                        MaxPpHeTransit = 1.4,
                                        MaxPpN2Deco = 1.6,
                                        MaxPpN2Transit = 1.4,
                                        MaxPpO2Deco = 1.6,
                                        MaxPpO2Transit = 1.4,
                                        SacRateBottom = 15,
                                        SacRateDeco = 10
                                    };

            _classUnderTest = new Zh_L16A();

            List<GasPlan.GasPlanPart> expected = new List<GasPlan.GasPlanPart>
                                                     {
                                                         new GasPlan.GasPlanPart
                                                             {
                                                                 Action = GasPlan.GasPlanPart.ActionTypeEnum.Descend,
                                                                 DecoCeiling = 1,
                                                                 Depth = 35,
                                                                 Minutes = 3.5,
                                                                 BestGas = new Gas(TestLitres, 0.21),
                                                                 RemainingGas = new List<Gas>()
                                                             },
                                                         new GasPlan.GasPlanPart
                                                             {
                                                                 Action = GasPlan.GasPlanPart.ActionTypeEnum.Level,
                                                                 DecoCeiling = 1,
                                                                 Depth = 35,
                                                                 Minutes = 10,
                                                                 BestGas = new Gas(TestLitres, 0.21),
                                                                 RemainingGas = new List<Gas>()
                                                             },
                                                         new GasPlan.GasPlanPart
                                                             {
                                                                 Action = GasPlan.GasPlanPart.ActionTypeEnum.Ascend,
                                                                 DecoCeiling = 1,
                                                                 Depth = 25,
                                                                 Minutes = 0.9090909090909091,
                                                                 BestGas = new Gas(TestLitres, 0.21),
                                                                 RemainingGas = new List<Gas>()
                                                             },
                                                         new GasPlan.GasPlanPart
                                                             {
                                                                 Action = GasPlan.GasPlanPart.ActionTypeEnum.Level,
                                                                 DecoCeiling = 1,
                                                                 Depth = 25,
                                                                 Minutes = 15,
                                                                 BestGas = new Gas(TestLitres, 0.50),
                                                                 RemainingGas = new List<Gas>()
                                                             },
                                                         new GasPlan.GasPlanPart
                                                             {
                                                                 Action = GasPlan.GasPlanPart.ActionTypeEnum.Ascend,
                                                                 DecoCeiling = 1,
                                                                 Depth = 20,
                                                                 Minutes = 0.4545454545454545,
                                                                 BestGas = new Gas(TestLitres, 0.50),
                                                                 RemainingGas = new List<Gas>()
                                                             },
                                                         new GasPlan.GasPlanPart
                                                             {
                                                                 Action = GasPlan.GasPlanPart.ActionTypeEnum.Level,
                                                                 DecoCeiling = 1,
                                                                 Depth = 20,
                                                                 Minutes = 15,
                                                                 BestGas = new Gas(TestLitres, 0.50),
                                                                 RemainingGas = new List<Gas>()
                                                             },
                                                         new GasPlan.GasPlanPart
                                                             {
                                                                 Action = GasPlan.GasPlanPart.ActionTypeEnum.Ascend,
                                                                 DecoCeiling = 1,
                                                                 Depth = 6,
                                                                 Minutes = 1.272727272727273,
                                                                 BestGas = new Gas(TestLitres, 0.50),
                                                                 RemainingGas = new List<Gas>()
                                                             },
                                                         new GasPlan.GasPlanPart
                                                             {
                                                                 Action = GasPlan.GasPlanPart.ActionTypeEnum.DecoStop,
                                                                 DecoCeiling = 1,
                                                                 Depth = 6,
                                                                 Minutes = 3,
                                                                 BestGas = new Gas(TestLitres, 0.50),
                                                                 RemainingGas = new List<Gas>()
                                                             },
                                                         new GasPlan.GasPlanPart
                                                             {
                                                                 Action = GasPlan.GasPlanPart.ActionTypeEnum.Ascend,
                                                                 DecoCeiling = 1,
                                                                 Depth = 0,
                                                                 Minutes = 1,
                                                                 BestGas = new Gas(TestLitres, 0.50),
                                                                 RemainingGas = new List<Gas>()
                                                             }
                                                     };

            // Act
            GasPlan gasPlan = _classUnderTest.Generate(availableGasses, divePlan, personal);

            // Assert
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Bulhmann_Zh_L16A_CalcCompartment()
        {
            // arrange
            Zh_L16A classUnderTest = new Zh_L16A();

            // Act
            /*
            A diver descends from the surface to 30 metres on air and waits there ten minutes.
            The partial pressure of nitrogen in the breathing gas (Pgas) is 4 x 0.79 = 3.16 bar. Let's pick a
            compartment, say number five. The nitrogen half-time for compartment five (tht) is 27 minutes. The
            nitrogen partial pressure in compartment five on the surface (pBegin) is 0.79, assuming the diver hasn't
            already been diving or subject to any altitude changes. The length of the exposure (exposure) is ten minutes.
            */

            double actual = classUnderTest.CalcCompartment(0.79, 3.16, 10, 27);

            // Assert
            Assert.AreEqual(1.3266, Math.Round(actual, 4), "Unexpected value");
        }

        [TestMethod]
        public void Bulhmann_Zh_L16A_CalcAValue()
        {
            // arrange
            Zh_L16A classUnderTest = new Zh_L16A();

            // act
            double actual = classUnderTest.CalcAValue(27);

            // Assert
            Assert.AreEqual(0.6667, Math.Round(actual, 4), "Unexpected value");
        }

        [TestMethod]
        public void Bulhmann_Zh_L16A_CalcBValue()
        {
            // arrange
            Zh_L16A classUnderTest = new Zh_L16A();

            // act
            double actual = classUnderTest.CalcBValue(27);

            // Assert
            Assert.AreEqual(0.8125, Math.Round(actual, 4), "Unexpected value");
        }

        [TestMethod]
        public void Bulhmann_Zh_L16A_CalcSafeAscentDepth_ProvidingJustCompartmentHalfTime_CalculatesDepth_ReturnsDepthInBar()
        {
            // arrange
            Zh_L16A classUnderTest = new Zh_L16A();

            // act
            double actual = classUnderTest.CalcSafeAscentDepth(1.33, 27);

            // Assert
            Assert.AreEqual(0.54, Math.Round(actual, 2), "Unexpected value");
        }

        [TestMethod]
        public void Bulhmann_Zh_L16A_CalcSafeAscentDepth_ProvidingAAndBValues_CalculatesDepth_ReturnsDepthInBar()
        {
            // arrange
            Zh_L16A classUnderTest = new Zh_L16A();

            double a = classUnderTest.CalcAValue(27);
            double b = classUnderTest.CalcBValue(27);

            // act
            double actual = classUnderTest.CalcSafeAscentDepth(1.33, a, b);

            // Assert
            Assert.AreEqual(0.54, Math.Round(actual, 2), "Unexpected value");
        }
    }
}