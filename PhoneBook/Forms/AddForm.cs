using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhoneBook
{
    public partial class AddForm : Form
    {
        public delegate void AddData(string name);

        Form1 parentForm = null;      
        AddData addData = null;

        public PhoneBook.Objects.Language language = null;

        public AddForm(PhoneBook.Objects.Language language, string up_text, Form1 parentForm,AddData deleg)
        {
            InitializeComponent();
            this.language = language;
            InitStrings();
            this.Text = language.Add + " " + up_text;
            this.parentForm = parentForm;
            this.addData = deleg;
        }
        public AddForm(PhoneBook.Objects.Language language, string up_text, string name,Form1 parentForm,AddData deleg)
        {
            InitializeComponent();
            this.language = language;
            InitStrings();
            this.Text = language.Edit + " " + up_text;
            this.textBox1.Text = name;
            this.parentForm = parentForm;
            this.addData = deleg;
        }

        private void InitStrings()
        {
            ConfirmButton.Text = language.Accept;
            CancelButton.Text = language.Cancel;
            label1.Text = language.EnterName;          
        }
        private void CancelButton_click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ConfirmButton_click(object sender, EventArgs e)
        {
            if (textBox1.Text != string.Empty)
            {
                addData(textBox1.Text);
                this.Close();
            }
        }
        private void AddForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            parentForm.Enabled = true;
        }
    }
}
