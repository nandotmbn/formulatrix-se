
using FormulatrixA;

var fooBar = new FooBar
{
  RuleName = RuleName.FooBar,
  Start = 1,
  End = 200,
  Rules = [
    new Rule { Input = 3, Output = "foo" },
    new Rule { Input = 4, Output = "baz" },
    new Rule { Input = 5, Output = "bar" },
    new Rule { Input = 7, Output = "jazz" },
  ]
};

fooBar.Rules.Add(new Rule { Input = 9, Output = "huzz" });

fooBar.Run();

Console.WriteLine(fooBar.Result);