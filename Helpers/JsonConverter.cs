using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Simple_Api.Helpers
{
    public class GuidJsonConverter : JsonConverter<Guid>
    {
        public override Guid Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) => new Guid(reader.GetString());

        public override void Write(
            Utf8JsonWriter writer,
            Guid id,
            JsonSerializerOptions options) =>
                writer.WriteStringValue(GenericHelper.ConvertToUpper(id));
    }
    public class ObjectJsonConverter : JsonConverter<string>
    {
        public override string Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) => reader.GetString();

        public override void Write(
            Utf8JsonWriter writer,
            string id,
            JsonSerializerOptions options) =>
                writer.WriteRawValue(id);
    }
}