using System.Numerics;

namespace Tickets;

public class TicketsTask
{
    public static BigInteger Solve(int halfLen, int totalSum)
    {
        if (totalSum % 2 != 0) return default;
        var halfSum = totalSum / 2;
        var table = new BigInteger[halfLen + 1, halfSum + 1];

        for (int i = 0; i <= halfLen; ++i) 
            table[i, 0] = 1;
        for (int j = 1; j <= halfSum; ++j)
            table[0, j] = 0;

        for (int i = 1; i <= halfLen; ++i)
            for(int j = 1; j <= halfSum; ++j)
                for (int k = 0; k <= 9 && j - k >= 0; ++k)
                    table[i, j] += table[i - 1, j - k];

        return BigInteger.Pow(table[halfLen, halfSum], 2);
    }
}
