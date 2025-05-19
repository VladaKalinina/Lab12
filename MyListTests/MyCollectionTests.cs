using Plants;
using Lab12;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace Lab12Test
{
    [TestClass]
    public class MyCollectionTests
    {
        // ���������������� ����� ������ ��� �������� ��������
        private class CollisionKey
        {
            private readonly int value;
            private readonly int hashCode;

            public CollisionKey(int value, int hashCode)
            {
                this.value = value;
                this.hashCode = hashCode;
            }

            public override int GetHashCode()
            {
                return hashCode;
            }

            public override bool Equals(object obj)
            {
                if (obj == null || GetType() != obj.GetType())
                    return false;
                CollisionKey other = (CollisionKey)obj;
                return value == other.value;
            }
        }

        // ���� ������������ ������ ���������
        [TestMethod]
        public void Constructor_EmptyCollection_HasZeroCount()
        {
            var collection = new MyCollection<int, Plant>();
            Assert.AreEqual(0, collection.Count, "������ ��������� ������ ����� Count = 0.");
        }

        // ���� ������������ � �������� ��������
        [TestMethod]
        public void Constructor_WithSize_HasCorrectCapacity()
        {
            var collection = new MyCollection<int, Plant>(5);
            Assert.AreEqual(5, collection.Capacity, "��������� ������ ����� �������� �������.");
            Assert.AreEqual(0, collection.Count, "Count ������ ���� 0 ����� ��������.");
        }

        // ���� ���������� ��������
        [TestMethod]
        public void Add_ValidData_IncreasesCount()
        {
            var collection = new MyCollection<int, Plant>();
            var plant = new Plant("TestPlant", "Green", 1);
            collection.Add(1, plant);
            Assert.AreEqual(1, collection.Count, "����� ���������� �������� Count ������ ���� 1.");
        }

        // ���� ������ �������� �� �����
        [TestMethod]
        public void FindByKey_ExistingKey_ReturnsValue()
        {
            var collection = new MyCollection<int, Plant>();
            var plant = new Plant("TestPlant", "Green", 1);
            collection.Add(1, plant);
            var foundPlant = collection.FindByKey(1);
            Assert.AreEqual("TestPlant", foundPlant.Name, "��������� ������� ������ ����� ���������� ���.");
        }

        // ���� �������� ������������� ��������
        [TestMethod]
        public void Remove_ExistingKey_ReducesCount()
        {
            var collection = new MyCollection<int, Plant>();
            var plant = new Plant("TestPlant", "Green", 1);
            collection.Add(1, plant);
            collection.Remove(1);
            Assert.AreEqual(0, collection.Count, "����� �������� �������� Count ������ ���� 0.");
        }

        // ���� ������� ���������
        [TestMethod]
        public void Clear_ValidCollection_SetsCountToZero()
        {
            var collection = new MyCollection<int, Plant>();
            var plant = new Plant("TestPlant", "Green", 1);
            collection.Add(1, plant); // ��������� ������� ����� ��������
            collection.Clear();
            Assert.AreEqual(0, collection.Count, "����� ������� ��������� Count ������ ���� 0.");
        }

        // ���� �������� ������� �����
        [TestMethod]
        public void ContainsKey_ExistingKey_ReturnsTrue()
        {
            var collection = new MyCollection<int, Plant>();
            var plant = new Plant("TestPlant", "Green", 1);
            collection.Add(1, plant);
            Assert.IsTrue(collection.ContainsKey(1), "��������� ������ ��������� ���� 1.");
        }

        // ���� ����������� ��������� � ������
        [TestMethod]
        public void CopyTo_ValidArray_CopiesElements()
        {
            var collection = new MyCollection<int, Plant>();
            var plant = new Plant("TestPlant", "Green", 1);
            collection.Add(1, plant);
            var array = new KeyValuePair<int, Plant>[1];
            collection.CopyTo(array, 0);
            Assert.AreEqual("TestPlant", array[0].Value.Name, "������������� ������� ������ ����� ���������� ���.");
        }

        // ���� ������ ��������� � �������
        [TestMethod]
        public void Print_ValidCollection_OutputsCorrectFormat()
        {
            var collection = new MyCollection<int, Plant>();
            var plant = new Plant("TestPlant", "Green", 1);
            collection.Add(1, plant);
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                collection.Print();
                string output = sw.ToString();
                Assert.IsTrue(output.Contains("TestPlant"), "����� ������ ��������� ��� ��������.");
            }
        }

        // ���� ��������� ������������ ���������
        [TestMethod]
        public void Constructor_DeepCopy_DoesNotModifyOriginal()
        {
            var original = new MyCollection<int, Plant>();
            var plant = new Plant("TestPlant", "Green", 1);
            original.Add(1, plant);
            var cloned = new MyCollection<int, Plant>(original);
            cloned.FindByKey(1).Name = "ModifiedPlant";
            Assert.AreEqual("TestPlant", original.FindByKey(1).Name, "������������ ������� �� ������ ���������� ����� ������������.");
        }

        // ���� ���������� �������� � ��� ������������ ������
        [TestMethod]
        public void Add_DuplicateKey_ThrowsArgumentException()
        {
            var collection = new MyCollection<int, Plant>();
            var plant1 = new Plant("TestPlant1", "Green", 1);
            var plant2 = new Plant("TestPlant2", "Red", 2);
            collection.Add(1, plant1);
            try
            {
                collection.Add(1, plant2);
                Assert.Fail("���������� �������� � ������������ ������ ������ ��������� ����������.");
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(true, "��������� ���������� ArgumentException ���������.");
            }
        }

        // ���� ������ ��������������� �����
        [TestMethod]
        public void FindByKey_NonExistingKey_ReturnsDefault()
        {
            var collection = new MyCollection<int, Plant>();
            var result = collection.FindByKey(999);
            Assert.IsNull(result, "����� ��������������� ����� ������ ������� null.");
        }

        // ���� ������������� ����������� ��� ��������� ��������
        [TestMethod]
        public void Indexer_Get_ExistingKey_ReturnsValue()
        {
            var collection = new MyCollection<int, Plant>();
            var plant = new Plant("TestPlant", "Green", 1);
            collection.Add(1, plant);
            var value = collection[1];
            Assert.AreEqual("TestPlant", value.Name, "���������� ������ ������� ������� � ���������� ������.");
        }

        // ���� ������������� ����������� ��� ��������� ��������
        [TestMethod]
        public void Indexer_Set_ExistingKey_UpdatesValue()
        {
            var collection = new MyCollection<int, Plant>();
            var plant1 = new Plant("TestPlant1", "Green", 1);
            var plant2 = new Plant("TestPlant2", "Red", 2);
            collection.Add(1, plant1); // ������ ���������� ������ ������������� ������������
            collection[1] = plant2;
            Assert.AreEqual("TestPlant2", collection.FindByKey(1).Name, "���������� ������ �������� �������� ��������.");
        }

        // ���� ������ Contains ��� KeyValuePair
        [TestMethod]
        public void Contains_ExistingPair_ReturnsTrue()
        {
            var collection = new MyCollection<int, Plant>();
            var plant = new Plant("TestPlant", "Green", 1);
            collection.Add(1, plant);
            var pair = new KeyValuePair<int, Plant>(1, plant);
            Assert.IsTrue(collection.Contains(pair), "��������� ������ ��������� ��������� ���� ����-��������.");
        }

        // ���� �������� Keys
        [TestMethod]
        public void Keys_ValidCollection_ReturnsCorrectKeys()
        {
            var collection = new MyCollection<int, Plant>();
            var plant1 = new Plant("TestPlant1", "Green", 1);
            var plant2 = new Plant("TestPlant2", "Red", 2);
            collection.Add(1, plant1);
            collection.Add(2, plant2);
            var keys = collection.Keys;
            Assert.AreEqual(2, keys.Count, "�������� Keys ������ ������� 2 �����.");
        }

        // ���� �������� Values
        [TestMethod]
        public void Values_ValidCollection_ReturnsCorrectValues()
        {
            var collection = new MyCollection<int, Plant>();
            var plant1 = new Plant("TestPlant1", "Green", 1);
            var plant2 = new Plant("TestPlant2", "Red", 2);
            collection.Add(1, plant1);
            collection.Add(2, plant2);
            var values = collection.Values;
            Assert.AreEqual(2, values.Count, "�������� Values ������ ������� 2 ��������.");
        }

        // ���� ������ TryGetValue ��� ������������� �����
        [TestMethod]
        public void TryGetValue_ExistingKey_ReturnsTrue()
        {
            var collection = new MyCollection<int, Plant>();
            var plant = new Plant("TestPlant", "Green", 1);
            collection.Add(1, plant);
            Plant value;
            var result = collection.TryGetValue(1, out value);
            Assert.IsTrue(result, "TryGetValue ������ ������� true ��� ������������� �����.");
        }

        // ���� ������ TryGetValue ��� ��������������� �����
        [TestMethod]
        public void TryGetValue_NonExistingKey_ReturnsFalse()
        {
            var collection = new MyCollection<int, Plant>();
            Plant value;
            var result = collection.TryGetValue(999, out value);
            Assert.IsFalse(result, "TryGetValue ������ ������� false ��� ��������������� �����.");
        }

        // ���� ���������� null ��������
        [TestMethod]
        public void Add_NullValue_ThrowsArgumentNullException()
        {
            var collection = new MyCollection<int, Plant>();
            try
            {
                collection.Add(1, null);
                Assert.Fail("���������� null �������� ������ ��������� ����������.");
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true, "��������� ���������� ArgumentNullException ���������.");
            }
        }

        // ���� ������� � ��������������� ����� ����� ����������
        [TestMethod]
        public void Indexer_Get_NonExistingKey_ThrowsKeyNotFoundException()
        {
            var collection = new MyCollection<int, Plant>();
            try
            {
                var value = collection[999];
                Assert.Fail("������ � ��������������� ����� ������ ��������� ����������.");
            }
            catch (KeyNotFoundException)
            {
                Assert.IsTrue(true, "��������� ���������� KeyNotFoundException ���������.");
            }
        }

        // ���� CopyTo � ������������ arrayIndex
        [TestMethod]
        public void CopyTo_NegativeArrayIndex_ThrowsArgumentOutOfRangeException()
        {
            var collection = new MyCollection<int, Plant>();
            var plant = new Plant("TestPlant", "Green", 1);
            collection.Add(1, plant);
            var array = new KeyValuePair<int, Plant>[1];
            try
            {
                collection.CopyTo(array, -1);
                Assert.Fail("CopyTo � ������������� arrayIndex ������ ��������� ����������.");
            }
            catch (ArgumentOutOfRangeException)
            {
                Assert.IsTrue(true, "��������� ���������� ArgumentOutOfRangeException ���������.");
            }
        }

        // ���� CopyTo � ������������� �������� �������
        [TestMethod]
        public void CopyTo_InsufficientArraySize_ThrowsArgumentException()
        {
            var collection = new MyCollection<int, Plant>();
            var plant1 = new Plant("TestPlant1", "Green", 1);
            var plant2 = new Plant("TestPlant2", "Red", 2);
            collection.Add(1, plant1);
            collection.Add(2, plant2);
            var array = new KeyValuePair<int, Plant>[1];
            try
            {
                collection.CopyTo(array, 0);
                Assert.Fail("CopyTo � ������������� �������� ������� ������ ��������� ����������.");
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(true, "��������� ���������� ArgumentException ���������.");
            }
        }

        // ���� CopyTo � null ��������
        [TestMethod]
        public void CopyTo_NullArray_ThrowsArgumentNullException()
        {
            var collection = new MyCollection<int, Plant>();
            var plant = new Plant("TestPlant", "Green", 1);
            collection.Add(1, plant);
            try
            {
                collection.CopyTo(null, 0);
                Assert.Fail("CopyTo � null �������� ������ ��������� ����������.");
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true, "��������� ���������� ArgumentNullException ���������.");
            }
        }

        // ���� �������� ��������������� �����
        [TestMethod]
        public void Remove_NonExistingKey_ReturnsFalse()
        {
            var collection = new MyCollection<int, Plant>();
            var result = collection.Remove(999);
            Assert.IsFalse(result, "�������� ��������������� ����� ������ ������� false.");
        }

        // ���� �������� ����� KeyValuePair
        [TestMethod]
        public void Remove_ExistingPair_ReducesCount()
        {
            var collection = new MyCollection<int, Plant>();
            var plant = new Plant("TestPlant", "Green", 1);
            collection.Add(1, plant);
            var pair = new KeyValuePair<int, Plant>(1, plant);
            collection.Remove(pair);
            Assert.AreEqual(0, collection.Count, "����� �������� ���� Count ������ ���� 0.");
        }

        // ���� �������� �������������� ����
        [TestMethod]
        public void Remove_NonExistingPair_ReturnsFalse()
        {
            var collection = new MyCollection<int, Plant>();
            var plant = new Plant("TestPlant", "Green", 1);
            var pair = new KeyValuePair<int, Plant>(999, plant);
            var result = collection.Remove(pair);
            Assert.IsFalse(result, "�������� �������������� ���� ������ ������� false.");
        }

        // ���� ���������� ������� ������� ��� ���������� fillRatio
        [TestMethod]
        public void Add_TriggerResize_IncreasesCapacity()
        {
            var collection = new MyCollection<int, Plant>(2); // ��������� ������� 2
            var plant1 = new Plant("TestPlant1", "Green", 1);
            var plant2 = new Plant("TestPlant2", "Red", 2);
            var plant3 = new Plant("TestPlant3", "Blue", 3);
            collection.Add(1, plant1); // ������ ���������� ���������
            collection.Add(2, plant2);
            collection.Add(3, plant3); // ������ ������� ���������� �������
            Assert.IsTrue(collection.Capacity > 2, "������� ������ ����������� ����� ���������� fillRatio.");
        }

        // ���� ��������� �� ������ ���������
        [TestMethod]
        public void GetEnumerator_EmptyCollection_ReturnsNoElements()
        {
            var collection = new MyCollection<int, Plant>();
            int count = 0;
            foreach (var item in collection)
            {
                count++;
            }
            Assert.AreEqual(0, count, "�������� ������ ��������� �� ������ ���������� ��������.");
        }

        // ���� ��������� �� �������� ���������
        [TestMethod]
        public void GetEnumerator_NonEmptyCollection_ReturnsAllElements()
        {
            var collection = new MyCollection<int, Plant>();
            var plant1 = new Plant("TestPlant1", "Green", 1);
            var plant2 = new Plant("TestPlant2", "Red", 2);
            collection.Add(1, plant1);
            collection.Add(2, plant2);
            int count = 0;
            foreach (var item in collection)
            {
                count++;
            }
            Assert.AreEqual(2, count, "�������� ������ ������� ��� 2 ��������.");
        }

        // ���� ���������� � ��������� ������
        [TestMethod]
        public void Add_WithCollision_HandlesQuadraticProbing()
        {
            var collection = new MyCollection<CollisionKey, Plant>();
            var plant1 = new Plant("TestPlant1", "Green", 1);
            var plant2 = new Plant("TestPlant2", "Red", 2);
            var key1 = new CollisionKey(1, 0); // ��� ����� ����� ���-��� 0
            var key2 = new CollisionKey(2, 0);
            collection.Add(key1, plant1);
            collection.Add(key2, plant2);
            Assert.AreEqual(2, collection.Count, "��������� ������ ��������� ���������� �������� � �������� 2 ��������.");
        }

        // ���� ���������� � ����������� ����������
        [TestMethod]
        public void Add_WithMultipleCollisions_HandlesQuadraticProbing()
        {
            var collection = new MyCollection<CollisionKey, Plant>();
            var plant1 = new Plant("TestPlant1", "Green", 1);
            var plant2 = new Plant("TestPlant2", "Red", 2);
            var plant3 = new Plant("TestPlant3", "Blue", 3);
            var plant4 = new Plant("TestPlant4", "Yellow", 4);
            var key1 = new CollisionKey(1, 0); // ��� ����� ����� ���-��� 0
            var key2 = new CollisionKey(2, 0);
            var key3 = new CollisionKey(3, 0);
            var key4 = new CollisionKey(4, 0);
            collection.Add(key1, plant1);
            collection.Add(key2, plant2);
            collection.Add(key3, plant3);
            collection.Add(key4, plant4);
            Assert.AreEqual(4, collection.Count, "��������� ������ ���������� 4 �������� � �������� ��� ��������.");
        }

        // ���� ������ � ��������� ������
        [TestMethod]
        public void FindByKey_WithCollision_ReturnsCorrectValue()
        {
            var collection = new MyCollection<CollisionKey, Plant>();
            var plant1 = new Plant("TestPlant1", "Green", 1);
            var plant2 = new Plant("TestPlant2", "Red", 2);
            var key1 = new CollisionKey(1, 0); // ��� ����� ����� ���-��� 0
            var key2 = new CollisionKey(2, 0);
            collection.Add(key1, plant1);
            collection.Add(key2, plant2);
            var foundPlant = collection.FindByKey(key2);
            Assert.AreEqual("TestPlant2", foundPlant.Name, "FindByKey ������ ����� ������ ������� ��� ��������.");
        }

        // ���� ������ � ����������� �����
        [TestMethod]
        public void FindByKey_LoopCompletes_ReturnsDefault()
        {
            var collection = new MyCollection<CollisionKey, Plant>();
            var plant1 = new Plant("TestPlant1", "Green", 1);
            var plant2 = new Plant("TestPlant2", "Red", 2);
            var plant3 = new Plant("TestPlant3", "Blue", 3);
            var key1 = new CollisionKey(1, 0);
            var key2 = new CollisionKey(2, 0);
            var key3 = new CollisionKey(3, 0);
            var keyNotFound = new CollisionKey(4, 0);
            collection.Add(key1, plant1);
            collection.Add(key2, plant2);
            collection.Add(key3, plant3);
            var result = collection.FindByKey(keyNotFound);
            Assert.IsNull(result, "FindByKey ������ ������� null, ���� ���� �� ������ ����� ������� ������.");
        }

        // ���� �������� � ��������� ������
        [TestMethod]
        public void Remove_WithCollision_RemovesCorrectElement()
        {
            var collection = new MyCollection<CollisionKey, Plant>();
            var plant1 = new Plant("TestPlant1", "Green", 1);
            var plant2 = new Plant("TestPlant2", "Red", 2);
            var key1 = new CollisionKey(1, 0); // ��� ����� ����� ���-��� 0
            var key2 = new CollisionKey(2, 0);
            collection.Add(key1, plant1);
            collection.Add(key2, plant2);
            collection.Remove(key1);
            Assert.AreEqual(1, collection.Count, "Remove ������ ������� ������ �������, ������� ������.");
        }

        // ���� �������� � ����������� �����
        [TestMethod]
        public void Remove_LoopCompletes_ReturnsFalse()
        {
            var collection = new MyCollection<CollisionKey, Plant>();
            var plant1 = new Plant("TestPlant1", "Green", 1);
            var plant2 = new Plant("TestPlant2", "Red", 2);
            var plant3 = new Plant("TestPlant3", "Blue", 3);
            var key1 = new CollisionKey(1, 0);
            var key2 = new CollisionKey(2, 0);
            var key3 = new CollisionKey(3, 0);
            var keyNotFound = new CollisionKey(4, 0);
            collection.Add(key1, plant1);
            collection.Add(key2, plant2);
            collection.Add(key3, plant3);
            var result = collection.Remove(keyNotFound);
            Assert.IsFalse(result, "Remove ������ ������� false, ���� ���� �� ������ ����� ������� ������.");
        }

        // ���� ���������� ����� �������� (������������� deleted �����)
        [TestMethod]
        public void Add_AfterRemove_ReusesDeletedSlot()
        {
            var collection = new MyCollection<int, Plant>(3);
            var plant1 = new Plant("TestPlant1", "Green", 1);
            var plant2 = new Plant("TestPlant2", "Red", 2);
            collection.Add(1, plant1);
            collection.Remove(1); // �������� ���� ��� ��������
            collection.Add(1, plant2); // ������ ������ �������� ����
            Assert.AreEqual("TestPlant2", collection.FindByKey(1).Name, "���������� ����� �������� ������ ������ �������� ����.");
        }

        // ���� ������� ���������� �������
        [TestMethod]
        public void Add_FullTable_ThrowsInvalidOperationException()
        {
            var collection = new MyCollection<CollisionKey, Plant>();
            var plant1 = new Plant("TestPlant1", "Green", 1);
            var plant2 = new Plant("TestPlant2", "Red", 2);
            var plant3 = new Plant("TestPlant3", "Blue", 3);
            var plant4 = new Plant("TestPlant4", "Yellow", 4);
            var plant5 = new Plant("TestPlant5", "Purple", 5);
            var key1 = new CollisionKey(1, 0); // ��� ����� ����� ���-��� 0
            var key2 = new CollisionKey(2, 0);
            var key3 = new CollisionKey(3, 0);
            var key4 = new CollisionKey(4, 0);
            var key5 = new CollisionKey(5, 0);
            collection.Add(key1, plant1);
            collection.Add(key2, plant2);
            collection.Add(key3, plant3);
            collection.Add(key4, plant4);
            try
            {
                collection.Add(key5, plant5);
                Assert.Fail("���������� � ��������� ����������� ������� ������ ��������� ����������.");
            }
            catch (InvalidOperationException)
            {
                Assert.IsTrue(true, "��������� ���������� InvalidOperationException ���������.");
            }
        }
    }
}
