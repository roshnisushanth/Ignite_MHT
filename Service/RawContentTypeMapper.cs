using System.ServiceModel.Channels;

namespace IgniteServices
{
    public class RawContentTypeMapper : WebContentTypeMapper
    {
        public override WebContentFormat GetMessageFormatForContentType(string contentType)
        {
            if (contentType.Contains("text/xml") || contentType.Contains("application/json") || contentType.Contains("application/json; charset=utf-8") || contentType.Contains("application/json") || contentType.Contains("application/xml") || contentType.Contains("application/vnd.google.safebrowsing-update"))
            {
                return WebContentFormat.Raw;
            }
            else
            {
                return WebContentFormat.Default;
            }
        }
    }
}