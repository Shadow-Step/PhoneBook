using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Objects
{
    public enum LangName
    {
        Russian,
        English
    }
    public class Language
    {
        LangName lang_name = LangName.Russian;
        public string app_version = "1.0";

        public string AboutProgram = Res.StringRes.AboutProgram;
        public string Accept = Res.StringRes.Accept;
        public string Add = Res.StringRes.Add;
        public string AddPerson = Res.StringRes.AddPerson;
        public string Address = Res.StringRes.Address;
        public string Cancel = Res.StringRes.Cancel;
        public string Country = Res.StringRes.Country;
        public string CountryUpper = Res.StringRes.CountryUpper;
        public string Create = Res.StringRes.Create;
        public string CreateBase = Res.StringRes.CreateBase;
        public string DataLost = Res.StringRes.DataLost;
        public string DeleteCountry = Res.StringRes.DeleteCountry;
        public string DeleteOperator = Res.StringRes.DeleteOperator;
        public string DeletePerson = Res.StringRes.DeletePerson;
        public string DeleteSelected = Res.StringRes.DeleteSelected;
        public string Edit = Res.StringRes.Edit;
        public string EditPerson = Res.StringRes.EditPerson;
        public string EditSelected = Res.StringRes.EditSelected;
        public string EnterName = Res.StringRes.EnterName;
        public string Exit = Res.StringRes.Exit;
        public string ExitProgram = Res.StringRes.ExitProgram;
        public string File = Res.StringRes.File;
        public string Help = Res.StringRes.Help;
        public string Lang = Res.StringRes.Lang;
        public string Name = Res.StringRes.Name;
        public string Number = Res.StringRes.Number;
        public string Open = Res.StringRes.Open;
        public string Operator = Res.StringRes.Operator;
        public string OperatorUpper = Res.StringRes.OperatorUpper;
        public string Options = Res.StringRes.Options;
        public string PersonsUpper = Res.StringRes.PersonsUpper;
        public string PhoneNumber = Res.StringRes.PhoneNumber;
        public string Save = Res.StringRes.Save;
        public string SaveAs = Res.StringRes.SaveAs;
        public string LoadBase = Res.StringRes.LoadBase;

        //default Russian
        public Language()
        {

        }
        public Language(LangName name)
        {
            switch (name)
            {
                case LangName.Russian:
                    SwitchToRussian();
                    break;
                case LangName.English:
                    SwitchToEnglish();
                    break;
            }
        }
        //Methods
        public void SwitchLanguage(LangName name)
        {
            switch (name)
            {
                case LangName.Russian:
                    SwitchToRussian();
                    break;
                case LangName.English:
                    SwitchToEnglish();
                    break;
            }
        }
        public LangName CurrentLanguage() => lang_name;

        private void SwitchToRussian()
        {
        AboutProgram = Res.StringRes.AboutProgram;
        Accept = Res.StringRes.Accept;
        Add = Res.StringRes.Add;
        AddPerson = Res.StringRes.AddPerson;
        Address = Res.StringRes.Address;
        Cancel = Res.StringRes.Cancel;
        Country = Res.StringRes.Country;
        CountryUpper = Res.StringRes.CountryUpper;
        Create = Res.StringRes.Create;
        CreateBase = Res.StringRes.CreateBase;
        DataLost = Res.StringRes.DataLost;
        DeleteCountry = Res.StringRes.DeleteCountry;
        DeleteOperator = Res.StringRes.DeleteOperator;
        DeletePerson = Res.StringRes.DeletePerson;
        DeleteSelected = Res.StringRes.DeleteSelected;
        Edit = Res.StringRes.Edit;
        EditPerson = Res.StringRes.EditPerson;
        EditSelected = Res.StringRes.EditSelected;
        EnterName = Res.StringRes.EnterName;
        Exit = Res.StringRes.Exit;
        ExitProgram = Res.StringRes.ExitProgram;
        File = Res.StringRes.File;
        Help = Res.StringRes.Help;
        Lang = Res.StringRes.Lang;
            LoadBase = Res.StringRes.LoadBase;
            Name = Res.StringRes.Name;
        Number = Res.StringRes.Number;
        Open = Res.StringRes.Open;
        Operator = Res.StringRes.Operator;
        OperatorUpper = Res.StringRes.OperatorUpper;
        Options = Res.StringRes.Options;
        PersonsUpper = Res.StringRes.PersonsUpper;
        PhoneNumber = Res.StringRes.PhoneNumber;
        Save = Res.StringRes.Save;
        SaveAs = Res.StringRes.SaveAs;
    }
        private void SwitchToEnglish()
        {
            AboutProgram = Res.EngRes.AboutProgram;
            Accept = Res.EngRes.Accept;
            Add = Res.EngRes.Add;
            AddPerson = Res.EngRes.AddPerson;
            Address = Res.EngRes.Address;
            Cancel = Res.EngRes.Cancel;
            Country = Res.EngRes.Country;
            CountryUpper = Res.EngRes.CountryUpper;
            Create = Res.EngRes.Create;
            CreateBase = Res.EngRes.CreateBase;
            DataLost = Res.EngRes.DataLost;
            DeleteCountry = Res.EngRes.DeleteCountry;
            DeleteOperator = Res.EngRes.DeleteOperator;
            DeletePerson = Res.EngRes.DeletePerson;
            DeleteSelected = Res.EngRes.DeleteSelected;
            Edit = Res.EngRes.Edit;
            EditPerson = Res.EngRes.EditPerson;
            EditSelected = Res.EngRes.EditSelected;
            EnterName = Res.EngRes.EnterName;
            Exit = Res.EngRes.Exit;
            ExitProgram = Res.EngRes.ExitProgram;
            File = Res.EngRes.File;
            Help = Res.EngRes.Help;
            Lang = Res.EngRes.Lang;
            LoadBase = Res.EngRes.LoadBase;
            Name = Res.EngRes.Name;
            Number = Res.EngRes.Number;
            Open = Res.EngRes.Open;
            Operator = Res.EngRes.Operator;
            OperatorUpper = Res.EngRes.OperatorUpper;
            Options = Res.EngRes.Options;
            PersonsUpper = Res.EngRes.PersonsUpper;
            PhoneNumber = Res.EngRes.PhoneNumber;
            Save = Res.EngRes.Save;
            SaveAs = Res.EngRes.SaveAs;
        }
    }
}
