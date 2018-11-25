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
    public partial class AddAwardForm : Form
    {
        private IAwardService _awardService;
        public AddAwardForm(IAwardService awardService)
        {
            InitializeComponent();
            _awardService = awardService;
            nameBox.Validating += ValidateString;
            descriptionBlock.Validating += ValidateString;
        }
        private void ValidateString(object sender, CancelEventArgs args)
        {
            var textBox = sender as TextBox;
            if (!Regex.IsMatch(textBox.Text, @"^.{2,}$"))
            {
                args.Cancel = true;
                errorProvider.SetError(textBox, "не менее 2х символов");
            }
            else
            {
                errorProvider.SetError(textBox, String.Empty);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var award = new Award()
            {
                Title = nameBox.Text,
                Description = descriptionBlock.Text
            };
            _awardService.AddAward(award);            
            Close();
        }
    }
}
