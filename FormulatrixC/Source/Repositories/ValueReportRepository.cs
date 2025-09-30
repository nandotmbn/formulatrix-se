using FormulatrixC.Interfaces;

namespace FormulatrixC.Repositories;

public class ValueReporter : IValueReporter
{
  public void Report(double value)
  {
    Console.WriteLine($"Value: {value}");
  }
}