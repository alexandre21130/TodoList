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



    public partial class MainForm : Form
    {
        private MainFormControler _controler;
        private Boolean _disableEvents;
        private Font _fontCompleted;
        private Font _fontNotCompleted;

        public MainForm()
        {
            InitializeComponent();
            _controler = new MainFormControler(this);
            _disableEvents = false;
            _fontCompleted = new Font(this.Font, FontStyle.Strikeout);
            _fontNotCompleted = new Font(this.Font, FontStyle.Regular);
        }

        /// <summary>
        /// Apply format on an item of a listview for the list of tasks
        /// </summary>
        /// <param name="item"></param>
        /// <param name="task"></param>
        private void FormatTaskItem(ListViewItem item, TaskToDo task)
        {
            if (task.IsCompleted)
            {
                item.ForeColor = Color.DarkGreen;
                item.Font = _fontCompleted;
            }
            else
            {
                item.Font = _fontNotCompleted;
                if (task.Progression.HasBegun)
                    item.ForeColor = Color.DarkOrange;
                else
                    item.ForeColor = Color.RoyalBlue;
            }
        }

        /// <summary>
        /// Format a treenode of the current selected task
        /// </summary>
        /// <param name="node"></param>
        /// <param name="task"></param>
        private void FormatSubtaskItem(TreeNode node, TaskToDo task)
        {
            if (task != null)
            {
                if (task.IsCompleted)
                {
                    node.NodeFont = _fontCompleted;
                    node.ForeColor = Color.DarkGreen;
                }
                else
                {
                    node.NodeFont = _fontNotCompleted;
                    if (task.Progression.HasBegun)
                        node.ForeColor = Color.DarkOrange;
                    else
                        node.ForeColor = Color.RoyalBlue;
                }
            }
        }


        /// <summary>
        /// Reload the list of all tasks
        /// 
        /// </summary>
        /// <param name="tasks">the list of all tasks</param>
        /// <param name="selectedTask">the task to select, can be null if nothing is selected</param>
        public void HardRefreshListOfTasks(IEnumerable<TaskToDo> tasks, TaskToDo selectedTask)
        {
            _disableEvents = true;
            listViewAllTasks.Items.Clear();
            foreach (TaskToDo currentTask in tasks)
            {
                ListViewItem newItem = new ListViewItem(currentTask.Name);
                newItem.Tag = currentTask;
                FormatTaskItem(newItem, currentTask);
                listViewAllTasks.Items.Add(newItem);
                if (currentTask == selectedTask)
                {
                    newItem.Selected = true;
                    newItem.Text = "=>   " + newItem.Text;
                }

            }
            _disableEvents = false;
        }

        /// <summary>
        /// Refreshes the list of tasks but keeps existing items
        /// Just changes the font, the color ... 
        /// </summary>
        /// <param name="selectedTask">identify the selected task</param>
        public void SoftRefreshListOfTasks(TaskToDo selectedTask)
        {
            _disableEvents = true;
            foreach (ListViewItem currentItem in listViewAllTasks.Items)
            {
                TaskToDo currentTask = (TaskToDo)currentItem.Tag;
                FormatTaskItem(currentItem, currentTask);
                currentItem.Selected = (currentTask == selectedTask);
                String prefix = currentItem.Selected ? "=>   " : String.Empty;
                currentItem.Text = prefix + currentTask.Name;
            }
            _disableEvents = false;
        }



        /// <summary>
        /// Refresh the description textbox
        /// </summary>
        /// <param name="description"></param>
        public void RefreshDescription(String description)
        {
            descriptionTextBox.Text = String.IsNullOrEmpty(description) ? String.Empty : description;
        }


        /// <summary>
        /// Refresh the treeview of the current task
        /// </summary>
        /// <param name="currentTask"></param>
        /// <param name="selectedSubtask">the subtask that is currently selected, null if nothing is selected</param>
        public void HardRefreshCurrentTask(TaskToDo currentTask, TaskToDo selectedSubtask, Boolean hideCompletedSubtasks)
        {
            _disableEvents = true;
            currentTaskTreeView.Nodes.Clear();
            if (currentTask == null) //nothing to display
            {
                TreeNode newNode = new TreeNode("Nothing selected");
                newNode.Tag = null;
                FormatSubtaskItem(newNode, null);
                currentTaskTreeView.Nodes.Add(newNode);
            }
            else //something to display
            {
                TreeNode newNode = CreateRecursiveNodesFromTask(currentTask, hideCompletedSubtasks);
                currentTaskTreeView.Nodes.Add(newNode);
            }
            currentTaskTreeView.ExpandAll();
            currentTaskTreeView.SelectedNode = FindSubtaskNode(currentTaskTreeView.TopNode, selectedSubtask); //select the right node
            _disableEvents = false;
        }


        /// <summary>
        /// Refresh context menus (and context buttons)
        /// </summary>
        /// <param name="newTask"></param>
        /// <param name="editTask"></param>
        /// <param name="deleteTask"></param>
        /// <param name="moveUpTask"></param>
        /// <param name="moveDownTask"></param>
        /// <param name="deleteSubtask"></param>
        /// <param name="moveUpSubtask"></param>
        /// <param name="moveDownSubtask"></param>
        public void RefreshContextMenus(
            Boolean newTask,
            Boolean editTask,
            Boolean deleteTask,
            Boolean moveUpTask,
            Boolean moveDownTask,
            Boolean deleteSubtask,
            Boolean moveUpSubtask,
            Boolean moveDownSubtask,
            Boolean setCompleted,
            Boolean setNotCompleted)
        {
            btnNewTask.Enabled = cxtMenuListOfTasksNew.Enabled = newTask;
            btnEditTask.Enabled = cxtMenuListOfTasksEdit.Enabled = editTask;
            btnDeleteTask.Enabled = cxtMenuListOfTasksDelete.Enabled = deleteTask;
            cxtMenuListOfTasksMoveUp.Enabled = moveUpTask;
            cxtMenuListOfTasksMoveDown.Enabled = moveDownTask;
            cxtMenuCurrentTaskDeleteSubtask.Enabled = deleteSubtask;
            cxtMenuCurrentTaskMoveUp.Enabled = moveUpSubtask;
            cxtMenuCurrentTaskMoveDown.Enabled = moveDownSubtask;
            cxtMenuCurrentTaskSetCompleted.Enabled = setCompleted;
            cxtMenuCurrentTaskSetNotCompleted.Enabled = setNotCompleted;
        }

        /// <summary>
        /// Find the treenode that matches with a given task
        /// can be called recursively
        /// returns null if there is no node matching with the task
        /// </summary>
        /// <param name="taskToSelect"></param>
        private TreeNode FindSubtaskNode(TreeNode rootNode, TaskToDo taskToFind)
        {
            TreeNode result = null;
            if (taskToFind != null) //if task to find is null, result will always be null
            {
                if (rootNode.Tag == taskToFind) //current node is the the one we are looking for
                    result = rootNode;
                else //not found, try with child nodes
                {
                    foreach (TreeNode childNode in rootNode.Nodes)
                    {
                        result = FindSubtaskNode(childNode, taskToFind);
                        if (result != null) //node found, useless to continue
                            break;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Create a recursive treenode from a task
        /// in mode hideCompletedSubtasks, child nodes for completed subtasks are not created
        /// (top level is always created even if it is completed)
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        private TreeNode CreateRecursiveNodesFromTask(TaskToDo task, Boolean hideCompletedSubtasks)
        {
            TreeNode result = new TreeNode(task.Name);
            result.Tag = task;
            FormatSubtaskItem(result, task);
            foreach (TaskToDo subtask in task.Subtasks)
            {
                if (hideCompletedSubtasks && subtask.IsCompleted) //do not create subtasks that are completed in filtered mode
                    continue;
                result.Nodes.Add(CreateRecursiveNodesFromTask(subtask,hideCompletedSubtasks));
            }
            return result;
        }

        /// <summary>
        /// Refresh the current task panel but without creating new items (reuse current items)
        /// </summary>
        /// <param name="selectedSubtask"></param>
        public void SoftRefreshCurrentTask(TaskToDo selectedSubtask)
        {
            SoftRefreshTreeNodeRecursive(currentTaskTreeView.TopNode);
        }

        /// <summary>
        /// Refresh format of treenode of current task. Can be called
        /// </summary>
        /// <param name="treenode"></param>
        private void SoftRefreshTreeNodeRecursive(TreeNode treenode)
        {
            if (treenode.Tag != null)
            {
                TaskToDo task = (TaskToDo)treenode.Tag;
                FormatSubtaskItem(treenode, task);
            }
            foreach (TreeNode childNode in treenode.Nodes)
                SoftRefreshTreeNodeRecursive(childNode);
        }

        /// <summary>
        /// Display a message box
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        public void DisplayMessageBox(String message, String title)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Display a message box with a given exception
        /// </summary>
        public void ErrorBox(Exception ex)
        {
            ErrorBox(ex.ToString());
        }

        /// <summary>
        /// Dsiplay a message box with an error message
        /// </summary>
        /// <param name="message"></param>
        public void ErrorBox(String message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }




        /// <summary>
        /// Occurs when the form is loading
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            _controler.Init();
        }


        /// <summary>
        /// occurs when the selection of the selected tasks changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewAllTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_disableEvents)
            {
                TaskToDo selectedTask = null;
                if (listViewAllTasks.SelectedItems.Count == 1)
                    selectedTask = (TaskToDo)listViewAllTasks.SelectedItems[0].Tag;
                _controler.SelectCurrentTask(selectedTask);
            }
        }

        /// <summary>
        /// occurs when the selected node in treeview changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void currentTaskTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (!_disableEvents)
            {
                TaskToDo currentSubtask = null;
                TreeNode selectedNode = currentTaskTreeView.SelectedNode;
                if (selectedNode != null)
                    currentSubtask = (TaskToDo)selectedNode.Tag;
                _controler.SelectCurrentSubTask(currentSubtask);
            }
        }


        /// <summary>
        /// Click on the menu New to create a new task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controler.CreateNewTask();
        }

        /// <summary>
        /// occurs when we press a key on the tree view of current task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void currentTaskTreeView_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space: //Space = Set completed or reset
                    _controler.SetOrResetCurrentTaskCompleted();
                    break;
                case Keys.F2: //F2 = switch to edit mode
                    _controler.EditCurrentTask();
                    break;
                case Keys.Up: //Ctrl  + Up arrow = move selected subtask up
                    if (e.Control)
                        _controler.MoveSelectedSubtaskUp();
                    break;
                case Keys.Down: //Ctrl  + Down arrow = move selected subtask down
                    if (e.Control)
                        _controler.MoveSelectedSubtaskDown();
                    break;
                case Keys.Delete: //Suppr = delete the current selected task
                    _controler.DeleteSelectedSubtask();
                    break;
            }

        }

        /// <summary>
        /// occurs when we press a key when list of tasks is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewAllTasks_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F2:
                    _controler.EditCurrentTask();
                    break;
                case Keys.Delete:
                    _controler.DeleteSelectedTask();
                    break;
                case Keys.N: //Ctrl + N => New task
                    if (e.Control)
                        _controler.CreateNewTask();
                    break;
            }
        }

        /// <summary>
        /// Click on the menu edit to switch current task in edition mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controler.EditCurrentTask();
        }

        /// <summary>
        /// click on the menu delete to delete current task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controler.DeleteSelectedTask();
        }

        /// <summary>
        /// resize of the main windows (used to place it in the notification tray)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                MinimizeToTray();
            }
        }

        /// <summary>
        /// Minimizes the form to tray
        /// </summary>
        private void MinimizeToTray()
        {
            notifyIcon1.Visible = true;
            this.Hide();
            notifyIcon1.ShowBalloonTip(3000, "TodoList", "TodoList minimized here", ToolTipIcon.Info);
        }

        /// <summary>
        /// Restore the window from tray
        /// </summary>
        private void RestoreFromTray()
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        /// <summary>
        /// double click on the notify icon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            RestoreFromTray();
        }

        /// <summary>
        /// context menu tray to restore the windows that was minimized
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RestoreFromTray();
        }


        /// <summary>
        /// Closes the application
        /// </summary>
        private void CloseApplication()
        {
            this.Close();
        }

        /// <summary>
        /// context menu tray to close the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseApplication();
        }

        /// <summary>
        /// context menu "Move up" on subtask
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void moveUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controler.MoveSelectedSubtaskUp();
        }

        /// <summary>
        /// context menu "Move down" on subtask
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void moveDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controler.MoveSelectedSubtaskDown();
        }

        /// <summary>
        /// context menu "Set completed" on subtask
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setCompletedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controler.SetCurrentSubTaskCompleted();
        }

        /// <summary>
        /// Context menu "Set not completed" on subtask
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setNotCompletedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controler.SetCurrentSubTaskNotCompleted();
        }

        /// <summary>
        /// context menu "Delete" on a subtask
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _controler.DeleteSelectedSubtask();
        }

       


        /// <summary>
        /// Refresh the main menu
        /// </summary>
        /// <param name="hideCompletedSubtasks"></param>
        public void RefreshFilterOptions(Boolean hideCompletedSubtasks)
        {
            _disableEvents = true;
            checkHideCompletedSubtasks.Checked = hideCompletedSubtasks;
            _disableEvents = false;
        }

        /// <summary>
        /// click on the button New
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewTask_Click(object sender, EventArgs e)
        {
            _controler.CreateNewTask();
        }

        /// <summary>
        /// click on the button edit current selected task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditTask_Click(object sender, EventArgs e)
        {
            _controler.EditCurrentTask();
        }

        /// <summary>
        /// click on button Delete current task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteTask_Click(object sender, EventArgs e)
        {
            _controler.DeleteSelectedTask();
        }

        /// <summary>
        /// Click on checkbox to hide / view completed subtasks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkHideCompletedSubtasks_CheckedChanged(object sender, EventArgs e)
        {
            if (_disableEvents)
                return;
            _controler.ChangeSubtaskFilter(checkHideCompletedSubtasks.Checked);
        }

        /// <summary>
        /// Click on context menu Edit on a subtask
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cxtMenuCurrentTaskEdit_Click(object sender, EventArgs e)
        {
            _controler.EditCurrentTask();
        }

        /// <summary>
        /// context menu New over list of tasks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cxtMenuListOfTasksNew_Click(object sender, EventArgs e)
        {
            _controler.CreateNewTask();
        }

        /// <summary>
        /// context menu Edit over list of tasks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cxtMenuListOfTasksEdit_Click(object sender, EventArgs e)
        {
            _controler.EditCurrentTask();
        }

        /// <summary>
        /// context menu "Delete" over list of tasks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cxtMenuListOfTasksDelete_Click(object sender, EventArgs e)
        {
            _controler.DeleteSelectedTask();
        }

        /// <summary>
        /// context menu "Move up" over list of tasks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cxtMenuListOfTasksMoveUp_Click(object sender, EventArgs e)
        {
            //TODO : to handle
        }

        /// <summary>
        /// context menu "Move down" over list of tasks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cxtMenuListOfTasksMoveDown_Click(object sender, EventArgs e)
        {
            //TODO : to handle
        }
    }
}
