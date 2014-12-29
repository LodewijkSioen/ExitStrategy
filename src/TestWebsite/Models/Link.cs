namespace ExitStrategy.TestWebsite.Models
{
    public class Link
    {
        public Link(string text, string url)
        {
            Text = text;
            Url = url;
        }

        public string Text { get; set; }
        public string Url { get; set; }
    }
}