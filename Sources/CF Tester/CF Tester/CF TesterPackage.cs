namespace NotACompany.CF_Tester
{
    using Microsoft.VisualStudio.Shell;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Design;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Runtime.InteropServices;
    using System.Text;
    using Models;

    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(GuidList.guidCF_TesterPkgString)]
    public sealed class CF_TesterPackage : Package
    {
        private List<Contest> recentContests;
        private int currentContest;

        public CF_TesterPackage()
        {
            currentContest = 0;
        }

        #region Package Members

        /// <summary>
        /// Performs all necessary initialization.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;

            for (uint i = PkgCmdIDList.cmdidTestACommand; i <= PkgCmdIDList.cmdidTestGCommand; i++)
            {
                CommandID cmdID = new CommandID(GuidList.guidCF_TesterCmdSet, (int)i);
                OleMenuCommand mc = new OleMenuCommand(new EventHandler(MenuItemCallback), cmdID);

                mcs.AddCommand(mc);
            }

            recentContests = Codeforces.getRecentContests();

            for (uint i = PkgCmdIDList.cmdidContestOption1Command; i <= PkgCmdIDList.cmdidContestOption6Command; i++)
            {
                CommandID cmdID = new CommandID(GuidList.guidCF_TesterCmdSet, (int)i);
                OleMenuCommand mc = new OleMenuCommand(new EventHandler(MenuItemCallback), cmdID);

                mc.Text = "#" + recentContests[(int)(i - PkgCmdIDList.cmdidContestOption1Command)].id.ToString() +
                    " " + recentContests[(int)(i - PkgCmdIDList.cmdidContestOption1Command)].name;

                mcs.AddCommand(mc);
            }
        }
        #endregion

        /// <summary>
        /// The common command event handler. Is Invoked every time a toolbar button is clicked.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event params.</param>
        private void MenuItemCallback(object sender, EventArgs e)
        {
            OleMenuCommand mc = sender as OleMenuCommand;
            var command = sender as IOleMenuCommand;
            int commandId = mc.CommandID.ID;

            if (commandId <= PkgCmdIDList.cmdidTestGCommand)
            {
                Codeforces.getTests(recentContests[currentContest].id, (char)(commandId - PkgCmdIDList.cmdidTestACommand + (int)'A'));

                // TODO: Do stuff with tests.
            }
            else
            {
                ChangeContest((int)((uint)commandId - PkgCmdIDList.cmdidContestOption1Command));
            }            
        }

        /// <summary>
        /// Changes the contest chosen by user.
        /// </summary>
        /// <param name="newContest">New contest id (in recentContests).</param>
        private void ChangeContest(int newContest)
        {
            Contest temp = recentContests[newContest];
            for (int i = newContest - 1; i >= 0; i--) recentContests[i + 1] = recentContests[i];
            recentContests[0] = temp;

            for (int j, i = 1; i < recentContests.Count; i++)
            {
                temp = recentContests[i];
                j = i + 1;
                while (j < recentContests.Count && temp.id < recentContests[j].id)
                {
                    recentContests[j - 1] = recentContests[j];
                    j++;
                }
                recentContests[j - 1] = temp;
            }

            OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;

            for (uint i = PkgCmdIDList.cmdidContestOption1Command; i <= PkgCmdIDList.cmdidContestOption6Command; i++)
            {
                CommandID cmdID = new CommandID(GuidList.guidCF_TesterCmdSet, (int)i);
                OleMenuCommand mc =  (OleMenuCommand)mcs.FindCommand(cmdID);

                mc.Text = "#" + recentContests[(int)(i - PkgCmdIDList.cmdidContestOption1Command)].id.ToString() +
                    " " + recentContests[(int)(i - PkgCmdIDList.cmdidContestOption1Command)].name;
            }
        }
    }
}
