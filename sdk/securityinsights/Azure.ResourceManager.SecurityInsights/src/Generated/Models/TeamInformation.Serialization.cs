// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;
using System.Text.Json;
using Azure.Core;

namespace Azure.ResourceManager.SecurityInsights.Models
{
    public partial class TeamInformation : IUtf8JsonSerializable
    {
        void IUtf8JsonSerializable.Write(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            writer.WriteEndObject();
        }

        internal static TeamInformation DeserializeTeamInformation(JsonElement element)
        {
            Optional<string> teamId = default;
            Optional<Uri> primaryChannelUrl = default;
            Optional<DateTimeOffset> teamCreationTimeUtc = default;
            Optional<string> name = default;
            Optional<string> description = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("teamId"))
                {
                    teamId = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("primaryChannelUrl"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        primaryChannelUrl = null;
                        continue;
                    }
                    primaryChannelUrl = new Uri(property.Value.GetString());
                    continue;
                }
                if (property.NameEquals("teamCreationTimeUtc"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    teamCreationTimeUtc = property.Value.GetDateTimeOffset("O");
                    continue;
                }
                if (property.NameEquals("name"))
                {
                    name = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("description"))
                {
                    description = property.Value.GetString();
                    continue;
                }
            }
            return new TeamInformation(teamId.Value, primaryChannelUrl.Value, Optional.ToNullable(teamCreationTimeUtc), name.Value, description.Value);
        }
    }
}
