using Assignment3;
using Assignment3.TestCase1;

Product product1 = new Product
{
    Id = new Guid("12979002-445A-4624-82FD-52C21DBB956A"),
    SKU = "ABC123",
    Name = "Product 1",
    Price = 29.90,
    Colors = new List<Color>
    {
        new Color { Id = new Guid("6f6e6e34-70e7-4e47-9884-b26038e9e93e"), Name = "Red", Code = "#FF0000" },
        new Color { Id = new Guid("4eb92b9e-9e39-4bb7-ba21-5f1f11aa6e85"), Name = "Blue", Code = "#0000FF" }
    },
    Feedbacks = new List<Feedback>
    {
        new Feedback { Id = new Guid("377dd928-dc6d-4659-ba29-df03341f25ac"), FeedbackGiver = new User { Id = new Guid("bb429384-0a16-4ba7-93ac-36d7b750a64d"), Name = "John", Email = "john@example.com" }, Rating = 4.5, Comment = "Great product!" },
        new Feedback { Id = new Guid("b1838d5d-8c82-4c07-bc8f-e78818b0a6df"), FeedbackGiver = new User { Id = new Guid("b1838d5d-8c82-4c07-bc8f-e78818b0a6df"), Name = "Emma", Email = "emma@example.com" }, Rating = 3.8, Comment = "Could be better." }
    },
    IsActive = false
};

Product product2 = new Product
{
    Id = Guid.NewGuid(),
    SKU = "DEF456",
    Name = "Product 2",
    Price = 39.90,
    Colors = new List<Color>
    {
        new Color { Id = Guid.NewGuid(), Name = "Green", Code = "#00FF00" },
        new Color { Id =Guid.NewGuid(), Name = "Yellow", Code = "#FFFF00" }
    },
    Feedbacks = new List<Feedback>
    {
        new Feedback { Id = Guid.NewGuid(), FeedbackGiver = new User { Id = Guid.NewGuid(), Name = "Alice", Email = "alice@example.com" }, Rating = 4.2, Comment = "Good quality." },
        new Feedback { Id = Guid.NewGuid(), FeedbackGiver = new User { Id = Guid.NewGuid(), Name = "Bob", Email = "bob@example.com" }, Rating = 3.5, Comment = "Average." }
    },
    IsActive = true
};



var orm =new MyORM<Guid, Product>();
orm.Insert(product2);
//orm.Insert(product1);
//var p = orm.GetById(product.);
var c2 = 0;
