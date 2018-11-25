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
    public partial class EditUser : Form
    {
        public EditUser(IAwardService service, User user)
        {
            InitializeComponent();
            firstNameBox.Text = user.FirstName;
            lastNameBox.Text = user.LastName;
            dateTimePicker1.Value = user.BirthDate;
            foreach (var award in service.Awards)
            {
                var isChecked = user.Awards.Exists(award1 => award1.Id == award.Id);
                checkedListBox1.Items.Add(award, isChecked);
            }
            firstNameBox.Validating += ValidateString;
            firstNameBox.Validated += (sender, args) => {
                errorProvider.SetError(firstNameBox, string.Empty);
            };
            lastNameBox.Validating += ValidateString;
            lastNameBox.Validated += (sender, args) => {
                errorProvider.SetError(lastNameBox, string.Empty);
            };
            button1.Click += (sender, args) =>
            {
                user.FirstName = firstNameBox.Text;
                user.LastName = lastNameBox.Text;
                user.BirthDate = dateTimePicker1.Value;
                user.Awards.Clear();
                foreach (var award in checkedListBox1.CheckedItems)
                {
                    user.Awards.Add((Award)award);
                }
                Close();
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

    }
}
