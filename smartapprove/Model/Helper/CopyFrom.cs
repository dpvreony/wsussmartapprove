// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CopyFrom.cs" company="DHGMS Solutions">
//   2004-2012 DHGMS Solutions. Some Rights Reserved. Licensed under GNU General Public License version 2 (GPLv2). Some code may be covered by other licenses.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SmartApprove.Model.Helper
{
    using System;

    using Microsoft.UpdateServices.Administration;

    /// <summary>
    /// Copy approvals from one target group to another.
    /// </summary>
    /// <remarks>
    /// Contribution from jeddytier4 (http://www.codeplex.com/site/users/view/jeddytier4)
    /// Based upon work by Torgeir Bakken
    /// Sources
    /// http://msmvps.com/blogs/athif/archive/2006/04/05/89368.aspx
    /// http://groups.google.com/group/microsoft.public.windows.server.update_services/msg/0a62b129af4c8a7c?dmode=source&amp;hl=en
    /// </remarks>
    public static class CopyFrom
    {
        #region Public Methods and Operators

        /// <summary>
        /// Entry point for copying approvals from one target group to another
        /// </summary>
        /// <param name="server">
        /// The server.
        /// </param>
        /// <param name="sourceGroup">
        /// The source group.
        /// </param>
        /// <param name="destinationGroup">
        /// The destination group.
        /// </param>
        /// <param name="isTest">
        /// Whether this is a test run.
        /// </param>
        public static void DoCopy(
            IUpdateServer server, IComputerTargetGroup sourceGroup, IComputerTargetGroup destinationGroup, bool isTest)
        {
            // get IComputerTargetGroup references for the source and destination groups
            if (isTest)
            {
                Console.Out.Write("(TEST) ");
            }

            Console.Out.WriteLine(
                "Copying update approvals from group {0} to group {1}.", sourceGroup.Name, destinationGroup.Name);

            // loop over all updates, copying approvals from the source group to the destination
            // group as necessary
            var updates = server.GetUpdates();

            foreach (IUpdate update in updates)
            {
                var sourceApprovals = update.GetUpdateApprovals(sourceGroup);
                var destinationApprovals = update.GetUpdateApprovals(destinationGroup);

                // for simplicity, this program assumes that an update has
                // at most one approval to a given group
                if (sourceApprovals.Count > 1)
                {
                    Console.Out.WriteLine(
                        "{0} had multiple approvals to group {1}; skipping.", update.Title, sourceGroup.Name);
                    continue;
                }

                if (destinationApprovals.Count > 1)
                {
                    Console.Out.WriteLine(
                        "{0} had multiple approvals to group {1}; skipping.", update.Title, destinationGroup.Name);
                    continue;
                }

                IUpdateApproval sourceApproval = null;
                IUpdateApproval destinationApproval = null;

                if (sourceApprovals.Count > 0)
                {
                    sourceApproval = sourceApprovals[0];
                }

                if (destinationApprovals.Count > 0)
                {
                    destinationApproval = destinationApprovals[0];
                }

                if (sourceApproval == null)
                {
                    // the update is not approved to the source group
                    if (destinationApproval != null)
                    {
                        // the update is not approved to the source group, but it is approved
                        // to the destination group
                        // unapprove the update for the destination group to match the source
                        Console.Out.WriteLine("Unapproving {0} to group {1}.", update.Title, destinationGroup.Name);
                        if (isTest)
                        {
                            Console.Out.Write("(TEST) ");
                        }
                        else
                        {
                            destinationApproval.Delete();
                        }
                    }

                    // neither the source group nor the destination group have an approval;
                    // do nothing
                }
                else
                {
                    // the source group has an approval
                    if (destinationApproval != null)
                    {
                        // destination group has an approval; check to see if we need to overwrite it
                        if (destinationApproval.Action != sourceApproval.Action)
                        {
                            // the approvals are different; overwrite
                            Console.Out.WriteLine(
                                "Changing approval for {0} from {1} to {2} for group {3}.", 
                                update.Title, 
                                destinationApproval.Action, 
                                sourceApproval.Action, 
                                destinationGroup.Name);
                            if (isTest)
                            {
                                Console.Out.Write("(TEST) ");
                            }

                            Program.ApproveUpdateForTargetGroup(update, destinationGroup, isTest, null);
                        }
                    }
                    else
                    {
                        // destination group does not have an approval; approve
                        Console.Out.WriteLine(
                            "Approving {0} for {1} for group {2}.", 
                            update.Title, 
                            sourceApproval.Action, 
                            destinationGroup.Name);
                        if (isTest)
                        {
                            Console.Out.Write("(TEST) ");
                        }

                        Program.ApproveUpdateForTargetGroup(update, destinationGroup, isTest, null);
                    }
                }
            }
        }

        #endregion
    }
}