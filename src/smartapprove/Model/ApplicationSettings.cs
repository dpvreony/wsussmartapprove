//////////////////////////////////////////////////////////////////////////
// Licensed under GNU General Public License version 2 (GPLv2)
//
// WSUS Smart Approve by DHGMS Solutions
// http://wsussmartapprove.codeplex.com
//////////////////////////////////////////////////////////////////////////

namespace SmartApprove.Model
{
	/// <summary>
	/// Represents the Application Settings
	/// </summary>
	class ApplicationSettings
		: System.Configuration.ConfigurationSection
	{
		#region fields

		/// <summary>
		/// Server connection info
		/// </summary>
		[System.Configuration.ConfigurationProperty("Server", IsRequired = false)]
		public SmartLib.Model.Server Server
		{
			get
			{
				return (SmartLib.Model.Server)this["Server"];
			}
			set
			{
				this["Server"] = value;
			}
		}

		/// <summary>
		/// List of Run Sets
		/// </summary>
		[System.Configuration.ConfigurationProperty("RunSets", IsRequired = false)]
		public RunSetCollection RunSets
		{
			get
			{
				return (RunSetCollection)this["RunSets"];
			}
			set
			{
				this["RunSets"] = value;
			}
		}

		/// <summary>
		/// Default rules to be applied to all run sets
		/// </summary>
		[System.Configuration.ConfigurationProperty("NoRunSet", IsRequired = true)]
		public Rule NoRunSet
		{
			get
			{
				return (Rule)this["NoRunSet"];
			}
			set
			{
				this["NoRunSet"] = value;
			}
		}

		#endregion
	}
}
