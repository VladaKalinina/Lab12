using Plants;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab12
{
    // Интерфейс для коллекции
    public interface IMyCollection<TKey, TValue> where TValue : IInit, Plants.ICloneable, IComparable<TValue>
    {
        int Capacity { get; } // Ёмкость коллекции
        int Count { get; } // Количество элементов
        void Add(TKey key, TValue value); // Добавить элемент
        bool Remove(TKey key); // Удалить элемент по ключу
        TValue FindByKey(TKey key); // Найти элемент по ключу
        void Clear(); // Очистить коллекцию
        bool ContainsKey(TKey key); // Проверить наличие ключа
        void Print(); // Вывести коллекцию
    }

    // Класс обобщённой коллекции (хеш-таблица с открытой адресацией)
    public class MyCollection<TKey, TValue> : IMyCollection<TKey, TValue>, IDictionary<TKey, TValue>, IEnumerable<KeyValuePair<TKey, TValue>>
        where TValue : IInit, Plants.ICloneable, IComparable<TValue>
    {
        private Pair<TKey, TValue>[] table; // Массив для хранения пар
        private int count = 0; // Счётчик элементов
        private double fillRatio; // Коэффициент заполнения
        private bool[] deleted; // Флаги удаления

        public int Capacity => table.Length; // Ёмкость таблицы
        public int Count => count; // Количество элементов

        // Пустой конструктор
        public MyCollection()
        {
            table = new Pair<TKey, TValue>[10];
            deleted = new bool[10];
            fillRatio = 0.72;
        }

        // Конструктор с заданным размером
        public MyCollection(int size)
        {
            if (size < 1)
                throw new ArgumentException("Размер таблицы должен быть больше 0.", nameof(size));
            table = new Pair<TKey, TValue>[size];
            deleted = new bool[size];
            fillRatio = 0.72;

            Random rnd = new Random();
            for (int i = 0; i < size; i++)
            {
                TValue value = (TValue)Activator.CreateInstance(typeof(TValue));
                value.RandomInit();
                TKey key = (TKey)(object)((value as Plant)?.Id.Number ?? rnd.Next());
                AddData(key, value);
                count++;
            }
        }

        // Конструктор копирования
        public MyCollection(MyCollection<TKey, TValue> other)
        {
            table = new Pair<TKey, TValue>[other.Capacity];
            deleted = new bool[other.Capacity];
            fillRatio = other.fillRatio;
            count = 0;

            for (int i = 0; i < other.Capacity; i++)
            {
                if (other.table[i] != null && !other.deleted[i])
                {
                    AddData(other.table[i].Key, (TValue)other.table[i].Value.Clone());
                    count++;
                }
            }
        }

        public void Print() // Выводит коллекцию
        {
            Console.WriteLine($"      Коллекция (Хеш-таблица, Ёмкость: {Capacity,-3})           ");
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

        private int GetIndex(TKey key) // Вычисляет индекс для ключа
        {
            return Math.Abs(key.GetHashCode()) % Capacity;
        }

        private void AddData(TKey key, TValue value) // Добавляет данные в таблицу
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            int index = GetIndex(key);
            int originalIndex = index;
            int i = 1;

            while (table[index] != null && !deleted[index]) // Ищет свободное место
            {
                if (EqualityComparer<TKey>.Default.Equals(table[index].Key, key))
                    throw new ArgumentException("Ключ уже существует.", nameof(key));
                index = (originalIndex + i * i) % Capacity;
                i++;
                if (index == originalIndex)
                    throw new InvalidOperationException("Хеш-таблица заполнена.");
            }

            table[index] = new Pair<TKey, TValue>(key, (TValue)(value as Plants.ICloneable)?.Clone() ?? value);
            deleted[index] = false;
        }

        private void AddItem(TKey key, TValue value) // Добавляет элемент с расширением таблицы
        {
            if ((double)(Count + 1) / Capacity > fillRatio)
            {
                Pair<TKey, TValue>[] temp = (Pair<TKey, TValue>[])table.Clone();
                bool[] tempDeleted = (bool[])deleted.Clone();
                table = new Pair<TKey, TValue>[temp.Length * 2];
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
            count++;
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

        public bool Remove(TKey key) // Удаляет элемент по ключу
        {
            int index = Math.Abs(key.GetHashCode()) % Capacity;
            int originalIndex = index;
            int i = 1;

            while (table[index] != null)
            {
                if (!deleted[index] && EqualityComparer<TKey>.Default.Equals(table[index].Key, key))
                {
                    deleted[index] = true;
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

        // Реализация IDictionary<TKey, TValue>
        public TValue this[TKey key]
        {
            get
            {
                TValue value = FindByKey(key);
                if (value == null && !ContainsKey(key))
                    throw new KeyNotFoundException("Ключ не найден.");
                return value;
            }
            set => AddItem(key, value);
        }

        public ICollection<TKey> Keys
        {
            get
            {
                List<TKey> keys = new List<TKey>();
                for (int i = 0; i < Capacity; i++)
                {
                    if (table[i] != null && !deleted[i])
                        keys.Add(table[i].Key);
                }
                return keys;
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                List<TValue> values = new List<TValue>();
                for (int i = 0; i < Capacity; i++)
                {
                    if (table[i] != null && !deleted[i])
                        values.Add(table[i].Value);
                }
                return values;
            }
        }

        public void Add(TKey key, TValue value) => AddItem(key, value);

        public bool ContainsKey(TKey key) => FindByKey(key) != null;

        public bool TryGetValue(TKey key, out TValue value)
        {
            value = FindByKey(key);
            return value != null;
        }

        // Реализация ICollection<KeyValuePair<TKey, TValue>>
        public void Add(KeyValuePair<TKey, TValue> item) => AddItem(item.Key, item.Value);

        public void Clear()
        {
            table = new Pair<TKey, TValue>[Capacity];
            deleted = new bool[Capacity];
            count = 0;
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            TValue value = FindByKey(item.Key);
            return value != null && EqualityComparer<TValue>.Default.Equals(value, item.Value);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            if (array.Length - arrayIndex < Count)
                throw new ArgumentException("Недостаточно места в массиве.");

            int index = arrayIndex;
            for (int i = 0; i < Capacity; i++)
            {
                if (table[i] != null && !deleted[i])
                {
                    array[index] = new KeyValuePair<TKey, TValue>(table[i].Key, (TValue)table[i].Value.Clone());
                    index++;
                }
            }
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if (Contains(item))
                return Remove(item.Key);
            return false;
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => false;

        // Реализация IEnumerable<KeyValuePair<TKey, TValue>>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            for (int i = 0; i < Capacity; i++)
            {
                if (table[i] != null && !deleted[i])
                    yield return new KeyValuePair<TKey, TValue>(table[i].Key, table[i].Value);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
