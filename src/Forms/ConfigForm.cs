using System;
using System.Windows.Forms;

namespace LWDock.Forms
{
    public enum IconQuality : int { Minimum, Small, Medium, Big };

    public partial class ConfigForm : Form
    {
        

        private bool initializing = false;

        public ConfigForm()
        {
            this.initializing = true;
            InitializeComponent();
            this.reloadProperties();
            this.initializing = false;
        }

        private void reloadProperties()
        {
            this.maxPopupsNumber.Value = Settings.getInstance().maxPopups;
            this.keepOnTopCheckBox.Checked = Settings.getInstance().keepOnTop;
            this.noPopupsCheckBox.Checked = Settings.getInstance().maxPopups == 0;
            this.foldersFirstCheck.Checked = Settings.getInstance().foldersFirst;
            this.iconQuality.SelectedIndex = Settings.getInstance().iconQuality;
            this.runWithWindows.Checked = Settings.getInstance().runWithWindows;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Settings.getInstance().maxPopups = this.noPopupsCheckBox.Checked ? 0 : (int)this.maxPopupsNumber.Value;
            Settings.getInstance().keepOnTop = this.keepOnTopCheckBox.Checked;
            Settings.getInstance().foldersFirst = this.foldersFirstCheck.Checked;
            Settings.getInstance().iconQuality = this.iconQuality.SelectedIndex;
            Settings.getInstance().runWithWindows = this.runWithWindows.Checked;
            Settings.getInstance().preloadPopups = this.preloadPopups.Checked;
            Settings.getInstance().OnChanged();
            Settings.getInstance().save();
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.maxPopupsNumber.Enabled = !this.noPopupsCheckBox.Checked;
            this.maxPopupsLabel.Enabled = !this.noPopupsCheckBox.Checked;
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            this.initializing = true;
            Settings.getInstance().resetDefaults();
            this.reloadProperties();
            this.initializing = false;
        }

        private void iconQuality_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.iconQuality.SelectedIndex == (int)IconQuality.Big && !this.initializing)
            {
                MessageBox.Show(this, "Big icon quality requires a lot of memory\nand may slow your computer down!", "Careful!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void preloadPopups_CheckedChanged(object sender, EventArgs e)
        {
            if (this.preloadPopups.Checked)
            {
                MessageBox.Show(this, "If a big folder is chosen and this option is\nenabled it may slow your computer down!", "Careful!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
