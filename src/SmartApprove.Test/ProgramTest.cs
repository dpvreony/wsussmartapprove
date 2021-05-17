//////////////////////////////////////////////////////////////////////////
// Licensed under GNU General Public License version 2 (GPLv2)
//
// WSUS Smart Approve by DHGMS Solutions
// http://wsussmartapprove.codeplex.com
//////////////////////////////////////////////////////////////////////////

using System;
using Xunit;

namespace SmartApprove.Test
{


    /// <summary>
    ///This is a test class for ProgramTest and is intended
    ///to contain all ProgramTest Unit Tests
    ///</summary>
	public class ProgramTest
	{

		/// <summary>
		///A test for Main
		///</summary>
		[Fact]
		public void MainTestNoArgs()
		{
			string[] args = {};

            SmartApprove.Program.Main(args);
		}


		/// <summary>
		///A test for Main
		///</summary>
        [Fact]
		public void MainTestNoRunSet()
		{
			string[] args =
			{
				"/test",
				"/norunset"
			};
            SmartApprove.Program.Main(args);
        }

		/// <summary>
		///A test for Main
		///</summary>
        [Fact]
		public void MainTestNoRunSetAndRunSet()
		{
			string[] args =
			{
				"/test",
				"/norunset",
				"/runset",
				"runsetname"
			};

            Assert.Throws<ArgumentException>(() => SmartApprove.Program.Main(args));
        }

		/// <summary>
		///A test for Main
		///</summary>
        [Fact]
		public void MainTestRunSet()
		{
			string[] args =
			{
				"/test",
				"/runset",
				"normal"
			};

            SmartApprove.Program.Main(args);
        }

		/// <summary>
		///A test for Main
		///</summary>
        [Fact]
		public void MainTest_AllComputerGroups_AllClassifications()
		{
			string[] args =
			{
				"/test",
				"/runset",
				"AllComputerGroups_AllClassifications"
			};

            SmartApprove.Program.Main(args);
        }

		/// <summary>
		///A test for Main
		///</summary>
        [Fact]
		public void MainTestHelp()
		{
			string[] args =
			{
				"/?"
			};

            SmartApprove.Program.Main(args);
        }

		/// <summary>
		///this test checks to make sure specifying a runset with both target group settings fails
		///</summary>
        [Fact]
		public void MainTestBothTargetGroupSettings()
		{
			string[] args =
			{
				"/test",
				"/runset",
				"BothTargetGroups"
			};

            Assert.Throws<ArgumentException>(() => SmartApprove.Program.Main(args));
		}

		/// <summary>
		/// This test should fail as both classifications are provided
		/// </summary>
        [Fact]
		public void MainTest_AllComputers_BothClassSettings()
		{
			string[] args =
			{
				"/test",
				"/runset",
				"AllComputerGroups_BothClassSetting"
			};

            Assert.Throws<ArgumentException>(() => SmartApprove.Program.Main(args));
		}

		/// <summary>
		/// This test should fail as both classifications are provided
		/// </summary>
        [Fact]
		public void MainTest_SpecificGroup_BothClassifications()
		{
			string[] args =
			{
				"/test",
				"/runset",
				"SpecificGroup_BothClassifications"
			};

            Assert.Throws<ArgumentException>(() => SmartApprove.Program.Main(args));
		}

		/// <summary>
		/// This test should fail as both classifications are provided
		/// </summary>
        [Fact]
		public void MainTest_IncorrectGroup()
		{
			string[] args =
			{
				"/test",
				"/runset",
				"IncorrectGroup"
			};

            Assert.Throws<ArgumentException>(() => SmartApprove.Program.Main(args));
		}

		/// <summary>
		/// This test should fail as both classifications are provided
		/// </summary>
        [Fact]
		public void MainTest_SpecificGroup_IncorrectClassification()
		{
			string[] args =
			{
				"/test",
				"/runset",
				"SpecificGroup_IncorrectClassification"
			};

            Assert.Throws<ArgumentException>(() => SmartApprove.Program.Main(args));
		}

		/// <summary>
		///A test for a specific category and product
		///</summary>
        [Fact]
		public void MainTest_AllComputerGroups_SpecificCategory_SpecificProduct()
		{
			string[] args =
			{
				"/test",
				"/runset",
				"AllComputerGroups_SpecificCategory_SpecificProduct"
			};

            SmartApprove.Program.Main(args);
        }

		/// <summary>
		/// This test should fail as the product id is invalid
		/// </summary>
        [Fact]
		public void MainTest_SpecificGroup_IncorrectProduct()
		{
			string[] args =
			{
				"/test",
				"/runset",
				"AllComputerGroups_SpecificCategory_InvalidProduct"
			};

            Assert.Throws<ArgumentException>(() => SmartApprove.Program.Main(args));
		}
    }
}
