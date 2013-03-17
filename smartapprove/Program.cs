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
    using System.Reflection;

    using SmartApprove.Controller;

    /// <summary>
    /// The applications main class
    /// </summary>
    internal class Program
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
                var windows8 = new Version(6, 2);
                if (Environment.OSVersion.Version < windows8)
                {
                    // need to try to load old version
                    if (!alreadyTriedRedirect)
                    {
                        var programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                        if (!programFiles.EndsWith(@"\"))
                        {
                            programFiles = programFiles + @"\";
                        }

                        redirect = Assembly.LoadFile(string.Format("{0}Microsoft.UpdateServices.Administration.dll", programFiles));
                        alreadyTriedRedirect = true;
                    }

                    return redirect;
                }
            }

            return null;
        }

        /// <summary>
        /// Program entry point
        /// </summary>
        /// <param name="args">
        /// command line arguments
        /// </param>
        private static void Main(string[] args)
        {
            var job = new Job();
            job.Execute();
        }
    }
}