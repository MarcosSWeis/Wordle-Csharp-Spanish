using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordle.Service
{
    public class Letter
    {
        public StatusLetters Status { get; set; }

        public string Character { get; }


        //podria hacerlo con delegados

        public Letter(StatusLetters statusLetter,string character)
        {
            Status = statusLetter;
            Character = character;

        }

        public Letter(Letter letter)
        {
            Status = letter.Status;
            Character = letter.Character;

        }

    }
}
