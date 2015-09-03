namespace NotACompany.CF_Tester
{
    using Microsoft.VisualStudio.Shell;
    using Microsoft.VisualStudio.VCProjectEngine;
    using NotACompany.CF_Tester.Models;
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    public sealed partial class CF_TesterPackage : Package
    {
        /// <summary>
        /// Analyzes the test results shows a summary to the user.
        /// </summary>
        /// <param name="tests">Tests.</param>
        /// <param name="results">Results.</param>
        private void ShowResults(List<Test> tests, List<Result> results)
        {
            int firstDifferent = 0;

            while (firstDifferent < tests.Count &&
                !results[firstDifferent].crashed &&
                results[firstDifferent].output == tests[firstDifferent].output) firstDifferent++;

            if (firstDifferent == tests.Count)
            {
                MessageBox.Show("All tests have been passed successfully.", "Codeforces Tester", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            else
            {
                Dialog dialog = new Dialog(tests, results);
                dialog.ShowDialog();
            }
        }

        /// <summary>
        /// Returns complete path user's executable file (including it's name).
        /// </summary>
        /// <returns>Output file path.</returns>
        private string GetFullOutputPath()
        {
            string fullPath = "";
            string outputPath = "";
            string outputFileName = "";
            string primaryOutput = "";

            foreach (EnvDTE.Project project in dte.Solution.Projects)
            {
                try // C++
                {
                    VCProject vcProject = (VCProject)(project.Object);
                    IVCCollection configurations = (IVCCollection)vcProject.Configurations;
                    VCConfiguration configuration = (VCConfiguration)configurations.Item("Debug");

                    primaryOutput = configuration.PrimaryOutput;
                    return primaryOutput;
                }
                catch
                {                   
                }

                try // C#, VB
                {
                    fullPath = project.Properties.Item("FullPath").Value as string ?? "";
                    outputPath = project.ConfigurationManager.ActiveConfiguration.Properties.Item("OutputPath").Value as string ?? "";
                    outputFileName = project.Properties.Item("OutputFileName").Value as string ?? "";
                    return fullPath + outputPath + outputFileName;
                }
                catch
                {
                }

                break;
            }

            throw new InvalidOperationException("Unable to retrieve the output file path.");
        }
    }
}
