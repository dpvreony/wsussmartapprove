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
    ///This is a test class for ApplicationSettingsTest and is intended
    ///to contain all ApplicationSettingsTest Unit Tests
    ///</summary>
	public class ApplicationSettingsTest
	{
        /// <summary>
		///A test for DefaultRule
		///</summary>
		[Fact]
		public void NoRunSetTest()
		{
			ApplicationSettings target = new ApplicationSettings();
			Rule expected = new Rule();
			target.NoRunSet = expected;
			Rule actual = target.NoRunSet;
			Assert.Equal(expected, actual);
		}

		/// <summary>
		///A test for RunSets
		///</summary>
		[Fact]
		public void RunSetsTest()
		{
			ApplicationSettings target = new ApplicationSettings();
			RunSetCollection expected = new RunSetCollection();
			target.RunSets = expected;
			RunSetCollection actual = target.RunSets;
			Assert.Equal(expected, actual);
		}

		/// <summary>
		///A test for Server
		///</summary>
		[Fact]
		public void ServerTest()
		{
			ApplicationSettings target = new ApplicationSettings();
			SmartLib.Model.Server expected = new SmartLib.Model.Server();
			target.Server = expected;
			SmartLib.Model.Server actual = target.Server;
			Assert.Equal(expected, actual);
		}
	}
}
