using static MoogleEngine.Constant;
namespace MoogleEngine
{
    public class QueryDocument
    {
        public static Dictionary<string,List<string>> Operators;
        public static Dictionary<string,int> Relevance;
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
            string newContent = Content.Replace("\t", " ").Replace("\n", " ").Replace(",", " ").Replace(".", " ")
            .Replace(";", " ").Replace(">", " ").Replace("<", " ").Replace("?", " ")
            .Replace("¿", " ").Replace("¡", " ").Replace("/", " ").Replace("\""," ");
            var tempwords = newContent.ToLower().Split(" ").Where(s => !string.IsNullOrEmpty(s));
            List<string> words = new List<string>();
            foreach (var temp in tempwords)
            {
                string temp2 = temp;
                if (temp[0]=='^')
                {
                    temp2 = temp.Remove(0,1);
                    Operators["^"].Add(temp2);
                }
                  if (temp[0]=='!')
                {
                    temp2 = temp.Remove(0,1);
                    Operators["!"].Add(temp2);
                }
                  if (temp[0]=='~')
                {
                    temp2 = temp.Remove(0,1);
                    Operators["~"].Add(temp2);
                }
                if (temp[0]=='*')
                {
                   temp2= CountRelevant(temp);
                }
              words.Add(temp2);
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
          public string CountRelevant(string word)
        {
            int counterRelevance=0;
            while(word[0]=='*')
            {
                counterRelevance+=1;
                word=word.Remove(0,1);
            }
            Relevance.Add(word,counterRelevance);
            Operators["*"].Add(word);
            return word;
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