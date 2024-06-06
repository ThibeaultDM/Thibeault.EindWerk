﻿namespace Thibeault.EindWerk.Base.Objects
{
    public class Customer
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
    }
}