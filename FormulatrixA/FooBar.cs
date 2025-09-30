
namespace FormulatrixA;

public enum RuleName
{
  None,
  FooBar
}

class Rule
{
  public int Input { get; set; } = 0;
  public string Output { get; set; } = string.Empty;
}

class FooBar
{
  public RuleName RuleName { get; set; } = RuleName.None;
  public int Start { get; set; }
  public int End { get; set; }
  public string Result { get; set; } = string.Empty;
  public List<Rule> Rules { get; set; } = [];

  public void Run()
  {
    if (RuleName == RuleName.FooBar)
    {
      for (int i = Start; i <= End; i++)
      {
        string str = "";
        foreach (var rule in Rules)
        {
          if (i % rule.Input == 0) str += rule.Output;
        }
        if (str == "") str = i.ToString();
        Result += str + ", ";
      }
      Result = Result.TrimEnd(',', ' ');
    }
    else
    {
      Result = "Rule not found";
    }
  }
};