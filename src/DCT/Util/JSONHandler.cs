using System;
using System.Runtime.Serialization;
//using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;

namespace DCT.Util
{
    // see http://stackoverflow.com/questions/1212344/parse-json-in-c

    internal static class JSONHandler
    {
        internal static T Deserialize<T>(string json)
        {
            T obj = Activator.CreateInstance<T>();
            using (MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json)))
            {
                //DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                //obj = (T)serializer.ReadObject(ms);
                return obj;
            }
        }

    }
}
