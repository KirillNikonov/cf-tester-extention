namespace NotACompany.CF_Tester
{
    using EnvDTE;
    using Exceptions;
    using Microsoft.VisualStudio.Shell;
    using Microsoft.VisualStudio.Shell.Interop;
    using Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.Design;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideAutoLoad(UIContextGuids80.NotBuildingAndNotDebugging)]
    [Guid(GuidList.guidCF_TesterPkgString)]
    public sealed class CF_TesterPackage : Package
    {
        private List<Contest> recentContests;
        private List<Problem> problems;
        private List<Test> tests;
        private EnvDTE80.DTE2 dte;
        private Solution solution;

        public CF_TesterPackage()
        {
        }

        #region Package Members

        /// <summary>
        /// Performs all necessary initialization.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;

            try
            {
                recentContests = Codeforces.getRecentContests();
                problems = recentContests.Count > 0 ? Codeforces.getProblems(recentContests[0].id) : new List<Problem>();

                // Test <problemIndex> commands
                for (uint i = PkgCmdIDList.cmdidTestACommand; i <= PkgCmdIDList.cmdidTestTCommand; i++)
                {
                    CommandID cmdID = new CommandID(GuidList.guidCF_TesterCmdSet, (int)i);
                    OleMenuCommand mc = new OleMenuCommand(new EventHandler(MenuItemCallback), cmdID);

                    if ((int)(i - PkgCmdIDList.cmdidTestACommand) < problems.Count) mc.Text = problems[(int)(i - PkgCmdIDList.cmdidTestACommand)].index;
                    mc.Visible = (int)(i - PkgCmdIDList.cmdidTestACommand) < problems.Count;

                    mcs.AddCommand(mc);
                }

                // Set current contest commands
                for (uint i = PkgCmdIDList.cmdidContestOption1Command; i <= PkgCmdIDList.cmdidContestOption6Command; i++)
                {
                    CommandID cmdID = new CommandID(GuidList.guidCF_TesterCmdSet, (int)i);
                    OleMenuCommand mc = new OleMenuCommand(new EventHandler(MenuItemCallback), cmdID);

                    mc.Text = "#" + recentContests[(int)(i - PkgCmdIDList.cmdidContestOption1Command)].id.ToString() +
                        " " + recentContests[(int)(i - PkgCmdIDList.cmdidContestOption1Command)].name;

                    mcs.AddCommand(mc);
                }

                // Refresh command
                OleMenuCommand refresh = new OleMenuCommand(new EventHandler(MenuItemCallback), new CommandID(GuidList.guidCF_TesterCmdSet, (int)PkgCmdIDList.cmdidRefreshCommand));
                mcs.AddCommand(refresh);

                // Debug sample test cammands
                for (uint i = PkgCmdIDList.cmdidDebug1Command; i <= PkgCmdIDList.cmdidDebug9Command; i++)
                {
                    CommandID cmdID = new CommandID(GuidList.guidCF_TesterCmdSet, (int)i);
                    OleMenuCommand mc = new OleMenuCommand(new EventHandler(MenuItemCallback), cmdID);

                    mc.Visible = tests != null && (int)(i - PkgCmdIDList.cmdidDebug1Command) < tests.Count;

                    mcs.AddCommand(mc);
                }
            }
            catch (FailedRequestException exception)
            {
                MessageBox.Show("Failed to connect to Codeforces. The server may be unavailable.", "Codeforces Tester", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        /// <summary>
        /// The common command event handler. Is invoked every time a toolbar button is clicked.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event params.</param>
        private void MenuItemCallback(object sender, EventArgs e)
        {
            OleMenuCommand mc = sender as OleMenuCommand;
            var command = sender as IOleMenuCommand;
            int commandId = mc.CommandID.ID;

            if (commandId >= PkgCmdIDList.cmdidTestACommand && commandId <= PkgCmdIDList.cmdidTestTCommand) // Test problem command
            {
                TestAgainst((int)(commandId - PkgCmdIDList.cmdidTestACommand));
            }
            else if (commandId >= PkgCmdIDList.cmdidContestOption1Command && commandId <= PkgCmdIDList.cmdidContestOption6Command) // Choose contest command
            {
                ChangeContest((int)((uint)commandId - PkgCmdIDList.cmdidContestOption1Command));
            }
            else if (commandId == PkgCmdIDList.cmdidRefreshCommand) // Refresh command
            {
                ChangeContest(0);
            }
            else if (commandId >= PkgCmdIDList.cmdidDebug1Command && commandId <= PkgCmdIDList.cmdidDebug9Command) // Debug sample test command
            {
                DebugTest(commandId - (int)PkgCmdIDList.cmdidDebug1Command);
            }
        }

        /// <summary>
        /// Starts the debugging process and sends the test input.
        /// </summary>
        /// <param name="newContest">Test ID (number).</param>
        private void DebugTest(int testId)
        {
            //solution.SolutionBuild.Debug();

            string reformattedInput = new Regex(@"[\n]").Replace(tests[testId].input, Environment.NewLine);

            Clipboard.SetText(reformattedInput);

            dte.Debugger.Go();

            //IEnumerable activeProjects = dte.ActiveSolutionProjects as IEnumerable;
            //string solutionDir = Path.GetDirectoryName(solution.FullName);

            //string projectName = "";

            //foreach (var item in activeProjects)
            //{
            //    projectName = ((Project)item).Name;
            //    break;
            //}

            //EnvDTE.Process debuggedProcess = null;

            //bool attached = false;
            //for (int i = 0; i < 100 && !attached; i++)
            //{
            //    attached = false;
            //    EnvDTE.Processes processes = dte.Debugger.LocalProcesses;
            //    foreach (EnvDTE.Process process in processes)
            //    {
            //        if (process.Name.IndexOf(projectName + ".exe") != -1)
            //        {
            //            attached = true;
            //            process.Attach();
            //            debuggedProcess = process;
            //        }
            //    }
            //}

            //if (debuggedProcess != null)
            //{
            //    int y = 0;
            //}
        }

        /// <summary>
        /// Changes the contest chosen by user.
        /// </summary>
        /// <param name="newContest">New contest id (in recentContests).</param>
        private void ChangeContest(int newContest)
        {
            if (newContest > 0)
            {
                Contest temp = recentContests[0];
                recentContests[0] = recentContests[newContest];

                for (; newContest > 1 && recentContests[newContest - 1].id < temp.id; newContest--) recentContests[newContest] = recentContests[newContest - 1];
                for (; newContest < 5 && recentContests[newContest + 1].id > temp.id; newContest++) recentContests[newContest] = recentContests[newContest + 1];

                recentContests[newContest] = temp;
            }

            OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;

            for (uint i = PkgCmdIDList.cmdidContestOption1Command; i <= PkgCmdIDList.cmdidContestOption6Command; i++)
            {
                CommandID cmdID = new CommandID(GuidList.guidCF_TesterCmdSet, (int)i);
                OleMenuCommand mc =  (OleMenuCommand)mcs.FindCommand(cmdID);

                mc.Text = "#" + recentContests[(int)(i - PkgCmdIDList.cmdidContestOption1Command)].id.ToString() +
                    " " + recentContests[(int)(i - PkgCmdIDList.cmdidContestOption1Command)].name;
            }

            try
            {
                this.problems = Codeforces.getProblems(recentContests[0].id);

                for (uint i = PkgCmdIDList.cmdidTestACommand; i <= PkgCmdIDList.cmdidTestTCommand; i++)
                {
                    CommandID cmdID = new CommandID(GuidList.guidCF_TesterCmdSet, (int)i);
                    OleMenuCommand mc = (OleMenuCommand)mcs.FindCommand(cmdID);

                    if ((int)(i - PkgCmdIDList.cmdidTestACommand) < problems.Count) mc.Text = problems[(int)(i - PkgCmdIDList.cmdidTestACommand)].index;
                    mc.Visible = (int)(i - PkgCmdIDList.cmdidTestACommand) < problems.Count;
                }
            }
            catch (FailedRequestException exception)
            {
                MessageBox.Show("Failed to connect to Codeforces. The server may be unavailable.", "Codeforces Tester", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Tests the solution.
        /// </summary>
        /// <param name="problemId">Problem id (1-7).</param>
        private void TestAgainst(int problemId)
        {
            try
            {
                tests = Codeforces.getTests(recentContests[0].id, (char)(problemId + (int)'A'));
                List<Result> results = new List<Result>();

                dte = this.GetService(typeof(Microsoft.VisualStudio.Shell.Interop.SDTE)) as EnvDTE80.DTE2;
                solution = dte.Solution;

                if (solution.Count > 0)
                {
                    solution.SolutionBuild.Build(true);

                    if (solution.SolutionBuild.LastBuildInfo > 0)
                    {
                        MessageBox.Show("There were build errors. Please recheck your solution.", "Codeforces Tester", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        IEnumerable activeProjects = dte.ActiveSolutionProjects as IEnumerable;
                        string solutionDir = Path.GetDirectoryName(solution.FullName);

                        string projectName = "";

                        foreach (var item in activeProjects)
                        {
                            projectName = ((Project)item).Name;
                            break;
                        }

                        foreach (Test test in tests)
                        {
                            System.Diagnostics.Process process = new System.Diagnostics.Process();
                            process.StartInfo.FileName = solutionDir + "\\Debug\\" + projectName + ".exe";

                            process.StartInfo.UseShellExecute = false;
                            process.StartInfo.RedirectStandardInput = true;
                            process.StartInfo.RedirectStandardOutput = true;
                            process.StartInfo.RedirectStandardError = true;

                            process.Start();

                            Result result;

                            process.StandardInput.WriteLine(test.input);
                            process.StandardInput.Close();

                            result = new Result(process.StandardOutput.ReadToEnd());

                            process.WaitForExit();

                            if (process.ExitCode != 0) result = new Result("", true);
                            results.Add(result);
                        }

                        ShowResults(tests, results);
                    }
                }

                OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;

                for (uint i = PkgCmdIDList.cmdidDebug1Command; i <= PkgCmdIDList.cmdidDebug9Command; i++)
                {
                    CommandID cmdID = new CommandID(GuidList.guidCF_TesterCmdSet, (int)i);
                    OleMenuCommand mc = (OleMenuCommand)mcs.FindCommand(cmdID);

                    mc.Visible = tests != null && (int)(i - PkgCmdIDList.cmdidDebug1Command) < tests.Count;
                }
            }
            catch (FailedRequestException exception)
            {
                MessageBox.Show("Failed to connect to Codeforces. The server may be unavailable.", "Codeforces Tester", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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
                for (int i = firstDifferent; i < results.Count; i++)
                {
                    if (results[i].crashed)
                    {
                        MessageBox.Show("The program crashed at test #" + (i + 1).ToString() + ".\n\n" + "Input:\n\n" + tests[i].input + "\n", "Codeforces Tester", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                for (int i = firstDifferent; i < tests.Count; i++)
                {
                    if (String.Compare(results[i].output, tests[i].output) != 0)
                    {
                        string message = "";
                        message += "==================== " + "Test #" + (i + 1).ToString() + " ====================\n\n";
                        message += "Input:\n\n" + tests[i].input + "\n\n";
                        message += "Expected:\n\n" + tests[i].output + "\n\n";
                        message += "Output:\n\n" + results[i].output + "\n\n";

                        MessageBox.Show(message, "Codeforces Tester", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }   
            }
        }
    }
}
