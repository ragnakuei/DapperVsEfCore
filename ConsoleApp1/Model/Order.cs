﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleApp1
{
    [Table("Orders")]
    public class Order
    {
        public Order()
        {
            Customer  = new Customer();
            ShippedBy = new Shipper();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }

        [MaxLength(5)]
        public string CustomerID { get; set; }

        public int?      EmployeeID   { get; set; }
        public DateTime? OrderDate    { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate  { get; set; }
        public int?      ShipVia      { get; set; }
        public decimal?  Freight      { get; set; }

        [MaxLength(40)]
        public string ShipName { get; set; }

        [MaxLength(60)]
        public string ShipAddress { get; set; }

        [MaxLength(15)]
        public string ShipCity { get; set; }

        [MaxLength(15)]
        public string ShipRegion { get; set; }

        [MaxLength(10)]
        public string ShipPostalCode { get; set; }

        [MaxLength(15)]
        public string ShipCountry { get; set; }

        public Customer Customer  { get; set; }

        [ForeignKey(nameof(ShipVia))]
        public Shipper  ShippedBy { get; set; }
    }
}