using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using System.Text;

internal class Repository
{
    private static Repository Repro;
    public static List<string> MyRepository { get; set; }
    private static string Path;

    private Repository()
    {
        //Nothing over here
    }

    internal async static Task<Repository> OpenAsync(string path)
    {
        //This is short Dummy Code
        if (Repro==null)
        {
            Repro = new Repository();
        }

        path = "http://api.worldbank.org/countries?format=json";
        Path = path;

        return Repro;
    }

    internal async Task<String> ReadAsync()
    {
        //This is longer Dummy code
        HttpClient client = new HttpClient();
        StringBuilder sb = new StringBuilder();

        using (var response = await client.GetStreamAsync(Path))
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.Load(response);

            var result = from lnks in htmlDoc.DocumentNode.Descendants()
                         where lnks.Name == "a" &&
                         lnks.Attributes["href"] != null
                         select lnks.Attributes["href"].Value.ToString();


            foreach (var item in result)
            {
                sb.Append(item.ToString());
            }
        }

        return sb.ToString();
    }


    internal Task LogAsync(RepositoryException e)
    {
        //Dummy code
        return null;
    }

    internal Task CloseAsync()
    {
        //Dummy code
        return null;
    }
}