using Plants;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab12
{
    public class MyList<T> : IEnumerable<T> where T : IInit, Plants.ICloneable
    {
        public Point<T>? beg = null; // Начало списка
        public Point<T>? end = null; // Конец списка
        public int count = 0; // Количество элементов

        public int Count => count; // Свойство для получения количества

        public MyList()
        {
            beg = null;
        }

        public MyList(int length)
        {
            for (int i = 0; i < length; i++) // Создает список заданной длины
            {
                if (typeof(T) == typeof(Plant))
                {
                    Plant plant = new Plant();
                    plant.RandomInit(); // Инициализирует случайное растение
                    Add((T)(object)plant); // Добавляет в список
                }
                else
                {
                    throw new InvalidOperationException("MyList поддерживает только тип Plant");
                }
            }
        }

        public MyList(MyList<T> otherList)
        {
            Point<T>? current = otherList?.beg; // Копирует элементы из другого списка
            while (current != null)
            {
                T clonedItem = (T)current.Data.Clone(); // Клонирует данные
                Add(clonedItem);
                current = current.Next;
            }
        }

        public void Add(T data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data)); // Проверяет, не null ли данные
            T clonedData = (T)data.Clone(); // Клонирует входные данные
            Point<T> p = new Point<T>(clonedData); // Создает новый узел
            if (beg == null)
            {
                beg = p; // Устанавливает начало списка
                end = p; // Устанавливает конец списка
            }
            else
            {
                end.Next = p; // Связывает с концом
                p.Pred = end; // Устанавливает обратную связь
                end = p; // Обновляет конец
            }
            count++; // Увеличивает счетчик
        }

        public bool Remove(T data)
        {
            if (data == null) return false; // Проверяет, не null ли данные
            Point<T> current = beg;
            while (current != null)
            {
                if (Equals(current.Data, data)) // Находит элемент для удаления
                {
                    if (current == beg)
                    {
                        beg = current.Next; // Обновляет начало списка
                        if (beg != null)
                            beg.Pred = null;
                        else
                            end = null; // Очищает конец, если список пуст
                    }
                    else if (current == end)
                    {
                        end = current.Pred; // Обновляет конец списка
                        if (end != null)
                            end.Next = null;
                    }
                    else
                    {
                        current.Pred.Next = current.Next; // Перестраивает связи
                        current.Next.Pred = current.Pred;
                    }
                    count--; // Уменьшает счетчик
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        public int RemoveByName(string name)
        {
            if (string.IsNullOrEmpty(name)) return 0; // Проверяет корректность имени
            int removedCount = 0; // Счетчик удаленных элементов
            Point<T>? current = beg;
            while (current != null)
            {
                Point<T>? next = current.Next; // Сохраняет следующий элемент
                if (current.Data is Plant plant && plant.Name == name) // Проверяет имя
                {
                    Remove(current.Data); // Удаляет элемент
                    removedCount++;
                }
                current = next;
            }
            return removedCount; // Возвращает количество удаленных элементов
        }

        public void AddKToStart(int k)
        {
            if (k < 0) return; // Проверяет неотрицательность K
            Random rnd = new Random();
            for (int i = 0; i < k; i++) // Добавляет K элементов
            {
                int objectType = rnd.Next(1, 5); // Выбирает случайный тип
                T item = objectType switch
                {
                    1 => (T)(object)new Plant(), // Создает Plant
                    2 => (T)(object)new Tree("Дерево" + rnd.Next(1, 100), "Зеленый", rnd.NextDouble() * 20, rnd.Next()), // Создает Tree
                    3 => (T)(object)new Flower("Цветок" + rnd.Next(1, 100), "Красный", "Запах" + rnd.Next(1, 5), rnd.Next()), // Создает Flower
                    4 => (T)(object)new Rose("Роза" + rnd.Next(1, 100), "Розовый", "Запах" + rnd.Next(1, 5), rnd.Next(2) == 1, rnd.Next()), // Создает Rose
                    _ => (T)(object)new Plant()
                };
                item.RandomInit(); // Инициализирует случайными данными
                Point<T> p = new Point<T>(item); // Создает узел
                if (beg == null)
                {
                    beg = p; // Устанавливает начало
                    end = p; // Устанавливает конец
                }
                else
                {
                    p.Next = beg; // Связывает с началом
                    beg.Pred = p;
                    beg = p; // Обновляет начало
                }
                count++; // Увеличивает счетчик
            }
        }

        public MyList<T> DeepCopyList()
        {
            MyList<T> clonedList = new MyList<T>(); // Создает новый список
            Point<T>? current = beg;
            while (current != null)
            {
                T clonedItem = (T)current.Data.Clone(); // Клонирует данные
                clonedList.Add(clonedItem); // Добавляет в копию
                current = current.Next;
            }
            return clonedList; // Возвращает копию списка
        }

        public void DeleteList()
        {
            if (beg == null)
            {
                return;
            }
            Point<T>? current = beg;
            while (current != null)
            {
                Point<T>? next = current.Next; // Сохраняет следующий элемент
                current.Pred = null; // Очищает связи
                current.Next = null;
                current.Data = default;
                current = next;
            }
            beg = null; // Очищает начало
            end = null; // Очищает конец
            count = 0; // Сбрасывает счетчик
        }

        public IEnumerator<T> GetEnumerator()
        {
            Point<T>? current = beg;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}