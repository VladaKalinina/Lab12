using Plants;
using Lab12;

namespace Lab12Test
{
    [TestClass]
    public class MyHashTableTests
    {
        [TestMethod]
        public void AddItem_ValidKeyValue_IncreasesCount()
        {
            var hashTable = new MyHashTable<int, Plant>(5); // Создаём хеш-таблицу размером 5
            var plant = new Plant("TestPlant", "Green", 1); // Создаём тестовое растение
            hashTable.AddItem(1, plant); // Добавляем элемент с ключом 1
            Assert.AreEqual(1, hashTable.Count); // Проверяем, что Count увеличился
        }

        [TestMethod]
        public void AddItem_ExceedsFillRatio_IncreasesCapacity()
        {
            var hashTable = new MyHashTable<int, Plant>(3, 0.5); // fillRatio 0.5 для быстрого расширения
            hashTable.AddItem(1, new Plant("Plant1", "Green", 1)); // Добавляем первый элемент
            hashTable.AddItem(2, new Plant("Plant2", "Red", 2)); // Добавляем второй элемент, превышаем fillRatio
            Assert.AreEqual(6, hashTable.Capacity); // Проверяем, что ёмкость увеличилась до 6
        }

        [TestMethod]
        public void FindByKey_ExistingKey_ReturnsNotNull()
        {
            var hashTable = new MyHashTable<int, Plant>(5); // Создаём таблицу размером 5
            var plant = new Plant("TestPlant", "Green", 1);
            hashTable.AddItem(1, plant);
            Assert.IsNotNull(hashTable.FindByKey(1)); // Проверяем, что элемент с ключом 1 найден
        }

        [TestMethod]
        public void FindByKey_ExistingKey_ReturnsCorrectName()
        {
            var hashTable = new MyHashTable<int, Plant>(5);
            var plant = new Plant("TestPlant", "Green", 1);
            hashTable.AddItem(1, plant);
            Assert.AreEqual("TestPlant", hashTable.FindByKey(1).Name); // Проверяем имя найденного элемента
        }

        [TestMethod]
        public void FindByKey_NonExistingKey_ReturnsDefault()
        {
            var hashTable = new MyHashTable<int, Plant>(5);
            Assert.IsNull(hashTable.FindByKey(999)); // Проверяем, что несуществующий ключ возвращает null
        }

        [TestMethod]
        public void RemoveByKey_ExistingKey_RemovesItem()
        {
            var hashTable = new MyHashTable<int, Plant>(5);
            var plant = new Plant("TestPlant", "Green", 1);
            hashTable.AddItem(1, plant);
            Assert.IsTrue(hashTable.RemoveByKey(1)); // Проверяем успешное удаление
        }

        [TestMethod]
        public void RemoveByKey_ExistingKey_DecreasesCount()
        {
            var hashTable = new MyHashTable<int, Plant>(5);
            var plant = new Plant("TestPlant", "Green", 1);
            hashTable.AddItem(1, plant);
            hashTable.RemoveByKey(1); // Удаляем элемент с ключом 1
            Assert.AreEqual(0, hashTable.Count); // Проверяем, что Count уменьшился до 0
        }

        [TestMethod]
        public void RemoveByKey_ExistingKey_MakesKeyNotFound()
        {
            var hashTable = new MyHashTable<int, Plant>(5);
            var plant = new Plant("TestPlant", "Green", 1);
            hashTable.AddItem(1, plant);
            hashTable.RemoveByKey(1);
            Assert.IsNull(hashTable.FindByKey(1)); // Проверяем, что ключ больше не найден
        }

        [TestMethod]
        public void RemoveByKey_NonExistingKey_ReturnsFalse()
        {
            var hashTable = new MyHashTable<int, Plant>(5);
            Assert.IsFalse(hashTable.RemoveByKey(999)); // Проверяем удаление несуществующего ключа
        }

        [TestMethod]
        public void RemoveByKey_NonExistingKey_DoesNotChangeCount()
        {
            var hashTable = new MyHashTable<int, Plant>(5);
            hashTable.RemoveByKey(999); // Пытаемся удалить несуществующий ключ
            Assert.AreEqual(0, hashTable.Count); // Проверяем, что Count не изменился
        }

        [TestMethod]
        public void GetIndex_SameKey_ConsistentIndex()
        {
            var hashTable = new MyHashTable<int, Plant>(5);
            int key = 42; // Тестовый ключ
            int index1 = hashTable.GetIndex(key);
            int index2 = hashTable.GetIndex(key);
            Assert.AreEqual(index1, index2); // Проверяем, что индексы совпадают
        }

