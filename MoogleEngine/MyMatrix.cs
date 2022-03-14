namespace MoogleEngine
{

    public class MyMatrix
    {
        public MyMatrix(List<Document> documents)
        {
            CounterData = new Dictionary<string, int>();
            Files = documents ?? new List<Document>();
            Vocabulary = AllWords().ToList();
            Matrix = new float[Files.Count(), Vocabulary.Count()];

            CompleteMatrix();
            Console.WriteLine();
            //MultiplicateMatrix();

        }
        public float count;
        public List<Document> Files { get; set; }
        public List<string> Vocabulary { get; set; }

        public float[,] Matrix { get; set; }

        public Dictionary<string, int> CounterData;

        private void CompleteMatrix()
        {
            System.Console.WriteLine($"CompleteMatrix  Files: {Files.Count} VocabularyLenght: {Vocabulary.Count}");
            for (int i = 0; i < Files.Count; i++)
            {
                for (int j = 0; j < Vocabulary.Count; j++)
                {
                   var document = Files[i];
                    var word = Vocabulary[j];
                    Matrix[i, j] = document.WeightByWords.ContainsKey(word) ? document.WeightByWords[word] : 0;


                   //count= CounterData.ContainsKey(Vocabulary[j]) ? : CounterData[Vocabulary[]] Counter(Vocabulary[j], Files);

                    // if(!CounterData.ContainsKey(Vocabulary[j]))
                    //      CounterData[Vocabulary[j]] = Counter(Vocabulary[j], Files);
                    
                    // int count = CounterData[Vocabulary[j]];
                      
                    count = Moogle.CounterData[word];
                
                    Matrix[i,j]=Matrix[i,j]*(float)Math.Log(Files.Count/count);

                    //System.Console.WriteLine(j);
                }
            System.Console.WriteLine($"End for index{i}");    
            }
            System.Console.WriteLine("End CompleteMatrix");
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
            System.Console.WriteLine($"Complete AllWords --- MyMatrix.cs ----- Lenght: {allwords.Count}");
            return allwords;
        }
        private int Counter(string word, List<Document> docs)
        {
            int counter=0;
            foreach (var doc in docs)
            {
                if(doc.FrequencyByWords.ContainsKey(word))
                    counter++;
            }
          /* for (int j = 0; j < Vocabulary.Count; j++)
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
             
            }*/
            return counter;
        }
       /* private void MultiplicateMatrix()
        {
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    count= Counter(Vocabulary[j]);
                    Matrix[i,j]=Matrix[i,j]*(float)Math.Log10(Files.Count/count);
                }
            }
        }*/
    }
}