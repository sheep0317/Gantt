using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DesignGratt.Controllers;
using DesignGratt.Models;

namespace DesignGratt.Views
{
    public partial class Gratt : Form
    {
        private bool searchbtnFlag=false;
        private bool searchFocusFlag = false;
        private TaskView taskView;
        private int x = 0;
        private int y = 1;
        static public List<TaskWork> taskWork;
        static public int ID = 0;
        public Gratt()
        {
            InitializeComponent();

            searchStop.Stop();
            taskView = new TaskView(DateTime.Now, DateTime.Now.AddDays(14), 18, listShowEvent.Width / 11, 14);
            taskWork = new List<TaskWork>();
            using (var _context = new DGrattContext())
            {
                taskWork = _context.tbTaskWorks.ToList();
            }
            updateListEvent();
            changeList();
            

        }
        bool flag = false;
        Point Lastpoint;

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            flag = true;
            Lastpoint = e.Location;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (flag==true)
            {
                this.Location = new Point(
                    (this.Location.X-Lastpoint.X)+e.X,(this.Location.Y-Lastpoint.Y)+e.Y);
                this.Update();
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            flag = false;
        }
        private void changeList()
        {
            listShowEvent.Clear();
            listShowEvent.Controls.Clear();
            lb1.Visible = false;
            lb2.Visible = false;
            Button btn;
            int dayleght1 = 0;
            int dayleght2 = 0;
            int i;
            bool checkColor = false;
            int year1 = taskView.startList.Year;
            int year2 = year1 + 1;
            for (i = 0; i< 11; i++)
            {
                if(taskView.startList.AddDays(i).Year == year1)
                {
                    dayleght1++;
                }
                else if(taskView.startList.AddDays(i).Year == year2)
                {
                    dayleght2++;
                }
            }
            if(dayleght1 != 0)
            {
                checkColor = true;
                lb1.Text = year1.ToString();
                lb1.Width = taskView.widthDateColumn * dayleght1;
                lb1.ForeColor = Color.White;
                lb1.BackColor = Color.FromArgb(100, 100, 100);
                lb1.Visible = true;
            }
            if(dayleght2 != 0)
            {
                lb2.Location = new Point(lb1.Location.X + lb1.Width, lb1.Location.Y);
                lb2.Text = year2.ToString();
                lb2.Width = taskView.widthDateColumn * dayleght2;
                if (checkColor)
                {
                    lb2.BackColor = Color.FromArgb(183, 183, 183);
                }
                else lb2.BackColor = Color.Aqua;
                lb2.Visible = true;
            }
            

            for (i = 0; i < 11; i++)
            {
                listShowEvent.Columns.Add(taskView.startList.AddDays(i).ToString("dd/MM"), taskView.widthDateColumn, HorizontalAlignment.Left);
            }
            for(i = 0;i<taskWork.Count; i++)
            {   

                
                x = taskWork[i].dateStart.Date.Subtract(taskView.startList.Date).Days;
                y = i+1;
                btn = new Button();
                btn.Location = new Point(this.listShowEvent.Location.X +2 + taskView.widthDateColumn * x,( this.listShowEvent.Location.Y +1) + taskView.heightDateColumn * y);
                btn.Width = taskView.widthDateColumn * taskWork[i].duration;
                btn.Height = taskView.heightDateColumn;
                btn.BackColor = Color.LightCyan;
                listShowEvent.Controls.Add(btn);
            }


        }

        private void Gratt_Load(object sender, EventArgs e)
        {
            changeList();
        }

        private void lastDays_Click(object sender, EventArgs e)
        {
            taskView.startList = taskView.startList.AddDays(-1);
            changeList();
        }

