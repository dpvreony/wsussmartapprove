//////////////////////////////////////////////////////////////////////////
// Licensed under GNU General Public License version 2 (GPLv2)
//
// WSUS Smart Approve by DHGMS Solutions
// http://wsussmartapprove.codeplex.com
//////////////////////////////////////////////////////////////////////////

using SmartApprove.Model;
using System;
using Xunit;

namespace SmartApprove.Test
{
    /// <summary>
    ///This is a test class for ClassificationTest and is intended
    ///to contain all ClassificationTest Unit Tests
    ///</summary>
	public class ClassificationTest
	{
        /// <summary>
		///A test for Guid
		///</summary>
		[Fact()]
		public void GuidTest()
		{
			Classification target = new Classification(); // TODO: Initialize to an appropriate value
			Guid expected = Guid.NewGuid();
			target.Guid = expected;
			Guid actual = target.Guid;
			Assert.Equal(expected, actual);
		}

		/// <summary>
		///A test for Classification Constructor
		///</summary>
		[Fact()]
		public void ClassificationConstructorTest()
		{
			Classification target = new Classification();
		}
	}
}
