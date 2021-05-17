//////////////////////////////////////////////////////////////////////////
// Licensed under GNU General Public License version 2 (GPLv2)
//
// WSUS Smart Approve by DHGMS Solutions
// http://wsussmartapprove.codeplex.com
//////////////////////////////////////////////////////////////////////////

using SmartApprove.Model;
using Xunit;

namespace SmartApprove.Test
{
    /// <summary>
    ///This is a test class for RuleTest and is intended
    ///to contain all RuleTest Unit Tests
    ///</summary>
	public class RuleTest
	{
		/// <summary>
		///A test for Rule Constructor
		///</summary>
		[Fact]
		public void RuleConstructorTest()
		{
			Rule target = new Rule();
		}

		/// <summary>
		///A test for AcceptLicenseAgreement
		///</summary>
		[Fact()]
		public void AcceptLicenseAgreementTest()
		{
			Rule target = new Rule();
			Assert.False(target.AcceptLicenseAgreement);
			Assert.False(target.ApproveNeededUpdates);
			Assert.False(target.ApproveStaleUpdates);
			Assert.False(target.ApproveSupersededUpdates);
			target.AcceptLicenseAgreement = true;
			Assert.True(target.AcceptLicenseAgreement);
			Assert.False(target.ApproveNeededUpdates);
			Assert.False(target.ApproveStaleUpdates);
			Assert.False(target.ApproveSupersededUpdates);
		}

		/// <summary>
		///A test for ApproveNeededUpdates
		///</summary>
		[Fact()]
		public void ApproveNeededUpdatesTest()
		{
			Rule target = new Rule();
			Assert.False(target.AcceptLicenseAgreement);
			Assert.False(target.ApproveNeededUpdates);
			Assert.False(target.ApproveStaleUpdates);
			Assert.False(target.ApproveSupersededUpdates);
			target.ApproveNeededUpdates = true;
			Assert.False(target.AcceptLicenseAgreement);
			Assert.True(target.ApproveNeededUpdates);
			Assert.False(target.ApproveStaleUpdates);
			Assert.False(target.ApproveSupersededUpdates);
		}

		/// <summary>
		///A test for ApproveStaleUpdates
		///</summary>
		[Fact()]
		public void ApproveStaleUpdatesTest()
		{
			Rule target = new Rule();
			Assert.False(target.AcceptLicenseAgreement);
			Assert.False(target.ApproveNeededUpdates);
			Assert.False(target.ApproveStaleUpdates);
			Assert.False(target.ApproveSupersededUpdates);
			target.ApproveStaleUpdates = true;
			Assert.False(target.AcceptLicenseAgreement);
			Assert.False(target.ApproveNeededUpdates);
			Assert.True(target.ApproveStaleUpdates);
			Assert.False(target.ApproveSupersededUpdates);
		}

		/// <summary>
		///A test for ApproveSupersededUpdates
		///</summary>
		[Fact()]
		public void ApproveSupersededUpdatesTest()
		{
			Rule target = new Rule();
			Assert.False(target.AcceptLicenseAgreement);
			Assert.False(target.ApproveNeededUpdates);
			Assert.False(target.ApproveStaleUpdates);
			Assert.False(target.ApproveSupersededUpdates);
			target.ApproveSupersededUpdates = true;
			Assert.False(target.AcceptLicenseAgreement);
			Assert.False(target.ApproveNeededUpdates);
			Assert.False(target.ApproveStaleUpdates);
			Assert.True(target.ApproveSupersededUpdates);
		}
	}
}
