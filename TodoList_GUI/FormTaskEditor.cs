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
    public partial class FormTaskEditor : Form
    {
        private TaskToDo _task;
        private TaskToStringConvertor _convertor;
        private const int NB_SPACES_PER_TABULATION = 7; //fixed value that depends on the font of richtextbox and cannot be moved easily

        /// <summary>
        /// Gets the task object that was created
        /// </summary>
        public TaskToDo Task { get { return _task; } }

        /// <summary>
        /// default constructor but cannot called from outside
        /// </summary>
        private FormTaskEditor()
        {
            InitializeComponent();
            _convertor = new TaskToStringConvertor();
            _convertor.NbSpacesPerTabulation = NB_SPACES_PER_TABULATION;
            _task = null;
        }

        /// <summary>
        /// constructor with a task
        /// </summary>
        public FormTaskEditor(TaskToDo initialTask) : this()
        {
            txtTask.Text = _convertor.TaskToString(initialTask);
        }

        /// <summary>
        /// constructor with a default text
        /// </summary>
        public FormTaskEditor(String defaultText) : this()
        {
            txtTask.Text = defaultText;
        }


        /// <summary>
        /// click on the button OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                _task = _convertor.StringToTask(txtTask.Text);
                DialogResult = DialogResult.OK;
            }
            catch(Exception ex)
            {
                ErrorBox(ex);
            }
        }

        /// <summary>
        /// displays a message box to describe an error
        /// </summary>
        /// <param name="ex"></param>
        private void ErrorBox(Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Click on the button Cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

    }
}
