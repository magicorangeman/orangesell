using System;
using System.Collections;
using System.Collections.Generic;

namespace hashes
{
    public class ReadonlyBytes : IEnumerable<byte>
    {
        List<byte> array;
        public int Length { get { return array.Count; } }
        readonly int hashCode;

        public ReadonlyBytes(params byte[] bytes)
        {
            array = new List<byte>();
            if (bytes != null)
            {
                array.AddRange(bytes);
            }
            else throw new ArgumentNullException();
            hashCode = GetHashCode();
        }

        public byte this[int index]
        {
            get
            {
                if (index < 0 || index >= Length) throw new IndexOutOfRangeException();
                return array[index];
            }
        }

        public IEnumerator<byte> GetEnumerator()
        {
            for (int i = 0; i < Length; i++)
                yield return array[i]; ;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override int GetHashCode()
        {
            unchecked
            {
                if (hashCode != 0) return hashCode;
                int hash = 1223;
                int prime = 1117;
                foreach (var element in array)
                {
                    hash = hash * prime;
                    hash = hash ^ element;
                }
                return hash;
            }
        }

        public override string ToString()
        {
            string result = "[";
            result += string.Join(", ", array);
            result += "]";
            return result;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ReadonlyBytes)) return false;
            if (obj.GetType() != GetType()) return false;
            var argument = obj as ReadonlyBytes;
            if (argument == null && this == null) return true;
            else if (argument.Length != Length) return false;
            for (int i = 0; i < Length; i++)
                if (array[i] != argument.array[i])
                    return false;
            return true;
        }
    }
}