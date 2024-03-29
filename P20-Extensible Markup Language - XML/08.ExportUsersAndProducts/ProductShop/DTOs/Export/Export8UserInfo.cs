﻿namespace ProductShop.DTOs.Export
{
    using System.Xml.Serialization;

    [XmlType("User")]
    public class Export8UserInfo
    {
        [XmlElement("firstName")]
        public string FirstName { get; set; }

        [XmlElement("lastName")]
        public string LastName { get; set; }

        [XmlElement("age")]
        public int? Age { get; set; }

        public Export8SoldProductsCount SoldProducts { get; set; }
    }
}
