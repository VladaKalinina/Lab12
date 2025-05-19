using Plants;
using Lab12;

namespace Lab12Test
{
    [TestClass]
    public class MyListTests
    {
        [TestMethod]
        public void Add_ValidPlant_SetsBegData()
        {
            MyList<Plant> list = new MyList<Plant>();
            Plant plant = new Plant("TestPlant", "Green", 1);
            list.Add(plant);
            Assert.IsNotNull(list.beg);
        }

        [TestMethod]
        public void Add_ValidPlant_IncreasesCount()
        {
            MyList<Plant> list = new MyList<Plant>();
            Plant plant = new Plant("TestPlant", "Green", 1);
            list.Add(plant);
            Assert.AreEqual(1, list.Count);
        }

        [TestMethod]
        public void Remove_ExistingPlant_DecreasesCount()
        {
            MyList<Plant> list = new MyList<Plant>();
            Plant plant = new Plant("TestPlant", "Green", 1);
            list.Add(plant);
            list.Remove(plant);
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void Remove_NonExistingPlant_ReturnsFalse()
        {
            MyList<Plant> list = new MyList<Plant>();
            Plant plant = new Plant("TestPlant", "Green", 1);
            bool result = list.Remove(plant);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RemoveByName_ExistingName_RemovesElement()
        {
            MyList<Plant> list = new MyList<Plant>();
            Plant plant = new Plant("TestPlant", "Green", 1);
            list.Add(plant);
            int result = list.RemoveByName("TestPlant");
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void RemoveByName_NonExistingName_ReturnsZero()
        {
            MyList<Plant> list = new MyList<Plant>();
            Plant plant = new Plant("TestPlant", "Green", 1);
            list.Add(plant);
            int result = list.RemoveByName("OtherPlant");
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void AddKToStart_ValidK_IncreasesCount()
        {
            MyList<Plant> list = new MyList<Plant>();
            list.AddKToStart(3);
            Assert.AreEqual(3, list.Count);
        }

        [TestMethod]
        public void AddKToStart_ZeroK_KeepsCount()
        {
            MyList<Plant> list = new MyList<Plant>();
            list.AddKToStart(0);
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void DeepCopyList_NonEmptyList_ReturnsEqualCount()
        {
            MyList<Plant> list = new MyList<Plant>();
            list.Add(new Plant("TestPlant", "Green", 1));
            MyList<Plant> clonedList = list.DeepCopyList();
            Assert.AreEqual(list.Count, clonedList.Count);
        }

        [TestMethod]
        public void DeepCopyList_NonEmptyList_ReturnsDifferentInstance()
        {
            MyList<Plant> list = new MyList<Plant>();
            list.Add(new Plant("TestPlant", "Green", 1));
            MyList<Plant> clonedList = list.DeepCopyList();
            Assert.AreNotSame(list, clonedList);
        }

        [TestMethod]
        public void DeleteList_NonEmptyList_SetsCountToZero()
        {
            MyList<Plant> list = new MyList<Plant>();
            list.Add(new Plant("TestPlant", "Green", 1));
            list.DeleteList();
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void DeleteList_NonEmptyList_SetsBegToNull()
        {
            MyList<Plant> list = new MyList<Plant>();
            list.Add(new Plant("TestPlant", "Green", 1));
            list.DeleteList();
            Assert.IsNull(list.beg);
        }

        [TestMethod]
        public void Constructor_WithLength_SetsCorrectCount()
        {
            MyList<Plant> list = new MyList<Plant>(3);
            Assert.AreEqual(3, list.Count);
        }

        [TestMethod]
        public void Constructor_WithSource_CopiesCount()
        {
            MyList<Plant> source = new MyList<Plant>();
            source.Add(new Plant("TestPlant", "Green", 1));
            MyList<Plant> list = new MyList<Plant>(source);
            Assert.AreEqual(source.Count, list.Count);
        }

        [TestMethod]
        public void RemoveByName_NullName_ReturnsZero()
        {
            MyList<Plant> list = new MyList<Plant>();
            list.Add(new Plant("TestPlant", "Green", 1));
            int result = list.RemoveByName(null);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GetEnumerator_EmptyList_ReturnsEmpty()
        {
            MyList<Plant> list = new MyList<Plant>();
            int count = 0;
            foreach (var item in list)
                count++;
            Assert.AreEqual(0, count);
        }

        [TestMethod]
        public void GetEnumerator_NonEmptyList_ReturnsCorrectCount()
        {
            MyList<Plant> list = new MyList<Plant>();
            list.Add(new Plant("TestPlant1", "Green", 1));
            list.Add(new Plant("TestPlant2", "Red", 2));
            int count = 0;
            foreach (var item in list)
                count++;
            Assert.AreEqual(2, count);
        }

        [TestMethod]
        public void Remove_MiddleElement_DecreasesCount()
        {
            MyList<Plant> list = new MyList<Plant>();
            Plant plant1 = new Plant("TestPlant1", "Green", 1);
            Plant plant2 = new Plant("TestPlant2", "Red", 2);
            Plant plant3 = new Plant("TestPlant3", "Blue", 3);
            list.Add(plant1);
            list.Add(plant2);
            list.Add(plant3);
            list.Remove(plant2);
            Assert.AreEqual(2, list.Count);
        }

        [TestMethod]
        public void DeleteList_NonEmptyList_SetsEndToNull()
        {
            MyList<Plant> list = new MyList<Plant>();
            list.Add(new Plant("TestPlant", "Green", 1));
            list.DeleteList();
            Assert.IsNull(list.end);
        }

        [TestMethod]
        public void AddKToStart_NegativeK_KeepsCount()
        {
            MyList<Plant> list = new MyList<Plant>();
            list.AddKToStart(-1);
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void Remove_LastElement_DecreasesCount()
        {
            MyList<Plant> list = new MyList<Plant>();
            Plant plant1 = new Plant("TestPlant1", "Green", 1);
            Plant plant2 = new Plant("TestPlant2", "Red", 2);
            list.Add(plant1);
            list.Add(plant2);
            list.Remove(plant2);
            Assert.AreEqual(1, list.Count);
        }

        [TestMethod]
        public void Add_MultipleElements_SetsNextCorrectly()
        {
            MyList<Plant> list = new MyList<Plant>();
            Plant plant1 = new Plant("TestPlant1", "Green", 1);
            Plant plant2 = new Plant("TestPlant2", "Red", 2);
            list.Add(plant1);
            list.Add(plant2);
            Assert.IsNotNull(list.beg.Next);
        }

        [TestMethod]
        public void Add_MultipleElements_SetsPredCorrectly()
        {
            MyList<Plant> list = new MyList<Plant>();
            Plant plant1 = new Plant("TestPlant1", "Green", 1);
            Plant plant2 = new Plant("TestPlant2", "Red", 2);
            list.Add(plant1);
            list.Add(plant2);
            Assert.IsNotNull(list.end.Pred);
        }

        [TestMethod]
        public void GetEnumerator_NonEmptyList_ReturnsCorrectData()
        {
            MyList<Plant> list = new MyList<Plant>();
            Plant plant = new Plant("TestPlant", "Green", 1);
            list.Add(plant);
            Plant firstItem = null;
            foreach (var item in list)
            {
                firstItem = item;
                break;
            }
            Assert.IsNotNull(firstItem);
        }

        [TestMethod]
        public void RemoveByName_EmptyList_ReturnsZero()
        {
            MyList<Plant> list = new MyList<Plant>();
            int result = list.RemoveByName("TestPlant");
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Add_MultipleElements_SetsEndCorrectly()
        {
            MyList<Plant> list = new MyList<Plant>();
            Plant plant1 = new Plant("TestPlant1", "Green", 1);
            Plant plant2 = new Plant("TestPlant2", "Red", 2);
            list.Add(plant1);
            list.Add(plant2);
            Assert.IsNotNull(list.end.Data);
        }

        [TestMethod]
        public void Remove_FirstElement_DecreasesCount()
        {
            MyList<Plant> list = new MyList<Plant>();
            Plant plant1 = new Plant("TestPlant1", "Green", 1);
            Plant plant2 = new Plant("TestPlant2", "Red", 2);
            list.Add(plant1);
            list.Add(plant2);
            list.Remove(plant1);
            Assert.AreEqual(1, list.Count);
        }

        [TestMethod]
        public void Constructor_WithZeroLength_SetsCountToZero()
        {
            MyList<Plant> list = new MyList<Plant>(0);
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void Constructor_WithEmptySource_SetsCountToZero()
        {
            MyList<Plant> source = new MyList<Plant>();
            MyList<Plant> list = new MyList<Plant>(source);
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void AddKToStart_SingleElement_SetsBegData()
        {
            MyList<Plant> list = new MyList<Plant>();
            list.AddKToStart(1);
            Assert.IsNotNull(list.beg.Data);
        }

        [TestMethod]
        public void Remove_SingleElement_SetsBegToNull()
        {
            MyList<Plant> list = new MyList<Plant>();
            Plant plant = new Plant("TestPlant", "Green", 1);
            list.Add(plant);
            list.Remove(plant);
            Assert.IsNull(list.beg);
        }

        [TestMethod]
        public void DeepCopyList_EmptyList_ReturnsEmpty()
        {
            MyList<Plant> list = new MyList<Plant>();
            MyList<Plant> clonedList = list.DeepCopyList();
            Assert.AreEqual(0, clonedList.Count);
        }

        [TestMethod]
        public void Add_SingleElement_SetsEndData()
        {
            MyList<Plant> list = new MyList<Plant>();
            Plant plant = new Plant("TestPlant", "Green", 1);
            list.Add(plant);
            Assert.IsNotNull(list.end.Data);
        }

        [TestMethod]
        public void RemoveByName_MultipleElements_RemovesAllMatching()
        {
            MyList<Plant> list = new MyList<Plant>();
            Plant plant1 = new Plant("TestPlant", "Green", 1);
            Plant plant2 = new Plant("TestPlant", "Red", 2);
            list.Add(plant1);
            list.Add(plant2);
            int result = list.RemoveByName("TestPlant");
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void AddKToStart_CreatesPlantType()
        {
            MyList<Plant> list = new MyList<Plant>();
            list.AddKToStart(1);
            Assert.IsTrue(list.beg.Data is Plant);
        }

        [TestMethod]
        public void DeepCopyList_NonEmptyList_ClonesData()
        {
            MyList<Plant> list = new MyList<Plant>();
            Plant plant = new Plant("TestPlant", "Green", 1);
            list.Add(plant);
            MyList<Plant> clonedList = list.DeepCopyList();
            Assert.AreNotSame(plant, clonedList.beg.Data);
        }

        [TestMethod]
        public void Constructor_WithLength_CreatesPlantType()
        {
            MyList<Plant> list = new MyList<Plant>(1);
            Assert.IsTrue(list.beg.Data is Plant);
        }
    }
}