        private void forwardDays_Click(object sender, EventArgs e)
        {
            taskView.startList = taskView.startList.AddDays(1);
            changeList();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            TaskWork task = new TaskWork();

            DialogResult dr = new DialogResult();
            DetailForm form = new DetailForm(ref task);
            dr=form.ShowDialog();
            if(dr==DialogResult.OK)
            {
                if(task.title == "")
                {
                    return;
                }
                using (var _context = new DGrattContext())
                {
                    _context.tbTaskWorks.Add(task);
                    _context.SaveChanges();
                    taskWork = _context.tbTaskWorks.ToList();
                }
                updateListEvent();
                changeList();
                
            }
            else { return; }
            
        }
        private void updateListEvent()
        {
            listEvent.Items.Clear();
            for(int i = 0; i<taskWork.Count; i++)
            {
                ListViewItem item = new ListViewItem(taskWork[i].title);
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, taskWork[i].dateStart.ToString()));
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, taskWork[i].dateEnd.ToString()));
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, taskWork[i].duration.ToString()));
                this.listEvent.Items.Add(item);
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            if(searchbtnFlag==false)
            {
                SearchBox.Text = "Please! input work name.";
                searchbtnFlag = true;
                SearchBox.Visible = true;
                searchStop.Start();
                SearchBox.Focus();
            }
            else
            {
                string searchText = SearchBox.Text.Trim();
                searchFunc(searchText);
                //Application.Exit();
            }
            
        }
        void searchFunc(string searchText)
        {
            ///search owr dday;
        }
        private void searchStop_Tick(object sender, EventArgs e)
        {
            if(SearchBox.Text.Length <= 0 || SearchBox.Text == "Please! input work name.")
            {
                searchbtnFlag = false;
                SearchBox.Visible = false;
                searchStop.Stop();
                SearchBox.Text = "";
            }
        }

        private void SearchBox_Click(object sender, EventArgs e)
        {
            
        }

        private void listEvent_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    
        private void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            Byte count = 0;
            if(e.KeyCode==Keys.Enter)
            {
                if (!searchFocusFlag)
                {
                    for (int i = 0; i < listEvent.Items.Count; i++)
                    {

                        if (listEvent.Items[i].SubItems[0].Text.Contains(SearchBox.Text.Trim()))
                        {
                            count++;
                            listEvent.Focus();
                            listEvent.Items[i].Focused = true;
                            listEvent.FocusedItem.BackColor = Color.Red;
                            searchFocusFlag = true;
                        }
                    }
                }
                if(count<=0)
                {
                    MessageBox.Show("Work not Exist!");
                }
                
            }    
        }

        private void listEvent_Leave(object sender, EventArgs e)
        {
            if (listEvent.FocusedItem is null)
            {
                searchFocusFlag = false;
                return;
            }
            else
            {
                searchFocusFlag = false;
                listEvent.FocusedItem.BackColor = Color.Empty;
            }           
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is made by Hòa-Việt-Độ", "Help");
    
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dbTestController.initializeDB();
        }

        private void delButton_Click(object sender, EventArgs e)
        {
            try
            {
                int index = this.listEvent.SelectedItems[0].Index;
                int ID = taskWork[index].ID;
                TaskWork task = taskWork[index];
                using(var _context = new DGrattContext(task))
                {
                    var dbUser = (from u in _context.tbTaskWorks.AsEnumerable()
                                  where u.ID == ID
                                  select u).SingleOrDefault();
                    _context.tbTaskWorks.Remove(dbUser);
                    _context.SaveChanges();
                    taskWork = _context.tbTaskWorks.ToList();
                    updateListEvent();
                    changeList();
                }
                
            }
            catch { return; }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            int index;
            try
            {
                index = this.listEvent.SelectedItems[0].Index;
            }
            catch
            {
                index = 0;
            }
            int ID = taskWork[index].ID;
            TaskWork task = taskWork[index];

            DialogResult dr = new DialogResult();
            DetailForm form = new DetailForm(ref task);
            dr = form.ShowDialog();
            if (dr == DialogResult.OK)
            {
                if (task.title == "")
                    return;
                using (var _context = new DGrattContext())
                {
                    var dbUser = (from u in _context.tbTaskWorks.AsEnumerable()
                                  where u.ID == ID
                                  select u).SingleOrDefault();
                    dbUser.title = task.title;
                    dbUser.Note = task.Note;
                    dbUser.duration = task.duration;
                    dbUser.dateStart = task.dateStart;
                    dbUser.dateEnd = task.dateEnd;
                    _context.tbTaskWorks.AddOrUpdate(dbUser);
                    _context.SaveChanges();
                    taskWork = _context.tbTaskWorks.ToList();
                }
                updateListEvent();
                changeList();

            }
            else { return; }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            this.taskView.startList = dateTimePicker1.Value;
            changeList();
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
                updateListEvent();
                changeList();
            
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lb1_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer2_Panel1_Click(object sender, EventArgs e)
        {
            if (listEvent.FocusedItem is null)
            {
                searchFocusFlag = false;
                return;
            }
            else
            {
                searchFocusFlag = false;
                for (int i=0;i< listEvent.Items.Count;i++)
                listEvent.Items[i].BackColor = Color.Empty;
            }
        }






        //FocusItem.
        /* 
         listEvent.Items[0].Focused=true;
         listEvent.FocusedItem.BackColor = Color.Red;
     */
    }
}
