using Newtonsoft.Json;
using SimpleMapperSir;

House house = new House
{
    
    Rooms = new List<Room>
    {
        new Room
        {
            RoomNumber = "200",
            Windows = new List<Window>
            {
                new Window { Width = 200, Height = 300 },
                new Window { Width = 44, Height = 88 }
            }
        },
        new Room
        {
            RoomNumber = "300",
            Windows = new List<Window>
            {
                new Window { Width = 100, Height = 200 },
                new Window { Width = 350, Height = 500 }
            }
        }
    }
};

Building building = new Building();
SimpleMapper.Copy(house, building);
Console.WriteLine("Test 1:");
Console.WriteLine("Destination Object After Copy :");
Console.WriteLine(JsonConvert.SerializeObject(building, Newtonsoft.Json.Formatting.Indented));




// Including array to test

Window window1 = new Window { Width = 100, Height = 120, Name = "Living Room Window" };
Window window2 = new Window { Width = 80, Height = 100, Name = "Bedroom Window" };


Room livingRoom = new Room
{
    RoomNumber = "101",
    Windows = new List<Window> { window1, window2 },
    Features = new string[] { "Carpet flooring", "Fireplace" }
};

Room bedroom = new Room
{
    RoomNumber = "102",
    Windows = new List<Window> { window2 },
    Features = new string[] { "Wooden flooring", "Walk-in closet" }
};

House myHouse = new House
{
    Rooms = new List<Room> { livingRoom, bedroom },
    HouseFeatures = new string[] { "Swimming pool", "Garden" }
};

Building myBuilding = new Building();


SimpleMapper.Copy(myHouse, myBuilding);
Console.WriteLine("Test 2:");
Console.WriteLine("Destination Object After Copy :");
Console.WriteLine(JsonConvert.SerializeObject(myBuilding, Newtonsoft.Json.Formatting.Indented));