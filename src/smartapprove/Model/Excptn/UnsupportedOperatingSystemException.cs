//////////////////////////////////////////////////////////////////////////
// Licensed under GNU General Public License version 2 (GPLv2)
//
// WSUS Smart Approve by DHGMS Solutions
// http://wsussmartapprove.codeplex.com
//////////////////////////////////////////////////////////////////////////

namespace SmartApprove.Model.Excptn
{
    using System;

    /// <summary>
    /// Exception when the operating system isn't supported
    /// </summary>
    public class UnsupportedOperatingSystemException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnsupportedOperatingSystemException"/> class.
        /// </summary>
        /// <param name="message">
        /// The error message.
        /// </param>
        public UnsupportedOperatingSystemException(string message)
            : base(message)
        {
        }
    }
}
