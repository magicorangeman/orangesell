using System.Collections.Generic;

namespace TodoApplication
{
    public class ListModel<TItem>
    {
        public List<TItem> Items { get; }
        public LimitedSizeStack<(TItem, string, int)> Stack { get; }

        public ListModel(int limit)
        {
            Items = new List<TItem>();
            Stack = new LimitedSizeStack<(TItem, string, int)>(limit);
        }

        public ListModel(List<TItem> list, int limit)
        {
            Items = list;
            Stack = new LimitedSizeStack<(TItem, string, int)>(limit);
        }

        public void AddItem(TItem item)
        {
            Items.Add(item);
            Stack.Push((item, "Addition", Items.Count - 1));
        }

        public void RemoveItem(int index)
        {
            Stack.Push((Items[index], "Removing", index));
            Items.RemoveAt(index);
        }

        public bool CanUndo()
        {
            return Stack.Count != 0;
        }

        public void Undo()
        {
            (TItem item, string action, int index) = Stack.Pop();
            if (action.Equals("Addition"))
                Items.RemoveAt(Items.LastIndexOf(item));
            if (action.Equals("Removing"))
                Items.Insert(index, item);
        }
    }
}