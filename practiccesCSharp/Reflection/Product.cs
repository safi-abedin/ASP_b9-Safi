using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection
{
    public class Product
    {
        private int id;

        public string Name {  get; set; }

        protected double Price { get; set; }

        public void IncreasePrice(double price)
        {
            Price += price;
        }

        private void ChangeName(string name)
        {
            if(!string.IsNullOrEmpty(name))
            {
                Name = name;
            }
        }
    }
}
