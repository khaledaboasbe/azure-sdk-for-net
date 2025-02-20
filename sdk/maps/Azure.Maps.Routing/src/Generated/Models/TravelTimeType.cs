// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;
using System.ComponentModel;

namespace Azure.Maps.Routing.Models
{
    /// <summary> The ComputeTravelTime. </summary>
    public readonly partial struct TravelTimeType : IEquatable<TravelTimeType>
    {
        private readonly string _value;

        /// <summary> Initializes a new instance of <see cref="TravelTimeType"/>. </summary>
        /// <exception cref="ArgumentNullException"> <paramref name="value"/> is null. </exception>
        public TravelTimeType(string value)
        {
            _value = value ?? throw new ArgumentNullException(nameof(value));
        }

        private const string NoneValue = "none";
        private const string AllValue = "all";

        /// <summary> Does not compute additional travel times. </summary>
        public static TravelTimeType None { get; } = new TravelTimeType(NoneValue);
        /// <summary> Computes travel times for all types of traffic information and specifies all results in the fields noTrafficTravelTimeInSeconds, historicTrafficTravelTimeInSeconds and liveTrafficIncidentsTravelTimeInSeconds being included in the summaries in the route response. </summary>
        public static TravelTimeType All { get; } = new TravelTimeType(AllValue);
        /// <summary> Determines if two <see cref="TravelTimeType"/> values are the same. </summary>
        public static bool operator ==(TravelTimeType left, TravelTimeType right) => left.Equals(right);
        /// <summary> Determines if two <see cref="TravelTimeType"/> values are not the same. </summary>
        public static bool operator !=(TravelTimeType left, TravelTimeType right) => !left.Equals(right);
        /// <summary> Converts a string to a <see cref="TravelTimeType"/>. </summary>
        public static implicit operator TravelTimeType(string value) => new TravelTimeType(value);

        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj) => obj is TravelTimeType other && Equals(other);
        /// <inheritdoc />
        public bool Equals(TravelTimeType other) => string.Equals(_value, other._value, StringComparison.InvariantCultureIgnoreCase);

        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() => _value?.GetHashCode() ?? 0;
        /// <inheritdoc />
        public override string ToString() => _value;
    }
}
