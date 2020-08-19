using System;
using System.Collections.Generic;
using System.Text;
namespace console_storage_sent_msg
{
    public class order
    {
        public string Id { get; set; }
        public int quantity { get; set; }

        public order()
        {
            Id = Guid.NewGuid().ToString();
            Random rnd = new Random();
            quantity=rnd.Next(1000);
        }
        /// <summary>
        /// order method
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Id : {Id}, Quantity : {quantity}";
        }
    }
}