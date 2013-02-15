//////////////////////////////////////////////////////////////////////////
// Licensed under GNU General Public License version 2 (GPLv2)
//
// WSUS Smart Approve by DHGMS Solutions
// http://wsussmartapprove.codeplex.com
//////////////////////////////////////////////////////////////////////////

namespace SmartLib.Controller
{
	public sealed class Server
	{
		/// <summary>
		/// Connects to local WSUS server
		/// </summary>
		/// <returns>
		/// Object representing the WSUS Server
		/// </returns>
		public static Microsoft.UpdateServices.Administration.IUpdateServer Connect(
			Model.Server connectionSettings
			)
		{
			if (connectionSettings != null)
			{
				System.String hostname = connectionSettings.Hostname;
				if (System.String.IsNullOrEmpty(hostname))
				{
					throw new System.ArgumentException("Server hostname must be specified if using a server element in the config file.");
				}

				int port = connectionSettings.Port;
				bool secure = connectionSettings.Secure;

				System.Console.Out.WriteLine("Connecting to server: " + hostname + ":" + port);
				return Microsoft.UpdateServices.Administration.AdminProxy.GetUpdateServer(hostname, secure, port);
			}

			System.Console.Out.WriteLine("Connecting to server: localhost");
			return Microsoft.UpdateServices.Administration.AdminProxy.GetUpdateServer();
		}

		/// <summary>
		/// Gets a list of product categories from WSUS
		/// </summary>
		/// <param name="server">
		/// Object representing the WSUS server
		/// </param>
		/// <returns>
		/// Object representing the list of product categories
		/// </returns>
		public static Microsoft.UpdateServices.Administration.UpdateCategoryCollection GetRootUpdateCategories(
			Microsoft.UpdateServices.Administration.IUpdateServer server
			)
		{
			return server.GetRootUpdateCategories();
		}

		/// <summary>
		/// Gets the list of update classifications from WSUS
		/// </summary>
		/// <param name="server">
		/// Object representing the WSUS server
		/// </param>
		/// <returns>
		/// Object representing the list of classifications
		/// </returns>
		public static Microsoft.UpdateServices.Administration.UpdateClassificationCollection GetUpdateClassifications(
			Microsoft.UpdateServices.Administration.IUpdateServer server
			)
		{
			return server.GetUpdateClassifications();
		}

		/// <summary>
		/// Gets the "All Computers" group
		/// </summary>
		/// <param name="server">
		/// Object representing the WSUS server
		/// </param>
		/// <returns></returns>
		public static Microsoft.UpdateServices.Administration.IComputerTargetGroup GetRootTargetGroup(
			Microsoft.UpdateServices.Administration.IUpdateServer server
			)
		{
			return server.GetComputerTargetGroup(
				Microsoft.UpdateServices.Administration.ComputerTargetGroupId.AllComputers
				);
		}

	}
}