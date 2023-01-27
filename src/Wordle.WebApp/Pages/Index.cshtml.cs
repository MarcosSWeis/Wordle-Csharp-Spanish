using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Wordle.Service;

namespace Wordle.WebApp.Pages
{
    public class IndexModel :PageModel
    {

        public readonly KeyBoard KeyBoard;
        public readonly LoadWords Words;
        public CheckWord? CheckWord;


        // public List<Letter> Letters;
        public IndexModel(KeyBoard keyBoard,LoadWords words)
        {
            KeyBoard = keyBoard;
            Words = words;


        }

        public void OnGet()
        {
            CheckWord = new CheckWord(Words,KeyBoard);
        }
    }

}