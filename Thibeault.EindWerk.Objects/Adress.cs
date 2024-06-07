﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Thibeault.EindWerk.Objects
{
    public class Address
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string City { get; set; }
        public int PostCode { get; set; }
        public string StreetName { get; set; }
        public int HouseNumber { get; set; }
    }
}