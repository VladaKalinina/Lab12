using Plants;
using System;

namespace Lab12
{
    public class Point<T>
    {
        public T? Data { get; set; } // Хранит данные узла
        public Point<T>? Next { get; set; } // Ссылка на следующий узел
        public Point<T>? Pred { get; set; } // Ссылка на предыдущий узел

        public Point()
        {
            Data = default(T); // Инициализирует данные значением по умолчанию
            Next = null;
            Pred = null;
        }

        public Point(T data)
        {
            Data = data; // Устанавливает данные узла
            Next = null;
            Pred = null;
        }

        public Point<T> MakeRandomData()
        {
            if (typeof(T) == typeof(Plant)) // Проверяет, является ли тип Plant
            {
                Plant plant = new Plant();
                plant.RandomInit(); // Создает случайное растение
                return new Point<T>((T)(object)plant); // Возвращает новый узел
            }
            throw new InvalidOperationException("MakeRandomData поддерживает только тип Plant.");
        }

        public T MakeRandomItem()
        {
            if (typeof(T) == typeof(Plant)) // Проверяет, является ли тип Plant
            {
                Plant plant = new Plant();
                plant.RandomInit(); // Создает случайное растение
                return (T)(object)plant; // Возвращает объект Plant
            }
            throw new InvalidOperationException("MakeRandomItem поддерживает только тип Plant.");
        }

        public override string ToString()
        {
            return Data?.ToString() ?? ""; // Возвращает строковое представление данных
        }

        public override int GetHashCode()
        {
            return Data?.GetHashCode() ?? 0; // Возвращает хэш-код данных
        }
    }
}