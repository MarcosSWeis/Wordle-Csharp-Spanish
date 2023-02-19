using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wordle.Service.Enums;
using Wordle.Service.Interface;

namespace Wordle.Service
{
    public class Game :SettingsGame, IGame
    {
        public string StyleRotate = "";
        public string StyleStatus = "";
        public int Colum { get; protected set; } = 0;
        public int RowEnter { get; protected set; } = 0;
        public bool IsWinner { get; private set; } = false;
        public bool IsValidWord { get; private set; } = true;
        private Letter?[,] _lettersGril;
        private readonly ILoadWords _words;
        private IKeyBoard _keyBoard;
        private readonly INotification _swal;
        private readonly IJSRuntime _JS;

        public Game(SettingsGame settings) : base(settings.MaxColumLength,settings.MaxNumberOfAttempts)
        {
            _words = new LoadWords(settings.MaxColumLength);
            _keyBoard = new KeyBoard();
            _lettersGril = new Letter[settings.MaxNumberOfAttempts,settings.MaxColumLength];

        }

        public Game(INotification notification,SettingsGame settings) : base(settings.MaxColumLength,settings.MaxNumberOfAttempts)
        {
            _words = new LoadWords(settings.MaxColumLength);
            _keyBoard = new KeyBoard();
            _lettersGril = new Letter[settings.MaxNumberOfAttempts,settings.MaxColumLength];
            _swal = notification;
        }
        public Game(INotification notification,SettingsGame settings,IJSRuntime js) : base(settings.MaxColumLength,settings.MaxNumberOfAttempts)
        {
            _words = new LoadWords(settings.MaxColumLength);
            _keyBoard = new KeyBoard();
            _lettersGril = new Letter[settings.MaxNumberOfAttempts,settings.MaxColumLength];
            _swal = notification;
            _JS = js;
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
            CheckWord check = new CheckWord(_words,_keyBoard,MaxColumLength,MaxNumberOfAttempts);

            if (check.WordExists(_lettersGril,RowEnter))
            {
                if (check.WordIsCorrect(_lettersGril,RowEnter))
                {
                    //      await SetStyleRotateGril(); si gana no se verian todas en verde proque no ejecuto la funion esta ya que la tengo que ejeutar depues de actualizar todos lso estados
                    await _swal.SwalFireAsync("Winner","Felicitaciones ganaste",NotificationType.Success);
                    IsWinner = true;
                    ResetGame();
                } else
                {
                    for (int j = 0 ; j < MaxColumLength ; j++)
                    {
                        _lettersGril[RowEnter,j] = check.UpdateStatusLetter(_lettersGril,RowEnter)[j];
                    }
                    await SetStyleRotateGril();
                    RowEnter++;
                    Colum = 0;

                }

            } else
                await _swal.SwalFireAsync("Warning","palabra no contenida",NotificationType.Warning);


            if (RowEnter > MaxNumberOfAttempts - 1)
            {
                var showSecretWord = _words.GetCurrentWord();
                await _swal.SwalFireAsync("Game Over",$"Lo siento has perdido la palabra correcta era\n : {showSecretWord}",NotificationType.Info);
                ResetGame();
            }
        }

        private async Task SetStyleRotateGril()
        {
            await _JS.InvokeVoidAsync("RotateRow",RowEnter,JsonConvert.SerializeObject(StatusJs()),MaxColumLength);
        }
        private async Task ResetStylesGrilJS()
        {

            await _JS.InvokeVoidAsync("ResetStyleGril");
        }
        private string[] StatusJs()
        {
            var result = new string[MaxColumLength];
            for (int i = 0 ; i < MaxColumLength ; i++)
            {

                var status = _lettersGril[RowEnter,i].Status.ToString();
                result[i] = status;
            }
            return result;

        }

        private async void ResetGame()
        {
            //agregar el resteo de los colores de la grilla
            _words.GetNewWord();
            _keyBoard.ResetKeyBoard();
            ResetGril();
            ResetProperties();
            await ResetStylesGrilJS();
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
            _lettersGril = new Letter[MaxNumberOfAttempts,MaxColumLength];
        }
        public async Task SetLetterInGril(Letter letter)
        {

            if (_keyBoard.IsEnter(letter))
            {
                //if (RowEnter < 5 && Colum == MaxColumLength)
                await SendWord();
                //else
                //    await SendWord();
                //si no gano, entonces pierde proque era la ultima chence

            } else if (_keyBoard.IsClean(letter))
            {
                CleanWord();
            } else
            {
                if (Colum < MaxColumLength)
                    AddLetter(letter);
            }


        }
        private void CleanWord()
        {
            DiscountLetter();
        }

        private void AddLetter(Letter letter)
        {

            if (Colum < MaxColumLength)
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

        private Letter? GetLetterGril(int i,int j)
        {
            if (i < GetLettersGril().GetLength(0))
            {
                if (j < GetLettersGril().GetLength(1))
                {
                    return GetLettersGril()[i,j];
                }
            }
            return null;

        }

        public bool IsEnter(int i,int j)
        {
            return GetLetterGril(i,j) != null ? _keyBoard.IsEnter(GetLetterGril(i,j)) : false;
        }

        public string? GetCharacterGril(int i,int j)
        {
            return GetLetterGril(i,j) != null ? GetLetterGril(i,j)?.Character : "";
        }

        public StatusLetters? GetStatusGril(int i,int j)
        {
            return GetLetterGril(i,j) != null ? GetLetterGril(i,j)?.Status : null;
        }

        public string SetStyleRotateWord(int i,int j)
        {

            return (i < GetLettersGril().GetLength(0) && j < GetLettersGril().GetLength(1) && GetLettersGril()[i,j]?.Status != StatusLetters.Default) ? " rotate-word" : "";
        }
        public string SetStyleStatusInGril(int i,int j)
        {
            //"status-" + GetLettersGril()[i,j]?.Status.ToString()            
            var style = Task.Run(async delegate
            {
                await Task.Delay(j);
                return (i < GetLettersGril().GetLength(0) && j < GetLettersGril().GetLength(1)) ? "status-" + GetLettersGril()[i,j]?.Status.ToString() : "";
            });
            StyleStatus = style.Result;
            style.Wait();
            return style.Result;

        }


    }
}