using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StackOverFlow.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class TagsSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("0fc04e24-8ac1-4c5b-bd47-4fae76047855"), "CSS is used for styling web pages.", "CSS" },
                    { new Guid("15a8155b-1113-4661-8571-ae2c066b94f9"), "Swift is a powerful and intuitive programming language for macOS, iOS, watchOS, and tvOS.", "Swift" },
                    { new Guid("2cc5eb7b-3abf-4c04-ae89-2021eb349c0b"), "Ruby is a dynamic, reflective, object-oriented, general-purpose programming language.", "Ruby" },
                    { new Guid("42ccc0fe-d9c4-410b-b7d0-5b7579407a20"), "Angular is a platform and framework for building single-page client applications using HTML and TypeScript.", "Angular" },
                    { new Guid("4873f6fa-1cbe-4134-8956-a03302dadd6f"), "Go is an open source programming language that makes it easy to build simple, reliable, and efficient software.", "Go" },
                    { new Guid("4eb691c3-f1ec-473d-94b6-36aef248c9d6"), "TypeScript is a typed superset of JavaScript that compiles to plain JavaScript.", "TypeScript" },
                    { new Guid("500a46e2-f627-4251-972b-3b1fe2fdf4d8"), "Rust is a systems programming language that runs blazingly fast, prevents segfaults, and guarantees thread safety.", "Rust" },
                    { new Guid("50e98ea1-cff1-4196-9269-84ab517fd21c"), "React is a JavaScript library for building user interfaces.", "React" },
                    { new Guid("54139fbd-aff0-4205-b43c-0a1a96f1ef3e"), "Django is a high-level Python web framework that encourages rapid development and clean, pragmatic design.", "Django" },
                    { new Guid("5a9d0591-61f3-493c-8125-456b48a661cf"), "Java is a widely used programming language.", "Java" },
                    { new Guid("6cc04c6c-98e1-4c9b-b205-fe061f29d32a"), "Ruby on Rails, or Rails, is a server-side web application framework written in Ruby under the MIT License.", "Ruby on Rails" },
                    { new Guid("81f7f8ee-1bed-4f57-9aac-d16c19012ab3"), "HTML is the standard markup language for creating web pages.", "HTML" },
                    { new Guid("9aaff0af-2df7-43ef-8835-bc5f300c753f"), "Python is a high-level, interpreted programming language.", "Python" },
                    { new Guid("9c0b9230-508c-43c7-b9ba-60f08d08d775"), "PHP is a general-purpose scripting language especially suited to web development.", "PHP" },
                    { new Guid("a30bb9f1-30b6-41a6-81fe-7b962d61510d"), "Node.js is an open-source, cross-platform JavaScript runtime environment that executes JavaScript code outside of a browser.", "Node.js" },
                    { new Guid("a5715aea-b818-4803-a118-52a5b5217f93"), "Kotlin is a statically typed programming language that runs on the Java Virtual Machine and can be compiled to JavaScript source code or use the LLVM compiler infrastructure.", "Kotlin" },
                    { new Guid("b683007c-6970-4f60-9dbb-8a8d23e916b7"), "SQL is used for managing relational databases.", "SQL" },
                    { new Guid("b749dda7-47a9-493e-a9ab-824b750be9ab"), "Objective-C is a general-purpose, object-oriented programming language that adds Smalltalk-style messaging to the C programming language.", "Objective-C" },
                    { new Guid("dd729c92-cfce-4a8a-a48f-a531849ae7df"), "C# is a programming language developed by Microsoft.", "C#" },
                    { new Guid("de8d8d12-81b2-4a68-8c6b-c3fe3f962381"), "JavaScript is a server-side language.", "JavaScript" },
                    { new Guid("e491a529-370f-4fc9-94d8-50cc2ee30976"), "Vue.js is an open-source JavaScript framework for building user interfaces and single-page applications.", "Vue.js" },
                    { new Guid("fa6b186d-a87d-4daa-b399-323deed09153"), "ASP.NET is an open-source web framework for building modern web apps and services with .NET.", "ASP.NET" },
                    { new Guid("fd1c635f-5a55-4a91-b24b-6b690cce48d1"), "Scala is a general-purpose programming language providing support for functional programming and a strong static type system.", "Scala" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("0fc04e24-8ac1-4c5b-bd47-4fae76047855"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("15a8155b-1113-4661-8571-ae2c066b94f9"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("2cc5eb7b-3abf-4c04-ae89-2021eb349c0b"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("42ccc0fe-d9c4-410b-b7d0-5b7579407a20"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("4873f6fa-1cbe-4134-8956-a03302dadd6f"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("4eb691c3-f1ec-473d-94b6-36aef248c9d6"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("500a46e2-f627-4251-972b-3b1fe2fdf4d8"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("50e98ea1-cff1-4196-9269-84ab517fd21c"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("54139fbd-aff0-4205-b43c-0a1a96f1ef3e"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("5a9d0591-61f3-493c-8125-456b48a661cf"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("6cc04c6c-98e1-4c9b-b205-fe061f29d32a"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("81f7f8ee-1bed-4f57-9aac-d16c19012ab3"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("9aaff0af-2df7-43ef-8835-bc5f300c753f"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("9c0b9230-508c-43c7-b9ba-60f08d08d775"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("a30bb9f1-30b6-41a6-81fe-7b962d61510d"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("a5715aea-b818-4803-a118-52a5b5217f93"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("b683007c-6970-4f60-9dbb-8a8d23e916b7"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("b749dda7-47a9-493e-a9ab-824b750be9ab"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("dd729c92-cfce-4a8a-a48f-a531849ae7df"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("de8d8d12-81b2-4a68-8c6b-c3fe3f962381"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("e491a529-370f-4fc9-94d8-50cc2ee30976"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("fa6b186d-a87d-4daa-b399-323deed09153"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("fd1c635f-5a55-4a91-b24b-6b690cce48d1"));
        }
    }
}
