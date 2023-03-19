namespace ProductShop.DTOs.Import
{
    using System.Xml.Serialization;
    using ProductShop.Models;

    [XmlType("CategoryProduct")]
    public class ImportCategoryProductDto
    {
        [XmlElement("CategoryId")]
        public int CategoryId { get; set; }


        [XmlElement("ProductId")]
        public int ProductId { get; set; }
    }
}
/*
     <CategoryProduct>
        <CategoryId>4</CategoryId>
        <ProductId>1</ProductId>
    </CategoryProduct>
 */
