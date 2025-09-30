using System;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebAPI.Middlewares;

public class EnumConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        var isNullableEnum = Nullable.GetUnderlyingType(typeToConvert)?.IsEnum ?? false;
        return (typeToConvert.IsEnum || isNullableEnum) &&
               typeToConvert != typeof(HttpStatusCode) &&
               typeToConvert != typeof(HttpStatusCode?);
    }

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        Type enumType = Nullable.GetUnderlyingType(typeToConvert) ?? typeToConvert;
        bool isNullable = Nullable.GetUnderlyingType(typeToConvert) != null;

        var converterType = isNullable
            ? typeof(NullableEnumConverter<>).MakeGenericType(enumType)
            : typeof(EnumConverter<>).MakeGenericType(enumType);

        return (JsonConverter)Activator.CreateInstance(converterType)!;
    }
}

public class EnumConverter<T> : JsonConverter<T> where T : struct, Enum
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? stringValue = reader.GetString();
        return Enum.TryParse<T>(stringValue, ignoreCase: true, out var value)
            ? value
            : default;
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}

public class NullableEnumConverter<T> : JsonConverter<T?> where T : struct, Enum
{
    public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null) return null;

        string? stringValue = reader.GetString();
        return Enum.TryParse<T>(stringValue, ignoreCase: true, out var value)
            ? value
            : null;
    }

    public override void Write(Utf8JsonWriter writer, T? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
        {
            writer.WriteStringValue(value.Value.ToString());
        }
        else
        {
            writer.WriteNullValue();
        }
    }
}
