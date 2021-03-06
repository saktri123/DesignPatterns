using System.Collections.Generic;

namespace FlyWeight.CodeingExcercise
{
    public class Sentence
    {
        private readonly string plainText;
        private Dictionary<int, WordToken> tokens;
        private string[] words;
        public Sentence(string plainText)
        {
            this.plainText = plainText;
            words = plainText.Split(' ');
            tokens = new Dictionary<int, WordToken>();
        }

        public WordToken this[int index]
        {
            get
            {
                tokens.Add(index, new WordToken());
                return tokens[index];
            }
        }

        public override string ToString()
        {
            var ws = new List<string>();
            for (int i = 0; i < words.Length; i++)
            {
                var word = tokens.ContainsKey(i)  && tokens[i].Capitalize ? words[i].ToUpper() : words[i];
                ws.Add(word);

            }
            return string.Join(" ",ws);
        }

        public class WordToken
        {
            public bool Capitalize;
        }
    }

    public class Program {
       public static void Main() { }
    }
}
