public static class XmlConverter
{
    public static string ObjectToXmlString<T>(T obj)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        XmlWriterSettings settings = new XmlWriterSettings
        {
            Encoding = new UTF8Encoding(false),
            Indent = true,
            OmitXmlDeclaration = false,
        };

        using (var stringWriter = new Utf8StringWriter())
        using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, settings))
        {
            serializer.Serialize(xmlWriter, obj);
            return stringWriter.ToString();
        }
    }
}

public class Utf8StringWriter : StringWriter
{
    public override Encoding Encoding => new UTF8Encoding(false);
}
