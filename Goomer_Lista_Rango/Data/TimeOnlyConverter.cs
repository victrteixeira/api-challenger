﻿using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Goomer_Lista_Rango.TimeAndDateOnlyConverter;

public class TimeOnlyConverter : JsonConverter<TimeOnly>
{
    private const string Format = "HH:mm";
    public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return TimeOnly.ParseExact(reader.GetString(), Format, CultureInfo.InvariantCulture);
    }

    public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(Format, CultureInfo.InvariantCulture));
    }
}