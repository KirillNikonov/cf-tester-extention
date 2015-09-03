namespace NotACompany.CF_Tester
{
    using NotACompany.CF_Tester.Models;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    public partial class Dialog : Form
    {
        private int currentTest;
        private List<Test> tests;
        private List<Result> results;

        const int VERTICAL_INDENT = 70;
        const int HORIZONTAL_INDENT = 100;
        const int FOOTER_HEIGHT = 70;
        const int MINIMAL_INDENT = 10;

        /// <summary>
        /// Initializes a new instance of Dialog.
        /// </summary>
        public Dialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of Dialog.
        /// </summary>
        /// <param name="tests">A list of tests.</param>
        /// <param name="results">A list of results.</param>
        public Dialog(List<Test> tests, List<Result> results)
        {
            InitializeComponent();

            int width = 3 * Screen.PrimaryScreen.WorkingArea.Width / 5;
            int height = 3 * Screen.PrimaryScreen.WorkingArea.Height / 5;

            //
            // Form
            //
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Dialog_KeyDown);
            this.ClientSize = new Size(width, height);
            this.VerticalScroll.Value = 0;

            //
            // ToolPanel
            //
            this.ToolPanel.Size = new Size(0, height - FOOTER_HEIGHT);

            //
            // ContentPanel
            //
            this.ContentPanel.Location = new Point(this.ToolPanel.Width + HORIZONTAL_INDENT, VERTICAL_INDENT);
            this.ContentPanel.Size = new Size(width - 2 * HORIZONTAL_INDENT - this.ToolPanel.Width, height - VERTICAL_INDENT - FOOTER_HEIGHT - MINIMAL_INDENT);
            this.ContentPanel.MaximumSize = new Size(width - 2 * HORIZONTAL_INDENT - this.ToolPanel.Width, height - VERTICAL_INDENT - FOOTER_HEIGHT - MINIMAL_INDENT);

            int textBoxWidth = (this.ContentPanel.Size.Width - 30) / 3;

            //
            // Labels
            //
            this.TestLabel.Location = new Point(this.ContentPanel.Location.X + this.ContentPanel.Width / 2 - this.TestLabel.Width / 2, this.ContentPanel.Location.Y - MINIMAL_INDENT - this.TestLabel.Height);
            this.InputLabel.Location = new Point(MINIMAL_INDENT + textBoxWidth / 2 - this.InputLabel.Width / 2, MINIMAL_INDENT);
            this.OutputLabel.Location = new Point(MINIMAL_INDENT + textBoxWidth + textBoxWidth / 2 - this.OutputLabel.Width / 2, MINIMAL_INDENT);
            this.ExpectedLabel.Location = new Point(MINIMAL_INDENT + 2 * textBoxWidth + textBoxWidth / 2 - this.ExpectedLabel.Width / 2, MINIMAL_INDENT);

            int textBoxHeight = this.ContentPanel.Height - this.InputLabel.Height - 3 * MINIMAL_INDENT;

            //
            // Input, Output, Expected
            //
            this.InputText.Width = textBoxWidth;
            this.OutputText.Width = textBoxWidth;
            this.ExpectedText.Width = textBoxWidth;

            this.InputText.Location = new Point(MINIMAL_INDENT, this.InputLabel.Height + 2 * MINIMAL_INDENT);
            this.OutputText.Location = new Point(this.InputText.Location.X + textBoxWidth, this.InputText.Location.Y);
            this.ExpectedText.Location = new Point(this.OutputText.Location.X + textBoxWidth, this.InputText.Location.Y);

            this.InputText.Text = tests[0].input;
            this.OutputText.Text = results[0].output;
            this.ExpectedText.Text = tests[0].output;

            this.InputText.AutoSize = true;
            this.OutputText.AutoSize = true;
            this.ExpectedText.AutoSize = true;

            this.InputText.MinimumSize = new Size(textBoxWidth - 1, textBoxHeight);
            this.OutputText.MinimumSize = new Size(textBoxWidth - 1, textBoxHeight);
            this.ExpectedText.MinimumSize = new Size(textBoxWidth - 1, textBoxHeight);

            this.InputText.MaximumSize = new Size(textBoxWidth - 1, 0);
            this.OutputText.MaximumSize = new Size(textBoxWidth - 1, 0);
            this.ExpectedText.MaximumSize = new Size(textBoxWidth - 1, 0);

            //
            // Left button
            //
            this.Left.Location = new Point(this.ContentPanel.Location.X - MINIMAL_INDENT - this.Left.Size.Width, this.ContentPanel.Location.Y + this.ContentPanel.Size.Height / 2 - this.Left.Size.Height / 2);
            this.Left.Hide();

            //
            // Right button
            //
            this.Right.Location = new Point(this.ContentPanel.Location.X + this.ContentPanel.Size.Width + MINIMAL_INDENT, this.ContentPanel.Location.Y + this.ContentPanel.Size.Height / 2 - this.Right.Size.Height / 2);
            if (tests.Count <= 1) this.Right.Hide();

            //
            // Footer
            //
            this.Footer.Location = new Point(0, height - FOOTER_HEIGHT);
            this.Footer.Size = new Size(width, FOOTER_HEIGHT);

            //
            // OK button
            //
            this.OKButton.Location = new Point(width / 2 - this.OKButton.Size.Width / 2, FOOTER_HEIGHT - 2 * MINIMAL_INDENT - this.OKButton.Size.Height);

            //
            // Miscellaneous
            //
            this.currentTest = 0;
            this.tests = tests;
            this.results = results;

            this.CheckIfCrashed();
        }
    }
}
