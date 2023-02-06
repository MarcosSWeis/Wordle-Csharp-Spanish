using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordle.Service.Interface
{
    public interface ILoadWords
    {
        public string GetNewWord();
        public List<string> GetListWords();
        public string GetCurrentWord();
    }
}
