using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordle.Service.Extensions
{
    public static class StringExtensions
    {


        public static List<int>? IndexesOfManyCharaters(this string word,string character)
        {
            var sub = word.ToCharArray();

            var suba = character;

            List<int> indexes = new List<int>();
            for (int i = 0 ; i < word.Length ; i++)
            {
                if (character.ToLower() == word[i].ToString())
                {
                    indexes.Add(i);
                }

            }

            var a = indexes.Count();
            return indexes;

        }
    }
}
