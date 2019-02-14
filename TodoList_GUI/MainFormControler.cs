using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TodoList_Library;

namespace TodoList_GUI
{
    /// <summary>
    /// helper class to accumulate all changes that have to be applied to the main screen for a given operation
    /// </summary>
    public class RefreshContext
    {
        public Boolean Description { get; set; }
        public Boolean ListOfTasksHard { get; set; }
        public Boolean ListOfTasksSoft { get; set; }
        public Boolean CurrentTaskHard { get; set; }
        public Boolean CurrentTaskSoft { get; set; }
        public Boolean ContextMenus { get; set; }
        public Boolean FilterOption { get; set; }


        /// <summary>
        /// constructor, by default no one change is needed
        /// </summary>
        public RefreshContext()
        {
            Description = false;
            ListOfTasksHard = false;
            ListOfTasksSoft = false;
            CurrentTaskHard = false;
            CurrentTaskSoft = false;
            ContextMenus = false;
            FilterOption = false;
        }

        /// <summary>
        /// set all parts to refresh to true
        /// </summary>
        public void SetAll()
        {
            Description = true;
            ListOfTasksHard = true;
            ListOfTasksSoft = true;
            CurrentTaskHard = true;
            CurrentTaskSoft = true;
            ContextMenus = true;
            FilterOption = true;
        }


    }


    /// <summary>
    /// controller of the main form
    /// </summary>
    public class MainFormControler
    {
        private MainForm _view;
        private TaskCollection _tasks;
        private String _saveFileName;
        private TaskToDo _selectedTask;
        private TaskToDo _selectedSubtask;
        private Boolean _hideCompletedSubtasks;

        /// <summary>
        /// constructor
        /// </summary>
        public MainFormControler(MainForm form)
        {
            _view = form;
            _tasks = new TaskCollection();
            _saveFileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TasksToDo.txt");
            _selectedTask = null;
            _selectedSubtask = null;
            _hideCompletedSubtasks = false;
        }

        /// <summary>
        /// changes the selected task (inside the list of tasks)
        /// </summary>
        /// <param name="selectedTask"></param>
        /// <param name="refreshContext"></param>
        private void SetSelectedTask(TaskToDo selectedTask, RefreshContext refreshContext)
        {
            _selectedTask = selectedTask;
            refreshContext.ListOfTasksSoft = true;
            refreshContext.CurrentTaskHard = true;
            refreshContext.ContextMenus = true;
            refreshContext.Description = true;
        }

        /// <summary>
        /// changes the selected subtask (inside the current task)
        /// </summary>
        /// <param name="selectedSubtask"></param>
        /// <param name="refreshContext"></param>
        private void SetSelectedSubTask(TaskToDo selectedSubtask, RefreshContext refreshContext)
        {
            _selectedSubtask = selectedSubtask;
            refreshContext.CurrentTaskSoft = true;
            refreshContext.ContextMenus = true;
            refreshContext.Description = true;
        }

        /// <summary>
        /// gets true if completed subtasks have to be hidden
        /// </summary>
        public Boolean HideCompletedSubtasks { get { return _hideCompletedSubtasks; } }

        /// <summary>
        /// init the controller and the main view
        /// </summary>
        public void Init()
        {
            //try to load the saved file
            if (File.Exists(_saveFileName))
                _tasks.LoadFromFile(_saveFileName);
            else
                _view.DisplayMessageBox(String.Format("{0} doesn't exist, create a new empty collection of tasks", _saveFileName), "New file");
            //Refresh all the GUI
            RefreshContext refresher = new RefreshContext();
            refresher.SetAll();
            RefreshGui(refresher);
        }

        /// <summary>
        /// Change the current selected task
        /// </summary>
        /// <param name="selectedTask"></param>
        public void SelectCurrentTask(TaskToDo selectedTask)
        {
            RefreshContext refresher = new RefreshContext();
            SetSelectedTask(selectedTask, refresher);
            RefreshGui(refresher);
        }

        /// <summary>
        /// Change the current selected subtask
        /// </summary>
        /// <param name="selectedSubtask"></param>
        public void SelectCurrentSubTask(TaskToDo selectedSubtask)
        {
            RefreshContext refresher = new RefreshContext();
            SetSelectedSubTask(selectedSubtask, refresher);
            RefreshGui(refresher);
        }

        /// <summary>
        /// Set current selected subtask as completed
        /// </summary>
        public void SetCurrentSubTaskCompleted()
        {
            if (_selectedSubtask == null)
                return;
            RefreshContext refresher = new RefreshContext();
            //change status of current selected subtask
            _selectedSubtask.SetCompleted();
            SaveTasksToFile();
            //add needed update to the GUI
            refresher.CurrentTaskSoft = true;
            refresher.ContextMenus = true;
            refresher.ListOfTasksSoft = true;
            //change selected items if it becomes hidden
            if (_hideCompletedSubtasks) //become hidden, change the selected item for the top level task 
            {
                SetSelectedSubTask(_selectedTask, refresher); //task of top level is now selected
                refresher.CurrentTaskHard = true;
            }
            RefreshGui(refresher);
        }

