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
    public partial class AddPersonForm : Form
    {
        public delegate void AddData(string name, string phone, string address);

        public PhoneBook.Objects.Language language = null;
        Form1 parentForm = null;
        AddData addData = null;

        public AddPersonForm(PhoneBook.Objects.Language language,Form1 parentForm,AddData deleg)
        {
            InitializeComponent();
            this.language = language;
            InitStrings();

            this.parentForm = parentForm;
            this.addData = deleg;
            this.Text = language.AddPerson;
        }
        public AddPersonForm(PhoneBook.Objects.Language language, Form1 parentForm,AddData deleg,string name,string phone,string address)
        {
            InitializeComponent();
            this.language = language;
            InitStrings();
            this.Text = language.EditPerson;
            this.parentForm = parentForm;
            this.addData = deleg;
            nameBox.Text = name;
            phoneBox.Text = phone;
            addressBox.Text = address;
        }

        private void InitStrings()
        {
            label1.Text = language.Name;
            label2.Text = language.Number;
            label3.Text = language.Address;
            ConfirmButton.Text = language.Accept;
            CancelButton.Text = language.Cancel;
        }
        private void ConfirmButton_click(object sender, EventArgs e)
        {
            if (nameBox.Text != "" && phoneBox.Text != "" && addressBox.Text != "")
            {
                addData(nameBox.Text, phoneBox.Text, addressBox.Text);
                this.Close();
            }
        }
        private void CancelButton_click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void AddPersonForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            parentForm.Enabled = true;
        }
    }
}
