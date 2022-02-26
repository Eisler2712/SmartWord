using static MoogleEngine.Constant;
namespace MoogleEngine
{
    public class Document
    {
        public float MaxFrequency { set; get; }
        public Dictionary<string, float> FrequencyByWords { set; get; }
        public Dictionary<string, float> WeightByWords { set; get; }
        public string FileName { set; get; }
        public string Content { set; get; }
        public Document(string router)
        {
            WeightByWords = new();
            FrequencyByWords = new();
            FileName = Path.GetFileName(router);
            FileName = FileName.Substring(0, FileName.Length - 4);
            Content = File.ReadAllText(router);
            Tokenize();
            SetMaxFrequency();
            FillWeightByWord();
        }
        public void Tokenize()
        {
            string newContent = Content.Replace("\t", " ").Replace("\n", " ").Replace(",", " ").Replace(".", " ").Replace(";", " ").Replace("!", " ").Replace("'"," ").Replace("`"," ").Replace("“",(" ").Replace("”"," ").Replace("\""," "));
            var words = newContent.ToLower().Split(" ").Where(s => !string.IsNullOrEmpty(s));
            foreach (var word in words)
            {
                if (!conjuciones.Contains(word) && !preposiciones.Contains(word) && !pronombres.Contains(word))
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
                WeightByWords.Add(key, FrequencyByWords[key] / MaxFrequency);
            }
        }
        public string Snippet(string document)
        {
            int i = 0;
            string snippet = "";
            while (i < 200 && i < document.Length)
            {
                snippet += document[i];
                i++;
            }
            return snippet;
        }
    }
}