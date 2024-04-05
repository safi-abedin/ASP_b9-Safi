using StackOverFlow.Domain.Entities;
using System;

namespace StackOverFlow.Infrastructure.Data
{
    public class TagsSeed
    {
        public Tag[] Tags { get; set; }

        public TagsSeed()
        {
            Tags = new Tag[]
            {
                new Tag
                {
                    Id = new Guid("DE8D8D12-81B2-4A68-8C6B-C3FE3F962381"),
                    Name = "JavaScript",
                    Description = "JavaScript is a server-side language."
                },
                new Tag
                {
                    Id = Guid.NewGuid(),
                    Name = "C#",
                    Description = "C# is a programming language developed by Microsoft."
                },
                new Tag
                {
                    Id = Guid.NewGuid(),
                    Name = "Python",
                    Description = "Python is a high-level, interpreted programming language."
                },
                new Tag
                {
                    Id = Guid.NewGuid(),
                    Name = "Java",
                    Description = "Java is a widely used programming language."
                },
                new Tag
                {
                    Id = Guid.NewGuid(),
                    Name = "HTML",
                    Description = "HTML is the standard markup language for creating web pages."
                },
                new Tag
                {
                    Id = Guid.NewGuid(),
                    Name = "CSS",
                    Description = "CSS is used for styling web pages."
                },
                new Tag
                {
                    Id = Guid.NewGuid(),
                    Name = "SQL",
                    Description = "SQL is used for managing relational databases."
                },
                new Tag
                {
                    Id = Guid.NewGuid(),
                    Name = "React",
                    Description = "React is a JavaScript library for building user interfaces."
                },
                new Tag
                {
                    Id = Guid.NewGuid(),
                    Name = "Angular",
                    Description = "Angular is a platform and framework for building single-page client applications using HTML and TypeScript."
                },
                new Tag
                {
                    Id = Guid.NewGuid(),
                    Name = "Node.js",
                    Description = "Node.js is an open-source, cross-platform JavaScript runtime environment that executes JavaScript code outside of a browser."
                },
                new Tag
                {
                    Id = Guid.NewGuid(),
                    Name = "Ruby",
                    Description = "Ruby is a dynamic, reflective, object-oriented, general-purpose programming language."
                },
                new Tag
                {
                    Id = Guid.NewGuid(),
                    Name = "PHP",
                    Description = "PHP is a general-purpose scripting language especially suited to web development."
                },
                new Tag
                {
                    Id = Guid.NewGuid(),
                    Name = "Swift",
                    Description = "Swift is a powerful and intuitive programming language for macOS, iOS, watchOS, and tvOS."
                },
                new Tag
                {
                    Id = Guid.NewGuid(),
                    Name = "Go",
                    Description = "Go is an open source programming language that makes it easy to build simple, reliable, and efficient software."
                },
                new Tag
                {
                    Id = Guid.NewGuid(),
                    Name = "TypeScript",
                    Description = "TypeScript is a typed superset of JavaScript that compiles to plain JavaScript."
                },
                new Tag
                {
                    Id = Guid.NewGuid(),
                    Name = "Rust",
                    Description = "Rust is a systems programming language that runs blazingly fast, prevents segfaults, and guarantees thread safety."
                },
                new Tag
                {
                    Id = Guid.NewGuid(),
                    Name = "Kotlin",
                    Description = "Kotlin is a statically typed programming language that runs on the Java Virtual Machine and can be compiled to JavaScript source code or use the LLVM compiler infrastructure."
                },
                new Tag
                {
                    Id = Guid.NewGuid(),
                    Name = "Scala",
                    Description = "Scala is a general-purpose programming language providing support for functional programming and a strong static type system."
                },
                new Tag
                {
                    Id = Guid.NewGuid(),
                    Name = "Ruby on Rails",
                    Description = "Ruby on Rails, or Rails, is a server-side web application framework written in Ruby under the MIT License."
                },
                new Tag
                {
                    Id = Guid.NewGuid(),
                    Name = "Objective-C",
                    Description = "Objective-C is a general-purpose, object-oriented programming language that adds Smalltalk-style messaging to the C programming language."
                },
                new Tag
                {
                    Id = Guid.NewGuid(),
                    Name = "Vue.js",
                    Description = "Vue.js is an open-source JavaScript framework for building user interfaces and single-page applications."
                },
                new Tag
                {
                    Id = Guid.NewGuid(),
                    Name = "ASP.NET",
                    Description = "ASP.NET is an open-source web framework for building modern web apps and services with .NET."
                },
                new Tag
                {
                    Id = Guid.NewGuid(),
                    Name = "Django",
                    Description = "Django is a high-level Python web framework that encourages rapid development and clean, pragmatic design."
                }
            };
        }
    }
}
