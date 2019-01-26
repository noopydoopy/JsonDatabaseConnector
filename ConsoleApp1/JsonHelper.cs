using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace ConsoleApp1
{
    public static class JsonHelper
    {
        public static string WriteFromObject<T>(T obj) where T : class, new()
        {
            MemoryStream ms = new MemoryStream();

            // Serializer the User object to the stream.  
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            ser.WriteObject(ms, obj);
            byte[] json = ms.ToArray();
            ms.Close();
            return Encoding.UTF8.GetString(json, 0, json.Length);
        }

        public static T ReadToObject<T>(string json) where T : class, new()
        {
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            var deserialized = ser.ReadObject(ms) as T;
            ms.Close();
            return deserialized;
        }
    }
}
