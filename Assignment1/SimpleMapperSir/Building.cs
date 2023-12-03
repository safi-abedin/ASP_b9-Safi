using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMapperSir
{
    public class Building
    {
        public string BuildingNumber { get; set; }


        public string[] HouseFeatures { get; set; }
        public List<Room[]> Rooms { get; set; }
    }
}
