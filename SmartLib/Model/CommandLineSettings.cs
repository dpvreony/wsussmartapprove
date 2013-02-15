//////////////////////////////////////////////////////////////////////////
// Licensed under GNU General Public License version 2 (GPLv2)
//
// WSUS Smart Approve by DHGMS Solutions
// http://wsussmartapprove.codeplex.com
//////////////////////////////////////////////////////////////////////////

namespace SmartLib.Model
{
	public class CommandLineSettings
	{
		#region fields
		/// <summary>
		/// Indictates the user wants the help info displayed
		/// </summary>
		public bool WantsHelp
		{
			get;
			protected set;
		}
		#endregion

		/// <summary>
		/// 
		/// </summary>
		protected CommandLineSettings()
		{

		}
	}
}