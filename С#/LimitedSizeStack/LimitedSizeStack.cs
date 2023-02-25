using System;
using System.Collections.Generic;

namespace TodoApplication
{
    public class LimitedSizeStack<T>
    {
        public readonly LinkedList<T> LimitedStack;
        private int Limit { get; set; }
        public LimitedSizeStack(int limit)
        {
            LimitedStack = new LinkedList<T>();
            Limit = limit;
        }

        public void Push(T item)
        {
            LimitedStack.AddLast(item);
            if (LimitedStack.Count > Limit)
                LimitedStack.RemoveFirst();
        }

        public T Pop()
        {
            if (LimitedStack.Count == 0) throw new InvalidOperationException();
            var result = LimitedStack.Last;
            LimitedStack.RemoveLast();
            return result.Value;
        }

        public int Count
        {
            get
            {
                return LimitedStack.Count;
            }
        }
    }
}