using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList_Library
{
    /// <summary>
    /// class used to store the progression of a given task and its subtasks
    /// </summary>
    public class TaskProgression
    {
        private Int32 _totalNbTerminalTasks;
        private Int32 _totalNbCompletedTasks;

        /// <summary>
        /// default constructor
        /// </summary>
        public TaskProgression()
        {
            _totalNbTerminalTasks = 0;
            _totalNbCompletedTasks = 0;
        }


        /// <summary>
        /// Constructor from a given task
        /// </summary>
        public TaskProgression(TaskToDo task)
        {
            UpdateFromTask(task);
        }

        /// <summary>
        /// update the current progression from a given task
        /// </summary>
        /// <param name="task"></param>
        public void UpdateFromTask(TaskToDo task)
        {
            _totalNbCompletedTasks = 0;
            _totalNbTerminalTasks = 0;
            RecursiveUpdate(task);
        }

        /// <summary>
        /// Count recursively the number of terminal tasks and the number of terminal tasks that are completed
        /// </summary>
        /// <param name="task"></param>
        private void RecursiveUpdate(TaskToDo task)
        {
            if(task.IsTerminalTask)
            {
                _totalNbTerminalTasks++;
                if (task.IsCompleted)
                    _totalNbCompletedTasks++;
            }
            else
            {
                foreach(TaskToDo subtask in task.Subtasks)
                {
                    RecursiveUpdate(subtask);
                }
            }
        }

        /// <summary>
        /// gets the number of terminal tasks that has to be done
        /// </summary>
        public Int32 TerminalTasksCount { get { return _totalNbTerminalTasks; } }

        /// <summary>
        /// gets the number of terminal tasks that are completed
        /// </summary>
        public Int32 CompletedTerminalTasksCount { get { return _totalNbCompletedTasks; } }

        /// <summary>
        /// Gets the number of terminal tasks to to to complete the task
        /// </summary>
        public Int32 RemainingTerminalTasksToDo { get { return (_totalNbTerminalTasks - _totalNbCompletedTasks); } }

        /// <summary>
        /// Get the progression in percent
        /// </summary>
        public double PercentDone
        {
            get
            {
                double result = 0.0;
                if (_totalNbTerminalTasks == 0)
                    result = 100.0;
                else
                    result = 100.0 * ( (double)_totalNbCompletedTasks /_totalNbTerminalTasks);
                return result;
            }
        }

        /// <summary>
        /// returns true if at least one subtask is already completed
        /// </summary>
        public Boolean HasBegun { get { return _totalNbCompletedTasks > 0; } }

        /// <summary>
        /// returns true if all subtasks are completed, else returns false
        /// </summary>
        public Boolean IsCompleted { get { return (_totalNbCompletedTasks == _totalNbTerminalTasks); } }

    }
}
