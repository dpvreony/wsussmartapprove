//////////////////////////////////////////////////////////////////////////
// Licensed under GNU General Public License version 2 (GPLv2)
//
// WSUS Smart Approve by DHGMS Solutions
// http://wsussmartapprove.codeplex.com
//////////////////////////////////////////////////////////////////////////

using SmartApprove.Controller;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace SmartApprove.Test
{
    
    
    /// <summary>
    ///This is a test class for CommandLineTest and is intended
    ///to contain all CommandLineTest Unit Tests
    ///</summary>
	[TestClass()]
	public class CommandLineTest
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
		///A test for GetWantsHelp
		///</summary>
		[TestMethod()]
		public void GetWantsHelpTestFalse()
		{
			string[] args=
			{
				"/norunset"
			};
			CommandLine commandLine = new CommandLine(args);
			Assert.AreEqual(false, commandLine.GetWantsHelp());
		}

		/// <summary>
		///A test for GetWantsHelp
		///</summary>
		[TestMethod()]
		public void GetWantsHelpTestTrue()
		{
			string[] args =
			{
				"/?"
			};
			CommandLine commandLine = new CommandLine(args);
			Assert.AreEqual(true, commandLine.GetWantsHelp());
		}

		/// <summary>
		///A test for GetRequestedRunSetName
		///</summary>
		[TestMethod()]
		public void GetRequestedRunSetNameTestWrongNumberOfArgs()
		{
			string[] args =
			{
				"/runset"
			};

			try
			{
				CommandLine commandLine = new CommandLine(args);
			}
			catch (System.ArgumentException)
			{
				return;
			}
			catch (System.Exception ex)
			{
				Assert.Fail("Expected System.ArgumentException - Got: " + ex.ToString());
			}
			Assert.Fail("Expected System.ArgumentException - Got: no exception");
		}

		/// <summary>
		///A test for GetRequestedRunSetName
		///</summary>
		[TestMethod()]
		public void GetRequestedRunSetNameTestWrongNumberOfArgsSecondary()
		{
			string[] args =
			{
				"/test",
				"/runset"
			};

			try
			{
				CommandLine commandLine = new CommandLine(args);
			}
			catch (System.ArgumentException)
			{
				return;
			}
			catch (System.Exception ex)
			{
				Assert.Fail("Expected System.ArgumentException - Got: " + ex.ToString());
			}
			Assert.Fail("Expected System.ArgumentException - Got: no exception");
		}

		/// <summary>
		///A test for GetRequestedRunSetName
		///</summary>
		[TestMethod()]
		public void GetRequestedRunSetNameTestWrongNumberOfArgsTertiary()
		{
			string[] args =
			{
				"/runset",
				"/test"
			};

			try
			{
				CommandLine commandLine = new CommandLine(args);
			}
			catch (System.ArgumentException)
			{
				return;
			}
			catch (System.Exception ex)
			{
				Assert.Fail("Expected System.ArgumentException - Got: " + ex.ToString());
			}
			Assert.Fail("Expected System.ArgumentException - Got: no exception");
		}

		/// <summary>
		///A test for GetRequestedRunSetName
		///</summary>
		[TestMethod()]
		public void GetRequestedRunSetNameTestSucceed()
		{
			string[] args =
			{
				"/runset",
				"runsetname"
			};
			CommandLine commandLine = new CommandLine(args);

			Assert.AreEqual("runsetname", commandLine.GetRunSetName());
		}

		/// <summary>
		///A test for GetIsTest
		///</summary>
		[TestMethod()]
		public void GetIsTestTestFalse()
		{
			string[] args =
			{
				"/?"
			};
			CommandLine commandLine = new CommandLine(args);
			Assert.AreEqual(false, commandLine.GetIsTest());
		}

		/// <summary>
		///A test for GetIsTest
		///</summary>
		[TestMethod()]
		public void GetIsTestTestTrue()
		{
			string[] args =
			{
				"/test"
			};
			CommandLine commandLine = new CommandLine(args);
			Assert.AreEqual(true, commandLine.GetIsTest());
		}
	}
}
