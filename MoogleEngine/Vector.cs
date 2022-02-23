namespace MoogleEngine
{
    public class Vector
    {
        public List<string> Vocabulary{get;set;}
        public int[,] Matrix{get;set;}
        public QueryDocument Query{get;set;}

        public Vector(List<string> list,QueryDocument query)
        {
            Vocabulary=list;
            Query=query;
            Matrix = new int[1,Vocabulary.Count()];
            CompleteMatrix();

        }
         private void CompleteMatrix()
       { 
           
               for (int j = 0; j < Vocabulary.Count; j++)
               {

                   var word = Vocabulary[j];
                   Matrix[0,j]= Query.FrequencyByWords.ContainsKey(word) ? Query.FrequencyByWords[word] : 0;
               }
           
       }
    }
}