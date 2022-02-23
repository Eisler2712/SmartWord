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
        string root = @"../Content/";
        (items, AllDocs) = SearchingFolders(root, query);

        MyMatrix myMatrix = new MyMatrix(AllDocs);
        return new SearchResult(items, Corrector.DevelopWord(query, myMatrix));
    }
    private static (SearchItem[], List<Document>) SearchingFolders(string router, string query)
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
        System.Console.WriteLine(Corrector.LevenshteinDistance("tigra", "tigre"));
        var searchItems = AllDocs
                       .Select((d, i) => (d, Score(d, i, myMatrix, myVector)))
                       .Where(x => x.Item2 > 0)
                       .Select(t => new SearchItem(t.d.FileName, "", t.Item2))
                       .OrderByDescending(x => x.Score);


        return (searchItems.ToArray(), AllDocs);
    }
    public static float Score(Document d, int documentIndex, MyMatrix myMatrix, Vector myVector)
    {
        float counter = 0;
        for (int j = 0; j < myVector.Matrix.GetLength(1); j++)
        {
            if (myVector.Matrix[0, j] > 0 && myMatrix.Matrix[documentIndex, j] > 0)
            {
                counter += (float)myMatrix.Matrix[documentIndex, j];
            }
        }
        return counter;
    }

}
