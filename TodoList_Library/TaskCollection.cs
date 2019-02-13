
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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

        /// <summary>
        /// returns the string that isused to separate each task in a file 
        /// </summary>
        /// <returns></returns>
        private String GetTaskSeparator()
        {
            return "//" + new String('-', 78);
        }


        /// <summary>
        /// Fill a collection of tasks by loading the content of a file
        /// </summary>
        public void LoadFromFile(string file)
        {
            //read the file line by line and gather these lines into groups
            String separator = GetTaskSeparator();
            _allTasks.Clear();
            StringBuilder currentGroup = null;
            List<StringBuilder> listOfGroups = new List<StringBuilder>();
            using (StreamReader reader = new StreamReader(file))
            {
                while (!reader.EndOfStream)
                {
                    String line = reader.ReadLine();
                    if ((line == separator) || (currentGroup == null)) //begin of new group, add this group in the list
                    {
                        currentGroup = new StringBuilder();
                        listOfGroups.Add(currentGroup);
                    }
                    //add the line to the current group
                    currentGroup.AppendLine(line);
                }
            }
            //convert each group of lines into tasks and add them into the collection
            TaskToStringConvertor convertor = new TaskToStringConvertor();
            foreach (StringBuilder sb in listOfGroups)
            {
                TaskToDo task = convertor.StringToTask(sb.ToString());
                _allTasks.Add(task);
            }
        }




        /// <summary>
        /// Save the current collection to a text file
        /// </summary>
        /// <param name="fileName"></param>
        public void SaveToFile(string fileName)
        {
            String separator = GetTaskSeparator();
            using (StreamWriter writer = new StreamWriter(fileName, false))
            {
                TaskToStringConvertor convertor = new TaskToStringConvertor();
                foreach (TaskToDo task in _allTasks)
                {
                    writer.WriteLine(separator);
                    writer.WriteLine(convertor.TaskToString(task));
                    writer.WriteLine(); //empty line
                }
            }
        }

        /// <summary>
        /// replace a task in the collection by a new one
        /// </summary>
        /// <param name="taskToReplace"></param>
        /// <param name="taskToInsert"></param>
        public void ReplaceTask(TaskToDo taskToReplace, TaskToDo taskToInsert)
        {
            int index = _allTasks.IndexOf(taskToReplace);
            _allTasks.RemoveAt(index);
            _allTasks.Insert(index, taskToInsert);
        }

        /// <summary>
        /// returns true if a given task can be moved up
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public Boolean CanMoveTaskUp(TaskToDo task)
        {
            int index = _allTasks.IndexOf(task);
            return index > 0; //case where index is -1 is integrated in this condition
        }

        /// <summary>
        /// returns true if a given task can be moved down
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public Boolean CanMoveTaskDown(TaskToDo task)
        {
            int index = _allTasks.IndexOf(task);
            int lastIndex = _allTasks.Count - 1;
            return ((index >= 0) && (index < lastIndex)); //case where index is -1 is integrated in this condition
        }

        /// <summary>
        /// Move a given task up -swap it with previous task)
        /// </summary>
        /// <param name="task"></param>
        public void MoveTaskUp(TaskToDo task)
        {
            int currentIndex = _allTasks.IndexOf(task);
            if (currentIndex < 1)
                return;
            _allTasks.RemoveAt(currentIndex);
            _allTasks.Insert(currentIndex - 1, task);
        }

        /// <summary>
        /// Move a given task down (swap it with next task)
        /// </summary>
        /// <param name="task"></param>
        public void MoveTaskDown(TaskToDo task)
        {
            int currentIndex = _allTasks.IndexOf(task);
            int lastIndex = _allTasks.Count - 1;
            if (currentIndex < 0 || currentIndex >= lastIndex)
                return;
            _allTasks.RemoveAt(currentIndex);
            _allTasks.Insert(currentIndex + 1, task); 
        }



    }
}
