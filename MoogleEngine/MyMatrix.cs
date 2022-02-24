namespace MoogleEngine
{

    public class MyMatrix
    {
        public MyMatrix(List<Document> documents)
        {
            Files = documents ?? new List<Document>();
            Vocabulary = AllWords().ToList();
            Matrix = new float[Files.Count(), Vocabulary.Count()];

            CompleteMatrix();
            
            MultiplicateMatrix();

        }
        public float count;
        public List<Document> Files { get; set; }
        public List<string> Vocabulary { get; set; }

        public float[,] Matrix { get; set; }
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
        private float Counter(string word)
        {
            float counter=0;
           for (int j = 0; j < Vocabulary.Count; j++)
           {
               if (Vocabulary[j]==word)
               {
                     for (int i = 0; i < Matrix.GetLength(0); ++i)
                    {
                        if (Matrix[i,j]>0)
                        {
                            counter++;
                        }
                        
                    }
               }
             
            }
            return counter;
        }
        private void MultiplicateMatrix()
        {
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    count= Counter(Vocabulary[j]);
                    Matrix[i,j]=Matrix[i,j]*(float)Math.Log10(Files.Count/count);
                }
            }
        }
    }
}