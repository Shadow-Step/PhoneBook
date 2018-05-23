using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhoneBook.Forms
{
    public partial class AboutForm : Form
    {
        Form1 parentForm = null;
        PhoneBook.Objects.Language language = null;

        //Constructors
        public AboutForm(PhoneBook.Objects.Language language,Form1 parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm;
            this.language = language;
            InitStrings();
        }
        //Methods
        private void InitStrings()
        {
            this.Text = language.AboutProgram;
            label3.Text = "version: " + language.app_version;
        }
        //Events
        private void AboutForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            parentForm.Enabled = true;
        }
        private void OKButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
