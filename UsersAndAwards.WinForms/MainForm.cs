using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entities;
using UsersAndAwards.BLL.Interfaces;
using UsersAndAwards.BLL.Services;
using UsersAndAwards.DAL.Interfaces;
using UsersAndAwards.DAL.Repositories;

namespace UsersAndAwards.WinForms
{
    public partial class MainForm : Form
    {

        public IAwardService AwardService;
        public MainForm()
        {
            InitializeComponent();
            var cs = "Data Source=DESKTOP-2AITIHR;Initial Catalog=UsersAndAwards;Integrated Security=True";
            
            
            AwardService = new AwardService(RepositoryFactory.GetRepository());
            DisplayUsers();
            DisplayAwards();
            usersGrid.ColumnHeaderMouseClick += (sender, args) =>
            {
                if (args.ColumnIndex == 1)
                {
                    var binding = new BindingSource
                    {
                        DataSource = AwardService.Users.OrderBy(user => user.FirstName)
                    };
                    usersGrid.DataSource = binding;
                }
            };

            usersGrid.MouseClick += dataGridView1_MouseClick;
            awardsGrid.MouseClick += AwardsMenu;
        }

        private void DisplayUsers()
        {
            var binding = new BindingSource
            {
                DataSource = AwardService.Users
            };
            usersGrid.DataSource = binding;
        }

        private void DisplayAwards()
        {
            var binding = new BindingSource()
            {
                DataSource = AwardService.Awards
            };
            awardsGrid.DataSource = binding;

            
        }
        
        private void addAward_Click(object sender, EventArgs e)
        {
            (new AddAwardForm(AwardService)).ShowDialog();
            DisplayAwards();
        }


        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                int currentMouseOverRow = usersGrid.HitTest(e.X, e.Y).RowIndex;

                if (currentMouseOverRow >= 0)
                {
                    ContextMenu m = new ContextMenu();
                    var user = AwardService.GetUser(((User)usersGrid.Rows[currentMouseOverRow].DataBoundItem).Id);
                    var toAward = new MenuItem("Наградить");
                    toAward.Click += (o, args) =>
                    {
                        new AwardingForm(AwardService, user.Id).ShowDialog();
                        DisplayUsers();
                    };
                    m.MenuItems.Add(toAward);
                    var del = new MenuItem("Удалить");
                    del.Click += (o, args) =>
                    {
                        AwardService.RemoveUser(user.Id);
                        DisplayUsers();
                    };
                    m.MenuItems.Add(del);
                    var edit = new MenuItem("Редактировать");
                    edit.Click += (o, args) =>
                    {
                        new EditUser(AwardService, user)
                            .ShowDialog();
                        AwardService.UpdateUser(user);
                        DisplayUsers();
                    };
                    m.MenuItems.Add(edit);
                    m.Show(usersGrid, new Point(e.X, e.Y));
                }
            }
        }
        private void AwardsMenu(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                int currentMouseOverRow = awardsGrid.HitTest(e.X, e.Y).RowIndex;
                if (currentMouseOverRow >= 0)
                {
                    var award = AwardService.GetAward(((Award)awardsGrid.Rows[currentMouseOverRow].DataBoundItem).Id);
                    ContextMenu m = new ContextMenu();
                    var del = new MenuItem("Удалить");
                    del.Click += (o, args) =>
                    {
                        AwardService.RemoveAward(award.Id);
                        DisplayAwards();
                        DisplayUsers();
                    };
                    m.MenuItems.Add(del);
                    var edit = new MenuItem("Редактировать");
                    edit.Click += (o, args) =>
                    {
                        
                    };
                    m.MenuItems.Add(edit);
                    m.Show(awardsGrid, new Point(e.X, e.Y));
                }
            }
        }

        private void addUser_Click(object sender, EventArgs e)
        {
            new AddUserForm(AwardService).ShowDialog();
            DisplayUsers();
        }
    }
}
