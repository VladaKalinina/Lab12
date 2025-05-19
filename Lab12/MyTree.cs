using Plants;
using System;

namespace Lab12
{
    public class MyTree<T> where T : IInit, IComparable<T>, Plants.ICloneable, new()
    {
        public PointTree<T>? root = null; // корень дерева (может быть пустым)
        private int count = 0;
        public int Count => count; // свойство для получения количества узлов

        // конструктор для создания пустого дерева
        public MyTree() { }

        // конструктор для создания дерева из массива элементов
        public MyTree(T[] items)
        {
            if (items == null || items.Length == 0) return;
            count = items.Length;
            root = MakeBalancedTree(items, 0, count - 1); // индексы первого и последнего 
        }

        // создание идеально сбалансированного дерева рекурсивно
        private PointTree<T>? MakeBalancedTree(T[] items, int start, int end)
        {
            if (start > end) return null; // пустое поддерево

            int mid = (start + end) / 2;
            T data = (T)items[mid].Clone(); // копия элемента
            PointTree<T> newItem = new PointTree<T>(data); // формирует узел дерева

            newItem.Left = MakeBalancedTree(items, start, mid - 1);
            newItem.Right = MakeBalancedTree(items, mid + 1, end);
            newItem.Height = 1 + Math.Max(GetHeight(newItem.Left), GetHeight(newItem.Right));

            return newItem;
        }

        // вывод дерева по уровням
        public void ShowTree()
        {
            if (root == null)
            {
                Console.WriteLine("Дерево пусто.");
                return;
            }
            Show(root);
        }

        // вывод дерева по уровням
        private void Show(PointTree<T>? point, string indent = "", bool isLeft = false, bool isRoot = true)
        {
            if (point != null)
            {
                if (point.Right != null)
                {
                    Show(point.Right, indent + (isLeft ? "│   " : "    "), false, false);
                }

                Console.Write(indent);
                if (!isRoot)
                {
                    Console.Write(isLeft ? "└── " : "┌── ");
                }
                Console.WriteLine($"{point.Data} (Height: {point.Height})");

                if (point.Left != null)
                {
                    Show(point.Left, indent + (isLeft ? "    " : "│   "), true, false);
                }
            }
        }

        // получение высоты узла
        private int GetHeight(PointTree<T>? node)
        {
            return node?.Height ?? 0;
        }

        // вычисление баланса
        private int GetBalance(PointTree<T>? node)
        {
            if (node == null) return 0;
            return GetHeight(node.Left) - GetHeight(node.Right);
        }

        // правый поворот (левое поддерево выше правого)
        private PointTree<T> RightRotate(PointTree<T> y) // с балансом > 1
        {
            PointTree<T> x = y.Left; // х левый потомок у
            PointTree<T> T2 = x.Right; // присваетвает парвое поддерево х

            // поворот
            x.Right = y; // перестроение стурктуры (у правое поддерево х)
            y.Left = T2; // левое поддерево у

            y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;
            x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;

            return x; // корень
        }

        // левый поворот (правое поддерево выше левого)
        private PointTree<T> LeftRotate(PointTree<T> x) // с балансом < -1
        {
            PointTree<T> y = x.Right;
            PointTree<T> T2 = y.Left;

            y.Left = x; // перестроение стурктуры(x левое поддерево у)
            x.Right = T2; // правое поддерево

            x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;
            y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;

            return y;
        }

        // добавление узла в АВЛ-дерево
        public void AddPoint(T data)
        {
            root = Insert(root, data);
            count++;
        }

        // вставляет узел и балансирует
        private PointTree<T> Insert(PointTree<T>? node, T data)
        {
            if (node == null)
                return new PointTree<T>(data); // вставка в пустое место

            int compareResult = data.CompareTo(node.Data);
            if (compareResult < 0)
                node.Left = Insert(node.Left, data);
            else if (compareResult > 0)
                node.Right = Insert(node.Right, data);
            else
                return node; // дубликаты не добавляются

            // обновление высоты
            node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));

            // проверка баланса
            int balance = GetBalance(node);

            // левый-левый случай
            if (balance > 1 && data.CompareTo(node.Left.Data) < 0)
                return RightRotate(node);

            // правый-правый случай
            if (balance < -1 && data.CompareTo(node.Right.Data) > 0) // правое поддерево правого потомка
                return LeftRotate(node);

