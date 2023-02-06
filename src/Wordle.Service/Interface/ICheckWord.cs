using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wordle.Service.Enums;

namespace Wordle.Service.Interface
{
    public interface ICheckWord
    {
        public bool WordExists(Letter?[,] letters,int currentRow);

        public Letter[] UpdateStatusLetter(Letter?[,] letters,int currentRow);

        public bool WordIsCorrect(Letter?[,] letters,int currentRow);

    }
}
