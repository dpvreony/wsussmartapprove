//////////////////////////////////////////////////////////////////////////
// Licensed under GNU General Public License version 2 (GPLv2)
//
// WSUS Smart Approve by DHGMS Solutions
// http://wsussmartapprove.codeplex.com
//////////////////////////////////////////////////////////////////////////

using SmartApprove.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SmartApprove.Test
{
    
    
    /// <summary>
    ///This is a test class for TargetGroupTest and is intended
    ///to contain all TargetGroupTest Unit Tests
    ///</summary>
	[TestClass()]
	public class TargetGroupTest
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
		///A test for TargetGroup Constructor
		///</summary>
		[TestMethod()]
		public void TargetGroupConstructorTest()
		{
			TargetGroup target = new TargetGroup();
		}

		/// <summary>
		///A test for Classifications
		///</summary>
		[TestMethod()]
		public void ClassificationsTest()
		{
			TargetGroup target = new TargetGroup();
			ClassificationCollection expected = new ClassificationCollection();
			target.Classifications = expected;
			ClassificationCollection actual = target.Classifications;
			Assert.AreEqual(expected, actual);
		}

		/// <summary>
		///A test for Guid
		///</summary>
		[TestMethod()]
		public void GuidTest()
		{
			TargetGroup target = new TargetGroup(); // TODO: Initialize to an appropriate value
			Guid expected = Guid.NewGuid();
			target.Guid = expected;
			Guid actual = target.Guid;
			Assert.AreEqual(expected, actual);
		}
	}
}
