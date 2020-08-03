//////////////////////////////////////////////////////////////////////////
// Licensed under GNU General Public License version 2 (GPLv2)
//
// WSUS Smart Approve by DHGMS Solutions
// http://wsussmartapprove.codeplex.com
//////////////////////////////////////////////////////////////////////////

namespace SmartApprove.Model
{
	/// <summary>
	/// Represents a run set inside the app config
	/// one of them will match the arguement on the command line
	/// A run set is a set of rules specific to an invidiual run of the program
	/// i.e. a runset of "monday" for a mondays scheduled run
	/// or "england" for english machines
	/// </summary>
	class RunSet
		: System.Configuration.ConfigurationElement
	{
		/// <summary>
		/// The name of the run set
		/// </summary>
		//public System.String name;
		[System.Configuration.ConfigurationProperty("Name", IsRequired = true)]
		public System.String Name
		{
			get
			{
				return (System.String)this["Name"];
			}
			set
			{
				this["Name"] = value;
			}
		}

		/// <summary>
		/// All target groups
		/// used instead of specifying a list of target groups
		/// </summary>
		[System.Configuration.ConfigurationProperty("AllTargetGroups", IsRequired = false)]
		public AllTargetGroups AllTargetGroups
		{
			get
			{
				return (AllTargetGroups)this["AllTargetGroups"];
			}
			set
			{
				this["AllTargetGroups"] = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		//System.Collections.Generic.List<TargetGroup> targetGroups;
		[System.Configuration.ConfigurationProperty("TargetGroups", IsRequired = false)]
		public TargetGroupCollection TargetGroups
		{
			get
			{
				return (TargetGroupCollection)this["TargetGroups"];
			}
			set
			{
				this["TargetGroups"] = value;
			}
		}
	}
}
