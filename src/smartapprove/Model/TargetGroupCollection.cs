//////////////////////////////////////////////////////////////////////////
// Licensed under GNU General Public License version 2 (GPLv2)
//
// WSUS Smart Approve by DHGMS Solutions
// http://wsussmartapprove.codeplex.com
//////////////////////////////////////////////////////////////////////////

namespace SmartApprove.Model
{
	/// <summary>
	/// Represents a collection of target groups in the app config
	/// based upon http://blog.newslacker.net/2008/02/net-20-custom-configuration-sections.html
	/// </summary>
	class TargetGroupCollection
		: Generic.ConfigElementCollection<TargetGroup>
	{
		/// <summary>
		/// Gets the element key
		/// </summary>
		/// <param name="element">element</param>
		/// <returns>key</returns>
		protected override object GetElementKey(
			System.Configuration.ConfigurationElement element
			)
		{
			return ((TargetGroup)element).Guid;
		}
	}
}