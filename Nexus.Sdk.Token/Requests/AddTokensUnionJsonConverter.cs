using System.Text.Json;
using System.Text.Json.Serialization;

namespace Nexus.Sdk.Token.Requests;

/// <summary>
/// Converts <see cref="AddTokensUnion"/> to/from JSON.
/// Serializes as an array of strings when only <see cref="AddTokensUnion.TokenCodes"/> is set,
/// or as an array of objects when only <see cref="AddTokensUnion.TokenCodesWithData"/> is set.
/// </summary>
public class AddTokensUnionJsonConverter : JsonConverter<AddTokensUnion>
{
    public override AddTokensUnion? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
            return null;

        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;

        if (root.ValueKind != JsonValueKind.Array)
            return null;

        var firstElement = root.EnumerateArray().FirstOrDefault();
        if (firstElement.ValueKind == JsonValueKind.String)
        {
            var codes = root.Deserialize<string[]>(options);
            return codes is null ? null : AddTokensUnion.FromTokenCodes(codes);
        }

        var objects = root.Deserialize<TokenCodeWithData[]>(options);
        return objects is null ? null : AddTokensUnion.FromTokenCodesWithData(objects);
    }

    public override void Write(Utf8JsonWriter writer, AddTokensUnion value, JsonSerializerOptions options)
    {
        if (value.TokenCodes is not null)
        {
            JsonSerializer.Serialize(writer, value.TokenCodes, options);
        }
        else if (value.TokenCodesWithData is not null)
        {
            JsonSerializer.Serialize(writer, value.TokenCodesWithData, options);
        }
        else
        {
            writer.WriteNullValue();
        }
    }
}
