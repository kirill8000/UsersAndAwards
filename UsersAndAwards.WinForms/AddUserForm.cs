using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entities;
using UsersAndAwards.BLL.Interfaces;

namespace UsersAndAwards.WinForms
{
    public partial class AddUserForm : Form
    {
        private IAwardService _awardService;
        public AddUserForm(IAwardService service)
        {
            _awardService = service;
            InitializeComponent();
            firstNameBox.Validating += ValidateString;
            firstNameBox.Validated += (sender, args) => {
                errorProvider.SetError(firstNameBox, string.Empty);
            };
            lastNameBox.Validating += ValidateString;
            lastNameBox.Validated += (sender, args) => {
                errorProvider.SetError(lastNameBox, string.Empty);
            };
           
            dateTimePicker1.Validating += (sender, args) =>
            {
                
                if (DateTime.Now < dateTimePicker1.Value)
                {
                    errorProvider.SetError(dateTimePicker1, "Возраст не может быть отрицательным");
                    args.Cancel = true;
                }
            };
            dateTimePicker1.Validated += (sender, args) =>
            {
                errorProvider.SetError(dateTimePicker1, string.Empty);
            };
        }

        public void ValidateString(object sender, CancelEventArgs args)
        {
            var textBox = sender as TextBox;
            if (!Regex.IsMatch(textBox.Text, @"^\w{2,50}$"))
            {
                args.Cancel = true;
                errorProvider.SetError(textBox, "не менее 2х символов");
            }
            else
            {
                errorProvider.SetError(textBox, String.Empty);
            }
        }

        private void AddUserForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            var user = new User()
            {
                FirstName = firstNameBox.Text,
                LastName = lastNameBox.Text,
                BirthDate = dateTimePicker1.Value
            };
            _awardService.AddUser(user);
            Close();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
