
using System.ComponentModel.DataAnnotations;

namespace FormulatrixB.Models;

public enum ItemType
{
  None,
  Json,
  Xml
}

public class Item
{
  // Name as a primary key, so cannot be duplicated.
  [Key]
  public string Name { get; set; } = string.Empty;
  public string Content { get; set; } = string.Empty;
  public ItemType Type { get; set; } = ItemType.None;
}