using static MoogleEngine.Constant;
namespace MoogleEngine
{
    public class Document
    {
        public int MaxFrequency { set; get; }
        public Dictionary<string, int> FrequencyByWords { set; get; }
        public Dictionary<string, double> WeightByWords { set; get; }
        public string FileName { set; get; }
        public string Content { set; get; }
        public Document(string router)
        {
            WeightByWords= new ();
            FrequencyByWords = new ();
            FileName = Path.GetFileName(router);
            FileName = FileName.Substring(0, FileName.Length - 4);
            Content = File.ReadAllText(router);
            Tokenize();
            SetMaxFrequency();
            FillWeightByWord();
        }
        public void Tokenize()
        {
            string newContent = Content.Replace("\t", " ").Replace("\n", " ").Replace(",", " ").Replace(".", " ").Replace(";", " ").Replace("!", " ");
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
               WeightByWords.Add(key,0.5+0.5*FrequencyByWords[key]/(double)MaxFrequency);
           }
        }
    }
}