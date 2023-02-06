using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;
using Wordle.Service.Enums;
using Wordle.Service.Extensions;
using Wordle.Service.Interface;

namespace Wordle.Service
{
    public class CheckWord :ICheckWord
    {
        private IKeyBoard _keyBoard;
        private ILoadWords _words;
        public CheckWord(ILoadWords words,IKeyBoard keyBoard)
        {
            _keyBoard = keyBoard;
            _words = words;

        }
        public bool WordIsCorrect(Letter?[,] letters,int currentRow)
        {
            string word = _joinLetters(letters,currentRow);
            return _words?.GetCurrentWord() == word;
        }

        //cheque si la palabra esta en el array, para mandar error si puso cualquir cosa       
        /// <summary>
        /// habia que tenr un UpdateStatusLetterGril y Un UpdateStatusLetterKeyBoard (este privado)
        /// </summary>
        /// <param name="letters"></param>
        /// <param name="currentRow"></param>
        /// <returns></returns>
        public Letter[] UpdateStatusLetter(Letter?[,] letters,int currentRow)
        {
            var rowGridLetters = new Letter[5];
            for (int i = 0 ; i < 5 ; i++)
            {
                var letterKeyBoard = _keyBoard?.GetLetters().Find(x => x.Character == letters[currentRow,i]?.Character);
                var status = this.ContainLetter(letters[currentRow,i],i);
                var character = letters[currentRow,i]?.Character;

                if (letterKeyBoard != null && character != null)
                {
                    //change state letter keyboard
                    //si el estado ya fue cambiado a Ok , entonces no lo lo cambio por nada 
                    if (letterKeyBoard.Status != StatusLetters.Ok)
                    {
                        letterKeyBoard.Status = status;
                    }

                    rowGridLetters[i] = new Letter(status,character);
                }

            }
            return rowGridLetters;
        }

        private string _joinLetters(Letter?[,] letters,int currentRow)
        {
            string completeWord = "";
            for (int j = 0 ; j < 5 ; j++)
            {
                completeWord += letters[currentRow,j]?.Character;
            }

            return completeWord.ToLower();
        }

        private StatusLetters ContainLetter(Letter letter,int positionLetter)
        {
            if (_words.GetCurrentWord().Contains(letter.Character.ToLower()))
            {
                var index = _words.GetCurrentWord().IndexesOfManyCharaters(letter.Character.ToLower());
                var a = index?.Count;
                // if (index.Contains(positionLetter))
                if (_words.GetCurrentWord().IndexOf(letter.Character.ToLower()) == positionLetter)
                {
                    return StatusLetters.Ok;
                }
                return StatusLetters.Contains;

            } else
                return StatusLetters.Locked;
        }

        public bool WordExists(Letter?[,] letters,int currentRow)
        {
            string word = _joinLetters(letters,currentRow);
            return _words.GetListWords().Contains(word);
        }
    }
}