        /// <summary>
        /// Set current selected subtask as not completed
        /// </summary>
        public void SetCurrentSubTaskNotCompleted()
        {
            if (_selectedSubtask == null)
                return;
            //update task status
            Boolean hasBegun = _selectedSubtask.Progression.HasBegun; //need to know if we will modify one of its child
            _selectedSubtask.SetNotCompleted();
            SaveTasksToFile();
            //update GUI
            RefreshContext refresher = new RefreshContext();
            refresher.ContextMenus = true;
            refresher.ListOfTasksSoft = true;
            if (_hideCompletedSubtasks && hasBegun) //new child will appear
                refresher.CurrentTaskHard = true;
            else //all child were already visible
                refresher.CurrentTaskSoft = true;
            refresher.ListOfTasksSoft = true;
            RefreshGui(refresher);
        }

        /// <summary>
        /// Set the current selected task as completed if it is not completed
        /// if it is already completed, reset it as not completed
        /// </summary>
        public void SetOrResetCurrentTaskCompleted()
        {
            if (_selectedSubtask != null)
            {
                if (_selectedSubtask.IsCompleted)
                    SetCurrentSubTaskNotCompleted();
                else
                    SetCurrentSubTaskCompleted();
            }
        }

        /// <summary>
        /// Switch in edition mode for current task
        /// </summary>
        public void EditCurrentTask()
        {
            if (_selectedTask == null)
                return;
            TaskToDo editedTask = null;
            using (FormTaskEditor form = new FormTaskEditor(_selectedTask))
            {
                if (form.ShowDialog() == DialogResult.OK)
                    editedTask = form.Task;
            }
            if (editedTask != null)
            {
                //proceed to task replacement
                _tasks.ReplaceTask(_selectedTask, editedTask);
                SaveTasksToFile();
                //Change the selection with current task
                _selectedTask = editedTask;
                _selectedSubtask = editedTask;
                //Refresh GUI
                RefreshContext refresher = new RefreshContext();
                refresher.SetAll();
                RefreshGui(refresher);
            }
        }

        /// <summary>
        /// Create a new task
        /// </summary>
        public void CreateNewTask()
        {
            TaskToDo newTask = null;
            using (FormTaskEditor dialog = new FormTaskEditor("- New task"))
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    newTask = dialog.Task;
            }
            if (newTask != null)
            {
                //save the new task
                _tasks.AddTask(newTask);
                SaveTasksToFile();
                //select this new task
                _selectedTask = newTask;
                _selectedSubtask = newTask;
                //Refresh GUI
                RefreshContext refresher = new RefreshContext();
                refresher.SetAll();
                RefreshGui(refresher);
            }
        }

        /// <summary>
        /// Move the current selected subtask up (swap it with its previous brother)
        /// </summary>
        public void MoveSelectedSubtaskUp()
        {
            if (_selectedSubtask == null || !_selectedSubtask.CanMoveUp())
                return;
            //move the subtask up
            _selectedSubtask.MoveUp();
            SaveTasksToFile();
            //Refresh GUI
            RefreshContext refresher = new RefreshContext();
            refresher.CurrentTaskHard = true;
            refresher.ContextMenus = true;
            RefreshGui(refresher);
        }

        /// <summary>
        /// Move the current selected subtask down (swap it with its next brother)
        /// </summary>
        public void MoveSelectedSubtaskDown()
        {
            if (_selectedSubtask == null || !_selectedSubtask.CanMoveDown())
                return;
            //move the subtask down
            _selectedSubtask.MoveDown();
            SaveTasksToFile();
            //Refresh GUI
            RefreshContext refresher = new RefreshContext();
            refresher.CurrentTaskHard = true;
            refresher.ContextMenus = true;
            RefreshGui(refresher);
        }

        /// <summary>
        /// Removes the currently selected task from the collection
        /// </summary>
        public void DeleteSelectedTask()
        {
            if (_selectedTask == null)
                return;
            //remove the task
            _tasks.RemoveTask(_selectedTask);
            SaveTasksToFile();
            //Delete the selection
            _selectedTask = null;
            _selectedSubtask = null;
            //Refresh the GUI
            RefreshContext refresher = new RefreshContext();
            refresher.SetAll();
            RefreshGui(refresher);
        }

        /// <summary>
        /// Move the seleted task in list of tasks up
        /// </summary>
        public void MoveSelectedTaskUp()
        {
            if (_selectedTask == null || !_tasks.CanMoveTaskUp(_selectedTask))
                return;
            //Move task up
            _tasks.MoveTaskUp(_selectedTask);
            SaveTasksToFile(); 
            //Refresh GUI
            RefreshContext refresher = new RefreshContext();
            refresher.ContextMenus = true;
            refresher.ListOfTasksHard = true;
            RefreshGui(refresher);
        }

