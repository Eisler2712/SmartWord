using static MoogleEngine.Constant;
namespace MoogleEngine
{
    public class QueryDocument
    {
        public Dictionary<string, int> FrequencyByWords { set; get; }
        public string Content { set; get; }
        public QueryDocument(string query)
        {
            FrequencyByWords = new Dictionary<string, int>();
            Content = query;
            Tokenize();

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
    }
}