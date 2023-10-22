using System.Collections.Generic;

namespace WebScraper1
{
  internal class Problem
  {
    public int Id { get; private set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public Problem(int id)
    {
      Id = id;

      BuildComponents();
    }

    private void BuildComponents()
    {
      string html = GetProblemRaw();

      Title = GetProblemTitle(html);

      Description = GetProblemDescription(html);
    }

    #region Scrape Components
    private string GetProblemRaw()
    {
      string html = HTMLHandler.GetWebPageSourceAsync($"https://projecteuler.net/problem={Id}").Result;

      string raw = HTMLHandler.ExtractContentFromDiv(html, HTMLHandler.DivTypes.ID, "content");

      raw = raw.Replace("\n", "");
      raw = raw.Replace("\b", "");
      raw = raw.Replace("\r", "");

      return raw;
    }

    private string GetProblemTitle(string html)
    {
      return HTMLHandler.ExtractContentFromTag(html, "h2");
    }

    private string GetProblemDescription(string html)
    {
      string rawDesc = HTMLHandler.ExtractContentFromDiv(html, HTMLHandler.DivTypes.CLASS, "problem_content");

      List<string> paragraphs = HTMLHandler.ExtractContentFromTags(rawDesc, "p");

      string formattedDesc = "";

      foreach (string para in paragraphs)
      {
        if (formattedDesc.Length > 0) formattedDesc += "\n\n";

        formattedDesc += para.Replace("<br>", "\n");
      }

      return formattedDesc;
    }
    #endregion

    public override string ToString()
    {
      return $"Id: {Id}\nTitle: {Title}\nDescription: {Description}";
    }
  }
}
