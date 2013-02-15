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
    ///This is a test class for RuleTest and is intended
    ///to contain all RuleTest Unit Tests
    ///</summary>
	[TestClass()]
	public class RuleTest
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
		///A test for Rule Constructor
		///</summary>
		[TestMethod()]
		public void RuleConstructorTest()
		{
			Rule target = new Rule();
		}

		/// <summary>
		///A test for AcceptLicenseAgreement
		///</summary>
		[TestMethod()]
		public void AcceptLicenseAgreementTest()
		{
			Rule target = new Rule();
			Assert.AreEqual(target.AcceptLicenseAgreement, false);
			Assert.AreEqual(target.ApproveNeededUpdates, false);
			Assert.AreEqual(target.ApproveStaleUpdates, false);
			Assert.AreEqual(target.ApproveSupersededUpdates, false);
			target.AcceptLicenseAgreement = true;
			Assert.AreEqual(target.AcceptLicenseAgreement, true);
			Assert.AreEqual(target.ApproveNeededUpdates, false);
			Assert.AreEqual(target.ApproveStaleUpdates, false);
			Assert.AreEqual(target.ApproveSupersededUpdates, false);
		}

		/// <summary>
		///A test for ApproveNeededUpdates
		///</summary>
		[TestMethod()]
		public void ApproveNeededUpdatesTest()
		{
			Rule target = new Rule();
			Assert.AreEqual(target.AcceptLicenseAgreement, false);
			Assert.AreEqual(target.ApproveNeededUpdates, false);
			Assert.AreEqual(target.ApproveStaleUpdates, false);
			Assert.AreEqual(target.ApproveSupersededUpdates, false);
			target.ApproveNeededUpdates = true;
			Assert.AreEqual(target.AcceptLicenseAgreement, false);
			Assert.AreEqual(target.ApproveNeededUpdates, true);
			Assert.AreEqual(target.ApproveStaleUpdates, false);
			Assert.AreEqual(target.ApproveSupersededUpdates, false);
		}

		/// <summary>
		///A test for ApproveStaleUpdates
		///</summary>
		[TestMethod()]
		public void ApproveStaleUpdatesTest()
		{
			Rule target = new Rule();
			Assert.AreEqual(target.AcceptLicenseAgreement, false);
			Assert.AreEqual(target.ApproveNeededUpdates, false);
			Assert.AreEqual(target.ApproveStaleUpdates, false);
			Assert.AreEqual(target.ApproveSupersededUpdates, false);
			target.ApproveStaleUpdates = true;
			Assert.AreEqual(target.AcceptLicenseAgreement, false);
			Assert.AreEqual(target.ApproveNeededUpdates, false);
			Assert.AreEqual(target.ApproveStaleUpdates, true);
			Assert.AreEqual(target.ApproveSupersededUpdates, false);
		}

		/// <summary>
		///A test for ApproveSupersededUpdates
		///</summary>
		[TestMethod()]
		public void ApproveSupersededUpdatesTest()
		{
			Rule target = new Rule();
			Assert.AreEqual(target.AcceptLicenseAgreement, false);
			Assert.AreEqual(target.ApproveNeededUpdates, false);
			Assert.AreEqual(target.ApproveStaleUpdates, false);
			Assert.AreEqual(target.ApproveSupersededUpdates, false);
			target.ApproveSupersededUpdates = true;
			Assert.AreEqual(target.AcceptLicenseAgreement, false);
			Assert.AreEqual(target.ApproveNeededUpdates, false);
			Assert.AreEqual(target.ApproveStaleUpdates, false);
			Assert.AreEqual(target.ApproveSupersededUpdates, true);
		}
	}
}
