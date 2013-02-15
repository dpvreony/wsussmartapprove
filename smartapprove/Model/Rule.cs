namespace SmartApprove.Model
{
    using System;
    using System.Configuration;

    /// <summary>
    /// The rule.
    /// </summary>
    internal class Rule : ConfigurationElement
    {
        #region Public Properties

        /// <summary>
        /// Approve updates needing a license agreement
        /// </summary>
        [ConfigurationProperty("AcceptLicenseAgreement", IsRequired = true)]
        public bool AcceptLicenseAgreement
        {
            get
            {
                return (bool)this["AcceptLicenseAgreement"];
            }

            set
            {
                this["AcceptLicenseAgreement"] = value;
            }
        }

        /// <summary>
        /// Approve updates reported as needed by machines
        /// </summary>
        [ConfigurationProperty("ApproveNeededUpdates", IsRequired = true)]
        public bool ApproveNeededUpdates
        {
            get
            {
                return (bool)this["ApproveNeededUpdates"];
            }

            set
            {
                this["ApproveNeededUpdates"] = value;
            }
        }

        /// <summary>
        /// Approve Stale Updates
        /// This can be done by WSUS, unless a license agreement is needed
        /// </summary>
        [ConfigurationProperty("ApproveStaleUpdates", IsRequired = true)]
        public bool ApproveStaleUpdates
        {
            get
            {
                return (bool)this["ApproveStaleUpdates"];
            }

            set
            {
                this["ApproveStaleUpdates"] = value;
            }
        }

        /// <summary>
        /// Approve updates that supersede previously approved updates
        /// but have not been detected as needed
        /// </summary>
        [ConfigurationProperty("ApproveSupersededUpdates", IsRequired = true)]
        public bool ApproveSupersededUpdates
        {
            get
            {
                return (bool)this["ApproveSupersededUpdates"];
            }

            set
            {
                this["ApproveSupersededUpdates"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Copy.
        /// </summary>
        [ConfigurationProperty("Copy", IsRequired = false)]
        public bool Copy
        {
            get
            {
                return (bool)this["Copy"];
            }

            set
            {
                this["Copy"] = value;
            }
        }

        /// <summary>
        /// Copy a series of updates
        /// but have not been detected as needed
        /// </summary>
        [ConfigurationProperty("CopyFrom", IsRequired = false)]
        public Guid CopyFrom
        {
            get
            {
                return (Guid)this["CopyFrom"];
            }

            set
            {
                this["CopyFrom"] = value;
            }
        }

        #endregion
    }
}