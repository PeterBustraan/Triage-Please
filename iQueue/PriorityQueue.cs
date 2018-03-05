using System;
using System.Collections.Generic;

namespace PriorityQueue
{
    class MinHeap<T> where T : IComparable<T>
    {
        private List<T> array = new List<T>();
        public int Count { get { return array.Count; } }

        public void Add(T element)
        {
            // add element at end
            array.Add(element);

            // move up as far as possible
            MoveUp( array.Count - 1);
        }

        public T RemoveMin()
        {
            // set the return value to the top of the heap
            T ret = array[0];

            // move the last element to the top
            array[0] = array[array.Count - 1];
            array.RemoveAt(array.Count - 1);


            // move the top element down as far as possible
            MoveDown(0);

            return ret;
        }

        public void MoveDown(int parent)
        {
            int leftChild = 2 * parent + 1;
            int rightChild = 2 * parent + 2;
            int min = parent;

            // find whether the smaller of the two children is less than our 'loc'
            // first check left child
            if (leftChild < array.Count && array[leftChild].CompareTo(array[min]) < 0)
                min = leftChild;
            // second check right child (note that 'min' may be left child)
            if (rightChild < array.Count && array[rightChild].CompareTo(array[min]) < 0)
                min = rightChild;

            // if either child is less than the parent, swap and try again
            if (min != parent)
            {
                T tmp = array[parent];
                array[parent] = array[min];
                array[min] = tmp;
                MoveDown(min);
            }
        }

        private void MoveUp(int child)
        {
            int parent = (child - 1)/2;
            if (array[child].CompareTo(array[parent]) < 0)
            {
                T tmp = array[child];
                array[child] = array[parent];
                array[parent] = tmp;
                MoveUp(parent);
            }
        }

        public T Peek()
        {
            return array[0];
        }

    }

   
    public class PriorityQueue<T> : IQueue<T>
    {
        internal class Node : IComparable<Node>
        {
            public int Priority;
            public T O;
            public int CompareTo(Node other)
            {
                return Priority.CompareTo(other.Priority);
            }
        }

        private MinHeap<Node> minHeap = new MinHeap<Node>();
        public int Count { get { return minHeap.Count; } }

        public void Add(int priority, T element)
        {
            minHeap.Add(new Node() { Priority = priority, O = element });
        }

        public T Remove()
        {
            return minHeap.RemoveMin().O;
        }

        public T Peek()
        {
            return minHeap.Peek().O;
        }

    }

    public class PriorityQueue
    {
        static void ZMain(string[] args)
        {
            PriorityQueue<string> myQueue = new PriorityQueue<string>();
            myQueue.Add(10, "ten");
            myQueue.Add(8, "eight");
            myQueue.Add(6, "six");
            myQueue.Add(12, "twelve");
            myQueue.Add(4, "four");
            myQueue.Add(9, "nine");
            myQueue.Add(110, "1-ten");
            myQueue.Add(108, "1-eight");
            myQueue.Add(106, "1-six");
            myQueue.Add(112, "1-twelve");
            myQueue.Add(104, "1-four");
            myQueue.Add(109, "1-nine");
            while (myQueue.Count > 0)
            {
                //Console.WriteLine(myQueue.Remove());
            }
            //Console.ReadLine();
        }
    }
}
