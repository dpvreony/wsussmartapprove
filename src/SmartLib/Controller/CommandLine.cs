//////////////////////////////////////////////////////////////////////////
// Licensed under GNU General Public License version 2 (GPLv2)
//
// WSUS Smart Approve by DHGMS Solutions
// http://wsussmartapprove.codeplex.com
//////////////////////////////////////////////////////////////////////////

namespace SmartLib.Controller
{
    using System.Collections.Generic;

    public class CommandLine<T> where T : Model.CommandLineSettings
    {
        #region fields

        protected T CommandLineSettings
        {
            get;
            set;
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandLine{T}"/> class.
        /// </summary>
        protected CommandLine()
        {
        }

        /// <summary>
        /// Checks if a switch has been specified on the command line
        /// </summary>
        /// <param name="argCollection">command line arguments</param>
        /// <param name="parameter">The switch to check for</param>
        /// <returns>whether the switch has been specified</returns>
        protected static bool GetIsSingleArgumentSpecified(
            List<string> argCollection,
            string parameter)
        {
            var endPosition = argCollection.Count;
            var positionCounter = 0;
            while (positionCounter < endPosition)
            {
                if (argCollection[positionCounter].Equals(parameter, System.StringComparison.OrdinalIgnoreCase))
                {
                    argCollection.RemoveAt(positionCounter);
                    return true;
                }

                positionCounter++;
            }

            return false;
        }

        /// <summary>
        /// Checks if /? has been specified on the command line
        /// </summary>
        /// <returns>whether /? has been specified</returns>
        public bool GetWantsHelp()
        {
            return this.CommandLineSettings.WantsHelp;
        }
    }
}