        [TestMethod]
        public void AddItem_MultipleItems_HandlesCollisions()
        {
            var hashTable = new MyHashTable<int, Plant>(3);
            hashTable.AddItem(1, new Plant("Plant1", "Green", 1));
            hashTable.AddItem(4, new Plant("Plant2", "Red", 2)); // Ключи 1 и 4 могут вызвать коллизию
            Assert.AreEqual(2, hashTable.Count); // Проверяем, что оба элемента добавлены
        }

        [TestMethod]
        public void FindByKey_AfterCollision_ReturnsCorrectValue()
        {
            var hashTable = new MyHashTable<int, Plant>(3);
            hashTable.AddItem(1, new Plant("Plant1", "Green", 1));
            hashTable.AddItem(4, new Plant("Plant2", "Red", 2));
            Assert.AreEqual("Plant2", hashTable.FindByKey(4).Name); // Проверяем значение после коллизии
        }

        [TestMethod]
        public void RemoveByKey_AfterCollision_RemovesCorrectItem()
        {
            var hashTable = new MyHashTable<int, Plant>(3);
            hashTable.AddItem(1, new Plant("Plant1", "Green", 1));
            hashTable.AddItem(4, new Plant("Plant2", "Red", 2));
            hashTable.RemoveByKey(4); // Удаляем элемент с ключом 4
            Assert.IsNull(hashTable.FindByKey(4)); // Проверяем, что ключ 4 больше не найден
        }

        [TestMethod]
        public void AddItem_FullTable_ResizesAndAdds()
        {
            var hashTable = new MyHashTable<int, Plant>(3, 0.5);
            hashTable.AddItem(1, new Plant("Plant1", "Green", 1));
            hashTable.AddItem(2, new Plant("Plant2", "Red", 2));
            hashTable.AddItem(3, new Plant("Plant3", "Blue", 3));
            Assert.AreEqual(3, hashTable.Count);
        }

        [TestMethod]
        public void Capacity_InitialValue_Correct()
        {
            var hashTable = new MyHashTable<int, Plant>(5);
            Assert.AreEqual(5, hashTable.Capacity); // Проверяем начальную ёмкость
        }

        [TestMethod]
        public void AddItem_AfterRemove_ReusesSlot()
        {
            var hashTable = new MyHashTable<int, Plant>(3);
            hashTable.AddItem(1, new Plant("Plant1", "Green", 1));
            hashTable.RemoveByKey(1);
            hashTable.AddItem(1, new Plant("Plant2", "Red", 2));
            Assert.AreEqual("Plant2", hashTable.FindByKey(1).Name); // Проверяем повторное использование слота
        }

        [TestMethod]
        public void AddItem_NegativeKey_HandlesCorrectly()
        {
            var hashTable = new MyHashTable<int, Plant>(5);
            var plant = new Plant("TestPlant", "Green", 1);
            hashTable.AddItem(-1, plant); // Добавляем элемент с отрицательным ключом
            Assert.AreEqual(1, hashTable.Count);
        }

        [TestMethod]
        public void FindByKey_NegativeKey_ReturnsCorrectValue()
        {
            var hashTable = new MyHashTable<int, Plant>(5);
            var plant = new Plant("TestPlant", "Green", 1);
            hashTable.AddItem(-1, plant);
            Assert.AreEqual("TestPlant", hashTable.FindByKey(-1).Name); // Проверяем поиск с отрицательным ключом
        }

        [TestMethod]
        public void RemoveByKey_NegativeKey_RemovesCorrectly()
        {
            var hashTable = new MyHashTable<int, Plant>(5);
            var plant = new Plant("TestPlant", "Green", 1);
            hashTable.AddItem(-1, plant);
            Assert.IsTrue(hashTable.RemoveByKey(-1)); // Проверяем удаление с отрицательным ключом
        }

        [TestMethod]
        public void AddItem_ZeroKey_HandlesCorrectly()
        {
            var hashTable = new MyHashTable<int, Plant>(5);
            var plant = new Plant("TestPlant", "Green", 1);
            hashTable.AddItem(0, plant); // Добавляем элемент с нулевым ключом
            Assert.AreEqual(1, hashTable.Count);
        }

        [TestMethod]
        public void FindByKey_AfterMultipleRemoves_ReturnsCorrectValue()
        {
            var hashTable = new MyHashTable<int, Plant>(5);
            hashTable.AddItem(1, new Plant("Plant1", "Green", 1));
            hashTable.AddItem(2, new Plant("Plant2", "Red", 2));
            hashTable.RemoveByKey(1);
            Assert.AreEqual("Plant2", hashTable.FindByKey(2).Name);
        }

        [TestMethod]
        public void AddItem_LargeKey_HandlesCorrectly()
        {
            var hashTable = new MyHashTable<int, Plant>(5);
            var plant = new Plant("TestPlant", "Green", 1);
            hashTable.AddItem(int.MaxValue, plant); // Добавляем элемент с большим ключом
            Assert.AreEqual(1, hashTable.Count);
        }

