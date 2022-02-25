using static MoogleEngine.Constant;
namespace MoogleEngine
{
    public class QueryDocument
    {
        public Dictionary<string,List<string>> Operators;
        public Dictionary<string,int> Relevance;
        public Dictionary<string,float> WeightByWords;
        public float MaxFrequency{set;get;}
        public Dictionary<string, float> FrequencyByWords { set; get; }
        public string Content { set; get; }
        public QueryDocument(string query)
        {
            Operators=new Dictionary<string, List<string>>
            {
                {"^",new List<string>()},
                {"!",new List<string>()},
                {"`",new List<string>()},
                {"*",new List<string>()}
            };
            Relevance= new();
            WeightByWords= new();
            FrequencyByWords = new ();
            Content = query;
            Tokenize();
            SetMaxFrequency();
            FillWeightByWord();

        }
        public void Tokenize()
        {
            int counterRelevance=0;
            string newContent = Content.Replace("\t", " ").Replace("\n", " ").Replace(",", " ").Replace(".", " ")
            .Replace(";", " ").Replace("!", " ").Replace(">", " ").Replace("<", " ").Replace("?", " ")
            .Replace("¿", " ").Replace("¡", " ").Replace("*", " ").Replace("/", " ");
            var tempwords = newContent.ToLower().Split(" ").Where(s => !string.IsNullOrEmpty(s));
            List<string> words = new List<string>();
            foreach (var temp in tempwords)
            {
                if (temp[0]=='^')
                {
                    Operators["^"].Add(temp.Replace("^"," "));
                }
                  if (temp[0]=='!')
                {
                    Operators["!"].Add(temp.Replace("!"," "));
                }
                  if (temp[0]=='`')
                {
                    Operators["`"].Add(temp.Replace("`"," "));
                }
                if (temp[0]=='*')
                {
                    for (int i = 0; i < temp.Length; i++)
                    {
                        if (temp[i]=='*')
                        {
                            counterRelevance++;
                        }else
                        {
                            Operators["*"].Add(temp.Remove(0,i));
                        }
                    }
                    Relevance.Add(temp,counterRelevance);
                }
           
            }
            foreach (var tempWord in tempwords)
            {
                var word= tempWord.Replace("*"," ");
                words.Add(word);
            }
            foreach (var word in words)
            {
                if (!conjuciones.Contains(word) && !preposiciones.Contains(word))
                {
                    if (FrequencyByWords.ContainsKey(word))
                    {
                        FrequencyByWords[word]++;
                    }
                    else
                    {
                        FrequencyByWords.Add(word, 1);
                    }
                }
            }
        }
          private void SetMaxFrequency()
        {
            var values = FrequencyByWords.Values;
            MaxFrequency = values.Max();
        }
         private void FillWeightByWord()
        {

           foreach (var key in FrequencyByWords.Keys)
           {
               float TF=FrequencyByWords[key]/MaxFrequency;
               WeightByWords.Add(key,0.5f+0.5f*TF);
           }
        }
    }
}