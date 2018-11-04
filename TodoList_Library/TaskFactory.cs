using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList_Library
{
    public class FakeTaskFactory
    {

        /// <summary>
        /// Create a task without subtasks
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public static TaskToDo CreateTaskWithoutChild(String name, string description)
        {
            TaskToDo result = new TaskToDo(name, description);
            return result;
        }





    }
}
