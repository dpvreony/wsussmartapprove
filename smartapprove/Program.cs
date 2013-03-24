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