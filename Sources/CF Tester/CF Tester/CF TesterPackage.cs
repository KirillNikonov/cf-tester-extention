namespace NotACompany.CF_Tester
{
    using EnvDTE;
    using Exceptions;
    using Microsoft.VisualStudio.Shell;
    using Microsoft.VisualStudio.Shell.Interop;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Design;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideAutoLoad(UIContextGuids80.NotBuildingAndNotDebugging)]
    [Guid(GuidList.guidCF_TesterPkgString)]
    public sealed partial class CF_TesterPackage : Package
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

                // Test problem commands
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
    }
}
