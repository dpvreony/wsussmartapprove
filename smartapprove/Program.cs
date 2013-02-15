// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="Program.cs">
//   
// </copyright>
// <summary>
//   The applications main class
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------

namespace SmartApprove
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;

    using Microsoft.UpdateServices.Administration;

    using SmartApprove.Controller;
    using SmartApprove.Model;
    using SmartApprove.Model.Helper;

    using SmartLib.Model;

    using Configuration = SmartApprove.Model.Helper.Configuration;
    using Server = SmartLib.Controller.Server;

    /// <summary>
    /// The applications main class
    /// </summary>
    internal class Program
    {
        #region Public Methods and Operators

        /// <summary>
        /// Approves and update and checks for child target groups
        /// </summary>
        /// <param name="update">
        /// An update
        /// </param>
        /// <param name="targetGroup">
        /// A target group
        /// </param>
        /// <param name="isTest">
        /// Whether we are in test mode
        /// </param>
        /// <param name="alreadyProcessed">
        /// List of target groups that have already been processed
        /// </param>
        public static void ApproveUpdateForTargetGroup(
            IUpdate update, IComputerTargetGroup targetGroup, bool isTest, List<IComputerTargetGroup> alreadyProcessed)
        {
            if (isTest)
            {
                Console.Out.WriteLine("   (TEST)" + targetGroup.Name + ".");
            }
            else
            {
                // 1.0.0.2 issue with Adobe Flash not having an installable update
                if (update.InstallationBehavior.IsSupported)
                {
                    if (update.IsDeclined)
                    {
                        Console.Out.WriteLine("   " + update.Title + ": is declined.");
                    }
                    else
                    {
                        update.Approve(UpdateApprovalAction.Install, targetGroup);
                        Console.Out.WriteLine("   " + update.Title + ": approved for install.");
                    }
                }
                else
                {
                    Console.Out.WriteLine("   " + update.Title + ": doesn't support install approval.");
                }
            }

            ComputerTargetGroupCollection children = targetGroup.GetChildTargetGroups();
            if (children != null && children.Count > 0)
            {
                ApproveUpdate(update, children, isTest, alreadyProcessed);
            }
        }

        /// <summary>
        /// Loads the application settings
        /// </summary>
        /// <returns>
        /// </returns>
        public static ApplicationSettings LoadSettings()
        {
            var settings = (ApplicationSettings)ConfigurationManager.GetSection("ApplicationSettings");

            // validate the settings
            return settings;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Approves a superseded update
        /// Using previous approval settings
        /// </summary>
        /// <param name="newUpdate">
        /// The new update
        /// </param>
        /// <param name="previousUpdate">
        /// The previously approved update
        /// </param>
        /// <param name="isTest">
        /// Whether we are in test mode
        /// </param>
        /// <param name="recentlyApproved">
        /// List of recently approved updates
        /// </param>
        private static void ApproveSupersededUpdate(
            IUpdate newUpdate, IUpdate previousUpdate, bool isTest, List<Guid> recentlyApproved)
        {
            if (isTest)
            {
                Console.Out.Write("(TEST) ");
            }

            Console.Out.WriteLine(newUpdate.Title);
            recentlyApproved.Add(newUpdate.Id.UpdateId);

            if (isTest)
            {
                return;
            }

            UpdateApprovalCollection approvals = previousUpdate.GetUpdateApprovals();

            foreach (IUpdateApproval approval in approvals)
            {
                IComputerTargetGroup ctg = approval.GetComputerTargetGroup();

                newUpdate.Approve(UpdateApprovalAction.Install, ctg);

                Console.Out.WriteLine(" " + ctg.Name);
            }
        }

        /// <summary>
        /// Iterates through a collection of updates to approve
        /// </summary>
        /// <param name="update">
        /// An update
        /// </param>
        /// <param name="targetGroups">
        /// A collection of target groups
        /// </param>
        /// <param name="isTest">
        /// Whether we are in test mode
        /// </param>
        /// <param name="alreadyProcessed">
        /// List of target groups that have already been processed
        /// </param>
        private static void ApproveUpdate(
            IUpdate update, 
            ComputerTargetGroupCollection targetGroups, 
            bool isTest, 
            List<IComputerTargetGroup> alreadyProcessed)
        {
            foreach (IComputerTargetGroup group in targetGroups)
            {
                if (alreadyProcessed != null)
                {
                    if (!alreadyProcessed.Contains(group))
                    {
                        ApproveUpdateForTargetGroup(update, group, isTest, alreadyProcessed);
                        alreadyProcessed.Add(group);
                    }
                }
                else
                {
                    ApproveUpdateForTargetGroup(update, group, isTest, null);
                }
            }
        }

        /// <summary>
        /// Checks a classification for needed updates
        /// </summary>
        /// <param name="server">
        /// Object representing the WSUS server
        /// </param>
        /// <param name="classification">
        /// a single update classification
        /// </param>
        /// <param name="rootGroup">
        /// the "all computers" group
        /// </param>
        /// <param name="isTest">
        /// Whether we are in test mode
        /// </param>
        /// <param name="alreadyProcessed">
        /// List of target groups that have already been processed
        /// </param>
        private static void CheckClassification(
            IUpdateServer server, 
            IUpdateClassification classification, 
            IComputerTargetGroup rootGroup, 
            bool isTest, 
            List<IComputerTargetGroup> alreadyProcessed)
        {
            Console.Out.WriteLine("Getting list of updates for : " + classification.Title);

            var searchScope = new UpdateScope { IncludedInstallationStates = UpdateInstallationStates.NotInstalled };

            searchScope.Classifications.Add(classification);
            UpdateCollection updates = server.GetUpdates(searchScope);

            if (updates.Count > 0)
            {
                DoClassificationUpdates(updates, rootGroup, isTest, alreadyProcessed);
            }
            else
            {
                Console.Out.WriteLine(" No updates required.");
            }
        }

        /// <summary>
        /// The check needed updates.
        /// </summary>
        /// <param name="server">
        /// </param>
        /// <param name="ctg">
        /// </param>
        /// <param name="isTest">
        /// </param>
        private static void CheckNeededUpdates(
            IUpdateServer server, 
            IComputerTargetGroup ctg, 
            // Microsoft.UpdateServices.Administration.UpdateCategoryCollection products,
            bool isTest)
        {
            // check classifications
            Console.Out.WriteLine("Getting classifications");
            UpdateClassificationCollection classifications = Server.GetUpdateClassifications(server);

            // check for updates
            foreach (IUpdateClassification classification in classifications)
            {
                CheckClassification(server, classification, ctg, isTest, null);
            }
        }

        /// <summary>
        /// Checks a stale update to see if current rules allow it to be approved
        /// </summary>
        /// <param name="update">
        /// the update
        /// </param>
        /// <param name="isTest">
        /// whether we are in test most
        /// </param>
        /// <param name="approveLicenseAgreement">
        /// whether we auto accept license agreements
        /// </param>
        private static void CheckStaleUpdate(IUpdate update, bool isTest, bool approveLicenseAgreement)
        {
            if (update.RequiresLicenseAgreementAcceptance)
            {
                if (!approveLicenseAgreement)
                {
                    Console.Out.WriteLine(
                        "Warning: Unable to approve '" + update.Title
                        + "' as it requires a license agreement and the current settings prevent this.");
                    return;
                }

                if (!isTest)
                {
                    update.AcceptLicenseAgreement();
                }
            }

            if (isTest)
            {
                Console.Out.Write("(TEST) ");
            }
            else
            {
                update.RefreshUpdateApprovals();
            }

            Console.Out.WriteLine(update.Title);
        }

        /// <summary>
        /// Checks for updates that may have a new revision
        /// </summary>
        /// <param name="server">
        /// The WSUS Server
        /// </param>
        /// <param name="approveLicenseAgreement">
        /// Whether to approve any license agreement
        /// </param>
        /// <param name="computerTargetGroup">
        /// The target group to check
        /// </param>
        /// <param name="classifications">
        /// Classification to process
        /// </param>
        /// <param name="products">
        /// Collection of products to process
        /// </param>
        /// <param name="isTest">
        /// Whether we are in test mode
        /// </param>
        private static void CheckStaleUpdates(
            IUpdateServer server, 
            bool approveLicenseAgreement, 
            IComputerTargetGroup computerTargetGroup, 
            IUpdateClassification classifications, 
            UpdateCategoryCollection products, 
            bool isTest)
        {
            var searchScope = new UpdateScope { ApprovedStates = ApprovedStates.HasStaleUpdateApprovals };

            if (computerTargetGroup != null)
            {
                searchScope.ApprovedComputerTargetGroups.Add(computerTargetGroup);
            }

            if (classifications != null)
            {
                searchScope.Classifications.Add(classifications);
            }

            if (products != null)
            {
                searchScope.Categories.AddRange(products);
            }

            UpdateCollection updates = server.GetUpdates(searchScope);

            if (updates.Count > 0)
            {
                foreach (IUpdate update in updates)
                {
                    CheckStaleUpdate(update, isTest, approveLicenseAgreement);
                }
            }
            else
            {
                Console.Out.WriteLine(" No updates required.");
            }
        }

        /// <summary>
        /// Checks a superseded update
        /// </summary>
        /// <param name="update">
        /// The update that is marked as superseded
        /// </param>
        /// <param name="approveLicenseAgreement">
        /// Whether to approve any license agreement
        /// </param>
        /// <param name="isTest">
        /// Whether we are in test mode
        /// </param>
        /// <param name="recentlyApproved">
        /// List of recently approved updates
        /// </param>
        private static void CheckSupersededUpdate(
            IUpdate update, bool approveLicenseAgreement, bool isTest, List<Guid> recentlyApproved)
        {
            UpdateCollection superseding = update.GetRelatedUpdates(UpdateRelationship.UpdatesThatSupersedeThisUpdate);

            foreach (IUpdate newUpdate in superseding)
            {
                if (newUpdate.IsSuperseded)
                {
                    continue;
                }

                // check if we've already approved this update
                // this can happen if there has been more than 1 superseded update
                // and we've come across it during this run

                // we do it with a list as /test mode doesn't affect the .IsApproved state of an update
                if (!recentlyApproved.Contains(newUpdate.Id.UpdateId) && !newUpdate.IsApproved)
                {
                    if (newUpdate.RequiresLicenseAgreementAcceptance)
                    {
                        if (!approveLicenseAgreement)
                        {
                            Console.Out.WriteLine(
                                "Warning: Unable to approve '" + newUpdate.Title
                                + "' as it requires a license agreement and the current settings prevent this.");
                            continue;
                        }

                        newUpdate.AcceptLicenseAgreement();
                    }

                    ApproveSupersededUpdate(newUpdate, update, isTest, recentlyApproved);
                }

                return;
            }
        }

        /// <summary>
        /// checks for superseded updates and approves the new revision
        /// </summary>
        /// <param name="server">
        /// The WSUS Server
        /// </param>
        /// <param name="approveLicenseAgreement">
        /// Whether to approve any license agreement
        /// </param>
        /// <param name="computerTargetGroup">
        /// The target group to check
        /// </param>
        /// <param name="classification">
        /// Classification to process
        /// </param>
        /// <param name="products">
        /// Collection of products to process
        /// </param>
        /// <param name="isTest">
        /// Whether we are in test mode
        /// </param>
        private static void CheckSupersededUpdates(
            IUpdateServer server, 
            bool approveLicenseAgreement, 
            IComputerTargetGroup computerTargetGroup, 
            IUpdateClassification classification, 
            UpdateCategoryCollection products, 
            bool isTest)
        {
            var searchScope = new UpdateScope { ApprovedStates = ApprovedStates.HasStaleUpdateApprovals };

            if (computerTargetGroup != null)
            {
                searchScope.ApprovedComputerTargetGroups.Add(computerTargetGroup);
            }

            if (classification != null)
            {
                searchScope.Classifications.Add(classification);
            }

            if (products != null)
            {
                searchScope.Categories.AddRange(products);
            }

            UpdateCollection updates = server.GetUpdates(searchScope);

            if (updates.Count <= 0)
            {
                return;
            }

            var recentlyApproved = new List<Guid>();

            if (updates.Count == 0)
            {
                Console.Out.WriteLine(" No updates required.");
                return;
            }

            foreach (IUpdate update in updates)
            {
                if (update.IsSuperseded)
                {
                    CheckSupersededUpdate(update, approveLicenseAgreement, isTest, recentlyApproved);
                }
            }
        }

        /// <summary>
        /// Run through collection of updates for a classification
        /// </summary>
        /// <param name="updates">
        /// Collection of updates
        /// </param>
        /// <param name="targetGroup">
        /// A single target group
        /// </param>
        /// <param name="isTest">
        /// Whether we are in test mode
        /// </param>
        /// <param name="alreadyProcessed">
        /// List of target groups that have already been processed
        /// </param>
        private static void DoClassificationUpdates(
            UpdateCollection updates, 
            IComputerTargetGroup targetGroup, 
            bool isTest, 
            List<IComputerTargetGroup> alreadyProcessed)
        {
            Console.Out.WriteLine(" " + updates.Count + " updates required.");

            foreach (IUpdate update in updates)
            {
                if (update.IsDeclined)
                {
                    Console.WriteLine("Update has been declined:" + update.Title);
                    continue;
                }

                if (update.RequiresLicenseAgreementAcceptance)
                {
                    if (isTest)
                    {
                        Console.Out.Write("(TEST)");
                    }
                    else
                    {
                        update.AcceptLicenseAgreement();
                    }

                    Console.Out.WriteLine("  License Accepted: " + update.Title);
                }

                Console.Out.WriteLine("   " + update.Title + ".");
                ApproveUpdateForTargetGroup(update, targetGroup, isTest, alreadyProcessed);
            }
        }

        /// <summary>
        /// This is the path run if there are no run sets in the app.config
        /// This is the classic apply to all groups approach from 1.0.0.0
        /// </summary>
        /// <param name="settings">
        /// application settings
        /// </param>
        /// <param name="server">
        /// The WSUS Server
        /// </param>
        /// <param name="isTest">
        /// Whether we are in test mode
        /// </param>
        private static void DoNoRunSets(ApplicationSettings settings, IUpdateServer server, bool isTest)
        {
            Console.WriteLine("Performing \"No Run Set\" rule: ");

            // Get target groups
            Console.Out.WriteLine("Getting root target group: ");
            IComputerTargetGroup rootGroup = Server.GetRootTargetGroup(server);
            Console.Out.WriteLine("succeeded.");

            Rule defaultRule = settings.NoRunSet;

            if (defaultRule.ApproveSupersededUpdates)
            {
                // Check for superseded updates
                Console.Out.WriteLine("Checking Superseded Updates: ");
                CheckSupersededUpdates(server, defaultRule.AcceptLicenseAgreement, null, null, null, isTest);
            }

            if (defaultRule.ApproveStaleUpdates)
            {
                // Check for stale updates
                Console.Out.WriteLine("Checking Stale Updates: ");
                CheckStaleUpdates(server, defaultRule.AcceptLicenseAgreement, null, null, null, isTest);
            }

            if (defaultRule.ApproveNeededUpdates)
            {
                CheckNeededUpdates(server, rootGroup, isTest);
            }
        }

        /// <summary>
        /// performs a specific run set
        /// </summary>
        /// <param name="server">
        /// The WSUS Server
        /// </param>
        /// <param name="runSet">
        /// A specific run set
        /// </param>
        /// <param name="isTest">
        /// Whether we are in test mode
        /// </param>
        private static void DoRunSet(IUpdateServer server, RunSet runSet, bool isTest)
        {
            Console.WriteLine("Performing Run Set: " + runSet.Name);

            // get the target group rules
            Configuration.CheckIsValidRunSet(server, runSet);

            var alreadyProcessed = new List<IComputerTargetGroup>();

            if (runSet.TargetGroups.ElementInformation.IsPresent)
            {
                Console.WriteLine("Target Groups specified in Run Set.");
                TargetGroupCollection targetGroups = runSet.TargetGroups;
                foreach (TargetGroup targetGroup in targetGroups)
                {
                    DoRunSetTargetGroup(server, targetGroup, isTest, alreadyProcessed);
                }
            }
            else if (runSet.AllTargetGroups.ElementInformation.IsPresent)
            {
                AllTargetGroups allTargetGroups = runSet.AllTargetGroups;
                DoRunSetAllTargetGroups(server, allTargetGroups, isTest, alreadyProcessed);
            }
            else
            {
                throw new ConfigurationErrorsException(
                    "Couldn't find a \"targetGroup\" or \"allTargetGroups\" element in the runset.");
            }
        }

        /// <summary>
        /// The do run set all classifications.
        /// </summary>
        /// <param name="server">
        /// The server.
        /// </param>
        /// <param name="ctg">
        /// The ctg.
        /// </param>
        /// <param name="isTest">
        /// The is Test.
        /// </param>
        /// <param name="allClassifications">
        /// The all Classifications.
        /// </param>
        private static void DoRunSetAllClassifications(
            IUpdateServer server, IComputerTargetGroup ctg, bool isTest, Rule allClassifications)
        {
            // TODO: support products in all classifications element
            // Microsoft.UpdateServices.Administration.UpdateCategoryCollection products = null;
            if (allClassifications.Copy)
            {
                // Check for copy
                Console.Out.WriteLine("Checking TargetGroup to Copy From: ");
                IComputerTargetGroup cftg = server.GetComputerTargetGroup(allClassifications.CopyFrom);
                Console.WriteLine("Matched copy from guid to: " + cftg.Name);
                CopyFrom.DoCopy(server, cftg, ctg, isTest);
            }

            if (allClassifications.ApproveSupersededUpdates)
            {
                // Check for superseded updates
                Console.Out.WriteLine("Checking Superseded Updates: ");
                CheckSupersededUpdates(server, allClassifications.AcceptLicenseAgreement, ctg, null, null, isTest);
            }

            if (allClassifications.ApproveStaleUpdates)
            {
                // Check for stale updates
                Console.Out.WriteLine("Checking Stale Updates: ");
                CheckStaleUpdates(server, allClassifications.AcceptLicenseAgreement, ctg, null, null, isTest);
            }

            if (allClassifications.ApproveNeededUpdates)
            {
                CheckNeededUpdates(server, ctg, isTest);
            }
        }

        /// <summary>
        /// The do run set all target groups.
        /// </summary>
        /// <param name="server">
        /// </param>
        /// <param name="allTargetGroups">
        /// </param>
        /// <param name="isTest">
        /// </param>
        /// <param name="alreadyProcessed">
        /// List of target groups that have already been processed
        /// </param>
        private static void DoRunSetAllTargetGroups(
            IUpdateServer server, 
            AllTargetGroups allTargetGroups, 
            bool isTest, 
            List<IComputerTargetGroup> alreadyProcessed)
        {
            Console.Out.WriteLine("Getting root target group: ");
            IComputerTargetGroup rootGroup = Server.GetRootTargetGroup(server);

            ClassificationCollection classifications = allTargetGroups.Classifications;
            if (classifications.Count > 0)
            {
                DoRunSetClassifications(server, rootGroup, classifications, isTest, alreadyProcessed);
            }
            else
            {
                DoRunSetAllClassifications(server, rootGroup, isTest, allTargetGroups.AllClassifications);
            }
        }

        /// <summary>
        /// The do run set classification.
        /// </summary>
        /// <param name="server">
        /// Object representing the WSUS server
        /// </param>
        /// <param name="computerTargetGroup">
        /// The target group to check
        /// </param>
        /// <param name="classification">
        /// </param>
        /// <param name="isTest">
        /// Whether we are in test mode
        /// </param>
        /// <param name="alreadyProcessed">
        /// List of target groups that have already been processed
        /// </param>
        private static void DoRunSetClassification(
            IUpdateServer server, 
            IComputerTargetGroup computerTargetGroup, 
            Classification classification, 
            bool isTest, 
            List<IComputerTargetGroup> alreadyProcessed)
        {
            // check the classification exists on wsus
            IUpdateClassification uc = server.GetUpdateClassification(classification.Guid);

            // check for a product collection
            UpdateCategoryCollection products = classification.Products.ElementInformation.IsPresent
                                                    ? GetUpdateCategoryCollection(server, classification.Products)
                                                    : null;

            if (classification.ApproveStaleUpdates)
            {
                CheckStaleUpdates(
                    server, classification.AcceptLicenseAgreement, computerTargetGroup, uc, products, isTest);
            }

            if (classification.ApproveSupersededUpdates)
            {
                CheckSupersededUpdates(
                    server, classification.AcceptLicenseAgreement, computerTargetGroup, uc, products, isTest);
            }

            if (classification.ApproveNeededUpdates)
            {
                CheckClassification(server, uc, computerTargetGroup, isTest, alreadyProcessed);
            }
        }

        /*
        static void DoRunSetClassifications(
            Microsoft.UpdateServices.Administration.IUpdateServer server,
            Microsoft.UpdateServices.Administration.IComputerTargetGroup ctg,
            bool isTest,
            System.Collections.Generic.List<Microsoft.UpdateServices.Administration.IComputerTargetGroup> alreadyProcessed
            )
        {
            //Model.ClassificationCollection classifications,
            Microsoft.UpdateServices.Administration.UpdateClassificationCollection classifications
                =server.GetUpdateClassifications();
            foreach (Model.Classification classification in classifications)
            {
                DoRunSetClassification(server, ctg, classification, isTest, alreadyProcessed);
            }
        }
         * */

        /// <summary>
        /// The do run set classifications.
        /// </summary>
        /// <param name="server">
        /// Object representing the WSUS server
        /// </param>
        /// <param name="computerTargetGroup">
        /// The target group to check
        /// </param>
        /// <param name="classifications">
        /// Collection of Classifications to process
        /// </param>
        /// <param name="isTest">
        /// Whether we are in test mode
        /// </param>
        /// <param name="alreadyProcessed">
        /// List of target groups that have already been processed
        /// </param>
        private static void DoRunSetClassifications(
            IUpdateServer server, 
            IComputerTargetGroup computerTargetGroup, 
            ClassificationCollection classifications, 
            bool isTest, 
            List<IComputerTargetGroup> alreadyProcessed)
        {
            foreach (Classification classification in classifications)
            {
                DoRunSetClassification(server, computerTargetGroup, classification, isTest, alreadyProcessed);
            }
        }

        /// <summary>
        /// The do run set target group.
        /// </summary>
        /// <param name="server">
        /// Object representing the WSUS server
        /// </param>
        /// <param name="targetGroup">
        /// </param>
        /// <param name="isTest">
        /// Whether we are in test mode
        /// </param>
        /// <param name="alreadyProcessed">
        /// List of target groups that have already been processed
        /// </param>
        private static void DoRunSetTargetGroup(
            IUpdateServer server, TargetGroup targetGroup, bool isTest, List<IComputerTargetGroup> alreadyProcessed)
        {
            Console.WriteLine("Processing Target Groups: " + targetGroup.Guid);

            // check the target group exists on wsus
            IComputerTargetGroup ctg = server.GetComputerTargetGroup(targetGroup.Guid);

            Console.WriteLine("Matched target group guid to: " + ctg.Name);

            // Get classifications
            if (targetGroup.Classifications.ElementInformation.IsPresent)
            {
                Console.WriteLine("Found a collection of specific classifications to apply.");
                DoRunSetClassifications(server, ctg, targetGroup.Classifications, isTest, alreadyProcessed);
            }
            else if (targetGroup.AllClassifications.ElementInformation.IsPresent)
            {
                Console.WriteLine("Applying all classifications.");
                DoRunSetAllClassifications(server, ctg, isTest, targetGroup.AllClassifications);
            }
            else
            {
                throw new ConfigurationErrorsException(
                    "Unable to detect if we are running a specific set of classifications, or processing all classifications");
            }
        }

        /// <summary>
        /// This is run when there are a group of runsets in the app.config
        /// Run Sets allow for different options to be run on different occasions
        /// The runset is specified on the command line
        /// </summary>
        /// <param name="server">
        /// The WSUS Server
        /// </param>
        /// <param name="runSets">
        /// Group of runsets
        /// </param>
        /// <param name="commandLine">
        /// Information on what was specified on the command line
        /// </param>
        /// <param name="isTest">
        /// Whether we are in test mode
        /// </param>
        private static void DoRunSets(
            // Model.ApplicationSettings settings,
            IUpdateServer server, RunSetCollection runSets, CommandLine commandLine, bool isTest)
        {
            // we need to work out which runset is being done
            // we'll limit the command line to one runset
            string requestedRunSet = commandLine.GetRunSetName();

            RunSet requiredRunSet =
                runSets.Cast<RunSet>().FirstOrDefault(
                    runSet => requestedRunSet.Equals(runSet.Name, StringComparison.OrdinalIgnoreCase));

            if (requiredRunSet == null)
            {
                throw new ArgumentException(
                    "The RunSet '" + requestedRunSet
                    + "' as requested on the command line is not defined in the app.config.");
            }

            DoRunSet(server, requiredRunSet, isTest);
        }

        /// <summary>
        /// Gets a list of update categories by searching for the guids specified in the config
        /// </summary>
        /// <param name="server">
        /// wsus server connection
        /// </param>
        /// <param name="products">
        /// list of product guids from config
        /// </param>
        /// <returns>
        /// collection of update categories
        /// </returns>
        private static UpdateCategoryCollection GetUpdateCategoryCollection(
            IUpdateServer server, ProductCollection products)
        {
            if (products == null)
            {
                throw new ArgumentNullException("products");
            }

            if (products.Count < 1)
            {
                throw new ArgumentException("products has no product items.");
            }

            var result = new UpdateCategoryCollection();

            foreach (Product product in products)
            {
                IUpdateCategory category = server.GetUpdateCategory(product.Guid);

                result.Add(category);
            }

            return result;
        }

        /*
        static bool AreUpdatesNeededSinceLastRun(
            Microsoft.UpdateServices.Administration.IUpdateServer server
            )
        {
            Microsoft.UpdateServices.Administration.ComputerTargetScope computersReportedSinceLastRun = new Microsoft.UpdateServices.Administration.ComputerTargetScope();
            computersReportedSinceLastRun.FromLastReportedStatusTime = System.DateTime.Now;

            const Microsoft.UpdateServices.Administration.UpdateSources updateSources = new Microsoft.UpdateServices.Administration.UpdateSources();

            Microsoft.UpdateServices.Administration.UpdateServerStatus status = server.GetComputerStatus(computersReportedSinceLastRun, updateSources);
            if (status.NotApprovedUpdateCount > 0)
            {
                return true;
            }

            return false;
        }
         * */

        /// <summary>
        /// Program entry point
        /// </summary>
        /// <param name="args">
        /// command line arguments
        /// </param>
        private static void Main(string[] args)
        {
            ShowHeader();
            if (args == null || args.GetLength(0) == 0)
            {
                ShowHelp();
                return;
            }

            var commandLine = new CommandLine(args);
            if (commandLine.GetWantsHelp())
            {
                ShowHelp();
                return;
            }

            // Check config file
            ApplicationSettings settings = LoadSettings();

            // Connect to server
            IUpdateServer server = Server.Connect(settings.Server);

            // Check user has admin access
            UpdateServerUserRole role = server.GetCurrentUserRole();
            if (role != UpdateServerUserRole.Administrator)
            {
                throw new UnauthorizedAccessException("You don't have administrator access on the WSUS server.");
            }

            // Get the default rule
            bool isTest = commandLine.GetIsTest();
            RunSetCollection runSets = settings.RunSets;

            switch (commandLine.GetUseRunSet())
            {
                case TriState.Yes:
                    if (runSets == null || runSets.Count == 0)
                    {
                        throw new InvalidOperationException(
                            "/runset specified on the command line.  But there are no runsets defined in the config file.");
                    }

                    // we have run sets
                    // this means our approval logic is more complex
                    DoRunSets(server, runSets, commandLine, isTest);

                    break;

                case TriState.No:
                    DoNoRunSets(settings, server, isTest);

                    break;

                default:
                    if (runSets != null && runSets.Count > 0)
                    {
                        throw new InvalidOperationException(
                            "RunSets exist in the application config. /norunset or /runset must be specified on the command line.");
                    }

                    break;
            }
        }

        /// <summary>
        /// Shows the header information
        /// </summary>
        private static void ShowHeader()
        {
            const string version = "1.0.0.6";
            Console.Out.WriteLine("WSUS Smart Approve " + version + " (http://wsussmartapprove.codeplex.com)");
            Console.Out.WriteLine("(C)Copyright 2009-2012 DHGMS Solutions. Some Rights Reserved.\n");
        }

        /// <summary>
        /// Shows the help information
        /// </summary>
        private static void ShowHelp()
        {
            var arguments = new Dictionary<string, string> {
                    { "/?", "Show help information." }, 
                    { "/norunset", "Run using the no run set rule." }, 
                    { "/runset name", "Run a particular RunSet, as specified in the app.config" }, 
                    {
                        "/test", 
                        "Run in test mode. Changes will not be performed to WSUS, but will be displayed on the screen."
                        }
                };

            foreach (var kvp in arguments)
            {
                Console.Out.WriteLine(kvp.Key + "\t" + kvp.Value);
            }
        }

        #endregion
    }
}