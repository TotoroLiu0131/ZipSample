using System;
using System.Collections;

namespace ZipSample
{
    public class Key
    {
        public Boy OwnerBoy { get; set; }
    }

    public class Boy
    {
        public string Name { get; set; }
    }

    public class Girl
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class Order
    {
        public int OrderId { get; set; }
        public int Amount { get; set; }
        public DateTime CreateDate { get; set; }
        public string Name { get; set; }
        public int Key { get; set; }
    }

    public class OrderDto
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public string Date { get; set; }
        public string Name { get; set; }
        public int Key { get; set; }
    }
}