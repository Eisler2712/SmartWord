namespace MoogleEngine
{
    public class Vector
    {
        public List<string> Vocabulary{get;set;}
        public float[,] Matrix{get;set;}
        public QueryDocument Query{get;set;}

        public Vector(List<string> list,QueryDocument query)
        {
            Vocabulary=list;
            Query=query;
            Matrix = new float[1,Vocabulary.Count()];
            CompleteVector();

        }
         private void CompleteVector()
       { 
           
               for (int j = 0; j < Vocabulary.Count; j++)
               {

                   var word = Vocabulary[j];
                   Matrix[0,j]= Query.WeightByWords.ContainsKey(word) ? Query.WeightByWords[word] : 0;
               }
           
       }
        public void MultiplicateVector(float count,MyMatrix myMatrix)
        {
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    Matrix[0,j]=Matrix[0,j]*(float)Math.Log10(myMatrix.Files.Count/count);
                }
            
        }
    }
}