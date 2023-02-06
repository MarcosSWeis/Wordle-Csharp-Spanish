using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wordle.Service.Enums;
using Wordle.Service.Interface;

namespace Wordle.Service
{
    public class Game :IGame
    {
        public int Colum { get; private set; } = 0;
        public int RowEnter { get; private set; } = 0;
        public bool IsWinner { get; private set; } = false;
        public bool IsValidWord { get; private set; } = true;

        private Letter?[,] _lettersGril = new Letter[6,5];
        private readonly ILoadWords _words;
        private IKeyBoard _keyBoard;
        private readonly INotification _swal;

        public Game()
        {
            _words = new LoadWords();
            _keyBoard = new KeyBoard();

        }

        public Game(INotification notification)
        {
            _words = new LoadWords();
            _keyBoard = new KeyBoard();
            _swal = notification;
        }

        public IKeyBoard GetKeyBoard()
        {
            return _keyBoard;
        }

        public Letter?[,] GetLettersGril()
        {
            return _lettersGril;
        }



        public async Task SendWord()
        {
            CheckWord check = new CheckWord(_words,_keyBoard);

            if (check.WordExists(_lettersGril,RowEnter))
            {
                if (check.WordIsCorrect(_lettersGril,RowEnter))
                {
                    await _swal.SwalFireAsync("Winner","Felicitaciones ganastes",NotificationType.Success);
                    IsWinner = true;
                    ResetGame();
                } else
                {
                    for (int j = 0 ; j < 5 ; j++)
                    {
                        _lettersGril[RowEnter,j] = check.UpdateStatusLetter(_lettersGril,RowEnter)[j];
                    }
                    RowEnter++;
                    Colum = 0;
                }

            } else
                await _swal.SwalFireAsync("Warning","palabra no contenida",NotificationType.Warning);

            if (RowEnter > 5)
            {
                var showSecretWord = _words.GetCurrentWord();
                await _swal.SwalFireAsync("Game Over",$"Lo siento has perdido la palabra era\n : {showSecretWord}",NotificationType.Info);
                ResetGame();
            }
        }

        private void ResetGame()
        {
            _words.GetNewWord();
            _keyBoard.ResetKeyBoard();
            ResetGril();
            ResetProperties();
        }
        private void ResetProperties()
        {
            Colum = 0;
            RowEnter = 0;
            IsWinner = false;
            IsValidWord = false;
        }
        private void ResetGril()
        {
            _lettersGril = new Letter[6,5];
        }
        public async Task SetLetterInGril(Letter letter)
        {

            if (_keyBoard.IsEnter(letter))
            {
                if (RowEnter < 5 && Colum == 5)
                    await SendWord();
                else
                    await SendWord();
                //si no gano, entonces pierde proque era la ultima chence

            } else if (_keyBoard.IsClean(letter))
            {
                CleanWord();
            } else
            {
                if (Colum < 5)
                    AddLetter(letter);
            }


        }
        private void CleanWord()
        {
            DiscountLetter();
        }

        private void AddLetter(Letter letter)
        {

            if (Colum < 5)
            {
                _lettersGril[RowEnter,Colum] = new Letter(StatusLetters.Default,letter.Character);
                Colum++;
            }

        }
        private void DiscountLetter()
        {
            if (Colum > 0)
            {
                Colum--;
                _lettersGril[RowEnter,Colum] = null;
            }

        }
    }
}