using System.Xml.Linq;

namespace ThreeLayer.DAL.Xml.Extensions
{
    internal static class XDocumentExtensions
    {
        public static XDocument AddToRoot(this XDocument xDocument, XElement xElement)
        {
            xDocument.Root.Add(xElement);

            return xDocument;
        }
    }
}
