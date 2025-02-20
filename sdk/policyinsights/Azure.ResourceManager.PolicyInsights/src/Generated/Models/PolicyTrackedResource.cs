// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;

namespace Azure.ResourceManager.PolicyInsights.Models
{
    /// <summary> Policy tracked resource record. </summary>
    public partial class PolicyTrackedResource
    {
        /// <summary> Initializes a new instance of PolicyTrackedResource. </summary>
        internal PolicyTrackedResource()
        {
        }

        /// <summary> Initializes a new instance of PolicyTrackedResource. </summary>
        /// <param name="trackedResourceId"> The ID of the policy tracked resource. </param>
        /// <param name="policyDetails"> The details of the policy that require the tracked resource. </param>
        /// <param name="createdBy"> The details of the policy triggered deployment that created the tracked resource. </param>
        /// <param name="lastModifiedBy"> The details of the policy triggered deployment that modified the tracked resource. </param>
        /// <param name="lastUpdateUtc"> Timestamp of the last update to the tracked resource. </param>
        internal PolicyTrackedResource(string trackedResourceId, PolicyDetails policyDetails, TrackedResourceModificationDetails createdBy, TrackedResourceModificationDetails lastModifiedBy, DateTimeOffset? lastUpdateUtc)
        {
            TrackedResourceId = trackedResourceId;
            PolicyDetails = policyDetails;
            CreatedBy = createdBy;
            LastModifiedBy = lastModifiedBy;
            LastUpdateUtc = lastUpdateUtc;
        }

        /// <summary> The ID of the policy tracked resource. </summary>
        public string TrackedResourceId { get; }
        /// <summary> The details of the policy that require the tracked resource. </summary>
        public PolicyDetails PolicyDetails { get; }
        /// <summary> The details of the policy triggered deployment that created the tracked resource. </summary>
        public TrackedResourceModificationDetails CreatedBy { get; }
        /// <summary> The details of the policy triggered deployment that modified the tracked resource. </summary>
        public TrackedResourceModificationDetails LastModifiedBy { get; }
        /// <summary> Timestamp of the last update to the tracked resource. </summary>
        public DateTimeOffset? LastUpdateUtc { get; }
    }
}
