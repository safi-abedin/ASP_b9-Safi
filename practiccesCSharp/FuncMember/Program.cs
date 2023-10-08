

using System.Linq;

Member[] member =
{
    new Member{Id=1,Name="kalam",City="Dhaka"},
    new Member{Id=2,Name="adss",City="comilla"},
    new Member{Id=1,Name="hasan",City="Barisal"}
};

Func<Member, bool> funcMember = m => m.Id == 1;

var result = member.Where(funcMember);
foreach(var item in result)
{
    Console.WriteLine(item.Name);
}

string[] names = { "safi", "hasan", "Abdul", "Rahim", "jukarburg", "musk" };

Func<string, bool> haslength = x => x.Length > 5;

var result1 = names.Where(haslength);

foreach (var name in result1)
{
    Console.WriteLine(name);
}

