//////////////////////////////////////////////////////////////////////////
// Licensed under GNU General Public License version 2 (GPLv2)
//
// WSUS Smart Approve by DHGMS Solutions
// http://wsussmartapprove.codeplex.com
//////////////////////////////////////////////////////////////////////////

using SmartApprove.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;

namespace SmartApprove.Test
{
    
    
    /// <summary>
    ///This is a test class for TargetGroupCollectionTest and is intended
    ///to contain all TargetGroupCollectionTest Unit Tests
    ///</summary>
	[TestClass()]
	public class TargetGroupCollectionTest
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
		///A test for TargetGroupCollection Constructor
		///</summary>
		[TestMethod()]
		public void TargetGroupCollectionConstructorTest()
		{
			TargetGroupCollection target = new TargetGroupCollection();
		}

		/// <summary>
		///A test for CreateNewElement
		///</summary>
		[TestMethod()]
		[DeploymentItem("smartapprove.exe")]
		public void CreateNewElementTest()
		{
			TargetGroupCollection target = new TargetGroupCollection(); // TODO: Initialize to an appropriate value
			ConfigurationElement actual = target.CreateNewElement();
			Assert.AreNotEqual(null, actual);
		}

		/// <summary>
		///A test for GetElementKey
		///</summary>
		[TestMethod()]
		[DeploymentItem("smartapprove.exe")]
		public void GetElementKeyTest()
		{
			TargetGroupCollection target = new TargetGroupCollection();
			ConfigurationElement element = target.CreateNewElement();
			object actual = target.GetElementKey(element);
			Assert.AreNotEqual(null, actual);
		}

		/// <summary>
		///A test for Item
		///</summary>
		[TestMethod()]
		public void ItemTest()
		{
			TargetGroupCollection target = new TargetGroupCollection(); // TODO: Initialize to an appropriate value

			int index = 0;
			TargetGroup expected = new TargetGroup();
			target.Add(expected);
			TargetGroup actual = target[index];
			Assert.AreEqual(expected, actual);
		}
	}
}
