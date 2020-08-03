//////////////////////////////////////////////////////////////////////////
// Licensed under GNU General Public License version 2 (GPLv2)
//
// WSUS Smart Approve by DHGMS Solutions
// http://wsussmartapprove.codeplex.com
//////////////////////////////////////////////////////////////////////////

namespace SmartApprove.Model
{
	class AllTargetGroups
		: System.Configuration.ConfigurationElement
	{
		[System.Configuration.ConfigurationProperty("AllClassifications", IsRequired = false)]
		public Rule AllClassifications
		{
			get
			{
				return (Rule)this["AllClassifications"];
			}
			set
			{
				this["AllClassifications"] = value;
			}
		}

		[System.Configuration.ConfigurationProperty("Classifications", IsRequired = false)]
		public ClassificationCollection Classifications
		{
			get
			{
				return (ClassificationCollection)this["Classifications"];
			}
			set
			{
				this["Classifications"] = value;
			}
		}
	}
}