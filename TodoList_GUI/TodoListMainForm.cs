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

        private Font _fontForCompletedTasks;
        private Font _fontForNotCompletedTasks;
        Color _colorForCompletedTasks;
        Color _colorForNotCompletedTasks;
        Color _colorForTasksInProgress;


        /// <summary>
        /// constructor of the main form
        /// </summary>
        public TodoListMainForm()
        {
            InitializeComponent();
            _allTasks = new TaskCollection();
            _saveFileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TasksToDo.txt");

            _fontForCompletedTasks = new Font(this.Font, FontStyle.Strikeout);
            _fontForNotCompletedTasks = new Font(this.Font, FontStyle.Regular);
            _colorForCompletedTasks = Color.DarkGreen;
            _colorForNotCompletedTasks = this.ForeColor;
            _colorForTasksInProgress = Color.DarkOrange;

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
                _templateTask = File.ReadAllText(path, Encoding.UTF8);
                fileRead = true;
            }
            else
                _templateTask = String.Empty;
            return fileRead;
        }

        /// <summary>
        /// Closes the application
        /// </summary>
        private void CloseApplication()
        {
            this.Close(); //closes the main form, quits the application 
        }


        /// <summary>
        /// click on the button Quit (closes the application) 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            CloseApplication();
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
                TaskProgression progression = task.Progression;
                if (progression.IsCompleted)
                {
                    newItem.ForeColor = _colorForCompletedTasks;
                    newItem.Font = _fontForCompletedTasks;
                }
                else if (progression.HasBegun)
                {
                    newItem.ForeColor = _colorForTasksInProgress;
                    newItem.Font = _fontForNotCompletedTasks;
                }
                else
                {
                    newItem.ForeColor = _colorForNotCompletedTasks;
                    newItem.Font = _fontForNotCompletedTasks;
                }

                listViewAllTasks.Items.Add(newItem);
                if (task == taskToSelect)
                    newItem.Selected = true;
            }
            listViewAllTasks.EndUpdate();
            listViewAllTasks.Focus();
            EnableButtonsAccordingSelectedTask();
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
            EnableButtonsAccordingSelectedTask();
        }

        /// <summary>
        /// Open a dialog to create a new task and add this new task in the list
        /// </summary>
        private void CreateNewTask()
        {
            //open a dialog to create a new task in edition mode
            using (FormTaskEditor frm = new FormTaskEditor(_templateTask))
            {
                //if user completed the creation process, add the new task in list
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    TaskToDo newTask = frm.Task;
                    _allTasks.AddTask(newTask);
                    RefreshListOfTasks(newTask);
                    SaveAllTasksToFile();
                    //Open the newtask in a tab
                    OpenTaskInTab(newTask);
                }
            }
        }

        /// <summary>
        /// click on the button new task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewTask_Click(object sender, EventArgs e)
        {
            CreateNewTask();
        }

        
        /// <summary>
        /// returns true if the main tab is currently selected, else returns false
        /// </summary>
        /// <returns></returns>
        private Boolean IsMainTabSelected()
        {
            return tabs.SelectedIndex == 0;
        }

        /// <summary>
        /// Enable or disable buttons of the main tab according to the current selection
        /// </summary>
        private void EnableButtonsAccordingSelectedTask()
        {
            if(IsMainTabSelected()) //we are on the main tab
            {
                closeToolStripMenuItem.Enabled = false;
                int nbSelected = listViewAllTasks.SelectedItems.Count;
                switch (nbSelected)
                {
                    case 0:
                        newToolStripMenuItem.Enabled = true;
                        deleteToolStripMenuItem.Enabled = false;
                        openToolStripMenuItem.Enabled = false;
                        editToolStripMenuItem.Enabled = false;
                        break;
                    case 1:
                        newToolStripMenuItem.Enabled = true;
                        deleteToolStripMenuItem.Enabled = true;
                        openToolStripMenuItem.Enabled = true;
                        editToolStripMenuItem.Enabled = true;
                        break;
                    default: //multiselection
                        newToolStripMenuItem.Enabled = true;
                        deleteToolStripMenuItem.Enabled = true;
                        openToolStripMenuItem.Enabled = true;
                        editToolStripMenuItem.Enabled = false;
                        break;
                }
            }
            else //we are on a specific task tab
            {
                closeToolStripMenuItem.Enabled = true;
                newToolStripMenuItem.Enabled = true;
                deleteToolStripMenuItem.Enabled = true;
                openToolStripMenuItem.Enabled = false;
                editToolStripMenuItem.Enabled = true;
            }
        }

        /// <summary>
        /// gets the task to do tht is currently selected (null if nothing is selected)
        /// On the main tab, result is the task that is currently selected (if there is only one task)
        /// if we are on an other tab, result is the task associated to the current tab
        /// </summary>
        /// <returns></returns>
        private TaskToDo GetCurrentSelectedTask()
        {
            TaskToDo result = null;
            if(IsMainTabSelected())
            {
                if (listViewAllTasks.SelectedItems.Count == 1)
                    result = (TaskToDo)listViewAllTasks.SelectedItems[0].Tag;
            }
            else
            {
                result = (TaskToDo)tabs.SelectedTab.Tag;
            }
            
            return result;
        }

        /// <summary>
        /// occurs when the selected item in the list of tasks changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewAllTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableButtonsAccordingSelectedTask();
        }

        /// <summary>
        /// Delete the current selected task
        /// </summary>
        private void DeleteCurrentSelectedTask()
        {
            TaskToDo taskToDelete = GetCurrentSelectedTask();
            if (taskToDelete != null)
            {
                //remove the task from the internal collection
                _allTasks.RemoveTask(taskToDelete);
                //save the modified collection
                SaveAllTasksToFile();
                //redraw the main list
                RefreshListOfTasks(null);
                //close matching tab if opened
                TabPage openedTab = FindTabByTask(taskToDelete);
                if (openedTab != null)
                    tabs.TabPages.Remove(openedTab);
            }
        }

        /// <summary>
        /// Click on the button delete to delete a task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteCurrentSelectedTask();
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
        /// find a tab that is linked to a task
        /// returns null if there is no tab linked to the task
        /// </summary>
        /// <param name="taskToFind"></param>
        /// <returns></returns>
        private TabPage FindTabByTask(TaskToDo taskToFind)
        {
            TabPage result = null;
            for(int i=1; i<tabs.TabCount;i++)
            {
                TabPage currentPage = tabs.TabPages[i];
                TaskToDo task = (TaskToDo) currentPage.Tag;
                if(task == taskToFind)
                {
                    result = currentPage;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// open a given task in a new tab
        /// if there is already a tab linked to this task, switch to this tab
        /// </summary>
        /// <param name="task"></param>
        private void OpenTaskInTab(TaskToDo task)
        {
            //look if the task is already opened in an existing tab
            TabPage newTab = FindTabByTask(task);
            if (newTab == null)//task is not opened, create a new tab
            {
                //create a new tab with its components
                newTab = new TabPage(task.Name);
                TreeView treeView = new TreeView();
                treeView.Name = "treeViewTask";
                treeView.KeyDown += TreeView_KeyDown;
                newTab.Controls.Add(treeView);
                treeView.Dock = DockStyle.Fill;
                //link the tab to the task
                newTab.Tag = task;
                tabs.TabPages.Add(newTab);
                //Draw the tab content
                RefreshTaskTab(newTab, true);
            }

            //switch to the new tab
            tabs.SelectedTab = newTab;
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
            OpenTaskInTab(currentTask);
        }

        /// <summary>
        /// occurs when a key is pressed on a treeview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeView_KeyDown(object sender, KeyEventArgs e)
        {
            //get the treeview that raised the event
            TreeView tv = (TreeView)sender;
            //get the selected task
            TaskToDo selectedTask = GetSelectedTaskInTreeView(tv);
            //do something according to the key that was pressed
            switch(e.KeyCode)
            {
                case Keys.Space: //spacebar => set the current selected task as completed or not completed
                    if(selectedTask != null)
                    {
                        if (selectedTask.IsCompleted)
                            selectedTask.SetNotCompleted();
                        else
                            selectedTask.SetCompleted();

                        RefreshCurrentTaskTab();
                        SaveAllTasksToFile();
                        RefreshListOfTasks(GetCurrentSelectedTask());
                    }
                    break;
                case Keys.F2: //F2 => switch to edit mode
                    EditCurrentSelectedTask();
                    break;
            }
        }


        /// <summary>
        /// Update the presentation of the treeview (no changes on the nodes themselves)
        /// </summary>
        private void Treeview_SoftRefresh(TreeView tv)
        {
            TreeNode_SoftRefresh(tv.Nodes[0]);
        }

        /// <summary>
        /// refresh the presentation of a given task node in a treeview
        /// do a recursive call to refresh its subnodes too
        /// </summary>
        /// <param name="tn"></param>
        private void TreeNode_SoftRefresh(TreeNode tn)
        {
            //update the presentation of the current node
            TaskToDo task = (TaskToDo)tn.Tag;
            if(task.IsCompleted)
            {
                tn.NodeFont = _fontForCompletedTasks;
                tn.ForeColor = _colorForCompletedTasks;
            }
            else
            {
                tn.NodeFont = _fontForNotCompletedTasks;
                tn.ForeColor = _colorForNotCompletedTasks;
            }
            //update child nodes recursively
            foreach (TreeNode subNode in tn.Nodes)
                TreeNode_SoftRefresh(subNode);

        }

        /// <summary>
        /// Refresh the display of a given task tab 
        /// </summary>
        /// <param name="tabToRefresh"></param>
        private void RefreshTaskTab(TabPage tabToRefresh, Boolean hardRefresh)
        {
            //get the treeview associated to the tab
            TreeView treeView = (TreeView) (tabToRefresh.Controls["treeViewTask"]);
            if(hardRefresh)
            {
                //get the task associated to the tab
                TaskToDo task = (TaskToDo)tabToRefresh.Tag;
                //save the selected task in treeview
                TaskToDo selectedTask = GetSelectedTaskInTreeView(treeView);
                //Refresh tab title
                tabToRefresh.Text = task.Name;
                //draw the task in the treeView
                DrawTaskInTreeView(treeView, task);
                //Select the previously selected task
                SelectTaskInTreeView(treeView, selectedTask);
            }
            else
            {
                Treeview_SoftRefresh(treeView);
            }
            
        }

        /// <summary>
        /// Try to find a node that is linked to a given task
        /// returns null if there is no node with this task
        /// </summary>
        /// <param name="taskToFind"></param>
        /// <returns></returns>
        private TreeNode FindNode(TreeNode rootNode, TaskToDo taskToFind)
        {
            TreeNode result = null;
            TaskToDo currentTask = (TaskToDo) rootNode.Tag;
            if (currentTask == taskToFind) //current node matches
                result = rootNode;
            else //current node doesn't match, try with child nodes (recursive call)
            {
                foreach(TreeNode subNode in rootNode.Nodes)
                {
                    result = FindNode(subNode, taskToFind);
                    if (result != null)
                        break;
                }
            }
            return result;
        }



        /// <summary>
        /// Select the node that is linked to a given task in a treeview
        /// Select nothing if the task is not present in the treeview
        /// </summary>
        /// <param name="tv"></param>
        /// <param name="task"></param>
        private void SelectTaskInTreeView(TreeView tv, TaskToDo task)
        {
            tv.SelectedNode = FindNode(tv.Nodes[0], task);
        }

        /// <summary>
        /// Gets the selected task in a given treeview
        /// returns null if there is no selected task
        /// </summary>
        /// <param name="tv"></param>
        /// <returns></returns>
        private TaskToDo GetSelectedTaskInTreeView(TreeView tv)
        {
            TaskToDo result = null;
            if(tv.SelectedNode != null)
            {
                result = (TaskToDo)tv.SelectedNode.Tag;
            }

            return result;
        }

        /// <summary>
        /// Draw a task in a given treeView control
        /// </summary>
        /// <param name="tv"></param>
        /// <param name="task"></param>
        private void DrawTaskInTreeView(TreeView tv, TaskToDo task)
        {
            tv.Nodes.Clear();
            TreeNode rootNode = tv.Nodes.Add(task.Name);
            DrawTaskInTreeNodeRecursive(rootNode, task);
            tv.ExpandAll();
        }

        /// <summary>
        /// Draw a task in a treenode and create all subnodes for subtasks of this task
        /// This function should be called recursively at each level of the tree to draw all subtasks
        /// </summary>
        /// <param name="node"></param>
        /// <param name="task"></param>
        private void DrawTaskInTreeNodeRecursive(TreeNode node, TaskToDo task)
        {
            //draw the node at current level
            node.Text = task.Name;
            node.Tag = task;
            if(task.IsCompleted)
            {
                node.ForeColor = _colorForCompletedTasks;
                node.NodeFont = _fontForCompletedTasks;
            }
            else
            {
                node.ForeColor = _colorForNotCompletedTasks;
                node.NodeFont = _fontForNotCompletedTasks;
            }
            //draw each subtask
            foreach(TaskToDo subtask in task.Subtasks)
            {
                TreeNode subNode = node.Nodes.Add(subtask.Name);
                DrawTaskInTreeNodeRecursive(subNode, subtask);
            }
        }

        /// <summary>
        /// update the content of the current tab according to its linked task
        /// </summary>
        private void RefreshCurrentTaskTab()
        {
            //do nothing if selected tab is the main list
            if (IsMainTabSelected())
                return;
            //get the current tab
            TabPage currentTab = tabs.SelectedTab;
            //Refresh it
            RefreshTaskTab(currentTab, false);
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

        /// <summary>
        /// Switch to edition mode for the current selected task and update it 
        /// </summary>
        private void EditCurrentSelectedTask()
        {
            TaskToDo currentTask = GetCurrentSelectedTask();
            if (currentTask == null) //nothing to do if there is no selected task
                return;

            //edit the task
            TaskToDo newTask = null;
            using (FormTaskEditor frm = new FormTaskEditor(currentTask))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    newTask = frm.Task;
                    //task was edited, proceed to the swap
                    _allTasks.ReplaceTask(currentTask, newTask);
                    //save the replacement
                    SaveAllTasksToFile();
                    //Refresh the list of tasks
                    RefreshListOfTasks(newTask);
                    //refresh tab of the edited task if already opened
                    TabPage openedTab = FindTabByTask(currentTask);
                    if (openedTab != null)
                    {
                        openedTab.Tag = newTask;
                        RefreshTaskTab(openedTab, true);
                    }
                }
            }
        }

        /// <summary>
        /// click on the button Edit current task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditTask_Click(object sender, EventArgs e)
        {
            EditCurrentSelectedTask();
        }

        /// <summary>
        /// double click on the tab component
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabs_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            CloseSelectedTab();
        }

        /// <summary>
        /// occurs each time we change the selected tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableButtonsAccordingSelectedTask();
        }

        /// <summary>
        /// Closes the currently selected tab (do nothing if selected tab is the main tab)
        /// </summary>
        private void CloseSelectedTab()
        {
            if (!IsMainTabSelected())
                tabs.TabPages.Remove(tabs.SelectedTab);
        }

        /// <summary>
        /// click on the button Close Selected tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCloseSelectedTab_Click(object sender, EventArgs e)
        {
            CloseSelectedTab();
        }

        /// <summary>
        /// occurs on KeyDown on the main List of tasks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewAllTasks_KeyDown(object sender, KeyEventArgs e)
        {
            TaskToDo currentTask = GetCurrentSelectedTask();
            if (currentTask == null) //do nothing if there is no selected task
                return;
            switch(e.KeyCode)
            {
                case Keys.Space:
                    OpenSelectedTaskInANewTab();
                    break;
                case Keys.F2:
                    EditCurrentSelectedTask();
                    break;
                case Keys.Delete:
                    DeleteCurrentSelectedTask();
                    break;
            }
        }

        /// <summary>
        /// Occurs when a key is pressed at the level of the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TodoListMainForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Escape: //Escape => close the current tab if specific task, closes the application if mainTab
                    if (IsMainTabSelected())
                        CloseApplication();
                    else
                        CloseSelectedTab();
                    break;

            }

        }

        /// <summary>
        /// Click on the menu New task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewTask();
        }

        /// <summary>
        /// Click on the menu Open
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSelectedTaskInANewTab();
        }

        /// <summary>
        /// Click on the Edit menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditCurrentSelectedTask();
        }

        /// <summary>
        /// Click on the menu Delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteCurrentSelectedTask();
        }

        /// <summary>
        /// click on the menu Close current task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseSelectedTab();
        }

        /// <summary>
        /// Click on the Menu Close application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseApplication();
        }
    }
}
