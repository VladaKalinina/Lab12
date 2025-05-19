using Lab12;
using Plants;

namespace Lab12Test;

[TestClass]
public class PointTreeTests
{
    // Тест конструктора пустого узла
    [TestMethod]
    public void Constructor_EmptyNode_SetsDefaultValues()
    {
        var node = new PointTree<Plant>();
        Assert.IsNull(node.Data, "Data должно быть null.");
    }

    // Тест конструктора узла с данными
    [TestMethod]
    public void Constructor_WithData_SetsDataCorrectly()
    {
        var plant = new Plant("TestPlant", "Green", 1);
        var node = new PointTree<Plant>(plant);
        Assert.AreEqual(plant, node.Data, "Data должно быть равно переданному объекту.");
    }

    // Тест строкового представления узла
    [TestMethod]
    public void ToString_ValidData_ReturnsCorrectString()
    {
        var plant = new Plant("TestPlant", "Green", 1);
        var node = new PointTree<Plant>(plant);
        Assert.AreEqual("Растение: Имя=TestPlant, Цвет=Green", node.ToString(), "ToString должен возвращать корректную строку.");
    }

    // Тест строкового представления пустого узла
    [TestMethod]
    public void ToString_NullData_ReturnsEmptyString()
    {
        var node = new PointTree<Plant>();
        Assert.AreEqual("", node.ToString(), "ToString должен возвращать пустую строку для null Data.");
    }
}