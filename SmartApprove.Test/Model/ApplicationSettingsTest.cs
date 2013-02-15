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
    ///This is a test class for ApplicationSettingsTest and is intended
    ///to contain all ApplicationSettingsTest Unit Tests
    ///</summary>
	[TestClass()]
	public class ApplicationSettingsTest
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
		///A test for DefaultRule
		///</summary>
		[TestMethod()]
		public void NoRunSetTest()
		{
			ApplicationSettings target = new ApplicationSettings();
			Rule expected = new Rule();
			target.NoRunSet = expected;
			Rule actual = target.NoRunSet;
			Assert.AreEqual(expected, actual);
		}

		/// <summary>
		///A test for RunSets
		///</summary>
		[TestMethod()]
		public void RunSetsTest()
		{
			ApplicationSettings target = new ApplicationSettings();
			RunSetCollection expected = new RunSetCollection();
			target.RunSets = expected;
			RunSetCollection actual = target.RunSets;
			Assert.AreEqual(expected, actual);
		}

		/// <summary>
		///A test for Server
		///</summary>
		[TestMethod()]
		public void ServerTest()
		{
			ApplicationSettings target = new ApplicationSettings();
			SmartLib.Model.Server expected = new SmartLib.Model.Server();
			target.Server = expected;
			SmartLib.Model.Server actual = target.Server;
			Assert.AreEqual(expected, actual);
		}
	}
}
