using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriorityQueue
{
    public interface IQueue<T>
    {
        int Count { get; }
        void Add(int priority, T element);
        T Remove();
        T Peek();
    }
}
