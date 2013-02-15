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
    ///This is a test class for RunSetTest and is intended
    ///to contain all RunSetTest Unit Tests
    ///</summary>
	[TestClass()]
	public class RunSetTest
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
		///A test for RunSet Constructor
		///</summary>
		[TestMethod()]
		public void RunSetConstructorTest()
		{
			//SmartApprove.Model.ApplicationSettings appSettings = SmartApprove.Program.LoadSettings();
			RunSet target = new RunSet();
		}

		/// <summary>
		///A test for TargetGroups
		///</summary>
		[TestMethod()]
		public void TargetGroupsTest()
		{
			RunSet target = new RunSet();
			TargetGroupCollection expected = new TargetGroupCollection();
			target.TargetGroups = expected;
			TargetGroupCollection actual = target.TargetGroups;
			Assert.AreEqual(expected, actual);
		}
	}
}
