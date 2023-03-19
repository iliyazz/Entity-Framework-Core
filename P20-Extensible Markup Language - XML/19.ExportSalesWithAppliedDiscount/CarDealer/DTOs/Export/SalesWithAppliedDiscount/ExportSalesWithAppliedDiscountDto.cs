namespace CarDealer.DTOs.Export.SalesWithAppliedDiscount
{
    using System.Xml.Serialization;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;

    [XmlType("sale")]
    public class ExportSalesWithAppliedDiscountDto
    {
        [XmlElement("car")]
        public ExportInnerDto Car { get; set; }


        [XmlElement("discount")]
        public string Discount { get; set; }

        
        [XmlElement("customer-name")]
        public string CustomerName { get; set; }

        
        [XmlElement("price")]
        public decimal Price { get; set; }

        
        [XmlElement("price-with-discount")]
        public decimal PriceWithDiscount { get; set; }
    }
}
