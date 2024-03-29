﻿namespace ProductShop.Utilities
{
    using System.Xml.Serialization;


    public class XmlHelper
    {
        public T Deserialize<T>(string inputXml, string rootName)
        {
            //Deserialize from XML
            XmlRootAttribute xmlRoot = new XmlRootAttribute(rootName);
            //Serialize and deserialize        type data to serialize, XmlRootAttribute
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T), xmlRoot);
            StringReader reader = new StringReader(inputXml);
            T supplierDtos = (T)xmlSerializer.Deserialize(reader);
            return supplierDtos;
        }

        public IEnumerable<T> DeserializeCollection<T>(string inputXml, string rootName)
        {
            XmlRootAttribute xmlRoot = new XmlRootAttribute(rootName);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T[]), xmlRoot);
            StringReader reader = new StringReader(inputXml);
            T[] deserializedDtos = (T[])xmlSerializer.Deserialize(reader);
            return deserializedDtos;
        }
    }

}
