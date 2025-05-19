using Lab12;
using Plants;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Lab12Test
{
    [TestClass]
    public class PointTests
    {
        [TestMethod]
        public void Constructor_NoData_SetsDataToDefault()
        {
            Point<Plant> point = new Point<Plant>();
            Assert.IsNull(point.Data);
        }

        [TestMethod]
        public void Constructor_WithData_SetsCorrectData()
        {
            Plant plant = new Plant("TestPlant", "Green", 1);
            Point<Plant> point = new Point<Plant>(plant);
            Assert.AreEqual(plant.ToString(), point.Data.ToString());
        }

        [TestMethod]
        public void Constructor_WithData_SetsNextToNull()
        {
            Plant plant = new Plant("TestPlant", "Green", 1);
            Point<Plant> point = new Point<Plant>(plant);
            Assert.IsNull(point.Next);
        }

        [TestMethod]
        public void Constructor_WithData_SetsPredToNull()
        {
            Plant plant = new Plant("TestPlant", "Green", 1);
            Point<Plant> point = new Point<Plant>(plant);
            Assert.IsNull(point.Pred);
        }

        [TestMethod]
        public void MakeRandomData_PlantType_ReturnsPointWithPlant()
        {
            Point<Plant> point = new Point<Plant>();
            Point<Plant> result = point.MakeRandomData();
            Assert.IsNotNull(result.Data);
        }

        [TestMethod]
        public void MakeRandomData_ReturnsPlantType()
        {
            Point<Plant> point = new Point<Plant>();
            Point<Plant> result = point.MakeRandomData();
            Assert.IsTrue(result.Data is Plant);
        }

        [TestMethod]
        public void MakeRandomData_NonPlantType_ThrowsException()
        {
            Point<int> point = new Point<int>();
            Assert.ThrowsException<InvalidOperationException>(() => point.MakeRandomData());
        }

        [TestMethod]
        public void MakeRandomItem_PlantType_ReturnsPlant()
        {
            Point<Plant> point = new Point<Plant>();
            Plant result = point.MakeRandomItem();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void MakeRandomItem_ReturnsPlantType()
        {
            Point<Plant> point = new Point<Plant>();
            Plant result = point.MakeRandomItem();
            Assert.IsTrue(result is Plant);
        }

        [TestMethod]
        public void MakeRandomItem_NonPlantType_ThrowsException()
        {
            Point<int> point = new Point<int>();
            Assert.ThrowsException<InvalidOperationException>(() => point.MakeRandomItem());
        }

        [TestMethod]
        public void ToString_WithPlantData_ReturnsPlantString()
        {
            Plant plant = new Plant("TestPlant", "Green", 1);
            Point<Plant> point = new Point<Plant>(plant);
            Assert.AreEqual(plant.ToString(), point.ToString());
        }

        [TestMethod]
        public void ToString_WithNullData_ReturnsEmptyString()
        {
            Point<Plant> point = new Point<Plant>();
            Assert.AreEqual("", point.ToString());
        }

        [TestMethod]
        public void GetHashCode_WithPlantData_ReturnsPlantHashCode()
        {
            Plant plant = new Plant("TestPlant", "Green", 1);
            Point<Plant> point = new Point<Plant>(plant);
            Assert.AreEqual(plant.GetHashCode(), point.GetHashCode());
        }

        [TestMethod]
        public void GetHashCode_WithNullData_ReturnsZero()
        {
            Point<Plant> point = new Point<Plant>();
            Assert.AreEqual(0, point.GetHashCode());
        }

        [TestMethod]
        public void Data_SetValue_UpdatesData()
        {
            Point<Plant> point = new Point<Plant>();
            Plant plant = new Plant("TestPlant", "Green", 1);
            point.Data = plant;
            Assert.AreEqual(plant, point.Data);
        }

        [TestMethod]
        public void Next_SetValue_UpdatesNext()
        {
            Point<Plant> point = new Point<Plant>();
            Point<Plant> nextPoint = new Point<Plant>();
            point.Next = nextPoint;
            Assert.AreEqual(nextPoint, point.Next);
        }

        [TestMethod]
        public void Pred_SetValue_UpdatesPred()
        {
            Point<Plant> point = new Point<Plant>();
            Point<Plant> predPoint = new Point<Plant>();
            point.Pred = predPoint;
            Assert.AreEqual(predPoint, point.Pred);
        }
    }
}