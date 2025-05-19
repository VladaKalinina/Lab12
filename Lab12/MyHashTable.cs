using Plants;
using System;

namespace Lab12
{
    public class MyHashTable<TKey, TValue> where TValue : IInit, Plants.ICloneable, IComparable<TValue>
    {
        // Переименовываем Pair в HashPair, чтобы избежать конфликта
        private class HashPair<TKey, TValue>
        {
            public TKey Key { get; set; }
            public TValue Value { get; set; }

            public HashPair(TKey key, TValue value)
            {
                Key = key;
                Value = value;
            }
        }

        HashPair<TKey, TValue>[] table; // Массив для хранения пар ключ-значение
        int count = 0; // Счётчик элементов
        double fillRatio; // Коэффициент заполнения
        bool[] deleted; // Массив флагов удаления

        public int Capacity => table.Length; // Ёмкость таблицы
        public int Count => count; // Количество элементов

        public MyHashTable(int size = 10, double fillRatio = 0.72) // Конструктор с размером и коэффициентом
        {
            table = new HashPair<TKey, TValue>[size];
            deleted = new bool[size];
            this.fillRatio = fillRatio;
        }

        public void Print() // Выводит хеш-таблицу
        {
            Console.WriteLine($"      Хеш-таблица (Ёмкость: {Capacity,-3})           ");
            for (int i = 0; i < Capacity; i++)
            {
                Console.WriteLine($"      Элемент #{i,-2} ");
                if (table[i] != null && !deleted[i])
                {
                    Console.WriteLine($" Ключ: {table[i].Key}, Значение: {table[i].Value} ");
                }
                else
                {
                    Console.WriteLine("            Пусто                            ");
                }
                if (i < Capacity - 1)
                    Console.WriteLine("────────────────────────────────────────────────");
            }
        }

        public int GetIndex(TKey key) // Вычисляет индекс для ключа
        {
            return Math.Abs(key.GetHashCode()) % Capacity;
        }

        public void AddData(TKey key, TValue value) // Добавляет данные в таблицу
        {
            if (value == null) return;
            int index = GetIndex(key);
            int originalIndex = index;
            int i = 1;

            while (table[index] != null && !deleted[index]) // Ищет свободное место
            {
                if (EqualityComparer<TKey>.Default.Equals(table[index].Key, key))
                    return;
                index = (originalIndex + i * i) % Capacity;
                i++;
                if (index == originalIndex)
                    break;
            }

            table[index] = new HashPair<TKey, TValue>(key, (TValue)(value as Plants.ICloneable)?.Clone() ?? value); // Сохраняет пару
            deleted[index] = false; // Сбрасывает флаг удаления
        }

        public void AddItem(TKey key, TValue value) // Добавляет элемент с расширением таблицы
        {
            if ((double)(Count + 1) / Capacity > fillRatio) // Проверяет заполненность
            {
                HashPair<TKey, TValue>[] temp = (HashPair<TKey, TValue>[])table.Clone();
                bool[] tempDeleted = (bool[])deleted.Clone();
                table = new HashPair<TKey, TValue>[temp.Length * 2];
                deleted = new bool[temp.Length * 2];
                count = 0;

                for (int i = 0; i < temp.Length; i++)
                {
                    if (temp[i] != null && !tempDeleted[i])
                    {
                        AddData(temp[i].Key, temp[i].Value);
                        count++;
                    }
                }
            }

            AddData(key, value);
            count++; // Увеличивает счётчик
        }

        public TValue FindByKey(TKey key) // Ищет значение по ключу
        {
            int index = Math.Abs(key.GetHashCode()) % Capacity;
            int originalIndex = index;
            int i = 1;

            while (table[index] != null)
            {
                if (!deleted[index] && EqualityComparer<TKey>.Default.Equals(table[index].Key, key))
                    return table[index].Value;
                index = (index + i * i) % Capacity;
                i++;
                if (index == originalIndex)
                    break;
            }
            return default(TValue);
        }

        public bool RemoveByKey(TKey key) // Удаляет элемент по ключу
        {
            int index = Math.Abs(key.GetHashCode()) % Capacity;
            int originalIndex = index;
            int i = 1;

            while (table[index] != null)
            {
                if (!deleted[index] && EqualityComparer<TKey>.Default.Equals(table[index].Key, key))
                {
                    deleted[index] = true; // Помечает как удалённый
                    count--;
                    return true;
                }
                index = (index + i * i) % Capacity;
                i++;
                if (index == originalIndex)
                    break;
            }
            return false;
        }
    }
}
