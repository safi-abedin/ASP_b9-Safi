using FuncCalculator;
using System;
using static FuncCalculator.Calculator;

//instance method invoke - we have to create instance then pass it
//when the method is static we dont have to pass any ,we can just call the method
Calculator calculator = new Calculator();

Func<int, int, int> Add = calculator.Addition;

Func<int, int, int> Divison = Division;

Console.WriteLine(Divison(6,2));
Console.WriteLine(Add(5,2));