using Lab12;
using Plants;

namespace Lab12Test;

[TestClass]
public class PointTreeTests
{
    // ���� ������������ ������� ����
    [TestMethod]
    public void Constructor_EmptyNode_SetsDefaultValues()
    {
        var node = new PointTree<Plant>();
        Assert.IsNull(node.Data, "Data ������ ���� null.");
    }

    // ���� ������������ ���� � �������
    [TestMethod]
    public void Constructor_WithData_SetsDataCorrectly()
    {
        var plant = new Plant("TestPlant", "Green", 1);
        var node = new PointTree<Plant>(plant);
        Assert.AreEqual(plant, node.Data, "Data ������ ���� ����� ����������� �������.");
    }

    // ���� ���������� ������������� ����
    [TestMethod]
    public void ToString_ValidData_ReturnsCorrectString()
    {
        var plant = new Plant("TestPlant", "Green", 1);
        var node = new PointTree<Plant>(plant);
        Assert.AreEqual("��������: ���=TestPlant, ����=Green", node.ToString(), "ToString ������ ���������� ���������� ������.");
    }

    // ���� ���������� ������������� ������� ����
    [TestMethod]
    public void ToString_NullData_ReturnsEmptyString()
    {
        var node = new PointTree<Plant>();
        Assert.AreEqual("", node.ToString(), "ToString ������ ���������� ������ ������ ��� null Data.");
    }
}