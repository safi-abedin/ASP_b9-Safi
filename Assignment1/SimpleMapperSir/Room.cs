using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMapperSir
{
    public class Room
    {
        public string RoomNumber { get; set; }
        
        public List<Window> Windows { get; set; }
        public string[] Features { get; set; }
    }
}
