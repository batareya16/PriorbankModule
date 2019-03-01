using System.Xml.Serialization;
using System.IO;

namespace PriorbankModule.Common
{
    public static class Serializer
    {
        public static string Serialize<T>(T obj)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, obj);
                return writer.ToString();
            }
        }

        public static T Deserialize<T>(string @string)
        {
            var serializer = new XmlSerializer(typeof(T));
            var reader = new StringReader(@string);
            return (T)serializer.Deserialize(reader);
        }
    }
}
