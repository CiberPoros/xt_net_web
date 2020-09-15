﻿using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ThreeLayer.DAL.Xml.Extensions
{
    internal static class ObjectSerializationExtensions
    {
        public static XElement ToXElement<T>(this T obj) where T : class
        {
            using (var memoryStream = new MemoryStream())
            {
                using (TextWriter streamWriter = new StreamWriter(memoryStream))
                {
                    var xmlSerializer = new XmlSerializer(typeof(T));
                    xmlSerializer.Serialize(streamWriter, obj);
                    return XElement.Parse(Encoding.ASCII.GetString(memoryStream.ToArray()));
                }
            }
        }

        public static T FromXElement<T>(this XElement xElement) where T : class
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            return (T)xmlSerializer.Deserialize(xElement.CreateReader());
        }
    }
}
