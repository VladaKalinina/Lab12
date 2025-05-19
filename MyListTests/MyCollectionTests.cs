using Plants;
using Lab12;

namespace Lab12Test
{
    [TestClass]
    public class MyCollectionTests
    {
        // Пользовательский класс ключей для создания коллизий
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

        // Тест конструктора пустой коллекции
        [TestMethod]
        public void Constructor_EmptyCollection_HasZeroCount()
        {
            var collection = new MyCollection<int, Plant>();
            Assert.AreEqual(0, collection.Count, "Пустая коллекция должна иметь Count = 0.");
        }

        // Тест конструктора с заданным размером
        [TestMethod]
        public void Constructor_WithSize_HasCorrectCapacity()
        {
            var collection = new MyCollection<int, Plant>(5);
            Assert.AreEqual(5, collection.Capacity, "Коллекция должна иметь заданную ёмкость.");
            Assert.AreEqual(0, collection.Count, "Count должен быть 0 после создания.");
        }

        // Тест добавления элемента
        [TestMethod]
        public void Add_ValidData_IncreasesCount()
        {
            var collection = new MyCollection<int, Plant>();
            var plant = new Plant("TestPlant", "Green", 1);
            collection.Add(1, plant);
            Assert.AreEqual(1, collection.Count, "После добавления элемента Count должен быть 1.");
        }

        // Тест поиска элемента по ключу
        [TestMethod]
        public void FindByKey_ExistingKey_ReturnsValue()
        {
            var collection = new MyCollection<int, Plant>();
            var plant = new Plant("TestPlant", "Green", 1);
            collection.Add(1, plant);
            var foundPlant = collection.FindByKey(1);
            Assert.AreEqual("TestPlant", foundPlant.Name, "Найденный элемент должен иметь корректное имя.");
        }

        // Тест удаления существующего элемента
        [TestMethod]
        public void Remove_ExistingKey_ReducesCount()
        {
            var collection = new MyCollection<int, Plant>();
            var plant = new Plant("TestPlant", "Green", 1);
            collection.Add(1, plant);
            collection.Remove(1);
            Assert.AreEqual(0, collection.Count, "После удаления элемента Count должен быть 0.");
        }

        // Тест очистки коллекции
        [TestMethod]
        public void Clear_ValidCollection_SetsCountToZero()
        {
            var collection = new MyCollection<int, Plant>();
            var plant = new Plant("TestPlant", "Green", 1);
            collection.Add(1, plant); // Добавляем элемент перед очисткой
            collection.Clear();
            Assert.AreEqual(0, collection.Count, "После очистки коллекции Count должен быть 0.");
        }

        // Тест проверки наличия ключа
        [TestMethod]
        public void ContainsKey_ExistingKey_ReturnsTrue()
        {
            var collection = new MyCollection<int, Plant>();
            var plant = new Plant("TestPlant", "Green", 1);
            collection.Add(1, plant);
            Assert.IsTrue(collection.ContainsKey(1), "Коллекция должна содержать ключ 1.");
        }

        // Тест копирования элементов в массив
        [TestMethod]
        public void CopyTo_ValidArray_CopiesElements()
        {
            var collection = new MyCollection<int, Plant>();
            var plant = new Plant("TestPlant", "Green", 1);
            collection.Add(1, plant);
            var array = new KeyValuePair<int, Plant>[1];
            collection.CopyTo(array, 0);
            Assert.AreEqual("TestPlant", array[0].Value.Name, "Скопированный элемент должен иметь корректное имя.");
        }

