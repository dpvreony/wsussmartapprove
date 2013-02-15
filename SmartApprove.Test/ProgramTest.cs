//////////////////////////////////////////////////////////////////////////
// Licensed under GNU General Public License version 2 (GPLv2)
//
// WSUS Smart Approve by DHGMS Solutions
// http://wsussmartapprove.codeplex.com
//////////////////////////////////////////////////////////////////////////

using SmartApprove;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.UpdateServices.Administration;
using System;
using System.Collections.Generic;
using SmartApprove.Model;
using SmartApprove.Controller;

namespace SmartApprove.Test
{
    
    
    /// <summary>
    ///This is a test class for ProgramTest and is intended
    ///to contain all ProgramTest Unit Tests
    ///</summary>
	[TestClass()]
	public class ProgramTest
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
		///A test for Main
		///</summary>
		[TestMethod()]
		[DeploymentItem("smartapprove.exe")]
		public void MainTestNoArgs()
		{
			string[] args = {};
			Program_Accessor.Main(args);
		}


		/// <summary>
		///A test for Main
		///</summary>
		[TestMethod()]
		[DeploymentItem("smartapprove.exe")]
		public void MainTestNoRunSet()
		{
			string[] args =
			{
				"/test",
				"/norunset"
			};
			Program_Accessor.Main(args);
		}

		/// <summary>
		///A test for Main
		///</summary>
		[TestMethod()]
		[DeploymentItem("smartapprove.exe")]
		public void MainTestNoRunSetAndRunSet()
		{
			string[] args =
			{
				"/test",
				"/norunset",
				"/runset",
				"runsetname"
			};

			try
			{
				Program_Accessor.Main(args);
			}
			catch (System.ArgumentException)
			{
				return;
			}
			Assert.Fail("should have thrown an exception");
		}

		/// <summary>
		///A test for Main
		///</summary>
		[TestMethod()]
		[DeploymentItem("smartapprove.exe")]
		public void MainTestRunSet()
		{
			string[] args =
			{
				"/test",
				"/runset",
				"normal"
			};

			Program_Accessor.Main(args);
		}

		/// <summary>
		///A test for Main
		///</summary>
		[TestMethod()]
		[DeploymentItem("smartapprove.exe")]
		public void MainTest_AllComputerGroups_AllClassifications()
		{
			string[] args =
			{
				"/test",
				"/runset",
				"AllComputerGroups_AllClassifications"
			};

			Program_Accessor.Main(args);
		}

		/// <summary>
		///A test for Main
		///</summary>
		[TestMethod()]
		[DeploymentItem("smartapprove.exe")]
		public void MainTestHelp()
		{
			string[] args =
			{
				"/?"
			};
			Program_Accessor.Main(args);
		}

		/// <summary>
		///this test checks to make sure specifying a runset with both target group settings fails
		///</summary>
		[TestMethod()]
		[DeploymentItem("smartapprove.exe")]
		public void MainTestBothTargetGroupSettings()
		{
			string[] args =
			{
				"/test",
				"/runset",
				"BothTargetGroups"
			};

			try
			{
				Program_Accessor.Main(args);
			}
			catch (System.Exception)
			{
				return;
			}
			Assert.Fail("should have thrown an exception");
		}

		/// <summary>
		/// This test should fail as both classifications are provided
		/// </summary>
		[TestMethod()]
		[DeploymentItem("smartapprove.exe")]
		public void MainTest_AllComputers_BothClassSettings()
		{
			string[] args =
			{
				"/test",
				"/runset",
				"AllComputerGroups_BothClassSetting"
			};

			try
			{
				Program_Accessor.Main(args);
			}
			catch (System.Exception)
			{
				return;
			}
			Assert.Fail("should have thrown an exception");
		}

		/// <summary>
		/// This test should fail as both classifications are provided
		/// </summary>
		[TestMethod()]
		[DeploymentItem("smartapprove.exe")]
		public void MainTest_SpecificGroup_BothClassifications()
		{
			string[] args =
			{
				"/test",
				"/runset",
				"SpecificGroup_BothClassifications"
			};

			try
			{
				Program_Accessor.Main(args);
			}
			catch (System.Exception)
			{
				return;
			}
			Assert.Fail("should have thrown an exception");
		}

		/// <summary>
		/// This test should fail as both classifications are provided
		/// </summary>
		[TestMethod()]
		[DeploymentItem("smartapprove.exe")]
		public void MainTest_IncorrectGroup()
		{
			string[] args =
			{
				"/test",
				"/runset",
				"IncorrectGroup"
			};

			try
			{
				Program_Accessor.Main(args);
			}
			catch (System.Exception)
			{
				return;
			}
			Assert.Fail("should have thrown an exception");
		}

		/// <summary>
		/// This test should fail as both classifications are provided
		/// </summary>
		[TestMethod()]
		[DeploymentItem("smartapprove.exe")]
		public void MainTest_SpecificGroup_IncorrectClassification()
		{
			string[] args =
			{
				"/test",
				"/runset",
				"SpecificGroup_IncorrectClassification"
			};

			try
			{
				Program_Accessor.Main(args);
			}
			catch (System.Exception)
			{
				return;
			}
			Assert.Fail("should have thrown an exception");
		}

		/// <summary>
		///A test for a specific category and product
		///</summary>
		[TestMethod()]
		[DeploymentItem("smartapprove.exe")]
		public void MainTest_AllComputerGroups_SpecificCategory_SpecificProduct()
		{
			string[] args =
			{
				"/test",
				"/runset",
				"AllComputerGroups_SpecificCategory_SpecificProduct"
			};

			Program_Accessor.Main(args);
		}

		/// <summary>
		/// This test should fail as the product id is invalid
		/// </summary>
		[TestMethod()]
		[DeploymentItem("smartapprove.exe")]
		public void MainTest_SpecificGroup_IncorrectProduct()
		{
			string[] args =
			{
				"/test",
				"/runset",
				"AllComputerGroups_SpecificCategory_InvalidProduct"
			};

			try
			{
				Program_Accessor.Main(args);
			}
			catch (System.Exception)
			{
				return;
			}
			Assert.Fail("should have thrown an exception");
		}

	}
}
