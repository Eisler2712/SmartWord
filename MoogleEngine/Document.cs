using static MoogleEngine.Constant;
namespace MoogleEngine
{
    public class Document
    {
        public int MaxFrequency { set; get; }
        public Dictionary<string, int> FrequencyByWords { set; get; }
        public string FileName { set; get; }
        public string Content { set; get; }
        public Document(string router)
        {
            FrequencyByWords = new Dictionary<string, int>();
            FileName = Path.GetFileName(router);
            FileName = FileName.Substring(0, FileName.Length - 4);
            Content = File.ReadAllText(router);
            Tokenize();
            SetMaxFrequency();
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
    }
}