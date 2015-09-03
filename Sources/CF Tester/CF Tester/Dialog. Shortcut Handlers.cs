namespace NotACompany.CF_Tester
{
    using System.Windows.Forms;

    public partial class Dialog : Form
    {
        /// <summary>
        /// Key down handler.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Key event parameters.</param>
        private void Dialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode >= Keys.D1 && e.KeyCode <= Keys.D9 &&
                (int)(e.KeyCode - Keys.D1) >= 0 &&
                (int)(e.KeyCode - Keys.D1) < this.tests.Count)
            {
                while (this.currentTest > (int)(e.KeyCode - Keys.D1))
                    this.Left_Click(this, new KeyEventArgs(e.KeyData));
                while (this.currentTest < (int)(e.KeyCode - Keys.D1))
                    this.Right_Click(this, new KeyEventArgs(e.KeyData));
            }
            else
            {
                switch (e.KeyCode)
                {
                    case Keys.Q:
                        this.ShowNewlines.Checked = !this.ShowNewlines.Checked;
                        break;
                }
            }
        }

        /// <summary>
        /// Key down handler.
        /// </summary>
        /// <param name="sender">Message.</param>
        /// <param name="e">Key data.</param>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Left:
                    this.Left_Click(this, new KeyEventArgs(keyData));
                    return true;
                case Keys.Right:
                    this.Right_Click(this, new KeyEventArgs(keyData));
                    return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
