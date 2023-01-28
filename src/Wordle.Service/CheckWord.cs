using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordle.Service
{
    public class CheckWord
    {
        //palabras insetadas
        public ushort CountEmbeddedWords { get; } = 0;//se irian sumando hasta 6

        private KeyBoard _keyBoard;

        private LoadWords _words;

        public int Prueba = 10;

        [Inject] protected JSRuntime JS { get; set; }

        public CheckWord(LoadWords words,KeyBoard keyBoard)
        {
            _keyBoard = keyBoard;
            _words = words;

        }




        //ESTA DEVERIA TENER LA INTANCIA DE TECLADO PARA CAMBIAR LOS ESTADOS DE LAS TECLAS
        [JSInvokable]
        public async Task ContainsLetters()
        {
            //if (_keyBoard.Letters.Contains(new ))
            Prueba = 50;

        }


        //DEBERIA TENER LA  PABLARA RANDOM para comprar  (LoadWords)
        [JSInvokable]
        public async Task WordIsCorrect()
        {
            //if (_words.GetCurrentWord() == )
            Prueba = 25;

        }

        public async Task WordIsCorrectJavaScript()
        {

            // EL PASO EL NOMBRE DE LA FUNCION DE JS QUE QUIERO QUE EJECUTE
            //Y TAMBIEN LE PASO UNA REFERENCIA DE UN OBJETO DE .NET PERO QUE TAMBIEN SE PUEDE USAR DESE JS PARA ESO USAMOS DotNetObjectReference.Create()
            //y a ese create le pasamso la funcion que queremos usar en realida que seria la del WordIsCorrect() , le paso this que es la referecia a todo mi objeto CheckWord
            await JS.InvokeVoidAsync("unMetodoDeJSEnCsharp",DotNetObjectReference.Create(this));
        }

        private string ConcateLetters()
        {
            for (int i = 0 ; i < _words.CountLettersByWord ; i++)
            {


            }

            return "";
        }


        //DEBERIA TENER LAs intancias de cada letra para los estados, (saber si en tal posisicon la contenia o era correcta la posicion )



        /*

         *Avisar si la palabra no esta permitida
         *
         */

    }
}