        [TestMethod]
        public void FindByKey_LargeKey_ReturnsCorrectValue()
        {
            var hashTable = new MyHashTable<int, Plant>(5);
            var plant = new Plant("TestPlant", "Green", 1);
            hashTable.AddItem(int.MaxValue, plant);
            Assert.AreEqual("TestPlant", hashTable.FindByKey(int.MaxValue).Name);
        }

        [TestMethod]
        public void AddItem_ClonedValue_DoesNotModifyOriginal()
        {
            var hashTable = new MyHashTable<int, Plant>(5);
            var originalPlant = new Plant("TestPlant", "Green", 1);
            hashTable.AddItem(1, originalPlant);
            var foundPlant = hashTable.FindByKey(1);
            foundPlant.Name = "ModifiedPlant"; // Изменяем имя найденного элемента
            Assert.AreEqual("TestPlant", originalPlant.Name); // Проверяем, что оригинал не изменился
        }

        [TestMethod]
        public void AddItem_MultipleAdditions_MaintainsCorrectCount()
        {
            var hashTable = new MyHashTable<int, Plant>(5);
            hashTable.AddItem(1, new Plant("Plant1", "Green", 1));
            hashTable.AddItem(2, new Plant("Plant2", "Red", 2));
            hashTable.AddItem(3, new Plant("Plant3", "Blue", 3));
            Assert.AreEqual(3, hashTable.Count); // Проверяем, что добавлено 3 элемента
        }

        [TestMethod]
        public void RemoveByKey_AfterMultipleAdditions_RemovesOnlySpecified()
        {
            var hashTable = new MyHashTable<int, Plant>(5);
            hashTable.AddItem(1, new Plant("Plant1", "Green", 1));
            hashTable.AddItem(2, new Plant("Plant2", "Red", 2));
            hashTable.RemoveByKey(1);
            Assert.AreEqual("Plant2", hashTable.FindByKey(2).Name); // Проверяем, что второй элемент остался
        }

        [TestMethod]
        public void AddItem_DuplicateKey_RetainsFirstValue()
        {
            var hashTable = new MyHashTable<int, Plant>(3);
            hashTable.AddItem(1, new Plant("Plant1", "Green", 1));
            hashTable.AddItem(1, new Plant("Plant2", "Red", 2));
            Assert.AreEqual("Plant1", hashTable.FindByKey(1).Name); // Проверяем, что осталось первое значение
        }

        [TestMethod]
        public void AddItem_FullCycleInAddData_StopsAtOriginalIndex()
        {
            var hashTable = new MyHashTable<int, Plant>(3, 1.0); // Отключаем расширение
            hashTable.AddItem(0, new Plant("Plant1", "Green", 1));
            hashTable.AddItem(3, new Plant("Plant2", "Red", 2));
            hashTable.AddItem(6, new Plant("Plant3", "Blue", 3));
            hashTable.AddItem(9, new Plant("Plant4", "Yellow", 4));
            Assert.AreEqual(3, hashTable.Count); // Проверяем, что добавлено только 3 элемента
        }

        [TestMethod]
        public void FindByKey_FullCycle_StopsAtOriginalIndex()
        {
            var hashTable = new MyHashTable<int, Plant>(3);
            hashTable.AddItem(0, new Plant("Plant1", "Green", 1));
            hashTable.AddItem(3, new Plant("Plant2", "Red", 2));
            hashTable.AddItem(6, new Plant("Plant3", "Blue", 3));
            Assert.IsNull(hashTable.FindByKey(9)); // Проверяем полный цикл поиска
        }

        [TestMethod]
        public void RemoveByKey_FullCycle_StopsAtOriginalIndex()
        {
            var hashTable = new MyHashTable<int, Plant>(3);
            hashTable.AddItem(0, new Plant("Plant1", "Green", 1));
            hashTable.AddItem(3, new Plant("Plant2", "Red", 2));
            hashTable.AddItem(6, new Plant("Plant3", "Blue", 3));
            Assert.IsFalse(hashTable.RemoveByKey(9)); // Проверяем полный цикл удаления
        }


        [TestMethod]
        public void Print_WithElements_OutputsSeparators()
        {
            var hashTable = new MyHashTable<int, Plant>(3);
            hashTable.AddItem(1, new Plant("Plant1", "Green", 1));
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                hashTable.Print();
                Assert.IsTrue(sw.ToString().Contains("────────────────────────────────────────────────")); // Проверяем разделители
            }
        }

        [TestMethod]
        public void Constructor_SmallSize_HandlesCorrectly()
        {
            var hashTable = new MyHashTable<int, Plant>(1); // Тестируем минимальный размер
            hashTable.AddItem(1, new Plant("Plant1", "Green", 1));
            Assert.AreEqual(1, hashTable.Count);
        }
    }
}