using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList_Library
{

    /// <summary>
    /// Class to represent a task to do
    /// </summary>
    public class TaskToDo
    {
        #region PRIVATE_FIELDS
        private TaskToDo _parentTask;
        private String _name;
        private List<TaskToDo> _subtasks;
        private String _description;
        private Boolean _completed;
        #endregion PRIVATE_FIELDS

        /// <summary>
        /// constructor
        /// </summary>
        public TaskToDo(String name, String description)
        {
            _parentTask = null;
            _name = name;
            _subtasks = new List<TaskToDo>();
            _description = description;
            _completed = false;
        }        

        /// <summary>
        /// gets or sets the name of the task
        /// </summary>
        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Gets or sets the description of the task
        /// </summary>
        public String Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// set the current task as completed
        /// Can update the completed status of its child and its parents if requested
        /// </summary>
        private void SetCompletedStatus(Boolean isCompleted, Boolean updateParents, Boolean updateChild)
        {
            _completed = isCompleted; //set the task as completed or not
            //set all subtasks as completed or not if requested
            if (updateChild)
            {
                foreach (TaskToDo subtask in _subtasks)
                {
                    subtask.SetCompletedStatus(isCompleted, false, true); //descending recursivity to change the status of all child and child of child ...
                }
            }
            //refresh parents status if requested (ascending recursivity)
            if(updateParents)
            {
                if (_parentTask != null) //only if we have a parent
                    _parentTask.RefreshCompletedStatus(true);
            }
        }

        /// <summary>
        /// Set the current task as completed
        /// Report the completed status in all its subtasks and refresh the parent status
        /// </summary>
        public void SetCompleted()
        {
            SetCompletedStatus(true, true, true);
        }

        /// <summary>
        /// Set the current task as not completed
        /// Report this status in all its subtasks and refresh the parent status
        /// </summary>
        public void SetNotCompleted()
        {
            SetCompletedStatus(false, true, true);
        }

        /// <summary>
        /// Update the completed status of a task regarding its substasks (do nothing if there is no child)
        /// Can update recursively its parents too
        /// </summary>
        private void RefreshCompletedStatus(Boolean updateParents)
        {
            //refresh completed status of current task if there are child, do nothing if there is no child
            if(_subtasks.Count > 0)
            {
                _completed = AllSubtasksAreCompleted();
            }
            //update the parent and its parents ... if requested
            if (updateParents && _parentTask != null)
                _parentTask.RefreshCompletedStatus(true);
        }

        /// <summary>
        /// returns true if all subtasks are completed, else returns false
        /// if there is no subtasks, returns true
        /// </summary>
        private Boolean AllSubtasksAreCompleted()
        {
            if (IsTerminalTask)
                return true;
            else
                return _subtasks.Find(x => !x.IsCompleted) == null; //all subtasks are completed if we cannot find any subtask that is not completed
        }

        /// <summary>
        /// Gets the task progression
        /// </summary>
        public TaskProgression Progression { get { return new TaskProgression(this); } }


        /// <summary>
        /// returns true if a task is completed, else returns false
        /// </summary>
        public Boolean IsCompleted
        {
            get
            {
                return _completed;
            }
        }

        /// <summary>
        /// returns true if this task has child tasks (subtasks), else returns false
        /// </summary>
        public Boolean HasChild { get { return _subtasks.Count > 0; } }

        /// <summary>
        /// returns true if this task is not composed of subtasks (it is terminal), else returns false
        /// </summary>
        public Boolean IsTerminalTask { get { return !HasChild; } }

        /// <summary>
        /// Gets the parent task of this task (null if there is no parent)
        /// </summary>
        public TaskToDo ParentTask { get { return _parentTask; } }

        /// <summary>
        /// returns true if the current task has no parent, else returns false
        /// </summary>
        public Boolean IsRoot { get { return _parentTask == null; } }

        /// <summary>
        /// Get an enumeration of all subtasks of this task (will be empty if it is terminal)
        /// </summary>
        public IEnumerable<TaskToDo> Subtasks { get { return _subtasks; } }

        /// <summary>
        /// Add a new subtask to the current task
        /// </summary>
        /// <param name="newSubtask"></param>
        /// <returns></returns>
        public TaskToDo AddSubtask(TaskToDo newSubtask)
        {
            newSubtask._parentTask = this; //create the uplink between this and the child
            _subtasks.Add(newSubtask);
            RefreshCompletedStatus(true); //refresh the completed status of current task and all its parents
            return newSubtask;
        }

        /// <summary>
        /// Remove a subtask
        /// </summary>
        /// <param name="subtaskToRemove"></param>
        public void RemoveSubtask(TaskToDo subtaskToRemove)
        {
            if (_subtasks.Contains(subtaskToRemove))
            {
                _subtasks.Remove(subtaskToRemove);
                RefreshCompletedStatus(true); //refresh the completed status of current task and all its parents
            }
        }

        /// <summary>
        /// returns the taskTodo that is just before the current task at the same level
        /// returns null if there is no previous brother 
        /// </summary>
        /// <returns></returns>
        public TaskToDo PreviousBrother()
        {
            TaskToDo result = null;
            if(_parentTask != null)
            {
                int currentIndex = _parentTask._subtasks.IndexOf(this);
                if (currentIndex > 0)
                    result = _parentTask._subtasks[currentIndex - 1];
            }
            return result;
        }

        /// <summary>
        /// returns the taskTodo that is just after the current task at the same level
        /// returns null if there is no next brother 
        /// </summary>
        /// <returns></returns>
        public TaskToDo NextBrother()
        {
            TaskToDo result = null;
            if (_parentTask != null)
            {
                int currentIndex = _parentTask._subtasks.IndexOf(this);
                if (currentIndex < (_parentTask._subtasks.Count - 1))
                    result = _parentTask._subtasks[currentIndex + 1];
            }
            return result;
        }

        /// <summary>
        /// move up the current task (switch it with its previous brother)
        /// </summary>
        public void MoveUp()
        {
            if(_parentTask != null)
            {
                Int32 index = _parentTask._subtasks.IndexOf(this);
                if(index > 0) //there is a "previous brother" so we can switch our place with it
                {
                    _parentTask._subtasks.RemoveAt(index); //remove this
                    _parentTask._subtasks.Insert(index - 1, this); //and reinsert it one case before
                }
            } 
        }

        /// <summary>
        /// move down the current task (switch it with its next brother)
        /// </summary>
        public void MoveDown()
        {
            if (_parentTask != null)
            {
                Int32 index = _parentTask._subtasks.IndexOf(this);
                if (index < (_parentTask._subtasks.Count -1)) //there is a "next brother" so we can switch our place with it
                {
                    _parentTask._subtasks.RemoveAt(index); //remove original
                    _parentTask._subtasks.Insert(index + 1, this); //Copy current subtask one case after
                }
            }
        }


    } //end of the class

} //end of the namespace