            // левый-правый случай
            if (balance > 1 && data.CompareTo(node.Left.Data) > 0)
            {
                node.Left = LeftRotate(node.Left); // для левого поддерева
                return RightRotate(node); // для текущего узла
            }

            // правый-левый случай
            if (balance < -1 && data.CompareTo(node.Right.Data) < 0)
            {
                node.Right = RightRotate(node.Right); // для правого поддерева
                return LeftRotate(node); // для текущего узла
            }

            return node;
        }

        // преобразование в дерево поиска (с сохранением АВЛ-свойств)
        public MyTree<T> TransformToSearchTree()
        {
            T[] array = new T[count];
            int current = 0;
            TransformToArray(root, array, ref current); // заполнение массива

            MyTree<T> searchTree = new MyTree<T>(); // новое пустое дерево
            for (int i = 0; i < array.Length; i++) // заполнение
            {
                searchTree.AddPoint(array[i]);
            }
            return searchTree;
        }

        // преобразование дерева в массив (обход in-order)
        private void TransformToArray(PointTree<T>? point, T[] array, ref int current)
        {
            if (point != null)
            {
                TransformToArray(point.Left, array, ref current); // обход левого дерева
                array[current] = (T)point.Data.Clone(); // добавляем данные узла
                current++;
                TransformToArray(point.Right, array, ref current);
            }
        }

        // нахождение среднего арифметического 
        public double FindAverageAge()
        {
            if (root == null) return 0;
            double sum = 0;
            int totalCount = 0;
            CalculateAverage(root, ref sum, ref totalCount);
            return totalCount > 0 ? sum / totalCount : 0;
        }

        // суммирует Height
        private void CalculateAverage(PointTree<T>? point, ref double sum, ref int totalCount)
        {
            if (point != null)
            {
                if (point.Data is Tree tree) // является ли объектом tree
                {
                    sum += tree.Height;
                    totalCount++;
                }
                CalculateAverage(point.Left, ref sum, ref totalCount);
                CalculateAverage(point.Right, ref sum, ref totalCount);
            }
        }

        // удаление узла по ключу в АВЛ-дереве
        public bool RemoveByKey(T key)
        {
            int initialCount = count;
            root = DeleteNode(root, key);
            return count < initialCount; // возвращает тру, если узел был удалён
        }

        private PointTree<T>? DeleteNode(PointTree<T>? node, T key)
        {
            if (node == null)
                return null;

            int compareResult = key.CompareTo(node.Data);
            if (compareResult < 0)
                node.Left = DeleteNode(node.Left, key); // в левом
            else if (compareResult > 0)
                node.Right = DeleteNode(node.Right, key); // в правом
            else
            {
                // узел найден
                count--;

                // случай 1 узел — лист
                if (node.Left == null && node.Right == null)
                    return null;

                // случай 2 узел имеет одного потомка
                if (node.Left == null)
                    return node.Right; // заменяем удаляемый узел на правое поддерево
                if (node.Right == null)
                    return node.Left;

                // случай 3 узел имеет двух потомков
                PointTree<T> minNode = FindMin(node.Right); // мин узел в правом поддереве
                node.Data = (T)minNode.Data.Clone();
                node.Right = DeleteNode(node.Right, minNode.Data); // удаляем мин узел
            }

            // обновление высоты
            if (node != null)
            {
                node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
            }

            // проверка баланса
            int balance = GetBalance(node);

            // левый-левый случай
            if (balance > 1 && GetBalance(node.Left) >= 0)
                return RightRotate(node);

            // левый-правый случай
            if (balance > 1 && GetBalance(node.Left) < 0)
            {
                node.Left = LeftRotate(node.Left);
                return RightRotate(node);
            }

            // правый-правый случай
            if (balance < -1 && GetBalance(node.Right) <= 0)
                return LeftRotate(node);

            // правый-левый случай
            if (balance < -1 && GetBalance(node.Right) > 0)
            {
                node.Right = RightRotate(node.Right);
                return LeftRotate(node);
            }

            return node;
        }

        // поиск минимального узла
        private PointTree<T> FindMin(PointTree<T> point)
        {
            PointTree<T> min = point;
            while (min.Left != null) // есть ли левое поддерево
            {
                min = min.Left;
            }
            return min;
        }

        // удаление дерева
        public void DeleteTree()
        {
            root = null;
            count = 0;
        }
    }
}