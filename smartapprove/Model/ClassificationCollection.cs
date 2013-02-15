//////////////////////////////////////////////////////////////////////////
// Licensed under GNU General Public License version 2 (GPLv2)
//
// WSUS Smart Approve by DHGMS Solutions
// http://wsussmartapprove.codeplex.com
//////////////////////////////////////////////////////////////////////////

namespace SmartApprove.Model
{
	/// <summary>
	/// Represents a collection of classifications in the app config
	/// based upon http://blog.newslacker.net/2008/02/net-20-custom-configuration-sections.html
	/// </summary>
	class ClassificationCollection
		: Generic.ConfigElementCollection<Classification>
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
			return ((Classification)element).Guid;
		}
	}
}