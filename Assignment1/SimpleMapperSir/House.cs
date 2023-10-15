using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMapperSir
{
    public class House
    {
        public List<Room> Rooms { get; set; }

        public string[] HouseFeatures { get; set; }
    }
}
