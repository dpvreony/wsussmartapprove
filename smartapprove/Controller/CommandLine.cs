//////////////////////////////////////////////////////////////////////////
// Licensed under GNU General Public License version 2 (GPLv2)
//
// WSUS Smart Approve by DHGMS Solutions
// http://wsussmartapprove.codeplex.com
//////////////////////////////////////////////////////////////////////////

namespace SmartApprove.Controller
{
	sealed class CommandLine
		: SmartLib.Controller.CommandLine<Model.CommandLineSettings>
	{
		#region our methods

		/// <summary>
		/// Constructor.
		/// </summary>
		public CommandLine(
			System.String[] args
			)
		{
			if (args == null)
			{
				throw new System.ArgumentNullException("args");
			}

			if (args.Length == 0)
			{
				CommandLineSettings = new Model.CommandLineSettings(
					true,
					false,
					SmartLib.Model.TriState.Undefined,
					null
					);
				return;
			}

			System.Collections.Generic.List<System.String> argCollection
				= new System.Collections.Generic.List<System.String>(args);

			//check if /? specified
			bool wantsHelp = GetIsSingleArgumentSpecified(
				argCollection,
				"/?"
				);

			bool isTest = false;
			SmartLib.Model.TriState useRunSet = SmartLib.Model.TriState.Undefined;
			System.String runSetName = null;

			if (!wantsHelp)
			{
				//check for /test
				isTest = GetIsSingleArgumentSpecified(
					argCollection,
					"/test"
					);

				//check for /norunset
				if (GetIsSingleArgumentSpecified(
					argCollection,
					"/norunset"
					))
				{
					useRunSet = SmartLib.Model.TriState.No;
				}

				//check for /runset
				System.String value = null;
				if (GetIsPairArgumentsSpecified(
					argCollection,
					"/runset",
					ref value
					))
				{
					if (useRunSet == SmartLib.Model.TriState.No)
					{
						//we've already had /norunset
						//which is mutually exclusive
						throw new System.ArgumentException(
							"/norunset and /runset specified. These are mutually exclusive arguments, use one or the other."
							);
					}
					useRunSet = SmartLib.Model.TriState.Yes;
					runSetName = value;
				}

				if (argCollection.Count > 0)
				{
					//1 or more arguments haven't been handled
					throw new System.ArgumentException(
						"Unhandled arguments."
						);
				}
			}

			CommandLineSettings = new Model.CommandLineSettings(
				wantsHelp,
				isTest,
				useRunSet,
				runSetName
				);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns>whether the switch has been specified</returns>
		private static bool GetIsPairArgumentsSpecified(
			System.Collections.Generic.List<System.String> argCollection,
			System.String parameter,
			ref System.String value
			)
		{
			int endPosition = argCollection.Count - 1;
			int positionCounter = 0;
			while (positionCounter < endPosition)
			{
				if (argCollection[positionCounter].Equals(parameter, System.StringComparison.OrdinalIgnoreCase))
				{
					//check to make sure there is an argument after
					if (positionCounter == endPosition)
					{
						throw new System.ArgumentException("No value for " + parameter + " specified.");
					}

					//check the argument after isn't a switch
					System.String runSet = argCollection[positionCounter + 1];
					if (runSet[0].Equals('/'))
					{
						throw new System.ArgumentException("No value for " + parameter + " specified.");
					}

					value = runSet;
					argCollection.RemoveRange(positionCounter, 2);
					return true;
				}

				positionCounter++;
			}

			return false;
		}

		/// <summary>
		/// Checks if /test has been specified on the command line
		/// </summary>
		/// <returns>whether /test has been specified</returns>
		public bool GetIsTest()
		{
			return CommandLineSettings.IsTest;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public System.String GetRunSetName()
		{
			return CommandLineSettings.RunSetName;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public SmartLib.Model.TriState GetUseRunSet()
		{
			return CommandLineSettings.UseRunSet;
		}

		#endregion
	}
}