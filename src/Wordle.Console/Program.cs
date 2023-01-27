using Wordle.Service;

ILoadWords lw = new LoadWords();
KeyBoard key = new KeyBoard();




lw.StartReadFile();
Console.WriteLine($"poperty a  : {lw.GetWord()} ");
Console.WriteLine($"poperty a  : {lw.GetWord()} ");




