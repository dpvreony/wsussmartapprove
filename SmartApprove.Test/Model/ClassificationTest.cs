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
    ///This is a test class for ClassificationTest and is intended
    ///to contain all ClassificationTest Unit Tests
    ///</summary>
	[TestClass()]
	public class ClassificationTest
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
		///A test for Guid
		///</summary>
		[TestMethod()]
		public void GuidTest()
		{
			Classification target = new Classification(); // TODO: Initialize to an appropriate value
			Guid expected = Guid.NewGuid(); 
			target.Guid = expected;
			Guid actual = target.Guid;
			Assert.AreEqual(expected, actual);
		}

		/// <summary>
		///A test for Classification Constructor
		///</summary>
		[TestMethod()]
		public void ClassificationConstructorTest()
		{
			Classification target = new Classification();
		}
	}
}
