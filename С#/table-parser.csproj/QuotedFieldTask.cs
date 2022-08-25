using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TableParser
{
    [TestFixture]
    public class QuotedFieldTaskTests
    {
        [TestCase("''", 0, "", 2)]
        [TestCase("'a'", 0, "a", 3)]
        [TestCase("\"abc\"", 0, "abc", 5)]
        [TestCase("b \"a'\"", 2, "a'", 4)]
        [TestCase(@"'a\' b'", 0, "a' b", 7)]
        [TestCase("\"", 0, "", 1)]
        [TestCase("'", 0, "", 1)]
        [TestCase("''", 0, "", 2)]
        [TestCase("'a'", 0, "a", 3)]
        [TestCase("\"abc\"", 0, "abc", 5)]
        [TestCase("b \"a'\"", 2, "a'", 4)]
        [TestCase(@"'a\' b'", 0, "a' b", 7)]
        [TestCase("'", 0, "", 1)]
        [TestCase("\"", 0, "", 1)]
        [TestCase(@"'a\' b\'", 0, "a' b'", 8)]
        [TestCase(@"'a\' b'xx", 0, "a' b", 7)]
        [TestCase(@"'\'\''xx", 0, "''", 6)]
        [TestCase("'a\"\"'", 0, "a\"\"", 5)]
        [TestCase("\"abc\"asas", 0, "abc", 5)]
        [TestCase("sx\"a'\"", 2, "a'", 4)]
        [TestCase("'a\"\"'", 0, "a\"\"", 5)]
        [TestCase("'a' b'", 0, "a", 3)]
        [TestCase("'a", 0, "a", 2)]

        public void Test(string line, int startIndex, string expectedValue, int expectedLength)
        {
            var actualToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
            Assert.AreEqual(new Token(expectedValue, startIndex, expectedLength), actualToken);
        }
    }

    class QuotedFieldTask
    {
        public static Token ReadQuotedField(string line, int startIndex)
        {
            var token = new StringBuilder();
            char quotes = line[startIndex];
            int endIndex = 0;
            for (int i = startIndex + 1; i < line.Length - startIndex + 1; i++)
            {
                if ((line.Length > 1) && (line[i] != quotes) && (line[i] != '\\'))
                {
                    token.Append(line[i]);
                }
                else if ((line.Length - startIndex + 1 > 1) && (line[i] == '\\'))
                {
                    i++;
                    token.Append(line[i]);

                }
                else if (line.Length <= 1)
                {
                    break;
                }
                else
                {
                    endIndex = i;
                    break;
                }
            endIndex = i;
            }

            return new Token(token.ToString(), startIndex, endIndex - startIndex + 1);
        }
    }
}
