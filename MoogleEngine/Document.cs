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
        public void Tokenize()// Obtener todas las palabras de todos los documentos y su frecuencia
        {
            string newContent = Content.Replace("\t", " ").Replace("\n", " ").Replace(",", " ").
            Replace(".", " ").Replace(";", " ").Replace("!", " ").Replace("'", " ").Replace("`", " ").
            Replace("“", (" ").Replace("”", " ").Replace("\"", " ").Replace(">"," ").Replace("<"," ").
            Replace("_"," ").Replace("»","").Replace("»","").Replace("—"," "));
            var words = newContent.ToLower().Split(" ").Where(s => !string.IsNullOrEmpty(s));

            Dictionary<string, bool> @checked = new Dictionary<string, bool>();
            
            // Obtener la frecuencia de las palabras en cada documento
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

                    if (!Moogle.CounterData.ContainsKey(word)) Moogle.CounterData.Add(word, 1);
                    else
                    {
                        Moogle.CounterData[word] += (@checked.ContainsKey(word) ? 0 : 1);

                        if (!@checked.ContainsKey(word))
                            @checked.Add(word, true);
                    }

                }

            }
        }
        private void SetMaxFrequency()// Extraer la mayor frecuencia en cada documento
        {
            var values = FrequencyByWords.Values;
            MaxFrequency = values.Max();
        }
        private void FillWeightByWord()//completando el diccionario con los pesos de cada palabra en cada documento
        {

            foreach (var key in FrequencyByWords.Keys)
            {
                WeightByWords.Add(key, FrequencyByWords[key] / MaxFrequency);
            }
        }
        public string Snippet(string document)// Obtener un segmento del documento 
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