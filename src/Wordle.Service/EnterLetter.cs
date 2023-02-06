using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wordle.Service.Enums;

namespace Wordle.Service
{
    public class EnterLetter :Letter
    {
        public EnterLetter(StatusLetters statusLetter,string character) : base(statusLetter,character)
        {


        }


    }
}
