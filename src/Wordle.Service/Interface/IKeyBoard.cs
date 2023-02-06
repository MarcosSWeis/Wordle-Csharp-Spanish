using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordle.Service.Interface
{
    public interface IKeyBoard
    {
        public bool IsClean(Letter letter);
        public bool IsEnter(Letter letter);
        public List<Letter> GetLetters();
        public void ResetKeyBoard();
    }
}
