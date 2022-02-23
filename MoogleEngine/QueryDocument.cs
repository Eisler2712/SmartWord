using static MoogleEngine.Constant;
namespace MoogleEngine
{
    public class QueryDocument
    {
        public Dictionary<string,float> WeightByWords;
        public float MaxFrequency{set;get;}
        public Dictionary<string, int> FrequencyByWords { set; get; }
        public string Content { set; get; }
        public QueryDocument(string query)
        {
            WeightByWords= new();
            FrequencyByWords = new Dictionary<string, int>();
            Content = query;
            Tokenize();
            SetMaxFrequency();
            FillWeightByWord();

        }
        public void Tokenize()
        {
            string newContent = Content.Replace("\t", " ").Replace("\n", " ").Replace(",", " ").Replace(".", " ")
            .Replace(";", " ").Replace("!", " ").Replace(">", " ").Replace("<", " ").Replace("?", " ")
            .Replace("¿", " ").Replace("¡", " ").Replace("*", " ").Replace("/", " ");
            var words = newContent.ToLower().Split(" ").Where(s => !string.IsNullOrEmpty(s));
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