//////////////////////////////////////////////////////////////////////////
// Licensed under GNU General Public License version 2 (GPLv2)
//
// WSUS Smart Approve by DHGMS Solutions
// http://wsussmartapprove.codeplex.com
//////////////////////////////////////////////////////////////////////////

namespace ListGuids.Controller
{
	/// <summary>
	/// 
	/// </summary>
	sealed class CommandLine
		: SmartLib.Controller.CommandLine<Model.CommandLineSettings>
	{
		#region our methods

		/// <summary>
		/// Constructor.
		/// </summary>
		public CommandLine(
			System.Collections.Generic.IList<string> args
			)
		{
			if (args == null)
			{
				throw new System.ArgumentNullException("args");
			}

			if (args.Count == 0)
			{
				throw new System.ArgumentException("Command line must have arguments", "args");
			}

			System.String hostname = null;
			System.UInt16 port = 0;
			bool secure = false;

			System.Collections.Generic.List<System.String> argCollection
				= new System.Collections.Generic.List<System.String>(args);

			//check if /? specified
			bool wantsHelp = GetIsSingleArgumentSpecified(
				argCollection,
				"/?"
				);

			if (!wantsHelp)
			{
				//no help switch
				//there should be 1 or 3 arguments
				if (args.Count != 1
					&& args.Count != 3)
				{
					throw new System.ArgumentException("Wrong number of arguments on the command line. Got: " + args.Count + ". Expected: 1 or 3.");
				}

				//hostname
				hostname = args[0];

				if (args.Count == 3)
				{
					//port
					port = System.UInt16.Parse(args[1]);

					//secure
					switch (args[2])
					{
						case "nossl":
							break;
						case "ssl":
							secure = true;
							break;
						default:
							throw new System.ArgumentException("Parameter 3 is invalid. Got: " + args[2] + ". Expected 'nossl' or 'ssl'.");
					}
				}
				else
				{
					port = 8530;
				}

			}



			CommandLineSettings = new Model.CommandLineSettings(
				wantsHelp,
				hostname,
				port,
				secure
				);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public System.String GetHostname()
		{
			return CommandLineSettings.Hostname;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public System.UInt16 GetPort()
		{
			return CommandLineSettings.Port;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public bool GetSecure()
		{
			return CommandLineSettings.Secure;
		}



		#endregion

	}
}