namespace BlogMVC.BLL
{
    public class TagHelper
    {
        public static string GetTagNameForOutput(string tagName)
        {
            return tagName.Replace('.', '-').Replace(' ', '+');
        }

        public static string GetOriginalTagName(string tagName)
        {
            return tagName.Replace('-', '.').Replace('+', ' ');
        }
    }
}