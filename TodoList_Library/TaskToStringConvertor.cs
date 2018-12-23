using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList_Library
{
    /// <summary>
    /// class used to convert a task from and to a string 
    /// </summary>
    public class TaskToStringConvertor
    {
        private enum LineType { Empty, Task, Description, Comment, Unknown };
        private const int DEFAULT_TABULATION_LENGTH = 4;

        /// <summary>
        /// gets or sets the number of spaces that equivalent to a tabulation
        /// </summary>
        public int NbSpacesPerTabulation { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        public TaskToStringConvertor()
        {
            NbSpacesPerTabulation = DEFAULT_TABULATION_LENGTH;
        }




        /// <summary>
        /// parse a string and convert it into a task
        /// </summary>
        /// <param name="strToParse"></param>
        /// <returns></returns>
        public TaskToDo StringToTask(String strToParse)
        {
            TaskToDo rootTask = null;
            TaskToDo lastTask = null;
            int lastIndentation = 0;
            Stack<Tuple<int, TaskToDo>> stack = new Stack<Tuple<int, TaskToDo>>();


            String[] lines = strToParse.SplitLines(true);
            foreach(String line in lines)
            {
                int indentation = 0;
                String content;
                LineType type = ParseLine(line, out indentation, out content);
                switch(type)
                {
                    case LineType.Empty: //ignore empty line
                        break;
                    case LineType.Comment: //comment, ignore it
                        break;
                    case LineType.Description: //Description of the current task (could be on several lines)
                        if (lastTask != null) //if there is no current task, the description is ignored
                        {
                            if (!String.IsNullOrEmpty(lastTask.Description))
                                lastTask.Description += Environment.NewLine;
                            lastTask.Description += content;
                        }
                        break;
                    case LineType.Task: //task or subtask
                        TaskToDo newTask = ParseTaskLine(content);
                        if (lastTask == null) //this is the first task, use it as root
                        {
                            rootTask = newTask;
                            lastTask = newTask;
                        }
                        else //not the first task, insert it in the existing tree
                        {
                            if(indentation > lastIndentation) //newTask is a subtask of last task
                            {
                                lastTask.AddSubtask(newTask);
                            }
                            else //we have to go back in tree to find a parent 
                            {
                                Boolean parentFound = false;
                                while(!parentFound)
                                {
                                    if (stack.Count == 0)
                                        throw new ApplicationException("Indentation smaller than root level");
                                    var upperLevel = stack.Peek();
                                    if(upperLevel.Item1 < indentation) //parent found
                                    {
                                        parentFound = true;
                                        upperLevel.Item2.AddSubtask(newTask);

                                    }
                                    else //not found yet 
                                    {
                                        stack.Pop(); //go to level up
                                    }
                                } //end of search for parents
                            } //end of "Not subtask of last task
                        } //end of "Not root"
                        //save state for next loop
                        stack.Push(new Tuple<int, TaskToDo>(indentation, newTask));
                        lastTask = newTask;
                        lastIndentation = indentation;
                        break;
                    default: //ignore all other lines     
                        break;
                }
            } //end foreach line
            return rootTask;
        }

        /// <summary>
        /// convert a task to a string
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public String TaskToString(TaskToDo task)
        {
            if (task == null)
                return String.Empty;
            StringBuilder sb = new StringBuilder();
            RecursiveTaskToString(task, 0, sb);
            return sb.ToString();
        }

        /// <summary>
        /// write 
        /// </summary>
        /// <param name="task"></param>
        /// <param name="indentationLevel"></param>
        /// <param name="strToWrite"></param>
        private void RecursiveTaskToString(TaskToDo task, int indentationLevel, StringBuilder strToWrite)
        {
            //write name
            strToWrite.AppendLine(String.Format("{0}- {1}{2}", new String('\t', indentationLevel), task.Name, task.IsCompleted ? " [OK]" : String.Empty));
            //write description (possibly on several lines)
            if(!String.IsNullOrEmpty(task.Description))
            {
                String[] lines = task.Description.SplitLines(false);
                foreach (String line in lines)
                    strToWrite.AppendLine(new string('\t', indentationLevel + 1) + line);
                //add a blank line beetween description and subtasks
                strToWrite.AppendLine();
            }
            
            //write subtasks
            foreach(TaskToDo subtask in task.Subtasks)
            {
                RecursiveTaskToString(subtask, indentationLevel + 1, strToWrite);
            }
        }

        /// <summary>
        /// parse a line
        /// returns the type of line and fill indentation and content variables
        /// </summary>
        private LineType ParseLine(String line, out int indentation, out String content)
        {
            LineType type = LineType.Unknown;
            indentation = 0;
            content = String.Empty;
            //count blank char at the beginning of the string
            int blankChar = 0;
            foreach (char c in line)
            {
                if (c == ' ')
                {
                    indentation++;
                    blankChar++;
                }
                else if (c == '\t')
                {
                    indentation += NbSpacesPerTabulation;
                    blankChar++;
                }
                else //not a blanck char, break the loop
                    break;
            }
            content = line.Substring(blankChar);
            if (content.StartsWith("#") || content.StartsWith("//"))
                type = LineType.Comment;
            else if (content.StartsWith("-"))
                type = LineType.Task;
            else if (String.IsNullOrWhiteSpace(content))
                type = LineType.Empty;
            else
                type = LineType.Description;
            return type;
        }

        /// <summary>
        /// parse a task line and returns the corresponding task including its attributes
        /// </summary>
        /// <param name="contentToParse"></param>
        /// <returns></returns>
        private TaskToDo ParseTaskLine(String contentToParse)
        {
            //remove (and check) the '-' that starts the line
            if (!contentToParse.StartsWith("-"))
                throw new ApplicationException("Task line must begin with a -");
            //browse the string to separate the name and all attributes (between [])
            StringBuilder sbName = new StringBuilder();
            List<String> attributes = new List<string>();
            StringBuilder sbCurrentAttribute = new StringBuilder();
            Boolean inAttribute = false;
            
            for(int i=1; i<contentToParse.Length; i++) //starts from 1 to ignore the '-'
            {
                char c = contentToParse[i];
                switch (c)
                {
                    case '[': //starts a new attribute
                        if (inAttribute) throw new ApplicationException("Recursive attribute ([[)"); //already in an attribute, not managed
                        inAttribute = true;
                        sbCurrentAttribute.Clear();
                        break;
                    case ']': //ends an attribute
                        if (!inAttribute) throw new ApplicationException("End of attribute without begin (])");
                        attributes.Add(sbCurrentAttribute.ToString());
                        sbCurrentAttribute.Clear();
                        inAttribute = false;
                        break;
                    default: //any other character
                        if (inAttribute)
                            sbCurrentAttribute.Append(c);
                        else
                            sbName.Append(c);
                        break;
                }
            }
            //check we have not begun an attribute and is not finished
            if (inAttribute)
                throw new ApplicationException("Attribute started but without end ([)");
            //create the task with trimmed name
            TaskToDo result = new TaskToDo(sbName.ToString().Trim(), String.Empty);
            //check each attribute
            foreach(String attribute in attributes)
            {
                if (attribute == "OK")
                    result.SetCompleted();
                else
                    throw new ApplicationException("Unknwon attribute : " + attribute);
            }
            return result;
        }



    } //end of class
} //end of namespace

