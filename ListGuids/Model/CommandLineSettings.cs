//////////////////////////////////////////////////////////////////////////
// Licensed under GNU General Public License version 2 (GPLv2)
//
// WSUS Smart Approve by DPVreony
// http://wsussmartapprove.codeplex.com
//////////////////////////////////////////////////////////////////////////

namespace ListGuids.Model
{
	sealed class CommandLineSettings
		: SmartLib.Model.CommandLineSettings
	{
		#region fields

		/// <summary>
		/// 
		/// </summary>
		public System.String Hostname
		{
			get;
			private set;
		}

		/// <summary>
		/// 
		/// </summary>
		public System.UInt16 Port
		{
			get;
			private set;
		}


		/// <summary>
		/// 
		/// </summary>
		public bool Secure
		{
			get;
			private set;
		}

		#endregion

		#region methods

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="wantsHelp">Whether the user is requesting help</param>
		/// <param name="hostname">Hostname of the WSUS server</param>
		/// <param name="port">The port WSUS is running on</param>
		/// <param name="secure">Whether to use HTTPS</param>
		public CommandLineSettings(
			bool wantsHelp,
			System.String hostname,
			System.UInt16 port,
			bool secure
			)
		{
			WantsHelp = wantsHelp;
			Hostname = hostname;
			Port = port;
			Secure = secure;
		}

		#endregion
	}
}