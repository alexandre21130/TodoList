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
    public class MainFormControler
    {
        private MainForm _view;
        private TaskCollection _tasks;
        private String _saveFileName;
        private TaskToDo _selectedTask;
        private TaskToDo _selectedSubtask;

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
        }

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
            _view.HardRefreshListOfTasks(_tasks.Tasks, null);
            _view.HardRefreshCurrentTask(_selectedTask, _selectedSubtask);
            _view.RefreshContextMenuCurrentTask(_selectedSubtask);
            _view.RefreshDescription("Nothing selected for the moment");

        }

        /// <summary>
        /// Change the current selected task
        /// </summary>
        /// <param name="selectedTask"></param>
        public void SelectCurrentTask(TaskToDo selectedTask)
        {
            _selectedTask = selectedTask;
            _selectedSubtask = _selectedTask; //select the higer level in the current task
            _view.SoftRefreshListOfTasks(_selectedTask);
            _view.HardRefreshCurrentTask(_selectedTask, _selectedSubtask);
            _view.RefreshContextMenuCurrentTask(_selectedSubtask);
            _view.RefreshDescription(_selectedTask == null ? "Nothing selected" : _selectedTask.Description);
        }

        /// <summary>
        /// Change the current selected subtask
        /// </summary>
        /// <param name="selectedSubtask"></param>
        public void SelectCurrentSubTask(TaskToDo selectedSubtask)
        {
            _selectedSubtask = selectedSubtask;
            _view.SoftRefreshCurrentTask(_selectedSubtask);
            _view.RefreshContextMenuCurrentTask(_selectedSubtask);
            _view.RefreshDescription(_selectedSubtask != null ? _selectedSubtask.Description : (_selectedTask != null ? _selectedTask.Description : "Nothing selected"));
        }

        /// <summary>
        /// Set current selected subtask as completed
        /// </summary>
        public void SetCurrentSubTaskCompleted()
        {
            if(_selectedSubtask != null)
            {
                _selectedSubtask.SetCompleted();
                SaveTasksToFile();
                _view.SoftRefreshListOfTasks(_selectedTask);
                _view.SoftRefreshCurrentTask(_selectedSubtask);
                _view.RefreshContextMenuCurrentTask(_selectedSubtask);
            }
        }

        /// <summary>
        /// Set current selected subtask as not completed
        /// </summary>
        public void SetCurrentSubTaskNotCompleted()
        {
            if (_selectedSubtask != null)
            {
                _selectedSubtask.SetNotCompleted();
                SaveTasksToFile();
                _view.SoftRefreshListOfTasks(_selectedTask);
                _view.SoftRefreshCurrentTask(_selectedSubtask);
                _view.RefreshContextMenuCurrentTask(_selectedSubtask);
            }
        }

        /// <summary>
        /// Set the current selected task as completed if it is not completed
        /// if it is already completed, reset it as not completed
        /// </summary>
        public void SetOrResetCurrentTaskCompleted()
        {
            if(_selectedSubtask != null)
            {
                if (_selectedSubtask.IsCompleted)
                    _selectedSubtask.SetNotCompleted();
                else
                    _selectedSubtask.SetCompleted();
                SaveTasksToFile();
                _view.SoftRefreshListOfTasks(_selectedTask);
                _view.SoftRefreshCurrentTask(_selectedSubtask);
                _view.RefreshContextMenuCurrentTask(_selectedSubtask);
            }
        }

        /// <summary>
        /// Switch in edition mode for current task
        /// </summary>
        public void EditCurrentTask()
        {
            Boolean updated = false;
            if(_selectedTask != null)
            {
                using (FormTaskEditor form = new FormTaskEditor(_selectedTask))
                {
                    if(form.ShowDialog()== DialogResult.OK)
                    {
                        TaskToDo newTask = form.Task;
                        _tasks.ReplaceTask(_selectedTask, newTask);
                        _selectedTask = newTask;
                        _selectedSubtask = null;
                        updated = true;
                    }
                }
            }
            if(updated)
            {
                SaveTasksToFile();
                _view.HardRefreshListOfTasks(_tasks.Tasks, _selectedTask);
                _view.HardRefreshCurrentTask(_selectedTask, _selectedSubtask);
                _view.RefreshContextMenuCurrentTask(_selectedSubtask);
                _view.RefreshDescription(_selectedTask.Description);
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
            if(newTask != null)
            {
                _tasks.AddTask(newTask);
                SaveTasksToFile();
                _selectedTask = newTask;
                _selectedSubtask = newTask;
                _view.HardRefreshListOfTasks(_tasks.Tasks, _selectedTask);
                _view.HardRefreshCurrentTask(_selectedTask, _selectedSubtask);
                _view.RefreshContextMenuCurrentTask(_selectedSubtask);
                _view.RefreshDescription(_selectedTask.Description);
            }
        }

        /// <summary>
        /// Move the current selected subtask up (swap it with its previous brother)
        /// </summary>
        public void MoveSelectedSubtaskUp()
        {
            if(_selectedSubtask != null)
            {
                if(_selectedSubtask.CanMoveUp())
                {
                    _selectedSubtask.MoveUp();
                    SaveTasksToFile();
                    _view.HardRefreshCurrentTask(_selectedTask, _selectedSubtask);
                    _view.RefreshContextMenuCurrentTask(_selectedSubtask);
                }
            }
        }

        /// <summary>
        /// Move the current selected subtask down (swap it with its next brother)
        /// </summary>
        public void MoveSelectedSubtaskDown()
        {
            if (_selectedSubtask != null)
            {
                if (_selectedSubtask.CanMoveDown())
                {
                    _selectedSubtask.MoveDown();
                    SaveTasksToFile();
                    _view.HardRefreshCurrentTask(_selectedTask, _selectedSubtask);
                    _view.RefreshContextMenuCurrentTask(_selectedSubtask);
                }
            }
        }

        /// <summary>
        /// Removes the currently selected task from the collection
        /// </summary>
        public void DeleteSelectedTask()
        {
            if(_selectedTask != null)
            {
                _tasks.RemoveTask(_selectedTask);
                SaveTasksToFile();
                _selectedTask = null;
                _selectedSubtask = null;
                _view.HardRefreshListOfTasks(_tasks.Tasks, _selectedTask);
                _view.HardRefreshCurrentTask(_selectedTask, _selectedSubtask);
                _view.RefreshContextMenuCurrentTask(_selectedSubtask);
                _view.RefreshDescription("Task was deleted");
            }
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
            if(_selectedSubtask != null)
            {
                if (_selectedSubtask.IsRoot) //cannot delete a root task
                    return;
                TaskToDo parent = _selectedSubtask.ParentTask;
                parent.RemoveSubtask(_selectedSubtask);
                SaveTasksToFile();
                _selectedSubtask = parent;
                _view.SoftRefreshListOfTasks(_selectedTask);
                _view.HardRefreshCurrentTask(_selectedTask, _selectedSubtask);
                _view.RefreshContextMenuCurrentTask(_selectedSubtask);
                _view.RefreshDescription(_selectedSubtask.Description);
            }
        }


    }
}
