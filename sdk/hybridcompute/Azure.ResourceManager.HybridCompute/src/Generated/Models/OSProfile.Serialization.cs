// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Text.Json;
using Azure.Core;

namespace Azure.ResourceManager.HybridCompute.Models
{
    public partial class OSProfile : IUtf8JsonSerializable
    {
        void IUtf8JsonSerializable.Write(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            if (Optional.IsDefined(WindowsConfiguration))
            {
                writer.WritePropertyName("windowsConfiguration");
                writer.WriteObjectValue(WindowsConfiguration);
            }
            if (Optional.IsDefined(LinuxConfiguration))
            {
                writer.WritePropertyName("linuxConfiguration");
                writer.WriteObjectValue(LinuxConfiguration);
            }
            writer.WriteEndObject();
        }

        internal static OSProfile DeserializeOSProfile(JsonElement element)
        {
            Optional<string> computerName = default;
            Optional<OSProfileWindowsConfiguration> windowsConfiguration = default;
            Optional<OSProfileLinuxConfiguration> linuxConfiguration = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("computerName"))
                {
                    computerName = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("windowsConfiguration"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    windowsConfiguration = OSProfileWindowsConfiguration.DeserializeOSProfileWindowsConfiguration(property.Value);
                    continue;
                }
                if (property.NameEquals("linuxConfiguration"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    linuxConfiguration = OSProfileLinuxConfiguration.DeserializeOSProfileLinuxConfiguration(property.Value);
                    continue;
                }
            }
            return new OSProfile(computerName.Value, windowsConfiguration.Value, linuxConfiguration.Value);
        }
    }
}
