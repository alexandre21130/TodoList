using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TodoList_Library;


namespace TodoList_GUI
{
    public partial class FormNewTask : Form
    {
        private TaskToDo _template;
        private TaskToDo _task;

        /// <summary>
        /// constructor
        /// </summary>
        public FormNewTask(TaskToDo template)
        {
            InitializeComponent();
            _template = template;
            _task = null;
        }


        /// <summary>
        /// click onthe button OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            _task = new TaskToDo(txtName.Text, txtDescription.Text);
            this.DialogResult = DialogResult.OK;        
        }

        /// <summary>
        /// click on the button Cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// Gets the task that was built
        /// </summary>
        public TaskToDo Task { get { return _task; } }


        /// <summary>
        /// Occurs when the form is loading
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormNewTask_Load(object sender, EventArgs e)
        {
            if(_template == null)
            {
                txtName.Text = String.Empty;
                txtDescription.Text = String.Empty;
            }
            else
            {
                txtName.Text = _template.Name;
                txtDescription.Text = _template.Description;
            }
        }
    }
}
