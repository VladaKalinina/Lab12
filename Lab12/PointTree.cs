using System;

namespace Lab12
{
    public class PointTree<T> where T : IComparable<T>, Plants.ICloneable
    {
        public T? Data { get; set; }
        public PointTree<T>? Left { get; set; }
        public PointTree<T>? Right { get; set; }
        public int Height { get; set; } // высота узла для АВЛ-дерева

        public PointTree() // пустой узел
        {
            Data = default;
            Left = null;
            Right = null;
            Height = 1;
        }

        public PointTree(T data) //узел с данными
        {
            Data = data;
            Left = null;
            Right = null;
            Height = 1;
        }

        public override string ToString()
        {
            return Data?.ToString() ?? "";
        }
    }
}