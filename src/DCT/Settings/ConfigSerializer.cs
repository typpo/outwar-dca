using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace DCT.Settings
{
    internal class ConfigSerializer
    {
        private XmlSerializer s = null;
        private Type type = null;

        internal ConfigSerializer()
        {
            this.type = typeof(UserEditable);
            this.s = new XmlSerializer(this.type);
        }

        internal UserEditable Deserialize(string xml)
        {
            TextReader reader = new StringReader(xml);
            return Deserialize(reader);
        }

        internal UserEditable Deserialize(XmlDocument doc)
        {
            TextReader reader = new StringReader(doc.OuterXml);
            return Deserialize(reader);
        }

        internal UserEditable Deserialize(TextReader reader)
        {
            try
            {
                UserEditable o = (UserEditable)s.Deserialize(reader);
                reader.Close();
                return o;
            }
            catch (InvalidCastException)    // changed setting from long to int
            {
                return new UserEditable();
            }
        }

        internal XmlDocument Serialize(UserEditable UserEditable)
        {
            string xml = StringSerialize(UserEditable);
            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = true;
            doc.LoadXml(xml);
            return doc;
        }

        private string StringSerialize(UserEditable UserEditable)
        {
            TextWriter w = WriterSerialize(UserEditable);
            string xml = w.ToString();
            w.Close();
            return xml.Trim();
        }

        private TextWriter WriterSerialize(UserEditable UserEditable)
        {
            TextWriter w = new StringWriter();
            this.s = new XmlSerializer(this.type);
            s.Serialize(w, UserEditable);
            w.Flush();
            return w;
        }

        internal static UserEditable ReadFile(string file)
        {
            ConfigSerializer serializer = new ConfigSerializer();
            try
            {
                string xml = string.Empty;
                using (StreamReader reader = new StreamReader(file))
                {
                    xml = reader.ReadToEnd();
                    reader.Close();
                }
                return serializer.Deserialize(xml);
            }
            catch { }
            return new UserEditable();
        }

        internal static bool WriteFile(string file, UserEditable config)
        {
            bool ok = false;
            ConfigSerializer serializer = new ConfigSerializer();
            try
            {
                string xml = serializer.Serialize(config).OuterXml;
                using (StreamWriter writer = new StreamWriter(file, false))
                {
                    writer.Write(xml.Trim());
                    writer.Flush();
                    writer.Close();
                }
                ok = true;
            }
            catch { }
            return ok;
        }
    }
}