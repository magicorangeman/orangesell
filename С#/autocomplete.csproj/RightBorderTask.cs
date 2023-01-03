using System;
using System.Collections.Generic;

namespace Autocomplete
{
    public class RightBorderTask
    {
        public static int GetRightBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
        {
            while (left + 1 < right)
            {
                long t = ((long)left + (long)right) / 2;
                var m = (int)t;
                if ((string.Compare(phrases[m], prefix, StringComparison.OrdinalIgnoreCase) > 0) &&
                        !(phrases[m].StartsWith(prefix)))
                    right = m;
                else left = m;
            }
            if ((right != phrases.Count) && (string.Compare(phrases[right], prefix, StringComparison.OrdinalIgnoreCase) > 0))
                return right;
            return phrases.Count;
        }
    }
}