﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordle.Service
{
    public class CleanLetter :Letter
    {
        public CleanLetter(StatusLetters statusLetter,string character) : base(statusLetter,character)
        {
        }
    }
}

