using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Wordle.Service
{
    public class LoadWords :ILoadWords
    {

        private StreamReader _sr;

        private string _pathfileWordByLetters = "bin/Debug/net6.0/length/05.txt";

        private string _currentWord = "";

        public int CountLettersByWord { get; set; }



        private List<string> _words = new List<string>();

        public LoadWords(int countLettersByWord = 5)
        {
            CountLettersByWord = countLettersByWord;

            _pathfileWordByLetters = FileSelectorByNumeberOfLetters(countLettersByWord);

            _sr = new StreamReader(_pathfileWordByLetters);
            StartReadFile();

            GetWord();

        }

        private string FileSelectorByNumeberOfLetters(int byCountLettes)
        {
            if (byCountLettes > 0 && byCountLettes <= 26)
            {
                string switchPath = _pathfileWordByLetters.Replace("05","0" + byCountLettes.ToString());
                return Path.Combine(Directory.GetCurrentDirectory(),switchPath);
            }

            return Path.Combine(Directory.GetCurrentDirectory(),_pathfileWordByLetters);
        }

        public void StartReadFile()
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

        public string GetWord()
        {
            int rnd = GetNumberWordRandom();

            _currentWord = _words[rnd];

            return _currentWord;
        }



        private int GetNumberWordRandom()
        {
            Random rnd = new Random();
            return rnd.Next(1,_words.Count - 2);
        }
    }
}