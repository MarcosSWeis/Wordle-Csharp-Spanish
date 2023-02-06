using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordle.Service.Interface
{
    public interface IGame
    {
        public Task SendWord();

        public Task SetLetterInGril(Letter letter);
        public Letter?[,] GetLettersGril();
        public IKeyBoard GetKeyBoard();

    }
}
