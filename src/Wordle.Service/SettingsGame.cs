using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordle.Service
{
    public class SettingsGame
    {
        [Required]
        [Range(5,26,ErrorMessage = "Solo se admiten numero entre 5 y 26")]
        public int MaxColumLength { get; set; } = 5; //Numero de columnas
        [Required]
        [Range(6,27,ErrorMessage = "No puede tener menos de 6 intentos, ni mas de 27 intentos")]
        public int MaxNumberOfAttempts { get; set; } = 6;//Numeros de intentos , osea el numero de filas

        public readonly int MaxNumberOfFilesForWord = 26;
        public readonly int MinNumberOfFilesForWord = 5;

        public SettingsGame(int maxColumLength,int maxNumberOfAttempts)
        {
            MaxColumLength = maxColumLength;
            MaxNumberOfAttempts = maxNumberOfAttempts;

        }
        public SettingsGame()
        {

        }

        public void ChangeSettings(int maxColumLength,int maxNumberOfAttempts)
        {
            MaxColumLength = maxColumLength;
            MaxNumberOfAttempts = maxNumberOfAttempts;
        }


    }
}
