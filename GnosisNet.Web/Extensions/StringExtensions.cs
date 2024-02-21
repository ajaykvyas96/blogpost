namespace GnosisNet.Web.Extensions
{
    public static class StringExtensions
    {
        public static string TruncateAndAddReadMore(this string input, int maxLength, string readMoreUrl)
        {
            if (string.IsNullOrEmpty(input) || input.Length <= maxLength)
            {
                return input;
            }

            // Truncate the string
            var truncatedContent = input.Substring(0, maxLength);

            // Add read more link
            truncatedContent += $"<a href='{readMoreUrl}'>Read More</a>";

            return truncatedContent;
        }
    }
}
