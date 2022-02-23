namespace MoogleEngine
{

    public class MyMatrix
    {
        public MyMatrix(List<Document> documents)
        {
            Files = documents ?? new List<Document>();
            Vocabulary = AllWords().ToList();
            Matrix = new double[Files.Count(), Vocabulary.Count()];

            CompleteMatrix();
        }
        public List<Document> Files { get; set; }
        public List<string> Vocabulary { get; set; }

        public double[,] Matrix { get; set; }
        private void CompleteMatrix()
        {
            for (int i = 0; i < Files.Count; i++)
            {
                for (int j = 0; j < Vocabulary.Count; j++)
                {
                    var document = Files[i];
                    var word = Vocabulary[j];
                    Matrix[i, j] = document.WeightByWords.ContainsKey(word) ? document.WeightByWords[word] : 0;
                }
            }
        }
        private HashSet<string> AllWords()
        {
            var allwords = new HashSet<string>();
            foreach (var document in Files)
            {
                foreach (var word in document.FrequencyByWords.Keys)
                {
                    allwords.Add(word);
                }
            }
            return allwords;
        }
    }
}