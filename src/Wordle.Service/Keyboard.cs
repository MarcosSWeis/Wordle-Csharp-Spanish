using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wordle.Service.Enums;
using Wordle.Service.Interface;

namespace Wordle.Service
{
    public class KeyBoard :IKeyBoard
    {
        //posiciones
        //19 = enter
        //27 = ⌫

        //hasta p =>   <= 9 
        //hasta l =>   >9 && <=18
        //hasta m =>   > 18

        public string[] KeyBoardLetters = new string[] { "Q","W","E","R","T","Y","U","I","O","P","A","S","D","F","G","H","J","K","L","ENTER","Z","X","C","V","B","N","M","⌫" };

        private List<Letter> _letters;

        public List<Letter> GetLetters()
        {
            return _letters;
        }
        public KeyBoard()
        {
            SetKeyBoardLetters();
        }

        private void SetKeyBoardLetters()
        {
            _letters = new List<Letter>();


            for (int i = 0 ; i < KeyBoardLetters.Length ; i++)
            {
                if (i != 19 && i != 27)
                {
                    _letters.Add(new Letter(StatusLetters.Default,KeyBoardLetters[i]));

                } else if (i == 19)
                {
                    _letters.Add(new EnterLetter(StatusLetters.Default,KeyBoardLetters[i]));
                } else if (i == 27)
                {
                    _letters.Add(new CleanLetter(StatusLetters.Default,KeyBoardLetters[i]));
                }
            }
        }
        public void ResetKeyBoard()
        {
            SetKeyBoardLetters();
        }

        public bool IsEnter(Letter letter)
        {
            return typeof(EnterLetter) == letter.GetType();
        }

        public bool IsClean(Letter letter)
        {
            return typeof(CleanLetter) == letter.GetType();
        }

        //El teclado recibe una palabra y con esa palabra verifico
        /*
            1. Si esa plabra contiene o no la letas que toco al enviar


         */




    }
}
