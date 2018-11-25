using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entities;
using UsersAndAwards.BLL.Interfaces;

namespace UsersAndAwards.WinForms
{
    public partial class AwardingForm : Form
    {
        private IAwardService _awardService;
        public AwardingForm(IAwardService awardService, int userId)
        {
            InitializeComponent();
            _awardService = awardService;
            foreach (var award in _awardService.GetPossibleAwards(userId))
            {
                checkedListBox1.Items.Add(award);
            }

     
            button1.Click += (sender, args) =>
            {
                foreach (var award in checkedListBox1.CheckedItems)
                {
                    _awardService.ToAward(userId, ((Award)award).Id);
                }
                Close();
            };
        }



        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
