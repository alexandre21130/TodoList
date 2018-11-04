using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TodoList_GUI
{
    public partial class TodoListMainForm : Form
    {
        /// <summary>
        /// constructor of the main form
        /// </summary>
        public TodoListMainForm()
        {
            InitializeComponent();
        }


        /// <summary>
        /// click on the button Quit (closes the application) 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            this.Close(); //closes the main form, quits the application 
        }
    }
}
