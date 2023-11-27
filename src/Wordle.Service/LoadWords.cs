using System.Text.RegularExpressions;
using Wordle.Service.Interface;

namespace Wordle.Service
{
    public class LoadWords :ILoadWords
    {

        private StreamReader _sr;

        private string _pathfileWordByLetters = "DataWord/length/5.txt";

        private string _currentWord = "";

        public int CountLettersByWord { get; set; }



        private List<string> _words = new List<string>();

        //en vez de 5 iriran el _settingsGame.MaxColumLength
        public LoadWords(int maxColumLength = 5)
        {
            CountLettersByWord = maxColumLength;

            _pathfileWordByLetters = FileSelectorByNumeberOfLetters(maxColumLength);

            _sr = new StreamReader(_pathfileWordByLetters);

            StartReadFile();

            GetNewWord();

        }

        private string FileSelectorByNumeberOfLetters(int byCountLettes)
        {
            if (byCountLettes >= 5 && byCountLettes <= 26)
            {
                string switchPath = _pathfileWordByLetters.Replace("5",byCountLettes.ToString());
                return Path.Combine(Directory.GetCurrentDirectory(),switchPath);
            }

            return Path.Combine(Directory.GetCurrentDirectory(),_pathfileWordByLetters);
        }

        private void StartReadFile()
        {
            int cont = 0;
            while (cont < (int) _sr.BaseStream.Length)
            {
                //Si no tiene acentos se guarda en el array
                string? line = _sr.ReadLine();
                if (line != null)
                {
                    if (!Regex.IsMatch(line,@"\s*[^a-zA-Z0-9]+\s*"))
                    {
                        _words.Add(line);

                    }
                }
                cont++;

            }
            _sr.Close();
        }

        public List<string> GetListWords()
        {
            return _words;
        }
        public string GetCurrentWord()
        {
            return _currentWord;
        }

        public string GetNewWord()
        {
            int rnd = GetNumberWordRandom();

            _currentWord = _words[rnd];

            return _currentWord;
        }
        private int GetNumberWordRandom()
        {
            Random rnd = new Random();
            return rnd.Next(1,_words.Count);
        }
    }
}