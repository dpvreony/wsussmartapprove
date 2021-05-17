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
    ///This is a test class for TargetGroupTest and is intended
    ///to contain all TargetGroupTest Unit Tests
    ///</summary>
	public class TargetGroupTest
	{
        /// <summary>
		///A test for TargetGroup Constructor
		///</summary>
		[Fact()]
		public void TargetGroupConstructorTest()
		{
			TargetGroup target = new TargetGroup();
		}

		/// <summary>
		///A test for Classifications
		///</summary>
		[Fact()]
		public void ClassificationsTest()
		{
			TargetGroup target = new TargetGroup();
			ClassificationCollection expected = new ClassificationCollection();
			target.Classifications = expected;
			ClassificationCollection actual = target.Classifications;
			Assert.Equal(expected, actual);
		}

		/// <summary>
		///A test for Guid
		///</summary>
		[Fact()]
		public void GuidTest()
		{
			TargetGroup target = new TargetGroup(); // TODO: Initialize to an appropriate value
			Guid expected = Guid.NewGuid();
			target.Guid = expected;
			Guid actual = target.Guid;
			Assert.Equal(expected, actual);
		}
	}
}
