//////////////////////////////////////////////////////////////////////////
// Licensed under GNU General Public License version 2 (GPLv2)
//
// WSUS Smart Approve by DHGMS Solutions
// http://wsussmartapprove.codeplex.com
//////////////////////////////////////////////////////////////////////////

using System;
using SmartApprove.Controller;
using Xunit;

namespace SmartApprove.Test
{


    /// <summary>
    ///This is a test class for CommandLineTest and is intended
    ///to contain all CommandLineTest Unit Tests
    ///</summary>
    public class CommandLineTest
	{
        /// <summary>
		///A test for GetWantsHelp
		///</summary>
		[Fact]
		public void GetWantsHelpTestFalse()
		{
			string[] args=
			{
				"/norunset"
			};
			CommandLine commandLine = new CommandLine(args);
			Assert.False(commandLine.GetWantsHelp());
		}

		/// <summary>
		///A test for GetWantsHelp
		///</summary>
        [Fact]
		public void GetWantsHelpTestTrue()
		{
			string[] args =
			{
				"/?"
			};
			CommandLine commandLine = new CommandLine(args);
			Assert.True(commandLine.GetWantsHelp());
		}

		/// <summary>
		///A test for GetRequestedRunSetName
		///</summary>
        [Fact]
		public void GetRequestedRunSetNameTestWrongNumberOfArgs()
		{
			string[] args =
			{
				"/runset"
			};

            Assert.Throws<ArgumentException>(() => new CommandLine(args));
		}

		/// <summary>
		///A test for GetRequestedRunSetName
		///</summary>
        [Fact]
		public void GetRequestedRunSetNameTestWrongNumberOfArgsSecondary()
		{
			string[] args =
			{
				"/test",
				"/runset"
			};

            Assert.Throws<ArgumentException>(() => new CommandLine(args));
		}

		/// <summary>
		///A test for GetRequestedRunSetName
		///</summary>
        [Fact]
		public void GetRequestedRunSetNameTestWrongNumberOfArgsTertiary()
		{
			string[] args =
			{
				"/runset",
				"/test"
			};

            Assert.Throws<ArgumentException>(() => new CommandLine(args));
        }

		/// <summary>
		///A test for GetRequestedRunSetName
		///</summary>
        [Fact]
		public void GetRequestedRunSetNameTestSucceed()
		{
			string[] args =
			{
				"/runset",
				"runsetname"
			};
			CommandLine commandLine = new CommandLine(args);

			Assert.Equal("runsetname", commandLine.GetRunSetName());
		}

		/// <summary>
		///A test for GetIsTest
		///</summary>
        [Fact]
		public void GetIsTestTestFalse()
		{
			string[] args =
			{
				"/?"
			};
			CommandLine commandLine = new CommandLine(args);
			Assert.False(commandLine.GetIsTest());
		}

		/// <summary>
		///A test for GetIsTest
		///</summary>
        [Fact]
		public void GetIsTestTestTrue()
		{
			string[] args =
			{
				"/test"
			};
			CommandLine commandLine = new CommandLine(args);
			Assert.True(commandLine.GetIsTest());
		}
	}
}
