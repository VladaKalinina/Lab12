namespace Lab12
{
    // Обобщённый класс для хранения пары ключ-значение
    public class Pair<TKey, TValue>
    {
        public TKey Key { get; set; } // Ключ
        public TValue Value { get; set; } // Значение

        public Pair(TKey key, TValue value) // Конструктор пары
        {
            Key = key;
            Value = value;
        }
    }
}
