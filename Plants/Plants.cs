using System;

namespace Plants
{
    public interface IInit
    {
        void Init();
        void RandomInit();
    }

    public interface ICloneable
    {
        object Clone();
    }

    public class IdNumber : ICloneable
    {
        public int Number
        {
            get { return number; }
            set
            {
                if (value >= 0)
                    number = value;
                else
                    Console.WriteLine("Некорректное значение для числа IdNumber.");
            }
        }

        public object Clone()
        {
            return new IdNumber(Number);
        }

        private int number;

        public IdNumber()
        {
        }

        public IdNumber(int number)
        {
            Number = number;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            IdNumber other = (IdNumber)obj;
            return Number == other.Number;
        }

        public override int GetHashCode()
        {
            return Number.GetHashCode();
        }

        public override string ToString()
        {
            return $"IdNumber: {Number}";
        }
    }

    public class Plant : IInit, IComparable<Plant>, ICloneable
    {
        private string name;
        private string color;
        private static readonly Random random = new Random();
        public IdNumber Id { get; set; } = new IdNumber();

        public string Name
        {
            get { return name; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    name = value;
                else
                    Console.WriteLine("Некорректное значение для названия растения.");
            }
        }

        public string Color
        {
            get { return color; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    color = value;
                else
                    Console.WriteLine("Некорректное значение для цвета растения.");
            }
        }

        public Plant(string name, string color, int idNumber)
        {
            Name = name;
            Color = color;
            Id.Number = idNumber;
        }

        public Plant()
        {
        }

        public virtual void Show()
        {
            Console.WriteLine($"Название: {Name}, Цвет: {Color}");
        }

        public virtual void Init()
        {
            Console.Write("Введите название растения: ");
            Name = Console.ReadLine();
            Console.Write("Введите цвет растения: ");
            Color = Console.ReadLine();
        }

        public virtual void RandomInit()
        {
            Name = "Растение" + random.Next(1, 100);
            Color = "Цвет" + random.Next(1, 10);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            Plant other = (Plant)obj;
            return Name == other.Name && Color == other.Color;
        }

        public int CompareTo(Plant other)
        {
            return string.Compare(Name, other.Name);
        }

        public override string ToString()
        {
            return $"Растение: Имя={Name}, Цвет={Color}";
        }

        public object Clone()
        {
            Plant clonedPlant = new Plant(Name, Color, Id.Number);
            if (Id is ICloneable cloneableId)
            {
                clonedPlant.Id = (IdNumber)cloneableId.Clone();
            }
            return clonedPlant;
        }
    }

    public class Tree : Plant, IInit, ICloneable
    {
        private double height;
        private static readonly Random random = new Random();

        public double Height
        {
            get { return height; }
            set
            {
                if (value >= 0)
                    height = value;
                else
                    Console.WriteLine("Некорректное значение для высоты дерева.");
            }
        }

        public Tree(string name, string color, double height, int id) : base(name, color, id)
        {
            Height = height;
        }

        public Tree() : base()
        {
        }

        public override void Show()
        {
            base.Show();
            Console.WriteLine($"Высота: {Height} м");
        }

        public override void Init()
        {
            base.Init();
            Console.Write("Введите высоту дерева (в метрах): ");
            double.TryParse(Console.ReadLine(), out double treeHeight);
            Height = treeHeight;
        }

        public override void RandomInit()
        {
            base.RandomInit();
            Height = random.NextDouble() * 20;
        }

        public override bool Equals(object obj)
        {
            if (!base.Equals(obj))
                return false;
            Tree other = (Tree)obj;
            return Height == other.Height;
        }

        public new object Clone()
        {
            Plant baseClone = (Plant)base.Clone();
            return new Tree(baseClone.Name, baseClone.Color, Height, baseClone.Id.Number);
        }
    }

    public class Flower : Plant, IInit, ICloneable
    {
        private string smell;
        private static readonly Random random = new Random();

        public string Smell
        {
            get { return smell; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    smell = value;
                else
                    Console.WriteLine("Некорректное значение для запаха цветка.");
            }
        }

        public Flower(string name, string color, string smell, int id) : base(name, color, id)
        {
            Smell = smell;
        }

        public Flower() : base()
        {
        }

        public override void Show()
        {
            base.Show();
            Console.WriteLine($"Запах: {Smell}");
        }

        public override void Init()
        {
            base.Init();
            Console.Write("Введите запах цветка: ");
            Smell = Console.ReadLine();
        }

        public override void RandomInit()
        {
            base.RandomInit();
            Smell = "Smell" + random.Next(1, 5);
        }

        public override bool Equals(object obj)
        {
            if (!base.Equals(obj))
                return false;
            Flower other = (Flower)obj;
            return Smell == other.Smell;
        }

        public new object Clone()
        {
            Plant baseClone = (Plant)base.Clone();
            return new Flower(baseClone.Name, baseClone.Color, Smell, baseClone.Id.Number);
        }
    }

    public class Rose : Flower, IInit, ICloneable
    {
        private bool hasThorns;
        private static readonly Random random = new Random();

        public bool HasThorns
        {
            get { return hasThorns; }
            set { hasThorns = value; }
        }

        public Rose(string name, string color, string smell, bool hasThorns, int id) : base(name, color, smell, id)
        {
            HasThorns = hasThorns;
        }

        public Rose() : base()
        {
        }

        public override void Show()
        {
            base.Show();
            Console.WriteLine($"Наличие шипов: {(HasThorns ? "Да" : "Нет")}");
        }

        public override void Init()
        {
            base.Init();
            Console.Write("Есть ли шипы у розы? (true/false): ");
            bool.TryParse(Console.ReadLine(), out bool thorns);
            HasThorns = thorns;
        }

        public override void RandomInit()
        {
            base.RandomInit();
            HasThorns = random.Next(2) == 1;
        }

        public override bool Equals(object obj)
        {
            if (!base.Equals(obj))
                return false;
            Rose other = (Rose)obj;
            return HasThorns == other.HasThorns;
        }

        public new object Clone()
        {
            Plant baseClone = (Plant)base.Clone();
            return new Rose(baseClone.Name, baseClone.Color, Smell, HasThorns, baseClone.Id.Number);
        }
    }
}