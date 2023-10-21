using System.Collections.Generic;
using System.Linq;

namespace rocket_bot;

public class Channel<T> where T : class
{
    private List<T> Collection { get; set; }
	public int Count { get => Collection.Count; }
    public Channel() => Collection = new List<T>();

	/// <summary>
	/// Возвращает элемент по индексу или null, если такого элемента нет.
	/// При присвоении удаляет все элементы после.
	/// Если индекс в точности равен размеру коллекции, работает как Append.
	/// </summary>
	public T this[int index]
    {
        get
        {
			lock(this)
			{
                if (index >= 0 && index < Count) return Collection[index];
                return null;
            }
        }
        set
        {
			lock (this)
			{
				if (index == Count) AppendIfLastItemIsUnchanged(value, LastItem());
				else if (index >= 0 && index < Count)
				{
					Collection = Collection.Take(index).ToList();
                    Collection.Add(value);
                }
            }
        }

    }

	/// <summary>
	/// Возвращает последний элемент или null, если такого элемента нет
	/// </summary>
	public T LastItem()
	{
		lock (this) 
		{
			if (Collection.Count != 0)
				return Collection.Last();
			return null;
		}
	}

	/// <summary>
	/// Добавляет item в конец только если lastItem является последним элементом
	/// </summary>
	public void AppendIfLastItemIsUnchanged(T item, T knownLastItem)
	{
		lock (this)
		{
			if (LastItem() is null || LastItem().Equals(knownLastItem))
				Collection.Add(item);	
        }
    }
}