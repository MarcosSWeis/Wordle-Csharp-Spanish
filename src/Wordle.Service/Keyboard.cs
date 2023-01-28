using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordle.Service
{
    public class KeyBoard
    {
        //posiciones
        //19 = enter
        //27 = ⌫

        //hasta p =>   <= 9 
        //hasta l =>   >9 && <=18
        //hasta m =>   > 18

        public string[] KeyBoardLetters = new string[] { "Q","W","E","R","T","Y","U","I","O","P","A","S","D","F","G","H","J","K","L","ENTER","Z","X","C","V","B","N","M","⌫" };

        public List<Letter> Letters;


        public KeyBoard()
        {
            SetKeyBoardLetters();
        }

        private void SetKeyBoardLetters()
        {
            Letters = new List<Letter>();


            for (int i = 0 ; i < KeyBoardLetters.Length ; i++)
            {
                if (i != 19 && i != 27)
                {
                    Letters.Add(new Letter(StatusLetters.Default,KeyBoardLetters[i]));

                } else if (i == 19)
                {
                    Letters.Add(new Enter(StatusLetters.Default,KeyBoardLetters[i]));
                } else if (i == 27)
                {
                    Letters.Add(new CleanLetter(StatusLetters.Default,KeyBoardLetters[i]));
                }
            }
        }

        public bool IsEnter(Letter letter)
        {
            return typeof(Enter) == letter.GetType();
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
