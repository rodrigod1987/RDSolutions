namespace RDSolutions.Common.Extensions
{
    public static class StringExtensions
    {

        public static string GetImageExtension(this string base64)
        {
            if (string.IsNullOrEmpty(base64))
                return null;

            var base64Content = base64.Split(";");
            var extension = base64Content[0].Split("/")[1];

            return $".{extension}";
        }

        public static string GetImageContent(this string base64)
        {
            if (string.IsNullOrEmpty(base64))
                return null;

            var base64Content = base64.Split(";");

            return base64Content[1].Substring(7);
        }
    }
}
