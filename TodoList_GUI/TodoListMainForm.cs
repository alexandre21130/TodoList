using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TodoList_Library;

namespace TodoList_GUI
{
    public partial class TodoListMainForm : Form
    {

        private TaskCollection _allTasks;

        /// <summary>
        /// constructor of the main form
        /// </summary>
        public TodoListMainForm()
        {
            InitializeComponent();
            _allTasks = new TaskCollection();
        }


        /// <summary>
        /// click on the button Quit (closes the application) 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close(); //closes the main form, quits the application 
        }


        /// <summary>
        /// Fill the list of all tasks 
        /// </summary>
        private void RefreshListOfTasks(TaskToDo taskToSelect)
        {
            listViewAllTasks.BeginUpdate();
            listViewAllTasks.Items.Clear();
            listViewAllTasks.SelectedItems.Clear();
            foreach(TaskToDo task in _allTasks.Tasks)
            {
                ListViewItem newItem = new ListViewItem(task.Name);
                newItem.Tag = task;
                listViewAllTasks.Items.Add(newItem);
                if (task == taskToSelect)
                    newItem.Selected = true;
            }
            listViewAllTasks.EndUpdate();
            listViewAllTasks.Focus();
            RefreshButtonsAccordingSelectedTask();
        }


        /// <summary>
        /// occurs when the form is loading
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TodoListMainForm_Load(object sender, EventArgs e)
        {
            RefreshListOfTasks(null);
            RefreshButtonsAccordingSelectedTask();
        }

        /// <summary>
        /// click on the button new task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewTask_Click(object sender, EventArgs e)
        {
            //open a dialog to create a new task
            using (FormNewTask frm = new FormNewTask(null))
            {
                //if user completed the creation process, add the new task in list
                if(frm.ShowDialog() == DialogResult.OK)
                {
                    _allTasks.AddTask(frm.Task);
                    RefreshListOfTasks(frm.Task);
                }
            }
        }

        

        /// <summary>
        /// Enable or disable buttons of the main tab according to the current selection
        /// </summary>
        private void RefreshButtonsAccordingSelectedTask()
        {
            int nbSelected = listViewAllTasks.SelectedItems.Count;
            switch(nbSelected)
            {
                case 0:
                    btnNewTask.Enabled = true;
                    btnDelete.Enabled = false;
                    btnOpenSelectedTask.Enabled = false;
                    break;
                case 1:
                    btnNewTask.Enabled = true;
                    btnDelete.Enabled = true;
                    btnOpenSelectedTask.Enabled = true;
                    break;
                default: //multiselection
                    btnNewTask.Enabled = true;
                    btnDelete.Enabled = true;
                    btnOpenSelectedTask.Enabled = true;
                    break;
            }
        }

        /// <summary>
        /// gets the task to do tht is currently selected (null if nothing is selected)
        /// </summary>
        /// <returns></returns>
        private TaskToDo CurrentSelectedTask()
        {
            TaskToDo result = null;
            if(listViewAllTasks.SelectedItems.Count == 1)
                result = (TaskToDo)listViewAllTasks.SelectedItems[0].Tag;
            return result;
        }

        /// <summary>
        /// occurs when the selected item in the list of tasks changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewAllTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshButtonsAccordingSelectedTask();
        }

        /// <summary>
        /// Click on the button delete to delete a task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            TaskToDo taskToDelete = CurrentSelectedTask();
            if(taskToDelete != null)
            {
                _allTasks.RemoveTask(taskToDelete);
                RefreshListOfTasks(null);
            }
        }
    }
}
