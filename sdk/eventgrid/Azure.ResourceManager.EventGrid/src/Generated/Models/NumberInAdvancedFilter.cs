// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Collections.Generic;
using Azure.Core;

namespace Azure.ResourceManager.EventGrid.Models
{
    /// <summary> NumberIn Advanced Filter. </summary>
    public partial class NumberInAdvancedFilter : AdvancedFilter
    {
        /// <summary> Initializes a new instance of NumberInAdvancedFilter. </summary>
        public NumberInAdvancedFilter()
        {
            Values = new ChangeTrackingList<double>();
            OperatorType = AdvancedFilterOperatorType.NumberIn;
        }

        /// <summary> Initializes a new instance of NumberInAdvancedFilter. </summary>
        /// <param name="operatorType"> The operator type used for filtering, e.g., NumberIn, StringContains, BoolEquals and others. </param>
        /// <param name="key"> The field/property in the event based on which you want to filter. </param>
        /// <param name="values"> The set of filter values. </param>
        internal NumberInAdvancedFilter(AdvancedFilterOperatorType operatorType, string key, IList<double> values) : base(operatorType, key)
        {
            Values = values;
            OperatorType = operatorType;
        }

        /// <summary> The set of filter values. </summary>
        public IList<double> Values { get; }
    }
}
