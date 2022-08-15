using eShop.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.Domain.Aggregates
{
    public class Order:Entity,IAggregateRoot
    {
        public string Name { get; set; }
        public string Addresss { get; set; }
        public DateTime CreateAt { get; set; }
        public bool OrderStatus { get; set; }
        public Order(string name,string address)
        {
            Name= name;
            Addresss= address;
            OrderStatus= true;
            CreateAt = DateTime.Now;
        }
        public Order()
        {

        }
    }
}
