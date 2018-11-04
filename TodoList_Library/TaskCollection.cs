
using System.Collections.Generic;



namespace TodoList_Library
{
    /// <summary>
    /// class used to maintain a collection of different tasks
    /// </summary>
    public class TaskCollection
    {
        private List<TaskToDo> _allTasks;

        /// <summary>
        /// default constructor
        /// </summary>
        public TaskCollection()
        {
            _allTasks = new List<TaskToDo>();
        }

        /// <summary>
        /// Add a task into the collection
        /// returns the task that was added
        /// </summary>
        /// <param name="newTask"></param>
        /// <returns></returns>
        public TaskToDo AddTask(TaskToDo newTask)
        {
            if (!_allTasks.Contains(newTask))
                _allTasks.Add(newTask);
            return newTask;
        }

        /// <summary>
        /// remove a given task from the collection
        /// </summary>
        /// <param name="taskToRemove"></param>
        public void RemoveTask(TaskToDo taskToRemove)
        {
            if (_allTasks.Contains(taskToRemove))
                _allTasks.Remove(taskToRemove);
        }

        /// <summary>
        /// Gets an enumeration of all tasks of the collection
        /// </summary>
        public IEnumerable<TaskToDo> Tasks { get { return _allTasks; } }
    }
}
