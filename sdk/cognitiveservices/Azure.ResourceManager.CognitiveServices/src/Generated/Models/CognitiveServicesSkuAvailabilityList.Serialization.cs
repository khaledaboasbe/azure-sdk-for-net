// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Text.Json;
using Azure.Core;

namespace Azure.ResourceManager.CognitiveServices.Models
{
    public partial class CognitiveServicesSkuAvailabilityList
    {
        internal static CognitiveServicesSkuAvailabilityList DeserializeCognitiveServicesSkuAvailabilityList(JsonElement element)
        {
            Optional<string> kind = default;
            Optional<string> type = default;
            Optional<string> skuName = default;
            Optional<bool> skuAvailable = default;
            Optional<string> reason = default;
            Optional<string> message = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("kind"))
                {
                    kind = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("type"))
                {
                    type = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("skuName"))
                {
                    skuName = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("skuAvailable"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    skuAvailable = property.Value.GetBoolean();
                    continue;
                }
                if (property.NameEquals("reason"))
                {
                    reason = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("message"))
                {
                    message = property.Value.GetString();
                    continue;
                }
            }
            return new CognitiveServicesSkuAvailabilityList(kind.Value, type.Value, skuName.Value, Optional.ToNullable(skuAvailable), reason.Value, message.Value);
        }
    }
}
