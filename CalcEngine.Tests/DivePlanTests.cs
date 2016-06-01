using System.Diagnostics.CodeAnalysis;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalcEngine.Tests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class DivePlanTests
    {
        [TestMethod]
        public void DivePlanTests_ConstructorAndAddDivePlan_DivePlanHasBeenAddedWithTime()
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
    }
}