using FormulatrixB.Models;

namespace FormulatrixBWebAPI.Types;

public record ItemRequest(string Name, string Content, ItemType Type);