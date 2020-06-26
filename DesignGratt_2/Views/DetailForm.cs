using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DesignGratt.Models;
using DesignGratt.Controllers;

namespace DesignGratt.Views
{
    public partial class DetailForm : Form
    {
        bool drag=false;
        Point lastpoint;
        TaskWork tasks = new TaskWork();

        public DetailForm(ref TaskWork task)
        {
            InitializeComponent();
            
            if(!(task.title is null))
            {
                tasks = task;
                task = tasks;
                tbTitle.Text = task.title;
                rbNote.Text = task.Note;
                tpStart.Value = task.dateStart;
                tbDuration.Text = task.duration.ToString();
                tpEnd.Value = task.dateStart.AddDays(task.duration);
            }
            else
            {
                task = tasks;
            }
            
            tpEnd.Value = tpStart.Value.AddDays(1);
            
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            drag = true;
            lastpoint = e.Location;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag == true)
            {
                this.Location = new Point(
                    (this.Location.X - lastpoint.X) + e.X, (this.Location.Y - lastpoint.Y) + e.Y);
                this.Update();
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;
        }

   

        private void tbTitle_TextChanged(object sender, EventArgs e)
        {
            //bắt buộc nhập
        }

        private void OKbutton_Click(object sender, EventArgs e)
        {

            //Gratt.taskWork.Add(tasks);
            if (tbTitle.Text is null)
            {
                tasks.title = "";
            }
            else tasks.title = tbTitle.Text.Trim();
            if (rbNote.Text is null)
            {
                tasks.Note = "";
            }
            else tasks.Note = rbNote.Text.Trim();
            if(tpStart.Value<=tpEnd.Value)
            {
                tasks.dateStart = tpStart.Value;
                if (tbDuration.Text == "" || tbDuration.Text.Length <= 0 || tbDuration.Text is null)
                {
                    tbDuration.Text = "1";
                }
                tasks.dateEnd = tpStart.Value.AddDays(int.Parse(tbDuration.Text));
            }
            tasks.ID = Gratt.ID++;
            tasks.title = tbTitle.Text;
            tasks.Note = rbNote.Text;
            tasks.dateStart = tpStart.Value;
            tasks.dateEnd = tpEnd.Value;
            try
            {
                tasks.duration = Int32.Parse(tbDuration.Text);
            }
            catch
            {
                tasks.duration = 1;
            }
            //var x = tasks;
            //using (var _context = new DGrattContext(tasks))
            //{
            //    _context.tbTaskWorks.Add(x);
            //    _context.SaveChanges();
            //}

        }

        private void tbDuration_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int i = Int32.Parse(tbDuration.Text);
                if (tbDuration.Text == "" || tbDuration.Text.Trim().Length <= 0)
                {
                    tpEnd.Value = tpStart.Value.AddDays(1);
                }
                else
                    tpEnd.Value = tpStart.Value.AddDays(i);
                //giới hạn nhâp số.
            }
            catch
            {
                tbDuration.Text="";
            }
            if (tbDuration.Text.Trim().Length <= 0)
            {
                tpEnd.Value = tpStart.Value.AddDays(1);
            }

        }

        private void tpStart_ValueChanged(object sender, EventArgs e)
        {
            tpEnd.Value = tpStart.Value.AddDays(1);
            tbDuration.Text = "1";
        }
    }
}
