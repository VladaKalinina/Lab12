using Lab12;
using Plants;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Lab12Test
{
    [TestClass]
    public class PointTests
    {
        // Проверка конструктора без параметров (данные должны быть null)
        [TestMethod]
        public void Constructor_NoData_SetsDataToDefault()
        {
            Point<Plant> point = new Point<Plant>();
            Assert.IsNull(point.Data);
        }

        // Проверка конструктора с передачей данных
        [TestMethod]
        public void Constructor_WithData_SetsCorrectData()
        {
            Plant plant = new Plant("TestPlant", "Green", 1);
            Point<Plant> point = new Point<Plant>(plant);
            Assert.AreEqual(plant.ToString(), point.Data.ToString());
        }

        // Проверка инициализации ссылки Next (должна быть null)
        [TestMethod]
        public void Constructor_WithData_SetsNextToNull()
        {
            Plant plant = new Plant("TestPlant", "Green", 1);
            Point<Plant> point = new Point<Plant>(plant);
            Assert.IsNull(point.Next);
        }

        // Проверка инициализации ссылки  (должна быть null)
        [TestMethod]
        public void Constructor_WithData_SetsPredToNull()
        {
            Plant plant = new Plant("TestPlant", "Green", 1);
            Point<Plant> point = new Point<Plant>(plant);
            Assert.IsNull(point.Pred);
        }

        // Проверка создания  со случайными данными (данные не должны быть null)
        [TestMethod]
        public void MakeRandomData_PlantType_ReturnsPointWithPlant()
        {
            Point<Plant> point = new Point<Plant>();
            Point<Plant> result = point.MakeRandomData();
            Assert.IsNotNull(result.Data);
        }

        // Проверка типа создаваемых случайных данных (должен быть Plant)
        [TestMethod]
        public void MakeRandomData_ReturnsPlantType()
        {
            Point<Plant> point = new Point<Plant>();
            Point<Plant> result = point.MakeRandomData();
            Assert.IsTrue(result.Data is Plant);
        }

        // Проверка обработки неподдерживаемого типа для MakeRandomData
        [TestMethod]
        public void MakeRandomData_NonPlantType_ThrowsException()
        {
            Point<int> point = new Point<int>();
            Assert.ThrowsException<InvalidOperationException>(() => point.MakeRandomData());
        }

        // Проверка создания случайного элемента Plant (не должен быть null)
        [TestMethod]
        public void MakeRandomItem_PlantType_ReturnsPlant()
        {
            Point<Plant> point = new Point<Plant>();
            Plant result = point.MakeRandomItem();
            Assert.IsNotNull(result);
        }

        // Проверка типа создаваемого случайного элемента (должен быть Plant)
        [TestMethod]
        public void MakeRandomItem_ReturnsPlantType()
        {
            Point<Plant> point = new Point<Plant>();
            Plant result = point.MakeRandomItem();
            Assert.IsTrue(result is Plant);
        }

        // Проверка обработки неподдерживаемого типа для MakeRandomItem
        [TestMethod]
        public void MakeRandomItem_NonPlantType_ThrowsException()
        {
            Point<int> point = new Point<int>();
            Assert.ThrowsException<InvalidOperationException>(() => point.MakeRandomItem());
        }

        // Проверка строкового представления точки с данными
        [TestMethod]
        public void ToString_WithPlantData_ReturnsPlantString()
        {
            Plant plant = new Plant("TestPlant", "Green", 1);
            Point<Plant> point = new Point<Plant>(plant);
            Assert.AreEqual(plant.ToString(), point.ToString());
        }

        // Проверка строкового представления  без данных
        [TestMethod]
        public void ToString_WithNullData_ReturnsEmptyString()
        {
            Point<Plant> point = new Point<Plant>();
            Assert.AreEqual("", point.ToString());
        }

        // Проверка хэш-кода  с данными (должен совпадать с хэш-кодом данных)
        [TestMethod]
        public void GetHashCode_WithPlantData_ReturnsPlantHashCode()
        {
            Plant plant = new Plant("TestPlant", "Green", 1);
            Point<Plant> point = new Point<Plant>(plant);
            Assert.AreEqual(plant.GetHashCode(), point.GetHashCode());
        }

        // Проверка хэш-кода  без данных (должен быть 0)
        [TestMethod]
        public void GetHashCode_WithNullData_ReturnsZero()
        {
            Point<Plant> point = new Point<Plant>();
            Assert.AreEqual(0, point.GetHashCode());
        }

        // Проверка установки значения в свойство Data
        [TestMethod]
        public void Data_SetValue_UpdatesData()
        {
            Point<Plant> point = new Point<Plant>();
            Plant plant = new Plant("TestPlant", "Green", 1);
            point.Data = plant;
            Assert.AreEqual(plant, point.Data);
        }

        // Проверка установки ссылки на следующий элемент
        [TestMethod]
        public void Next_SetValue_UpdatesNext()
        {
            Point<Plant> point = new Point<Plant>();
            Point<Plant> nextPoint = new Point<Plant>();
            point.Next = nextPoint;
            Assert.AreEqual(nextPoint, point.Next);
        }

        // Проверка установки ссылки на предыдущий элемент
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
