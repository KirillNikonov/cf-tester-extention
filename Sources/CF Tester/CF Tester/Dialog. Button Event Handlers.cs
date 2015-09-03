namespace NotACompany.CF_Tester
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public partial class Dialog : Form
    {
        /// <summary>
        /// OK button onclick handler.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event parameters.</param>
        private void OKButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Left button onclick handler.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event parameters.</param>
        private void Left_Click(object sender, EventArgs e)
        {
            if (currentTest == 0) return;

            currentTest--;

            this.TestLabel.Text = "Test #" + (currentTest + 1).ToString();
            this.CheckIfCrashed();

            this.InputText.Text = this.tests[currentTest].input;
            this.OutputText.Text = this.results[currentTest].output;
            this.ExpectedText.Text = this.tests[currentTest].output;

            if (currentTest == 0)
            {
                this.Left.Hide();
            }

            this.Right.Show();

            // Keep focus on OK button.
            this.ActiveControl = this.OKButton;
        }

        /// <summary>
        /// Right button onclick handler.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event parameters.</param>
        private void Right_Click(object sender, EventArgs e)
        {
            if (currentTest == this.tests.Count - 1) return;

            currentTest++;

            this.TestLabel.Text = "Test #" + (currentTest + 1).ToString();
            this.CheckIfCrashed();

            this.InputText.Text = this.tests[currentTest].input;
            this.OutputText.Text = this.results[currentTest].output;
            this.ExpectedText.Text = this.tests[currentTest].output;

            this.Left.Show();

            if (currentTest == this.tests.Count - 1)
            {
                this.Right.Hide();
            }

            // Keep focus on OK button.
            this.ActiveControl = this.OKButton;
        }

        /// <summary>
        /// Show newlines checkbox onCheckedChanged handler.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event parameters.</param>
        private void ShowNewlines_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox)
            {
                if ((sender as CheckBox).Checked)
                {
                    this.ShowNewlines.ForeColor = Color.FromArgb(23, 150, 23);
                    this.ToggleNewlines(true);
                }
                else
                {
                    this.ShowNewlines.ForeColor = Color.FromArgb(0, 0, 0);
                    this.ToggleNewlines(false);
                }
            }

            // Keep focus on OK button.
            this.ActiveControl = this.OKButton;
        }
    }
}
