using Plants;
using Lab12;
using System.Reflection;

namespace Lab12Test
{
    [TestClass]
    public class MyTreeTests
    {
        // Тест конструктора пустого дерева
        [TestMethod]
        public void Constructor_EmptyTree_HasZeroCount()
        {
            var tree = new MyTree<Plant>();
            Assert.AreEqual(0, tree.Count, "Пустое дерево должно иметь Count = 0.");
        }

        // Тест конструктора с элементами - проверка создания сбалансированного дерева
        [TestMethod]
        public void Constructor_WithItems_CreatesBalancedTree()
        {
            var items = new Plant[] { new Plant("Plant1", "Green", 1), new Plant("Plant2", "Red", 2), new Plant("Plant3", "Blue", 3) };
            var tree = new MyTree<Plant>(items);
            Assert.AreEqual(3, tree.Count, "Дерево должно содержать 3 элемента.");
        }

        // Тест преобразования в АВЛ-дерево
        [TestMethod]
        public void TransformToSearchTree_ValidTree_CreatesAVLTree()
        {
            var items = new Plant[] { new Plant("Plant2", "Red", 2), new Plant("Plant1", "Green", 1), new Plant("Plant3", "Blue", 3) };
            var tree = new MyTree<Plant>(items);
            var searchTree = tree.TransformToSearchTree();
            Assert.AreEqual(3, searchTree.Count, "АВЛ-дерево должно содержать 3 элемента.");
        }

        // Тест добавления элемента
        [TestMethod]
        public void AddPoint_ValidData_IncreasesCount()
        {
            var tree = new MyTree<Plant>();
            var plant = new Plant("TestPlant", "Green", 1);
            tree.AddPoint(plant);
            Assert.AreEqual(1, tree.Count, "После добавления одного узла Count должен быть 1.");
        }

        // Тест балансировки после добавления
        [TestMethod]
        public void AddPoint_ValidData_MaintainsBalance()
        {
            var tree = new MyTree<Plant>();
            tree.AddPoint(new Plant("Plant2", "Red", 2));
            tree.AddPoint(new Plant("Plant1", "Green", 1));
            tree.AddPoint(new Plant("Plant3", "Blue", 3));
            Assert.IsTrue(IsBalanced(tree), "Дерево должно быть сбалансированным после добавления.");
        }

        // Тест удаления существующего элемента
        [TestMethod]
        public void RemoveByKey_ExistingKey_RemovesItem()
        {
            var items = new Plant[] { new Plant("Plant2", "Red", 2), new Plant("Plant1", "Green", 1), new Plant("Plant3", "Blue", 3) };
            var tree = new MyTree<Plant>(items);
            var keyToRemove = new Plant("Plant2", "Red", 2);
            tree.RemoveByKey(keyToRemove);
            Assert.AreEqual(2, tree.Count, "После удаления должно остаться 2 узла.");
        }

        // Тест балансировки после удаления
        [TestMethod]
        public void RemoveByKey_ExistingKey_MaintainsBalance()
        {
            var items = new Plant[] { new Plant("Plant2", "Red", 2), new Plant("Plant1", "Green", 1), new Plant("Plant3", "Blue", 3) };
            var tree = new MyTree<Plant>(items);
            var keyToRemove = new Plant("Plant2", "Red", 2);
            tree.RemoveByKey(keyToRemove);
            Assert.IsTrue(IsBalanced(tree), "Дерево должно быть сбалансированным после удаления.");
        }

        // Тест попытки удаления несуществующего элемента
        [TestMethod]
        public void RemoveByKey_NonExistingKey_ReturnsFalse()
        {
            var items = new Plant[] { new Plant("Plant1", "Green", 1), new Plant("Plant2", "Red", 2) };
            var tree = new MyTree<Plant>(items);
            var keyToRemove = new Plant("Plant999", "Blue", 999);
            bool removed = tree.RemoveByKey(keyToRemove);
            Assert.IsFalse(removed, "Удаление несуществующего узла должно вернуть false.");
        }

