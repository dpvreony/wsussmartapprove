//////////////////////////////////////////////////////////////////////////
// Licensed under GNU General Public License version 2 (GPLv2)
//
// WSUS Smart Approve by DHGMS Solutions
// http://wsussmartapprove.codeplex.com
//////////////////////////////////////////////////////////////////////////

namespace SmartApprove.Model
{
	/// <summary>
	/// Represents a product in WSUS
	/// </summary>
	class Product
		: System.Configuration.ConfigurationElement
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

	}
}
