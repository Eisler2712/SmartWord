namespace MoogleEngine
{
    public static class Corrector
    {
        public static string DevelopWord(QueryDocument querys, MyMatrix vocabulary)// Extraer la palabra mas similar a cada palabra de la query
        {
            string result = "";
            
            foreach (var query in querys.WeightByWords)
            {
                foreach (var word in vocabulary.Vocabulary)
                {
                    var x = LevenshteinDistance(query.Key, word);
                    if (x <= query.Key.Length / 4 && x > 0)
                    {
                        result += " " + word;
                        break;
                    }else if (x==0)
                    {
                        result.Remove(0);
                        break;
                    }
                }

            }
            return result;
        }

        public static int LevenshteinDistance(string queryWord, string documentWord)// toma cada palabra de la query y la compara con cada palabra de los documentos y devuelve el numero menor de cambios posibles para que las palabras sean iguales
        {
            int cost = 0;
            int queryWordLength = queryWord.Length;
            int documentWordLength = documentWord.Length;
            int[,] matrix = new int[queryWordLength + 1, documentWordLength + 1];
            if (documentWordLength == 0)
            {
                return queryWordLength;
            }
            if (queryWordLength == 0)
            {
                return documentWordLength;
            }
            for (int i = 0; i <= queryWordLength; matrix[i, 0] = i++) ;
            for (int j = 0; j <= documentWordLength; matrix[0, j] = j++) ;
            for (int i = 1; i <= queryWordLength; i++)
            {
                for (int j = 1; j <= documentWordLength; j++)
                {
                    cost = (queryWord[i - 1] == documentWord[j - 1]) ? 0 : 1;
                    matrix[i, j] = Math.Min(Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1), matrix[i - 1, j - 1] + cost);
                }
            }
            if (queryWord.Length>documentWord.Length)
            {
                
            }
            return matrix[queryWordLength, documentWordLength];
        }
    }
}