        // Тест вычисления среднего возраста для деревьев
        [TestMethod]
        public void FindAverageAge_OnlyTrees_ReturnsCorrectAverage()
        {
            var items = new Tree[] { new Tree("Tree1", "Green", 10, 1), new Tree("Tree2", "Red", 20, 2) };
            var tree = new MyTree<Tree>(items);
            double average = tree.FindAverageAge();
            Assert.AreEqual(15.0, average, 0.01, "Среднее арифметическое высоты деревьев должно быть 15.");
        }

        // Тест вычисления среднего возраста для смешанных элементов
        [TestMethod]
        public void FindAverageAge_MixedItems_IgnoresNonTrees()
        {
            var items = new Plant[] { new Tree("Tree1", "Green", 10, 1), new Plant("Plant1", "Blue", 3), new Tree("Tree2", "Red", 20, 2) };
            var tree = new MyTree<Plant>(items);
            double average = tree.FindAverageAge();
            Assert.AreEqual(15.0, average, 0.01, "Среднее арифметическое должно учитывать только деревья.");
        }

        // Тест вычисления среднего возраста для пустого дерева
        [TestMethod]
        public void FindAverageAge_EmptyTree_ReturnsZero()
        {
            var tree = new MyTree<Plant>();
            double average = tree.FindAverageAge();
            Assert.AreEqual(0.0, average, 0.01, "Пустое дерево должно возвращать среднее 0.");
        }

        // Тест глубокого копирования (не должно влиять на оригинал)
        [TestMethod]
        public void Clone_DeepCopy_DoesNotModifyOriginal()
        {
            var plant = new Plant("TestPlant", "Green", 1);
            var tree = new MyTree<Plant>();
            tree.AddPoint(plant);
            var searchTree = tree.TransformToSearchTree();
            var modifiedPlant = searchTree.TransformToSearchTree().root.Data;
            modifiedPlant.Name = "ModifiedPlant";
            Assert.AreEqual("TestPlant", plant.Name, "Оригинальный объект не должен быть изменён после клонирования.");
        }

        // Тест очистки дерева
        [TestMethod]
        public void DeleteTree_ValidTree_ClearsTree()
        {
            var items = new Plant[] { new Plant("Plant1", "Green", 1), new Plant("Plant2", "Red", 2) };
            var tree = new MyTree<Plant>(items);
            tree.DeleteTree();
            Assert.AreEqual(0, tree.Count, "После удаления дерева Count должен быть 0.");
        }

        // Тест вывода дерева в консоль
        [TestMethod]
        public void ShowTree_ValidTree_OutputsCorrectFormat()
        {
            var items = new Plant[] { new Plant("Plant2", "Red", 2), new Plant("Plant1", "Green", 1), new Plant("Plant3", "Blue", 3) };
            var tree = new MyTree<Plant>(items);
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                tree.ShowTree();
                string output = sw.ToString();
                Assert.IsTrue(output.Contains("Plant2"), "Вывод должен содержать корень дерева.");
            }
        }

        // Проверка сбалансированности дерева
        private bool IsBalanced(MyTree<Plant> tree)
        {
            return CheckBalance(tree, GetRoot(tree));
        }

        // Рекурсивная проверка баланса узлов
        private bool CheckBalance(MyTree<Plant> tree, PointTree<Plant> node)
        {
            if (node == null) return true;

            var getBalanceMethod = tree.GetType().GetMethod("GetBalance", BindingFlags.NonPublic | BindingFlags.Instance);
            int balance = (int)getBalanceMethod.Invoke(tree, new object[] { node });
            if (Math.Abs(balance) > 1) return false;

            return CheckBalance(tree, node.Left) && CheckBalance(tree, node.Right);
        }

        // Поиск минимального элемента по возрасту
        private Plant FindMinAge(MyTree<Plant> tree)
        {
            var root = GetRoot(tree);
            if (root == null) return null;

            var current = root;
            while (current.Left != null)
            {
                current = current.Left;
            }
            return current.Data;
        }

        // Получение высоты дерева
        private int GetTreeHeight(MyTree<Plant> tree)
        {
            var root = GetRoot(tree);
            if (root == null) return 0;
            return root.Height;
        }

        // Получение корня дерева через reflection
        private PointTree<Plant> GetRoot(MyTree<Plant> tree)
        {
            return tree.GetType().GetField("root", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(tree) as PointTree<Plant>;
        }
    }
}