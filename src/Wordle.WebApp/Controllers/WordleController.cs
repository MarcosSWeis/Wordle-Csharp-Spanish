using Microsoft.AspNetCore.Mvc;
using System.Text;
using Wordle.Service;
using Wordle.WebApp.Extensions;
using Wordle.WebApp.Pages.Wordles;
using Wordle.WebApp.Views.Wordle;

namespace Wordle.WebApp.Controllers
{
    public class WordleController :BaseController
    {
        private readonly LoadWords _words;
        private readonly KeyBoard _keyBoard;

        public WordleController(KeyBoard keyBoard,LoadWords words)
        {
            _keyBoard = keyBoard;
            _words = words;
        }




        [HttpPost]
        public async Task<ActionResult> Check()
        {
            string word = "";
            using (StreamReader stream = new StreamReader(HttpContext.Request.Body,Encoding.UTF8))
            {
                word = await stream.ReadToEndAsync();
                CheckWord checkWord = new CheckWord(_words,_keyBoard);
                if (!checkWord.ContainWord(word))
                {
                    BasicNotification("palabra no encontrada  en la lista",NotificationType.Warning,"Alerta");
                    return View(new CheckModel(_keyBoard,_words));
                }
                return View(new CheckModel(_keyBoard,_words));
                // body = "param=somevalue&param2=someothervalue"
            }

        }
    }
}
