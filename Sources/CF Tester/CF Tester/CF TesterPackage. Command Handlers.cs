namespace NotACompany.CF_Tester
{
    using Microsoft.VisualStudio.Shell;
    using NotACompany.CF_Tester.Exceptions;
    using NotACompany.CF_Tester.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Design;
    using System.Windows.Forms;

    public sealed partial class CF_TesterPackage : Package
    {
        /// <summary>
        /// Starts the debugging process and sends the test input.
        /// </summary>
        /// <param name="newContest">Test ID (number).</param>
        private void DebugTest(int testId)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();

            process.StartInfo.FileName = getFullOutputPath();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = false;
            process.StartInfo.RedirectStandardError = false;

            process.Start();

            dte.Debugger.DetachAll();
            foreach (EnvDTE.Process proc in dte.Debugger.LocalProcesses)
            {
                if (proc.Name == process.StartInfo.FileName)
                {
                    proc.Attach();
                }
            }

            process.StandardInput.WriteLine(tests[testId].input);
            process.StandardInput.Close();
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
                OleMenuCommand mc = (OleMenuCommand)mcs.FindCommand(cmdID);

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
                MessageBox.Show(exception.Message, "Codeforces Tester", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        foreach (Test test in tests)
                        {
                            System.Diagnostics.Process process = new System.Diagnostics.Process();

                            process.StartInfo.FileName = getFullOutputPath();
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

                        showResults(tests, results);
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
                MessageBox.Show(exception.Message, "Codeforces Tester", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
