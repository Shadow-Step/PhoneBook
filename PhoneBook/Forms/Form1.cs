using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace PhoneBook
{
    public partial class Form1 : Form
    {
        //Fields
        List<Country> countries = new List<Country>();
        Country current_country             = null;
        Operator current_operator           = null;
        Person current_person               = null;

        PhoneBook.Objects.Language language = null;

        bool changes                        = false;
        string path                         = null;

        //Constructors
        public Form1()
        {
            InitializeComponent();
            language = new PhoneBook.Objects.Language(Objects.LangName.Russian);
            InitStrings();
        }
        public Form1(string lang_cfg)
        {
            InitializeComponent();
            switch (lang_cfg)
            {
                case "ru":
                    language = new PhoneBook.Objects.Language(Objects.LangName.Russian);
                    break;
                case "eng":
                    language = new PhoneBook.Objects.Language(Objects.LangName.English);
                    break;
                default:
                    language = new PhoneBook.Objects.Language(Objects.LangName.Russian);
                    break;
            }
            InitStrings();
        }
        //MainForm
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show(
                    language.DataLost,
                    language.ExitProgram,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2);
            if (result == DialogResult.No)
                e.Cancel = true;
        }
        private void SwitchLanguage(PhoneBook.Objects.LangName langName)
        {
            language.SwitchLanguage(langName);
            InitStrings();
        }
        private void InitStrings()
        {
                        LanguageToolStripMenuItem.Text = language.Lang;
                        OptionsToolStripMenuItem.Text = language.Options;
                        HelpToolStripMenuItem.Text = language.Help;
                        AboutToolStripMenuItem.Text = language.AboutProgram;
                        groupBox1.Text = language.CountryUpper;
                        groupBox2.Text = language.OperatorUpper;
                        groupBox3.Text = language.PersonsUpper;
                        infoView.Columns[0].Text = language.PhoneNumber;
                        infoView.Columns[1].Text = language.Name;
                        infoView.Columns[2].Text = language.Address;
                        fileToolStripMenuItem.Text = language.File;
                        newToolStripMenuItem.Text = language.Create;
                        loadToolStripMenuItem1.Text = language.Open;
                        saveToolStripMenuItem.Text = language.Save;
                        saveAsToolStripMenuItem.Text = language.SaveAs;
                        exitToolStripMenuItem.Text = language.Exit;
                        AddToolStripMenuItem.Text = language.Add;
                        EditToolStripMenuItem.Text = language.EditSelected;
                        DeleteToolStripMenuItem.Text = language.DeleteSelected;
                        AddToolStripMenuItem1.Text = language.Add;
                        EditToolStripMenuItem1.Text = language.EditSelected;
                        DeleteToolStripMenuItem1.Text = language.DeleteSelected;
        }
        //Country list box
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            current_country = (Country)listBox1.SelectedItem;
            UpdateOperatorBox();
        }
        //Operators list box
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            current_operator = (Operator)listBox2.SelectedItem;
            UpdateInfoView();
            //Update
        }
        //InfoView
        private void InfoView_indexChanged(object sender, EventArgs e)
        {
            if (infoView.SelectedItems.Count != 0)
            {
                var item = infoView.SelectedItems[0];
                current_person = current_operator.persons.Find(
                    x => x.Phone == item.Text &&
                    x.Name == item.SubItems[1].Text &&
                    x.Address == item.SubItems[2].Text);
            }
        }
        //Buttons
        private void AddCountryClick(object sender, EventArgs e)
        {
            this.Enabled = false;
            AddForm addform = new AddForm(language, language.Country, this, AddCountry);
            addform.Show();
        }
        private void DeleteCountryButton_click(object sender, EventArgs e)
        {
            if (current_country != null)
            {
                DeleteCountry();
            }
        }

        private void AddOperatorButton_click(object sender, EventArgs e)
        {
            if (current_country != null)
            {
                this.Enabled = false;
                AddForm addform = new AddForm(language, language.Operator, this, AddOperator);
                addform.Show();
            }
        }
        private void DeleteOperatorButton_click(object sender, EventArgs e)
        {
            if (current_operator != null)
            {
                DeleteOperator();
            }
        }

        private void AddPersonButton_click(object sender, EventArgs e)
        {
            if (current_operator != null)
            {
                AddPersonForm addPersonForm = new AddPersonForm(language, this, AddPerson);
                this.Enabled = false;
                addPersonForm.Show();
            }
        }
        private void EditButton_click(object sender, EventArgs e)
        {
            if (current_person != null)
            {
                AddPersonForm addPersonForm = new AddPersonForm(
                    language,
                    this,
                    EditPerson,
                    current_person.Name,
                    current_person.Phone,
                    current_person.Address);
                this.Enabled = false;
                addPersonForm.Show();
            }
        }
        private void DeletePersonButton_click(object sender, EventArgs e)
        {
            if (current_person != null)
            {
                var result = MessageBox.Show(
                           language.DeletePerson + $": '{current_person.Name}'?",
                           language.DeletePerson,
                           MessageBoxButtons.YesNo,
                           MessageBoxIcon.Warning,
                           MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Yes)
                {
                    current_operator.persons.Remove(current_person);
                    current_person = null;
                    DoChanges();
                    UpdateInfoView();
                }
            }
        }
        //Up Menu
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = DialogResult.Yes;
            if (countries.Count != 0 && changes == true)
            {
                result = MessageBox.Show(
                    language.DataLost,
                    language.CreateBase + "?",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2);
            }
            if (result == DialogResult.Yes)
            {
                countries.Clear();
                UpdateAllListBox();
                path = null;
                this.Text = "PhoneBook ";
            }
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (path == null)
            {
                var result = saveFileDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    path = saveFileDialog1.FileName;
                    SaveAll(path);
                }
            }
            else
            {
                //if(changes == true)
                SaveAll(path);
            }
        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                path = saveFileDialog1.FileName;
                SaveAll(path);
            }
        }
        private void loadToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var res = DialogResult.Yes;
            if (changes == true)
            {
                res = MessageBox.Show(
                language.DataLost,
                language.LoadBase + "?",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);
            }
            if (res == DialogResult.Yes)
            {
                var result = openFileDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    path = openFileDialog1.FileName;
                    LoadAll(path);
                }
            }
        }
        //Methonds
        private void UpdateAllListBox()
        {
            UpdateCountryBox();
            UpdateOperatorBox();
        }
        private void UpdateCountryBox()
        {
            listBox1.DataSource = null;
            if (countries != null)
            {
                listBox1.DataSource = countries;
                listBox1.DisplayMember = "Name";
            }
        }
        private void UpdateOperatorBox()
        {
            listBox2.DataSource = null;
            if (current_country != null)
            {
                listBox2.DataSource = current_country.operators;
                listBox2.DisplayMember = "Name";
            }
        }
        private void UpdateInfoView()
        {
            infoView.Items.Clear();
            if (current_operator != null)
            {
                foreach (var item in current_operator.persons)
                {
                    infoView.Items.Add(new ListViewItem(new string[] { item.Phone, item.Name, item.Address }));
                }
            }
        }

        private void AddCountry(string name)
        {
            countries.Add(new Country(name));
            DoChanges();
            UpdateCountryBox();
            listBox1.SelectedItem = countries.Last();

        }
        private void AddOperator(string name)
        {
            if (current_country != null)
            {
                current_country.addOperator(name);
                DoChanges();
                UpdateOperatorBox();
                listBox2.SelectedItem = current_country.operators.Last();
            }
        }
        private void AddPerson(string name, string phone, string address)
        {
            if (current_operator != null)
            {
                current_operator.addPerson(name, phone, address);
                UpdateInfoView();
                DoChanges();
            }
        }
        private void EditPerson(string name, string phone, string address)
        {
            if (current_person != null)
            {
                current_person.Name = name;
                current_person.Phone = phone;
                current_person.Address = address;
                DoChanges();
                UpdateInfoView();
            }
        }
        private void EditCountry(string name)
        {
            if (current_country != null)
            {
                current_country.Name = name;
                DoChanges();
                UpdateCountryBox();
            }
        }
        private void EditOperator(string name)
        {
            if (current_operator != null)
            {
                current_operator.Name = name;
                DoChanges();
                UpdateOperatorBox();
            }
        }

        private void DeleteOperator()
        {
            var result = MessageBox.Show(
                      language.DeleteOperator + $": '{current_operator.Name}'?",
                      language.DeleteOperator,
                      MessageBoxButtons.YesNo,
                      MessageBoxIcon.Warning,
                      MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {
                current_country.operators.Remove(current_operator);
                current_operator = null;
                DoChanges();
                UpdateOperatorBox();
            }
        }
        private void DeleteCountry()
        {
            var result = MessageBox.Show(
                          language.DeleteCountry + $": '{current_country.Name}'?",
                          language.DeleteCountry,
                          MessageBoxButtons.YesNo,
                          MessageBoxIcon.Warning,
                          MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {
                countries.Remove(current_country);
                current_country = null;
                DoChanges();
                UpdateCountryBox();
            }
        }

        private void SaveAll(string path)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(stream, countries);
                stream.Close();
            }
            DelChanges();
        }
        private void LoadAll(string path)
        {
            countries.Clear();
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                countries = (List<Country>)formatter.Deserialize(stream);
                stream.Close();
            }
            this.Text = "PhoneBook " + path;
            DelChanges();
            UpdateAllListBox();
        }

        private void DoChanges()
        {
            if (changes == false)
            {
                changes = true;
                this.Text = "PhoneBook " + path + "*";
            }
        }
        private void DelChanges()
        {
            if (changes == true)
            {
                changes = false;
                this.Text = "PhoneBook " + path;
            }
        }
        //Country Context
        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            AddForm addform = new AddForm(language, language.Country, this, AddCountry);
            addform.Show();
        }
        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (current_country != null)
            {
                AddForm addForm = new AddForm(language, language.Country, current_country.Name, this, EditCountry);
                this.Enabled = false;
                addForm.Show();
            }
        }
        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (current_country != null)
            {
                DeleteCountry();
            }
        }
        //Operator Context
        private void ContextAddOperator_click(object sender, EventArgs e)
        {
            this.Enabled = false;
            AddForm addform = new AddForm(language, language.Operator, this, AddOperator);
            addform.Show();
        }
        private void ContextEditOperator_click(object sender, EventArgs e)
        {
            if (current_country != null)
            {
                AddForm addForm = new AddForm(language, language.Operator, current_operator.Name, this, EditOperator);
                this.Enabled = false;
                addForm.Show();
            }
        }
        private void ContextDelOperator_click(object sender, EventArgs e)
        {
            if (current_operator != null)
            {
                DeleteOperator();
            }
        }

        private void ClearLanguageCheckStates()
        {
            russianToolStripMenuItem.CheckState = CheckState.Unchecked;
            englishToolStripMenuItem.CheckState = CheckState.Unchecked;
        }
        private void russianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(russianToolStripMenuItem.CheckState == CheckState.Checked)
            {
                ClearLanguageCheckStates();
                SwitchLanguage(PhoneBook.Objects.LangName.Russian);
            }
            russianToolStripMenuItem.CheckState = CheckState.Checked;
        }
        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (englishToolStripMenuItem.CheckState == CheckState.Checked)
            {
                ClearLanguageCheckStates();
                SwitchLanguage(PhoneBook.Objects.LangName.English);
            }
            englishToolStripMenuItem.CheckState = CheckState.Checked;
        }
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.AboutForm aboutForm = new Forms.AboutForm(language,this);
            this.Enabled = false;
            aboutForm.Show();
        }
    }
}
