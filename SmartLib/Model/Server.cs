//////////////////////////////////////////////////////////////////////////
// Licensed under GNU General Public License version 2 (GPLv2)
//
// WSUS Smart Approve by DHGMS Solutions
// http://wsussmartapprove.codeplex.com
//////////////////////////////////////////////////////////////////////////

using System.Globalization;

namespace SmartLib.Model
{
	/// <summary>
	/// Represents the connection settings in the config
	/// </summary>
	public class Server
		: System.Configuration.ConfigurationElement
	{
		#region fields
		/// <summary>
		/// The hostname to connect to
		/// </summary>
		[System.Configuration.ConfigurationProperty("Hostname", IsRequired = true)]
		public System.String Hostname
		{
			get
			{
				return (System.String)this["Hostname"];
			}
			set
			{
				this["Hostname"] = value;
			}
		}

		/// <summary>
		/// The port to connect to
		/// </summary>
		[System.Configuration.ConfigurationProperty("Port", IsRequired = true)]
		public System.UInt16 Port
		{
			get
			{
				return System.UInt16.Parse(this["Port"].ToString());
			}
			set
			{
				this["Port"] = value.ToString(CultureInfo.InvariantCulture);
			}
		}

		/// <summary>
		/// Whether to use SSL or not
		/// </summary>
		[System.Configuration.ConfigurationProperty("Secure", IsRequired = true)]
		public bool Secure
		{
			get
			{
				return System.Boolean.Parse(this["Secure"].ToString());
			}
			set
			{
				this["Secure"] = value.ToString(CultureInfo.InvariantCulture);
			}
		}

		#endregion
	}
}
