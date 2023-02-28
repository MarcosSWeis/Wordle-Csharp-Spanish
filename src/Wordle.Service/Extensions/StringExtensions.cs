using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Wordle.Service.Extensions
{
    public static class StringExtensions
    {


        public static List<int> IndexesOfOneCharacters(this string word,string character)
        {
            List<int> indexes = new List<int>();
            for (int i = 0 ; i < word.Length ; i++)
            {
                if (character.ToLower() == word[i].ToString())
                {
                    indexes.Add(i);
                }

            }
            return indexes;

        }

        public static int NumberOfTimesTheLetterIsRepeated(this string word,string character)
        {
            int count = 0;
            for (int i = 0 ; i < word.Length ; i++)
            {
                if (word[i].Equals(character))
                {
                    count++;
                }
            }
            return count;

        }


        public static string SplitUppersWhitMiddleDash(this string str)
        {
            string result = str;
            for (int i = 1 ; i < str.Length ; i++)
            {
                if (char.IsUpper(str[i]))
                {
                    result = result.Insert(i,"-");
                    i++;
                }
            }
            return result.ToLower();

        }
    }
}
