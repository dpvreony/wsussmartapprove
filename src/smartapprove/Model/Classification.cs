//////////////////////////////////////////////////////////////////////////
// Licensed under GNU General Public License version 2 (GPLv2)
//
// WSUS Smart Approve by DHGMS Solutions
// http://wsussmartapprove.codeplex.com
//////////////////////////////////////////////////////////////////////////

namespace SmartApprove.Model
{
	/// <summary>
	/// Represents an update classification in WSUS
	/// </summary>
	class Classification
		: Rule
	{
		/// <summary>
		/// guid - matches the one in WSUS
		/// </summary>
		[System.Configuration.ConfigurationProperty("Guid", IsRequired = true)]
		public System.Guid Guid
		{
			get
			{
				return (System.Guid)this["Guid"];
			}
			set
			{
				this["Guid"] = value;
			}
		}

		/// <summary>
		/// list of products matching those in WSUS
		/// </summary>
		[System.Configuration.ConfigurationProperty("Products", IsRequired = false)]
		public ProductCollection Products
		{
			get
			{
				return (ProductCollection)this["Products"];
			}
			set
			{
				this["Products"] = value;
			}
		}

	}
}
