using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordle.Service
{
    public interface ILoadWords
    {

        public void StartReadFile();

        public string GetWord();
    }
}
