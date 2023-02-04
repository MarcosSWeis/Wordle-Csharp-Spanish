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
namespace Wordle.Service
{
    public class CheckWord
    {
        //palabras insetadas
        public ushort CountEmbeddedWords { get; } = 0;//se irian sumando hasta 6

        private KeyBoard? _keyBoard;

        private LoadWords? _words;

        public CheckWord(LoadWords words,KeyBoard keyBoard)
        {
            _keyBoard = keyBoard;
            _words = words;

        }
        public bool WordIsCorrect(Letter[,] letters,int currentRow)
        {
            string word = _joinLetters(letters,currentRow);
            return _words?.GetCurrentWord() == word;
        }

        //cheque si la palabra esta en el array, para mandar error si puso cualquir cosa


        //Actualiza los estados de la teclas dependiendo si fue usada, si la contiene
        public Letter[] UpdateStatusLetter(Letter?[,] letters,int currentRow)
        {
            var rowGridLetters = new Letter[5];
            for (int i = 0 ; i < 5 ; i++)
            {
                var letterKeyBoard = _keyBoard?.Letters.Find(x => x.Character == letters[currentRow,i]?.Character);
                var status = this.ContainLetter(letters[currentRow,i],i);
                var character = letters[currentRow,i].Character;

                if (letterKeyBoard != null)
                {
                    //change state letter keyboard
                    letterKeyBoard.Status = status;
                    rowGridLetters[i] = new Letter(status,character);
                }

            }
            return rowGridLetters;
        }

        private string _joinLetters(Letter[,] letters,int currentRow)
        {
            string completeWord = "";
            for (int j = 0 ; j < 5 ; j++)
            {
                completeWord += letters[currentRow,j].Character;
            }

            return completeWord.ToLower();
        }

        private StatusLetters ContainLetter(Letter letter,int positionLetter)
        {
            if (_words.GetCurrentWord().Contains(letter.Character.ToLower()))
            {
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

