using SamplesDelgate1;
using static SamplesDelgate1.Solution;

//instance method invoke - we have to create instance then pass it
//when the method is static we dont have to pass any ,we can just call the method
mySample mySample  = new mySample();
myMethod myd1 = mySample.myStrMethod;
myMethod myd2 = mySignMethod;
Console.WriteLine("{0} is {1}; use the sign \"{2}\".", 5, myd1(5), myd2(5));
Console.WriteLine("{0} is {1}; use the sign \"{2}\".", -3, myd1(-3), myd2(-3));
Console.WriteLine("{0} is {1}; use the sign \"{2}\".", 0, myd1(0), myd2(0));

Console.WriteLine("__||||||||||||_______||||||||||||||______");

Func<int, string> m1 = mySample.myStrMethod;

Func<int, string> m2 = mySignMethod;

Console.WriteLine("{0} is {1}; use the sign \"{2}\".", 5, m1(5), m2(5));
Console.WriteLine("{0} is {1}; use the sign \"{2}\".", -3, m1(-3), m2(-3));
Console.WriteLine("{0} is {1}; use the sign \"{2}\".", 0, m1(0), m2(0));