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
            return newSubtask;
        }

        /// <summary>
        /// Remove a subtask
        /// </summary>
        /// <param name="subtaskToRemove"></param>
        public void RemoveSubtask(TaskToDo subtaskToRemove)
        {
            if (_subtasks.Contains(subtaskToRemove))
                _subtasks.Remove(subtaskToRemove);
        }


    }
}
