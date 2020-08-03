//////////////////////////////////////////////////////////////////////////
// Licensed under GNU General Public License version 2 (GPLv2)
//
// WSUS Smart Approve by DHGMS Solutions
// http://wsussmartapprove.codeplex.com
//////////////////////////////////////////////////////////////////////////

using SmartApprove.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace SmartApprove.Test
{
    
    
    /// <summary>
    ///This is a test class for ServerTest and is intended
    ///to contain all ServerTest Unit Tests
    ///</summary>
	[TestClass()]
	public class ServerTest
	{


		private TestContext testContextInstance;

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}

		#region Additional test attributes
		// 
		//You can use the following additional attributes as you write your tests:
		//
		//Use ClassInitialize to run code before running the first test in the class
		//[ClassInitialize()]
		//public static void MyClassInitialize(TestContext testContext)
		//{
		//}
		//
		//Use ClassCleanup to run code after all tests in a class have run
		//[ClassCleanup()]
		//public static void MyClassCleanup()
		//{
		//}
		//
		//Use TestInitialize to run code before running each test
		//[TestInitialize()]
		//public void MyTestInitialize()
		//{
		//}
		//
		//Use TestCleanup to run code after each test has run
		//[TestCleanup()]
		//public void MyTestCleanup()
		//{
		//}
		//
		#endregion


		/// <summary>
		///A test for Server Constructor
		///</summary>
		[TestMethod()]
		public void ServerConstructorTest()
		{
			SmartLib.Model.Server target = new SmartLib.Model.Server();
		}

		/// <summary>
		///A test for Hostname
		///</summary>
		[TestMethod()]
		public void HostnameValidTest()
		{
			SmartLib.Model.Server target = new SmartLib.Model.Server();
			string expected = "localhost";
			target.Hostname = expected;
			string actual = target.Hostname;
			Assert.AreEqual(expected, actual);
		}

		/// <summary>
		///A test for Port
		///</summary>
		[TestMethod()]
		public void PortTest()
		{
			SmartLib.Model.Server target = new SmartLib.Model.Server();
			System.UInt16 expected = 8080;
			target.Port = expected;
			System.UInt16 actual = target.Port;
			Assert.AreEqual(expected, actual);
		}

		/// <summary>
		///A test for Secure
		///</summary>
		[TestMethod()]
		public void SecureTest()
		{
			SmartLib.Model.Server target = new SmartLib.Model.Server();
			target.Secure = true;
			Assert.AreEqual(true, target.Secure);

			target.Secure = false;
			Assert.AreEqual(false, target.Secure);
		}
	}
}
