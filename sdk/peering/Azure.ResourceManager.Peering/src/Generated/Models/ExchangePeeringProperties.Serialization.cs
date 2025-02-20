// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Collections.Generic;
using System.Text.Json;
using Azure.Core;
using Azure.ResourceManager.Resources.Models;

namespace Azure.ResourceManager.Peering.Models
{
    public partial class ExchangePeeringProperties : IUtf8JsonSerializable
    {
        void IUtf8JsonSerializable.Write(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            if (Optional.IsCollectionDefined(Connections))
            {
                writer.WritePropertyName("connections");
                writer.WriteStartArray();
                foreach (var item in Connections)
                {
                    writer.WriteObjectValue(item);
                }
                writer.WriteEndArray();
            }
            if (Optional.IsDefined(PeerAsn))
            {
                writer.WritePropertyName("peerAsn");
                JsonSerializer.Serialize(writer, PeerAsn);
            }
            writer.WriteEndObject();
        }

        internal static ExchangePeeringProperties DeserializeExchangePeeringProperties(JsonElement element)
        {
            Optional<IList<PeeringExchangeConnection>> connections = default;
            Optional<WritableSubResource> peerAsn = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("connections"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    List<PeeringExchangeConnection> array = new List<PeeringExchangeConnection>();
                    foreach (var item in property.Value.EnumerateArray())
                    {
                        array.Add(PeeringExchangeConnection.DeserializePeeringExchangeConnection(item));
                    }
                    connections = array;
                    continue;
                }
                if (property.NameEquals("peerAsn"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    peerAsn = JsonSerializer.Deserialize<WritableSubResource>(property.Value.ToString());
                    continue;
                }
            }
            return new ExchangePeeringProperties(Optional.ToList(connections), peerAsn);
        }
    }
}
