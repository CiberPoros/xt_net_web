using System.IO;
using System.Xml.Linq;
using ThreeLayer.Common.Entities;
using ThreeLayer.Common.Utils;
using ThreeLayer.DAL.Xml.Extensions;

namespace ThreeLayer.DAL.Xml.Abstract
{
    internal class EntityIdManager
    {
        internal static int GetNextId<T>(FileInfo storageFileInfo) where T : IEntityWithId => 
            XDocument.Load(storageFileInfo.FullName).Root
                     .Elements()
                     .MaxOrDefault(-1, xElement => xElement.FromXElement<T>().Id) + 1;
    }
}