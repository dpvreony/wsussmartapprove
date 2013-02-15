//////////////////////////////////////////////////////////////////////////
// Licensed under GNU General Public License version 2 (GPLv2)
//
// WSUS Smart Approve by DHGMS Solutions
// http://wsussmartapprove.codeplex.com
//////////////////////////////////////////////////////////////////////////

namespace SmartApprove.Model
{
	class TargetGroup
		: AllTargetGroups
	{
		//public System.Guid _Guid;
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
