using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace WebScraper1
{
  internal class HTMLHandler
  {
    public enum DivTypes { ID, CLASS, ROLE }

    #region HTML Requests
    public static async Task<string> GetWebPageSourceAsync(string url)
    {
      using (HttpClient client = new HttpClient())
      {
        try
        {
          HttpResponseMessage response = await client.GetAsync(url);

          if (response.IsSuccessStatusCode)
          {
            return await response.Content.ReadAsStringAsync();
          }

          throw new Exception($"Failed to retrieve the webpage. Status code: {response.StatusCode}");

        }
        catch (Exception ex)
        {
          throw new Exception($"An error occurred while fetching the webpage: {ex.Message}");
        }
      }
    }
    #endregion

    #region Data Extraction
    public static string ExtractContentFromDiv(string html, DivTypes divType, string divVal)
    {
      HtmlDocument doc = new HtmlDocument();
      doc.LoadHtml(html);

      string div;
      switch (divType)
      {
        case DivTypes.ID: div = "id"; break;
        case DivTypes.CLASS: div = "class"; break;
        case DivTypes.ROLE: div = "role"; break;
        default:
          throw new ArgumentException();
      }

      HtmlNode divNode = doc.DocumentNode.SelectSingleNode($"//div[@{div}='{divVal}']");

      if (divNode is null)
      {
        throw new Exception($"Div with {div} '{divVal}' not found in the HTML.");
      }

      return divNode.InnerHtml;
    }

    public static string ExtractContentFromTag(string html, string tagType)
    {
      HtmlDocument doc = new HtmlDocument();
      doc.LoadHtml(html);

      HtmlNode tagNode = doc.DocumentNode.SelectSingleNode($"//{tagType}");

      if (tagNode is null)
      {
        throw new Exception($"Tag with type '{tagType}' not found in the HTML.");
      }

      return tagNode.InnerHtml;
    }

    public static List<string> ExtractContentFromTags(string html, string tagType)
    {
      HtmlDocument doc = new HtmlDocument();
      doc.LoadHtml(html);

      HtmlNodeCollection tagNodes = doc.DocumentNode.SelectNodes($"//{tagType}");

      if (tagNodes is null)
      {
        throw new Exception($"Tag with type '{tagType}' not found in the HTML.");
      }

      List<string> tagNodesContent = new List<string>();

      foreach (HtmlNode node in tagNodes)
      {
        tagNodesContent.Add(node.InnerHtml);
      }

      return tagNodesContent;
    }
    #endregion
  }
}