        /// <summary>
        /// Move the seleted task in list of tasks down
        /// </summary>
        public void MoveSelectedTaskDown()
        {
            if (_selectedTask == null || !_tasks.CanMoveTaskDown(_selectedTask))
                return;
            //Move task down
            _tasks.MoveTaskDown(_selectedTask);
            SaveTasksToFile(); 
            //Refresh GUI
            RefreshContext refresher = new RefreshContext();
            refresher.ContextMenus = true;
            refresher.ListOfTasksHard = true;
            RefreshGui(refresher);
        }

        /// <summary>
        /// dump the current collection of tasks into a file
        /// </summary>
        private void SaveTasksToFile()
        {
            _tasks.SaveToFile(_saveFileName);
        }

        /// <summary>
        /// Deletes the selected subtask
        /// </summary>
        public void DeleteSelectedSubtask()
        {
            if (_selectedSubtask == null || _selectedSubtask.IsRoot) //cannot delete the root task
                return;
            //remove the subtask
            TaskToDo parent = _selectedSubtask.ParentTask;
            parent.RemoveSubtask(_selectedSubtask);
            SaveTasksToFile();
            //change the selection to its parent
            RefreshContext refresher = new RefreshContext();
            SetSelectedSubTask(parent, refresher);
            //update GUI
            refresher.CurrentTaskHard = true;
            refresher.ListOfTasksSoft = true;
            RefreshGui(refresher);
        }

        

        /// <summary>
        /// Changes the filter that is applied on subtasks
        /// </summary>
        /// <param name="hideCompletedSubtasks"></param>
        public void ChangeSubtaskFilter(Boolean hideCompletedSubtasks)
        {
            _hideCompletedSubtasks = hideCompletedSubtasks;
            RefreshContext refresher = new RefreshContext();
            //change the current selected subtask if becomes hidden
            if (_hideCompletedSubtasks && _selectedSubtask != null && _selectedSubtask.IsCompleted)
                SetSelectedSubTask(_selectedTask, refresher); //use the top level task because it is always displayed
            //refresh the GUI
            refresher.CurrentTaskHard = true;
            refresher.FilterOption = true;
            RefreshGui(refresher);
        }

        /// <summary>
        /// Ask to the view to refresh its description panel
        /// description will be filled with the current subtask description of with the current task
        /// </summary>
        private void RefreshDescription()
        {
            String description = String.Empty;
            if (_selectedSubtask != null)
                description = _selectedSubtask.Description;
            else if (_selectedTask != null)
                description = _selectedTask.Description;
            else
                description = "Nothing selected for the moment";
            _view.RefreshDescription(description);
        }


        /// <summary>
        /// Ask to the view to refresh its context menus
        /// </summary>
        private void RefreshContextMenus()
        {
            Boolean newTask = true; //always available
            Boolean editTask = _selectedTask != null;
            Boolean deleteTask = _selectedTask != null;
            Boolean moveUpTask = _selectedTask != null && _tasks.CanMoveTaskUp(_selectedTask);
            Boolean moveDownTask = _selectedTask != null && _tasks.CanMoveTaskDown(_selectedTask);
            Boolean deleteSubtask = _selectedSubtask != null && !_selectedSubtask.IsRoot;
            Boolean moveUpSubtask = _selectedSubtask != null && _selectedSubtask.CanMoveUp();
            Boolean moveDownSubtask = _selectedSubtask != null && _selectedSubtask.CanMoveDown();
            Boolean setCompleted = _selectedSubtask != null && !_selectedSubtask.IsCompleted;
            Boolean setNotCompleted = _selectedSubtask != null && _selectedSubtask.Progression.HasBegun;
            _view.RefreshContextMenus(
                newTask,
                editTask,
                deleteTask,
                moveUpTask,
                moveDownTask,
                deleteSubtask,
                moveUpSubtask,
                moveDownSubtask,
                setCompleted,
                setNotCompleted);
        }

        /// <summary>
        /// Refresh some parts of the GUI
        /// </summary>
        /// <param name="toRefresh"></param>
        private void RefreshGui(RefreshContext toRefresh)
        {
            //list of tasks
            if (toRefresh.ListOfTasksHard)
                _view.HardRefreshListOfTasks(_tasks.Tasks, _selectedTask);
            else if (toRefresh.ListOfTasksSoft)
                _view.SoftRefreshListOfTasks(_selectedTask);
            //current task
            if (toRefresh.CurrentTaskHard)
                _view.HardRefreshCurrentTask(_selectedTask, _selectedSubtask, _hideCompletedSubtasks);
            else if (toRefresh.CurrentTaskSoft)
                _view.SoftRefreshCurrentTask();
            //Description
            if (toRefresh.Description)
                RefreshDescription();
            //context Menus
            if (toRefresh.ContextMenus)
                RefreshContextMenus();
            //filter options
            if (toRefresh.FilterOption)
                _view.RefreshFilterOptions(_hideCompletedSubtasks);
        }
    }
}
