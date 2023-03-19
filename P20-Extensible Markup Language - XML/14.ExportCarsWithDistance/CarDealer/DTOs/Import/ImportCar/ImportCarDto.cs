﻿namespace CarDealer.DTOs.Import.ImportCar
{
    using System.Xml.Serialization;

    [XmlType("Car")]
    public class ImportCarDto
    {
        [XmlElement("make")]
        public string Make { get; set; } = null!;


        [XmlElement("model")]
        public string Model { get; set; } = null!;


        [XmlElement("traveledDistance")]
        public long TraveledDistance { get; set; }

        [XmlArray("parts")] 
        public ImportCarPartDto[] Parts { get; set; } = null!;
    }
}
/*
 <Cars>
  <Car>
    <make>Opel</make>
    <model>Omega</model>
    <>176664996</traveledDistance>
    <parts>
      <partId id="38"/>
      <partId id="102"/>
      <partId id="23"/>
      <partId id="116"/>
      <partId id="46"/>
      <partId id="68"/>
      <partId id="88"/>
      <partId id="104"/>
      <partId id="71"/>
      <partId id="32"/>
      <partId id="114"/>
    </parts>
 */
