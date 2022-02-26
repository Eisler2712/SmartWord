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
        MyMatrix myMatrix = new MyMatrix(AllDocs);
        Vector myVector = new Vector(myMatrix.Vocabulary, queryDocument);
        myVector.MultiplicateVector(myMatrix.count, myMatrix);
        var searchItems = AllDocs
                       .Select((d, i) => (d, Score(i, myMatrix, myVector)))
                       .Where(x => x.Item2 > 0)
                       .Select(t => new SearchItem(t.d.FileName, t.d.Snippet(t.d.Content), t.Item2))
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
            sumatoria1 += (float)myMatrix.Matrix[documentIndex, j] * (float)myMatrix.Matrix[documentIndex, j];
            sumatoria2 += (float)myVector.Matrix[0, j] * (float)myVector.Matrix[0, j];
        }

        float denominador = (float)(Math.Sqrt(sumatoria1) * Math.Sqrt(sumatoria2));
        return numerador / denominador;
    }

}
