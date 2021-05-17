//////////////////////////////////////////////////////////////////////////
// Licensed under GNU General Public License version 2 (GPLv2)
//
// WSUS Smart Approve by DHGMS Solutions
// http://wsussmartapprove.codeplex.com
//////////////////////////////////////////////////////////////////////////

using SmartApprove.Model;
using Xunit;

namespace SmartApprove.Test
{
    /// <summary>
    ///This is a test class for RunSetTest and is intended
    ///to contain all RunSetTest Unit Tests
    ///</summary>
	public class RunSetTest
	{


		/// <summary>
		///A test for RunSet Constructor
		///</summary>
		[Fact()]
		public void RunSetConstructorTest()
		{
			//SmartApprove.Model.ApplicationSettings appSettings = SmartApprove.Program.LoadSettings();
			RunSet target = new RunSet();
		}

		/// <summary>
		///A test for TargetGroups
		///</summary>
		[Fact()]
		public void TargetGroupsTest()
		{
			RunSet target = new RunSet();
			TargetGroupCollection expected = new TargetGroupCollection();
			target.TargetGroups = expected;
			TargetGroupCollection actual = target.TargetGroups;
			Assert.Equal(expected, actual);
		}
	}
}
