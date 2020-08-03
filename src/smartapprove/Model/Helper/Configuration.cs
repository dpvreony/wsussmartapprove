//////////////////////////////////////////////////////////////////////////
// Licensed under GNU General Public License version 2 (GPLv2)
//
// WSUS Smart Approve by DHGMS Solutions
// http://wsussmartapprove.codeplex.com
//////////////////////////////////////////////////////////////////////////

namespace SmartApprove.Model.Helper
{
	class Configuration
	{
		/// <summary>
		/// Constructor. Hidden to prevent instances
		/// </summary>
		private Configuration()
		{

		}

		static public void CheckAreValidProducts(
			Microsoft.UpdateServices.Administration.IUpdateServer server,
			ProductCollection products,
			System.String runSetName,
			System.String groupId,
			System.String classificationId
			)
		{
			//make sure we have at least one product
			if (products.Count < 1)
			{
				System.String groupPath = (groupId != null)
					? "\\TargetGroups\\" + groupId :
					"\\AllTargetGroups";

				throw new System.Configuration.ConfigurationErrorsException(
					"The products section in \"" + runSetName + groupPath + "\\Classifications\\" + classificationId + "\\\" must have at least 1 item."
				);
			}

			foreach (Product product in products)
			{
				//make sure the product exists on wsus
				try
				{
					server.GetUpdateCategory(product.Guid);
				}
				catch (System.Exception)
				{
					throw new System.ArgumentException(
							"The Product Guid \"" + product.Guid + "\" could not be found on the WSUS server. Run Set: " + runSetName
							);
				}

				//todo: make sure the product doesn't appear in the list twice
				//todo: make sure the product parent hasn't already been added to the list
			}
		}

		/// <summary>
		/// 
		/// </summary>
		static public void CheckIsValidRunSet(
			Microsoft.UpdateServices.Administration.IUpdateServer server,
			RunSet runSet
			)
		{
			TargetGroupCollection targetGroups = runSet.TargetGroups;
			AllTargetGroups allTargetGroups = runSet.AllTargetGroups;

			if (
				targetGroups.Count == 0
				&& allTargetGroups.ElementInformation.IsPresent == false
				)
			{
				throw new System.ArgumentException(
						"\"AllTargetGroups\" or \"TargetGroups\" missing from the runset \"" + runSet.Name + "\"."
						);
			}
			
			if(
				targetGroups.Count > 0
				&& allTargetGroups.ElementInformation.IsPresent
				)
			{
				throw new System.ArgumentException(
						"\"AllTargetGroups\" and \"TargetGroups\" are both specified in runset \"" + runSet.Name + "\".  You must specify one or the other."
						);
			}
			
			if (targetGroups.Count > 0)
			{
				CheckIsValidTargetGroups(server, targetGroups, runSet.Name);
			}
			else
			{
				CheckIsValidAllTargetGroups(server, allTargetGroups, runSet.Name);
			}

		}

		/// <summary>
		/// 
		/// </summary>
		static private void CheckIsValidAllTargetGroups(
			Microsoft.UpdateServices.Administration.IUpdateServer server,
			AllTargetGroups allTargetGroups,
			System.String runSetName
			)
		{
			Rule allClassifications = allTargetGroups.AllClassifications;
			ClassificationCollection classifications = allTargetGroups.Classifications;
			CheckIsValidClassifications(server, allClassifications, classifications, runSetName, null);
		}

		/// <summary>
		/// 
		/// </summary>
		static private void CheckIsValidTargetGroup(
			Microsoft.UpdateServices.Administration.IUpdateServer server,
			TargetGroup targetGroup,
			System.String runSetName
			)
		{
			//make sure the guid exists
			try
			{
				server.GetComputerTargetGroup(targetGroup.Guid);
			}
			catch (System.Exception)
			{
				throw new System.ArgumentException(
						"The TargetGroup Guid \"" + targetGroup.Guid + "\" could not be found on the WSUS server. Run Set: " + runSetName
						);
			}

			//check the classifications
			Rule allClassifications = targetGroup.AllClassifications;
			ClassificationCollection classifications = targetGroup.Classifications;
			CheckIsValidClassifications(server, allClassifications, classifications, runSetName, targetGroup.Guid.ToString());
		}

		/// <summary>
		/// 
		/// </summary>
		static private void CheckIsValidTargetGroups(
			Microsoft.UpdateServices.Administration.IUpdateServer server,
			TargetGroupCollection targetGroups,
			System.String runSetName
			)
		{
			if (targetGroups.Count < 1)
			{
				throw new System.Configuration.ConfigurationErrorsException(
					"The \"TargetGroups\" section in runset \"" + runSetName + "\" must have at least 1 item."
				);

			}

			foreach (TargetGroup targetGroup in targetGroups)
			{
				CheckIsValidTargetGroup(server, targetGroup, runSetName);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		static private void CheckIsValidClassification(
			Microsoft.UpdateServices.Administration.IUpdateServer server,
			Classification classification,
			System.String runSetName,
			System.String groupId
			)
		{
			//make sure the guid exists
			try
			{
				server.GetUpdateClassification(classification.Guid);
			}
			catch (System.Exception)
			{
				throw new System.ArgumentException(
						"The Classification Guid \"" + classification.Guid + "\" could not be found on the WSUS server. Run Set: " + runSetName
						);
			}

			//check for a product category collection
			ProductCollection products = classification.Products;

			if (products.ElementInformation.IsPresent)
			{
				//we have a collection
				CheckAreValidProducts(server, products, runSetName, groupId, classification.Guid.ToString());
			}
			
		}

		/// <summary>
		/// 
		/// </summary>
		static private void CheckIsValidClassifications(
			Microsoft.UpdateServices.Administration.IUpdateServer server,
			ClassificationCollection classifications,
			System.String runSetName,
			System.String groupId
			)
		{
			if (classifications.Count < 1)
			{
				throw new System.Configuration.ConfigurationErrorsException(
					"The \"Classifications\" section in runset \"" + runSetName + "\" must have at least 1 item."
				);

			}

			foreach (Classification classification in classifications)
			{
				CheckIsValidClassification(server, classification, runSetName, groupId);
			}
		}

		static private void CheckIsValidClassifications(
			Microsoft.UpdateServices.Administration.IUpdateServer server,
// ReSharper disable UnusedParameter.Local
			Rule allClassifications,
// ReSharper restore UnusedParameter.Local
			ClassificationCollection classifications,
			System.String runSetName,
			System.String groupId
			)
		{
			if (
				classifications.Count == 0
				&& allClassifications.ElementInformation.IsPresent == false
				)
			{
				throw new System.ArgumentException(
						"\"AllClassifications\" or \"Classifications\" missing from the runset \"" + runSetName + "\\AllTargetGroups\" section."
						);
			}
			
			if (
				classifications.Count > 0
				&& allClassifications.ElementInformation.IsPresent
				)
			{
				throw new System.ArgumentException(
						"\"AllClassifications\" and \"Classifications\" are both specified in runset \"" + runSetName + "\".  You must specify one or the other."
						);
			}
			
			if (classifications.Count > 0)
			{
				CheckIsValidClassifications(server, classifications, runSetName, groupId);
			}
			//else
			//{
			//nothing to do
			//AllClassifications is set to require all settings
			//and will fail before this point
			//}
		}
	}
}