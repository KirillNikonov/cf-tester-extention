namespace NotACompany.CF_Tester
{
    using NotACompany.CF_Tester.Models;
    using System.Drawing;
    using System.Windows.Forms;

    public partial class Dialog : Form
    {
        /// <summary>
        /// Checks of the program crashed on the current tests and adds an appropriate message.
        /// </summary>
        private void CheckIfCrashed()
        {
            if (this.results[currentTest].crashed == true)
            {
                this.TestLabel.Text += " (Crashed)";
                results[currentTest].output = "Program crashed :( - Codeforces Tester";
            }

            this.TestLabel.Location = new Point(this.ContentPanel.Location.X + this.ContentPanel.Width / 2 - this.TestLabel.Width / 2, this.ContentPanel.Location.Y - MINIMAL_INDENT - this.TestLabel.Height);
        }

        /// <summary>
        /// Shows or hides newline symbols in the displayed text.
        /// </summary>
        /// <param name="showNewlines">True, if newline symbols need to be displayed, false otherwise.</param>
        private void ToggleNewlines(bool showNewlines)
        {
            if (showNewlines)
            {
                foreach (Test test in this.tests)
                {
                    test.input = test.input.Replace("\n", "\\n\n");
                    test.output = test.output.Replace("\n", "\\n\n");
                }
                foreach (Result result in this.results)
                {
                    result.output = result.output.Replace("\n", "\\n\n");
                }
            }
            else
            {
                foreach (Test test in this.tests)
                {
                    test.input = test.input.Replace("\\n\n", "\n");
                    test.output = test.output.Replace("\\n\n", "\n");
                }
                foreach (Result result in this.results)
                {
                    result.output = result.output.Replace("\\n\n", "\n");
                }
            }

            this.InputText.Text = this.tests[currentTest].input;
            this.OutputText.Text = this.results[currentTest].output;
            this.ExpectedText.Text = this.tests[currentTest].output;
        }
    }
}
