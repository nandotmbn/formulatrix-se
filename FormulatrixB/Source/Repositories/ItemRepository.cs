using System.Text.Json;
using FormulatrixB.Database;
using FormulatrixB.Interfaces;
using FormulatrixB.Models;
using Microsoft.EntityFrameworkCore;

namespace FormulatrixB.Repositories;

public class ItemRepository(AppDBContext appDBContext) : IItem
{
  public async Task<Item> Deregister(string name)
  {
    using var transaction = appDBContext.Database.BeginTransaction();
    try
    {
      var item = await appDBContext.Items.Where(item => item.Name == name).FirstOrDefaultAsync() ?? throw new Exception("Item not found");

      appDBContext.Items.Remove(item);
      appDBContext.SaveChanges();

      transaction.Commit();

      return item;
    }
    catch
    {
      throw;
    }
  }

  public async Task<ItemType> GetType(string name)
  {
    return await appDBContext.Items.Where(item => item.Name == name).Select(item => item.Type).FirstOrDefaultAsync();
  }

  public async Task<Item> Register(string name, string content, ItemType type)
  {
    using var transaction = appDBContext.Database.BeginTransaction();
    try
    {
      var existingItem = await appDBContext.Items.AnyAsync(item => item.Name == name);
      if (existingItem) throw new Exception("Item already exists");

      var item = new Item
      {
        Name = name,
        Type = type
      };

      switch (item.Type)
      {
        case ItemType.Json:
          if (!ValidJson(content)) throw new Exception("Invalid JSON");
          item.Content = content;
          break;
        case ItemType.Xml:
          if (!ValidXml(content)) throw new Exception("Invalid XML");
          item.Content = content;
          break;
        default:
          throw new Exception("Invalid item type");
      }

      appDBContext.Items.Add(item);
      appDBContext.SaveChanges();

      transaction.Commit();

      return item;
    }
    catch
    {
      throw;
    }
  }

  public async Task<string> Retrieve(string name)
  {
    return await appDBContext.Items.Where(i => i.Name == name).Select(i => i.Content).FirstOrDefaultAsync() ?? throw new Exception("Item not found");
  }

  public bool ValidJson(string content)
  {
    try
    {
      var json = JsonSerializer.Deserialize<object>(content);
      return true;
    }
    catch
    {
      return false;
    }
  }

  public bool ValidXml(string content)
  {
    try
    {
      // https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xdocument.parse?view=net-9.0
      var xml = System.Xml.Linq.XDocument.Parse(content);
      return true;
    }
    catch
    {
      return false;
    }
  }
}