        // Тест вывода коллекции в консоль
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
                Assert.IsTrue(output.Contains("TestPlant"), "Вывод должен содержать имя элемента.");
            }
        }

        // Тест глубокого клонирования коллекции
        [TestMethod]
        public void Constructor_DeepCopy_DoesNotModifyOriginal()
        {
            var original = new MyCollection<int, Plant>();
            var plant = new Plant("TestPlant", "Green", 1);
            original.Add(1, plant);
            var cloned = new MyCollection<int, Plant>(original);
            cloned.FindByKey(1).Name = "ModifiedPlant";
            Assert.AreEqual("TestPlant", original.FindByKey(1).Name, "Оригинальный элемент не должен измениться после клонирования.");
        }

        // Тест добавления элемента с уже существующим ключом
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
                Assert.Fail("Добавление элемента с существующим ключом должно выбросить исключение.");
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(true, "Ожидаемое исключение ArgumentException выброшено.");
            }
        }

        // Тест поиска несуществующего ключа
        [TestMethod]
        public void FindByKey_NonExistingKey_ReturnsDefault()
        {
            var collection = new MyCollection<int, Plant>();
            var result = collection.FindByKey(999);
            Assert.IsNull(result, "Поиск несуществующего ключа должен вернуть null.");
        }

        // Тест использования индексатора для получения значения
        [TestMethod]
        public void Indexer_Get_ExistingKey_ReturnsValue()
        {
            var collection = new MyCollection<int, Plant>();
            var plant = new Plant("TestPlant", "Green", 1);
            collection.Add(1, plant);
            var value = collection[1];
            Assert.AreEqual("TestPlant", value.Name, "Индексатор должен вернуть элемент с корректным именем.");
        }

        // Тест использования индексатора для установки значения
        [TestMethod]
        public void Indexer_Set_ExistingKey_UpdatesValue()
        {
            var collection = new MyCollection<int, Plant>();
            var plant1 = new Plant("TestPlant1", "Green", 1);
            var plant2 = new Plant("TestPlant2", "Red", 2);
            collection.Add(1, plant1); // Ручное добавление вместо использования конструктора
            collection[1] = plant2;
            Assert.AreEqual("TestPlant2", collection.FindByKey(1).Name, "Индексатор должен обновить значение элемента.");
        }

        // Тест метода Contains для KeyValuePair
        [TestMethod]
        public void Contains_ExistingPair_ReturnsTrue()
        {
            var collection = new MyCollection<int, Plant>();
            var plant = new Plant("TestPlant", "Green", 1);
            collection.Add(1, plant);
            var pair = new KeyValuePair<int, Plant>(1, plant);
            Assert.IsTrue(collection.Contains(pair), "Коллекция должна содержать указанную пару ключ-значение.");
        }

        // Тест свойства Keys
        [TestMethod]
        public void Keys_ValidCollection_ReturnsCorrectKeys()
        {
            var collection = new MyCollection<int, Plant>();
            var plant1 = new Plant("TestPlant1", "Green", 1);
            var plant2 = new Plant("TestPlant2", "Red", 2);
            collection.Add(1, plant1);
            collection.Add(2, plant2);
            var keys = collection.Keys;
            Assert.AreEqual(2, keys.Count, "Свойство Keys должно вернуть 2 ключа.");
        }

        // Тест свойства Values
        [TestMethod]
        public void Values_ValidCollection_ReturnsCorrectValues()
        {
            var collection = new MyCollection<int, Plant>();
            var plant1 = new Plant("TestPlant1", "Green", 1);
            var plant2 = new Plant("TestPlant2", "Red", 2);
            collection.Add(1, plant1);
            collection.Add(2, plant2);
            var values = collection.Values;
            Assert.AreEqual(2, values.Count, "Свойство Values должно вернуть 2 значения.");
        }

        // Тест метода TryGetValue для существующего ключа
        [TestMethod]
        public void TryGetValue_ExistingKey_ReturnsTrue()
        {
            var collection = new MyCollection<int, Plant>();
            var plant = new Plant("TestPlant", "Green", 1);
            collection.Add(1, plant);
            Plant value;
            var result = collection.TryGetValue(1, out value);
            Assert.IsTrue(result, "TryGetValue должен вернуть true для существующего ключа.");
        }

        // Тест метода TryGetValue для несуществующего ключа
        [TestMethod]
        public void TryGetValue_NonExistingKey_ReturnsFalse()
        {
            var collection = new MyCollection<int, Plant>();
            Plant value;
            var result = collection.TryGetValue(999, out value);
            Assert.IsFalse(result, "TryGetValue должен вернуть false для несуществующего ключа.");
        }

        // Тест добавления null значения
        [TestMethod]
        public void Add_NullValue_ThrowsArgumentNullException()
        {
            var collection = new MyCollection<int, Plant>();
            try
            {
                collection.Add(1, null);
                Assert.Fail("Добавление null значения должно выбросить исключение.");
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true, "Ожидаемое исключение ArgumentNullException выброшено.");
            }
        }

        // Тест доступа к несуществующему ключу через индексатор
        [TestMethod]
        public void Indexer_Get_NonExistingKey_ThrowsKeyNotFoundException()
        {
            var collection = new MyCollection<int, Plant>();
            try
            {
                var value = collection[999];
                Assert.Fail("Доступ к несуществующему ключу должен выбросить исключение.");
            }
            catch (KeyNotFoundException)
            {
                Assert.IsTrue(true, "Ожидаемое исключение KeyNotFoundException выброшено.");
            }
        }

        // Тест CopyTo с некорректным arrayIndex
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
                Assert.Fail("CopyTo с отрицательным arrayIndex должен выбросить исключение.");
            }
            catch (ArgumentOutOfRangeException)
            {
                Assert.IsTrue(true, "Ожидаемое исключение ArgumentOutOfRangeException выброшено.");
            }
        }

        // Тест CopyTo с недостаточным размером массива
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
                Assert.Fail("CopyTo с недостаточным размером массива должен выбросить исключение.");
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(true, "Ожидаемое исключение ArgumentException выброшено.");
            }
        }

        // Тест CopyTo с null массивом
        [TestMethod]
        public void CopyTo_NullArray_ThrowsArgumentNullException()
        {
            var collection = new MyCollection<int, Plant>();
            var plant = new Plant("TestPlant", "Green", 1);
            collection.Add(1, plant);
            try
            {
                collection.CopyTo(null, 0);
                Assert.Fail("CopyTo с null массивом должен выбросить исключение.");
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true, "Ожидаемое исключение ArgumentNullException выброшено.");
            }
        }

        // Тест удаления несуществующего ключа
        [TestMethod]
        public void Remove_NonExistingKey_ReturnsFalse()
        {
            var collection = new MyCollection<int, Plant>();
            var result = collection.Remove(999);
            Assert.IsFalse(result, "Удаление несуществующего ключа должно вернуть false.");
        }

        // Тест удаления через KeyValuePair
        [TestMethod]
        public void Remove_ExistingPair_ReducesCount()
        {
            var collection = new MyCollection<int, Plant>();
            var plant = new Plant("TestPlant", "Green", 1);
            collection.Add(1, plant);
            var pair = new KeyValuePair<int, Plant>(1, plant);
            collection.Remove(pair);
            Assert.AreEqual(0, collection.Count, "После удаления пары Count должен быть 0.");
        }

        // Тест удаления несуществующей пары
        [TestMethod]
        public void Remove_NonExistingPair_ReturnsFalse()
        {
            var collection = new MyCollection<int, Plant>();
            var plant = new Plant("TestPlant", "Green", 1);
            var pair = new KeyValuePair<int, Plant>(999, plant);
            var result = collection.Remove(pair);
            Assert.IsFalse(result, "Удаление несуществующей пары должно вернуть false.");
        }

        // Тест увеличения размера таблицы при превышении fillRatio
        [TestMethod]
        public void Add_TriggerResize_IncreasesCapacity()
        {
            var collection = new MyCollection<int, Plant>(2); // Начальная ёмкость 2
            var plant1 = new Plant("TestPlant1", "Green", 1);
            var plant2 = new Plant("TestPlant2", "Red", 2);
            var plant3 = new Plant("TestPlant3", "Blue", 3);
            collection.Add(1, plant1); // Ручное добавление элементов
            collection.Add(2, plant2);
            collection.Add(3, plant3); // Должно вызвать увеличение таблицы
            Assert.IsTrue(collection.Capacity > 2, "Ёмкость должна увеличиться после превышения fillRatio.");
        }

        // Тест итератора на пустой коллекции
        [TestMethod]
        public void GetEnumerator_EmptyCollection_ReturnsNoElements()
        {
            var collection = new MyCollection<int, Plant>();
            int count = 0;
            foreach (var item in collection)
            {
                count++;
            }
            Assert.AreEqual(0, count, "Итератор пустой коллекции не должен возвращать элементы.");
        }

        // Тест итератора на непустой коллекции
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
            Assert.AreEqual(2, count, "Итератор должен вернуть все 2 элемента.");
        }

        // Тест добавления с коллизией ключей
        [TestMethod]
        public void Add_WithCollision_HandlesQuadraticProbing()
        {
            var collection = new MyCollection<CollisionKey, Plant>();
            var plant1 = new Plant("TestPlant1", "Green", 1);
            var plant2 = new Plant("TestPlant2", "Red", 2);
            var key1 = new CollisionKey(1, 0); // Оба ключа имеют хеш-код 0
            var key2 = new CollisionKey(2, 0);
            collection.Add(key1, plant1);
            collection.Add(key2, plant2);
            Assert.AreEqual(2, collection.Count, "Коллекция должна корректно обработать коллизию и добавить 2 элемента.");
        }

        // Тест добавления с несколькими коллизиями
        [TestMethod]
        public void Add_WithMultipleCollisions_HandlesQuadraticProbing()
        {
            var collection = new MyCollection<CollisionKey, Plant>();
            var plant1 = new Plant("TestPlant1", "Green", 1);
            var plant2 = new Plant("TestPlant2", "Red", 2);
            var plant3 = new Plant("TestPlant3", "Blue", 3);
            var plant4 = new Plant("TestPlant4", "Yellow", 4);
            var key1 = new CollisionKey(1, 0); // Все ключи имеют хеш-код 0
            var key2 = new CollisionKey(2, 0);
            var key3 = new CollisionKey(3, 0);
            var key4 = new CollisionKey(4, 0);
            collection.Add(key1, plant1);
            collection.Add(key2, plant2);
            collection.Add(key3, plant3);
            collection.Add(key4, plant4);
            Assert.AreEqual(4, collection.Count, "Коллекция должна обработать 4 коллизии и добавить все элементы.");
        }

        // Тест поиска с коллизией ключей
        [TestMethod]
        public void FindByKey_WithCollision_ReturnsCorrectValue()
        {
            var collection = new MyCollection<CollisionKey, Plant>();
            var plant1 = new Plant("TestPlant1", "Green", 1);
            var plant2 = new Plant("TestPlant2", "Red", 2);
            var key1 = new CollisionKey(1, 0); // Оба ключа имеют хеш-код 0
            var key2 = new CollisionKey(2, 0);
            collection.Add(key1, plant1);
            collection.Add(key2, plant2);
            var foundPlant = collection.FindByKey(key2);
            Assert.AreEqual("TestPlant2", foundPlant.Name, "FindByKey должен найти второй элемент при коллизии.");
        }

        // Тест поиска с завершением цикла
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
            Assert.IsNull(result, "FindByKey должен вернуть null, если ключ не найден после полного обхода.");
        }

        // Тест удаления с коллизией ключей
        [TestMethod]
        public void Remove_WithCollision_RemovesCorrectElement()
        {
            var collection = new MyCollection<CollisionKey, Plant>();
            var plant1 = new Plant("TestPlant1", "Green", 1);
            var plant2 = new Plant("TestPlant2", "Red", 2);
            var key1 = new CollisionKey(1, 0); // Оба ключа имеют хеш-код 0
            var key2 = new CollisionKey(2, 0);
            collection.Add(key1, plant1);
            collection.Add(key2, plant2);
            collection.Remove(key1);
            Assert.AreEqual(1, collection.Count, "Remove должен удалить первый элемент, оставив второй.");
        }

        // Тест удаления с завершением цикла
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
            Assert.IsFalse(result, "Remove должен вернуть false, если ключ не найден после полного обхода.");
        }

        // Тест добавления после удаления (использование deleted флага)
        [TestMethod]
        public void Add_AfterRemove_ReusesDeletedSlot()
        {
            var collection = new MyCollection<int, Plant>(3);
            var plant1 = new Plant("TestPlant1", "Green", 1);
            var plant2 = new Plant("TestPlant2", "Red", 2);
            collection.Add(1, plant1);
            collection.Remove(1); // Помечаем слот как удалённый
            collection.Add(1, plant2); // Должно занять удалённый слот
            Assert.AreEqual("TestPlant2", collection.FindByKey(1).Name, "Добавление после удаления должно занять удалённый слот.");
        }

        // Тест полного заполнения таблицы
        [TestMethod]
        public void Add_FullTable_ThrowsInvalidOperationException()
        {
            var collection = new MyCollection<CollisionKey, Plant>();
            var plant1 = new Plant("TestPlant1", "Green", 1);
            var plant2 = new Plant("TestPlant2", "Red", 2);
            var plant3 = new Plant("TestPlant3", "Blue", 3);
            var plant4 = new Plant("TestPlant4", "Yellow", 4);
            var plant5 = new Plant("TestPlant5", "Purple", 5);
            var key1 = new CollisionKey(1, 0); // Все ключи имеют хеш-код 0
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
                Assert.Fail("Добавление в полностью заполненную таблицу должно выбросить исключение.");
            }
            catch (InvalidOperationException)
            {
                Assert.IsTrue(true, "Ожидаемое исключение InvalidOperationException выброшено.");
            }
        }
    }
}
