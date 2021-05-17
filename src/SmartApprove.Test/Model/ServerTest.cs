//////////////////////////////////////////////////////////////////////////
// Licensed under GNU General Public License version 2 (GPLv2)
//
// WSUS Smart Approve by DHGMS Solutions
// http://wsussmartapprove.codeplex.com
//////////////////////////////////////////////////////////////////////////

using Xunit;

namespace SmartApprove.Test
{
    /// <summary>
    ///This is a test class for ServerTest and is intended
    ///to contain all ServerTest Unit Tests
    ///</summary>
	public class ServerTest
	{
		/// <summary>
		///A test for Server Constructor
		///</summary>
		[Fact]
		public void ServerConstructorTest()
		{
			SmartLib.Model.Server target = new SmartLib.Model.Server();
		}

		/// <summary>
		///A test for Hostname
		///</summary>
		[Fact]
		public void HostnameValidTest()
		{
			SmartLib.Model.Server target = new SmartLib.Model.Server();
			string expected = "localhost";
			target.Hostname = expected;
			string actual = target.Hostname;
			Assert.Equal(expected, actual);
		}

		/// <summary>
		///A test for Port
		///</summary>
        [Fact]
		public void PortTest()
		{
			SmartLib.Model.Server target = new SmartLib.Model.Server();
			System.UInt16 expected = 8080;
			target.Port = expected;
			System.UInt16 actual = target.Port;
			Assert.Equal(expected, actual);
		}

		/// <summary>
		///A test for Secure
		///</summary>
        [Fact]
		public void SecureTest()
		{
			SmartLib.Model.Server target = new SmartLib.Model.Server();
			target.Secure = true;
			Assert.Equal(true, target.Secure);

			target.Secure = false;
			Assert.Equal(false, target.Secure);
		}
	}
}
