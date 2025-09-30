
using FormulatrixB.Models;

namespace FormulatrixB.Interfaces;

public interface IItem
{
  bool ValidXml(string content);
  bool ValidJson(string content);
  Task<string> Retrieve(string name);
  Task<ItemType> GetType(string name);
  Task<Item> Register(string name, string content, ItemType type);
  Task<Item> Deregister(string name);
}