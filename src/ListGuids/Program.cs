namespace ListGuids
{
    using System;
    using System.Reflection;

    class Program
    {
        static bool alreadyTriedRedirect;
        static Assembly redirect;

        static Program()
        {
            AppDomain.CurrentDomain.AssemblyResolve += ResolveAssembly;
        }

        private static Assembly ResolveAssembly(object sender, ResolveEventArgs args)
        {
            var name = args.Name.Split(',')[0];

            if (name.Equals("Microsoft.UpdateServices.Administration"))
            {
                // Microsoft changed version numbers when putting WSUS Admin API into RSAT.
                // Need to try and maintain backward compatability.

                if (!alreadyTriedRedirect)
                {
                    alreadyTriedRedirect = true;

                    var programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                    if (!programFiles.EndsWith(@"\"))
                    {
                        programFiles = programFiles + @"\";
                    }

                    redirect = Assembly.LoadFrom(string.Format("{0}Update Services\\Api\\Microsoft.UpdateServices.Administration.dll", programFiles));
                }

                return redirect;
            }

            return null;
        }

        /// <summary>
        /// Displays a list of classifications and their guids
        /// </summary>
        /// <param name="classifications">classifications</param>
        static void ListClassifications(
            Microsoft.UpdateServices.Administration.UpdateClassificationCollection classifications
            )
        {
            foreach (Microsoft.UpdateServices.Administration.IUpdateClassification classification in classifications)
            {
                System.Console.Out.WriteLine(classification.Id + "\t" + classification.Title);
            }
        }

        /// <summary>
        /// Displays a list of target groups and their guids
        /// </summary>
        /// <param name="targetGroup">a target group</param>
        static void ListTargetGroups(
            Microsoft.UpdateServices.Administration.IComputerTargetGroup targetGroup
            )
        {
            System.Console.Out.WriteLine(targetGroup.Id + "\t" + targetGroup.Name);
            Microsoft.UpdateServices.Administration.ComputerTargetGroupCollection children
                = targetGroup.GetChildTargetGroups();
            
            if (children == null || children.Count <= 0)
            {
                return;
            }

            foreach (Microsoft.UpdateServices.Administration.IComputerTargetGroup child in children)
            {
                ListTargetGroups(child);
            }
        }

        /// <summary>
        /// Displays a list of products categories and their guids
        /// </summary>
        /// <param name="categories">categories</param>
        /// <param name="depthCount">How deep in we have iterated, used for console output</param>
        static void ListCategories(
            Microsoft.UpdateServices.Administration.UpdateCategoryCollection categories,
            int depthCount
            )
        {
            foreach (Microsoft.UpdateServices.Administration.IUpdateCategory category in categories)
            {
                for (int i = 0; i < depthCount; i++)
                {
                    System.Console.Out.Write(" ");
                }
                
                System.Console.Out.WriteLine(category.Id + "\t" + category.Title);
                Microsoft.UpdateServices.Administration.UpdateCategoryCollection subCategories
                    = category.GetSubcategories();
                if (subCategories != null
                    && subCategories.Count > 0)
                {
                    ListCategories(subCategories, depthCount + 1);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(
            string[] args
            )
        {
            ShowHeader();

            if (args == null
                || args.GetLength(0) == 0)
            {
                ShowHelp();
                return;
            }

            Controller.CommandLine commandLine = new Controller.CommandLine(args);
            if (commandLine.GetWantsHelp())
            {
                ShowHelp();
                return;
            }

            //Connect to server
            System.String hostname = commandLine.GetHostname();
            SmartLib.Model.Server connectionSettings = (hostname == null) ?
                null :
                new SmartLib.Model.Server();
            if (connectionSettings != null)
            {
                connectionSettings.Hostname = hostname;
                connectionSettings.Port = commandLine.GetPort();
                connectionSettings.Secure = commandLine.GetSecure();
            }

            Microsoft.UpdateServices.Administration.IUpdateServer server
                = SmartLib.Controller.Server.Connect(connectionSettings);

            //Check user has admin or reporter access
            Microsoft.UpdateServices.Administration.UpdateServerUserRole role = server.GetCurrentUserRole();
            if (role != Microsoft.UpdateServices.Administration.UpdateServerUserRole.Reporter
                && role != Microsoft.UpdateServices.Administration.UpdateServerUserRole.Administrator)
            {
                throw new System.UnauthorizedAccessException("You don't have reporter or administrator access on the WSUS server.");
            }


            //Get Classifications
            System.Console.Out.WriteLine("Getting classifications.");
            Microsoft.UpdateServices.Administration.UpdateClassificationCollection classifications =
                SmartLib.Controller.Server.GetUpdateClassifications(server);

            //Get product categories
            System.Console.Out.WriteLine("Getting product categories.");
            Microsoft.UpdateServices.Administration.UpdateCategoryCollection categories
                = SmartLib.Controller.Server.GetRootUpdateCategories(server);

            //Get target groups
            System.Console.Out.WriteLine("Getting root target group.");
            Microsoft.UpdateServices.Administration.IComputerTargetGroup rootGroup
                = SmartLib.Controller.Server.GetRootTargetGroup(server);

            System.Console.Out.WriteLine("\nClassifications:\n");
            ListClassifications(classifications);
            System.Console.Out.WriteLine("\nProduct Categories:\n");
            ListCategories(categories, 0);
            System.Console.Out.WriteLine("\nTarget Groups:\n");
            ListTargetGroups(rootGroup);
        }

        /// <summary>
        /// Shows the header information
        /// </summary>
        static void ShowHeader()
        {
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            System.Console.Out.WriteLine("WSUS Smart Tools ListGuids " + version + " (http://wsussmartapprove.codeplex.com)");
            System.Console.Out.WriteLine("by DHGMS Solutions. (C)Copyright 2009-2013. Some Rights Reserved.\n");
        }

        /// <summary>
        /// Shows the help information
        /// </summary>
        static void ShowHelp()
        {
            System.Collections.Generic.Dictionary<System.String, System.String> arguments
                = new System.Collections.Generic.Dictionary<System.String, System.String>
            {
                {"hostname", "WSUS Server to connect to."},
                {"port", "Port to connect to."},
                {"ssl", "Use SSL."},
                {"nossl", "Don't use SSL."},
                {"/?", "Show help information."}
            };

            System.Console.Out.WriteLine("listguids [hostname port [ssl | nossl]] [/?]");
            foreach (System.Collections.Generic.KeyValuePair<System.String, System.String> kvp in arguments)
            {
                System.Console.Out.WriteLine(kvp.Key + "\t" + kvp.Value);
            }

        }
    }
}
