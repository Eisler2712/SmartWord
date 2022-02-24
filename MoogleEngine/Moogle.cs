using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
namespace MoogleEngine;


public static class Moogle
{

    public static SearchResult Query(string query)
    {
        SearchItem[] items;
        List<Document> AllDocs;
        QueryDocument words;
        string root = @"../Content/";
        (items, AllDocs,words) = SearchingFolders(root, query);

        MyMatrix myMatrix = new MyMatrix(AllDocs);
        return new SearchResult(items, Corrector.DevelopWord(words, myMatrix));
    }
    private static (SearchItem[], List<Document>, QueryDocument) SearchingFolders(string router, string query)
    {
        QueryDocument queryDocument = new QueryDocument(query);
        List<Document> AllDocs = new List<Document>();
        List<string> files = Directory.GetFiles(router).ToList();
        files.Remove("../Content/.gitignore");
        foreach (var filepath in files)
        {
            Document Doc = new Document(filepath);
            AllDocs.Add(Doc);
        }
        System.Console.WriteLine("ESOOOOOOOOOO+ "+Corrector.LevenshteinDistance("halo","hola"));
        MyMatrix myMatrix = new MyMatrix(AllDocs);
        Vector myVector = new Vector(myMatrix.Vocabulary, queryDocument);
        myVector.MultiplicateVector(myMatrix.count, myMatrix);
        for (int i = 0; i < myMatrix.Matrix.GetLength(0); i++, System.Console.WriteLine())
        {
            for (int j = 0; j < myMatrix.Matrix.GetLength(1); j++)
            {
                System.Console.Write(myMatrix.Matrix[i, j] + " ");
            }
        }
        for (int j = 0; j < myVector.Matrix.GetLength(1); j++)
        {
            Console.Write(myVector.Matrix[0, j] + " ");
        }
        var searchItems = AllDocs
                       .Select((d, i) => (d, Score(i, myMatrix, myVector)))
                       .Where(x => x.Item2 > 0)
                       .Select(t => new SearchItem(t.d.FileName, "", t.Item2))
                       .OrderByDescending(x => x.Score);


        return (searchItems.ToArray(), AllDocs, queryDocument);
    }
    public static float Score(int documentIndex, MyMatrix myMatrix, Vector myVector)
    {
        float numerador = 0;
        float sumatoria1 = 0;
        float sumatoria2 = 0;
        for (int j = 0; j < myMatrix.Matrix.GetLength(1); j++)
        {
            numerador += (float)myMatrix.Matrix[documentIndex, j] * (float)myVector.Matrix[0, j];
            sumatoria1 += (float)Math.Sqrt((float)myMatrix.Matrix[documentIndex, j] * (float)myMatrix.Matrix[documentIndex, j]);
            sumatoria2 += (float)Math.Sqrt((float)myVector.Matrix[0, j] * (float)myVector.Matrix[0, j]);
        }

        float denominador = sumatoria1 * sumatoria2;
        return numerador / denominador;
    }

}
