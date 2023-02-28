using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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
    public class CheckWord :SettingsGame, ICheckWord
    {
        private IKeyBoard _keyBoard;
        private ILoadWords _words;
        public CheckWord(ILoadWords words,IKeyBoard keyBoard,int settingsMaxColumLength,int settingsMaxNumberOfAttempts) : base(settingsMaxColumLength,settingsMaxNumberOfAttempts)
        {
            _keyBoard = keyBoard;
            _words = words;

        }
        public bool WordIsCorrect(Letter?[,] letters,int currentRow)
        {
            string word = _joinLetters(letters,currentRow);
            return _words?.GetCurrentWord() == word;
        }
        public Letter[] UpdateStatusLetter(Letter?[,] letters,int currentRow)
        {
            var rowGridLetters = new Letter[MaxColumLength];
            int coutRepeatLetter = 0;
            for (int i = 0 ; i < MaxColumLength ; i++)
            {
                var letterKeyBoard = _keyBoard?.GetLetters().Find(x => x.Character == letters[currentRow,i]?.Character);
                var statusGril = this.ContainLetterGril(letters[currentRow,i],i,_joinLetters(letters,currentRow));
                var statusKeyboard = this.ContainLetterKeyBoard(letters[currentRow,i],i);


                var character = letters[currentRow,i]?.Character;

                if (letterKeyBoard != null && character != null)
                {
                    //change state letter keyboard
                    //si el estado ya fue cambiado a Ok , entonces no lo lo cambio por nada 
                    if (letterKeyBoard.Status != StatusLetters.Ok)
                    {
                        letterKeyBoard.Status = statusKeyboard;
                    }


                    rowGridLetters[i] = new Letter(statusGril,character);
                }

            }
            return rowGridLetters;
        }

        private StatusLetters ContainLetterKeyBoard(Letter letter,int positionLetter)
        {
            if (_words.GetCurrentWord().Contains(letter.Character.ToLower()))
            {
                List<int> indexes = _words.GetCurrentWord().IndexesOfOneCharacters(letter.Character.ToLower());

                //for (int i = 0 ; i < indexes.Count ; i++)
                //{
                //    if (indexes[i] == positionLetter)
                //    {
                //        return StatusLetters.Ok;
                //    }

                //}
                if (VefifyCorrectPosition(indexes,positionLetter))
                {
                    return StatusLetters.Ok;
                };

                return StatusLetters.Contains;

            } else
                return StatusLetters.Locked;
        }
        private string _joinLetters(Letter?[,] letters,int currentRow)
        {
            string completeWord = "";
            for (int j = 0 ; j < MaxColumLength ; j++)
            {
                completeWord += letters[currentRow,j]?.Character;
            }

            return completeWord.ToLower();
        }
        private StatusLetters ContainLetterGril(Letter letter,int positionLetter,string wordPlayer)
        {
            if (_words.GetCurrentWord().Contains(letter.Character.ToLower()))
            {

                List<int> indexes = _words.GetCurrentWord().IndexesOfOneCharacters(letter.Character.ToLower());
                List<int> indexesWordPlayer = wordPlayer.IndexesOfOneCharacters(letter.Character.ToLower());

                //for (int i = 0 ; i < indexes.Count ; i++)
                //{
                //    if (indexes[i] == positionLetter)
                //    {
                //        return StatusLetters.Ok;
                //    }

                //}            
                if (VefifyCorrectPosition(indexes,positionLetter))
                {
                    return StatusLetters.Ok;
                };

                if (indexesWordPlayer.Count > indexes.Count)
                {
                    if (indexesWordPlayer.Last() == positionLetter)
                    {
                        return StatusLetters.Contains;
                    } else
                    {
                        return StatusLetters.Locked;

                    }
                }

                return StatusLetters.Contains; //return StatusLetters.Contains;

            } else
                return StatusLetters.Locked;
        }

        private bool VefifyCorrectPosition(List<int> indexes,int positionLetter)
        {
            for (int i = 0 ; i < indexes.Count ; i++)
            {
                if (indexes[i] == positionLetter)
                {
                    //  return StatusLetters.Ok;
                    return true;
                }

            }
            return false;


        }
        public bool WordExists(Letter?[,] letters,int currentRow)
        {
            string word = _joinLetters(letters,currentRow);
            return _words.GetListWords().Contains(word);
        }
    }
}

