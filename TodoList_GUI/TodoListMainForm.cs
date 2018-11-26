using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TodoList_Library;

namespace TodoList_GUI
{
    public partial class TodoListMainForm : Form
    {

        private TaskCollection _allTasks;
        private String _templateTask;
        private String _saveFileName;


        /// <summary>
        /// constructor of the main form
        /// </summary>
        public TodoListMainForm()
        {
            InitializeComponent();
            _allTasks = new TaskCollection();
            _saveFileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TasksToDo.txt");
        }

        /// <summary>
        /// try to load the file that contains a template text used to create a new task
        /// returns false if the file doesn't exist (an empty string will be used in this case)
        /// </summary>
        /// <returns></returns>
        private Boolean TryLoadTemplateFile()
        {
            Boolean fileRead = false;
            String path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TemplateTask.txt");
            if (File.Exists(path))
            {
                _templateTask = File.ReadAllText(path);
                fileRead = true;
            }
            else
                _templateTask = String.Empty;
            return fileRead;
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
            TryLoadTemplateFile();
            Boolean fileLoaded = TryLoadAllTasksFromFile();
            if(!fileLoaded)
            {
                MessageBox.Show(String.Format("{0} doesn't exist, create a new empty collection of tasks", _saveFileName));
            }
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
            //open a dialog to create a new task in edition mode
            using (FormTaskEditor frm = new FormTaskEditor(_templateTask))
            {
                //if user completed the creation process, add the new task in list
                if(frm.ShowDialog() == DialogResult.OK)
                {
                    _allTasks.AddTask(frm.Task);
                    RefreshListOfTasks(frm.Task);
                    SaveAllTasksToFile();
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
        private TaskToDo GetCurrentSelectedTask()
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
            TaskToDo taskToDelete = GetCurrentSelectedTask();
            if(taskToDelete != null)
            {
                _allTasks.RemoveTask(taskToDelete);
                RefreshListOfTasks(null);
                SaveAllTasksToFile();
            }
        }


        /// <summary>
        /// click on the button View selected task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenSelectedTask_Click(object sender, EventArgs e)
        {
            OpenSelectedTaskInANewTab();  
        }


        /// <summary>
        /// Open the selected task in a new tab
        /// </summary>
        private void OpenSelectedTaskInANewTab()
        {
            //get the selected task
            TaskToDo currentTask = GetCurrentSelectedTask();
            if (currentTask == null)
                return;
            //create a new tab 
            TabPage newTab = new TabPage(currentTask.Name);
            //link the tab to the task
            newTab.Tag = currentTask;
            tabs.TabPages.Add(newTab);
            //switch to the new tab
            tabs.SelectedTab = newTab;
        }

        /// <summary>
        /// Double click on an item of the main list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewAllTasks_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenSelectedTaskInANewTab();
        }


        /// <summary>
        /// Save all tasks into a file
        /// </summary>
        private void SaveAllTasksToFile()
        {
            _allTasks.SaveToFile(_saveFileName);
        }

        /// <summary>
        /// Try to load the list of tasks from saved file
        /// Returns true if the list of tasks was successfully loaded
        /// Returns false if the file doesn't exist
        /// (throw an exception if the file exists but it is incorrect ...)
        /// </summary>
        /// <returns></returns>
        private Boolean TryLoadAllTasksFromFile()
        {
            Boolean fileLoaded = false;
            if(File.Exists(_saveFileName))
            {
                _allTasks.LoadFromFile(_saveFileName);
                fileLoaded = true;
            }
            return fileLoaded;
        }


        /// <summary>
        /// click on the button Test
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTest_Click(object sender, EventArgs e)
        {
            using (FormTaskEditor dlg = new FormTaskEditor(_templateTask))
            {
                if(dlg.ShowDialog()== DialogResult.OK)
                {
                    TaskToDo myTask = dlg.Task;
                    MessageBox.Show(myTask.Name);
                }
            }
        }
    }
}
