//////////////////////////////////////////////////////////////////////////
// Licensed under GNU General Public License version 2 (GPLv2)
//
// WSUS Smart Approve by DHGMS Solutions
// http://wsussmartapprove.codeplex.com
//////////////////////////////////////////////////////////////////////////

namespace SmartApprove.Model
{
	/// <summary>
	/// 
	/// </summary>
	sealed class CommandLineSettings
		: SmartLib.Model.CommandLineSettings
	{
		#region fields

		/// <summary>
		/// 
		/// </summary>
		public bool IsTest
		{
			get;
			private set;
		}

		/// <summary>
		/// 
		/// </summary>
		public System.String RunSetName
		{
			get;
			private set;
		}

		/// <summary>
		/// 
		/// </summary>
		public SmartLib.Model.TriState UseRunSet
		{
			get;
			private set;
		}

		#endregion

		#region methods

		/// <summary>
		/// 
		/// </summary>
		/// <param name="wantsHelp"></param>
		/// <param name="isTest"></param>
		/// <param name="useRunSet"></param>
		/// <param name="runSetName"></param>
		public CommandLineSettings(
			bool wantsHelp,
			bool isTest,
			SmartLib.Model.TriState useRunSet,
			System.String runSetName
			)
		{
			WantsHelp = wantsHelp;
			IsTest = isTest;
			UseRunSet = useRunSet;
			RunSetName = runSetName;
		}

		#endregion
	}
}