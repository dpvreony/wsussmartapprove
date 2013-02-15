//////////////////////////////////////////////////////////////////////////
// Licensed under GNU General Public License version 2 (GPLv2)
//
// WSUS Smart Approve by DHGMS Solutions
// http://wsussmartapprove.codeplex.com
//////////////////////////////////////////////////////////////////////////

namespace SmartApprove.Model.Generic
{
	/// <summary>
	/// Represents a collection of target groups in the app config
	/// based upon http://blog.newslacker.net/2008/02/net-20-custom-configuration-sections.html
	/// </summary>
	abstract class ConfigElementCollection<TConfigElement>
		: System.Configuration.ConfigurationElementCollection
		where TConfigElement : System.Configuration.ConfigurationElement, new()
	{
		/// <summary>
		/// Returns a element from the collection
		/// </summary>
		/// <param name="index">0 based position in list</param>
		/// <returns></returns>
		public TConfigElement this[int index]
		{
			get
			{
				return (TConfigElement)BaseGet(index);
			}

			set
			{
				if (BaseGet(index) != null)
				{
					BaseRemoveAt(index);
				}

				BaseAdd(index, value);
			}
		}

		/// <summary>
		/// Creates a new Element
		/// </summary>
		/// <returns>ConfigElement</returns>
		protected override System.Configuration.ConfigurationElement CreateNewElement()
		{
			return new TConfigElement();
		}

		public void Add(
			System.Configuration.ConfigurationElement element
			)
		{
			BaseAdd(element);
		}

		/// <summary>
		/// Gets the element key
		/// </summary>
		/// <param name="element">element</param>
		/// <returns>key</returns>
		abstract protected override object GetElementKey(
			System.Configuration.ConfigurationElement element
			);
	}
}