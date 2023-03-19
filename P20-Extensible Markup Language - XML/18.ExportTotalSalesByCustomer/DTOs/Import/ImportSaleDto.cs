﻿namespace CarDealer.DTOs.Import
{
    using System.Xml.Serialization;

    [XmlType("Sale")]
    public class ImportSaleDto
    {
        [XmlElement("discount")]
        public decimal Discount { get; set; }


        [XmlElement("carId")]
        public int? CarId { get; set; }


        [XmlElement("customerId")]
        public int CustomerId { get; set; }
    }
}
/*
 <Sales>
    <Sale>
        <carId>105</carId>
        <customerId>30</customerId>
        <discount>30</discount>
    </Sale>
 */
