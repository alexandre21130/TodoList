namespace TodoList_GUI
{
    partial class TodoListMainForm
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabAllTasks = new System.Windows.Forms.TabPage();
            this.btnOpenSelectedTask = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnNewTask = new System.Windows.Forms.Button();
            this.listViewAllTasks = new System.Windows.Forms.ListView();
            this.btnCloseApp = new System.Windows.Forms.Button();
            this.tabs.SuspendLayout();
            this.tabAllTasks.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabs
            // 
            this.tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabs.Controls.Add(this.tabAllTasks);
            this.tabs.Location = new System.Drawing.Point(12, 12);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(798, 325);
            this.tabs.TabIndex = 0;
            // 
            // tabAllTasks
            // 
            this.tabAllTasks.Controls.Add(this.btnOpenSelectedTask);
            this.tabAllTasks.Controls.Add(this.btnDelete);
            this.tabAllTasks.Controls.Add(this.btnNewTask);
            this.tabAllTasks.Controls.Add(this.listViewAllTasks);
            this.tabAllTasks.Location = new System.Drawing.Point(4, 22);
            this.tabAllTasks.Name = "tabAllTasks";
            this.tabAllTasks.Padding = new System.Windows.Forms.Padding(3);
            this.tabAllTasks.Size = new System.Drawing.Size(790, 299);
            this.tabAllTasks.TabIndex = 0;
            this.tabAllTasks.Text = "All tasks";
            this.tabAllTasks.UseVisualStyleBackColor = true;
            // 
            // btnOpenSelectedTask
            // 
            this.btnOpenSelectedTask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenSelectedTask.Location = new System.Drawing.Point(644, 41);
            this.btnOpenSelectedTask.Name = "btnOpenSelectedTask";
            this.btnOpenSelectedTask.Size = new System.Drawing.Size(140, 29);
            this.btnOpenSelectedTask.TabIndex = 1;
            this.btnOpenSelectedTask.Text = "View";
            this.btnOpenSelectedTask.UseVisualStyleBackColor = true;
            this.btnOpenSelectedTask.Click += new System.EventHandler(this.btnOpenSelectedTask_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Location = new System.Drawing.Point(647, 119);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(140, 29);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnNewTask
            // 
            this.btnNewTask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewTask.Location = new System.Drawing.Point(644, 6);
            this.btnNewTask.Name = "btnNewTask";
            this.btnNewTask.Size = new System.Drawing.Size(140, 29);
            this.btnNewTask.TabIndex = 1;
            this.btnNewTask.Text = "New";
            this.btnNewTask.UseVisualStyleBackColor = true;
            this.btnNewTask.Click += new System.EventHandler(this.btnNewTask_Click);
            // 
            // listViewAllTasks
            // 
            this.listViewAllTasks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewAllTasks.GridLines = true;
            this.listViewAllTasks.Location = new System.Drawing.Point(6, 6);
            this.listViewAllTasks.MultiSelect = false;
            this.listViewAllTasks.Name = "listViewAllTasks";
            this.listViewAllTasks.Size = new System.Drawing.Size(632, 287);
            this.listViewAllTasks.TabIndex = 0;
            this.listViewAllTasks.UseCompatibleStateImageBehavior = false;
            this.listViewAllTasks.View = System.Windows.Forms.View.List;
            this.listViewAllTasks.SelectedIndexChanged += new System.EventHandler(this.listViewAllTasks_SelectedIndexChanged);
            this.listViewAllTasks.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewAllTasks_MouseDoubleClick);
            // 
            // btnCloseApp
            // 
            this.btnCloseApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCloseApp.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCloseApp.Location = new System.Drawing.Point(660, 343);
            this.btnCloseApp.Name = "btnCloseApp";
            this.btnCloseApp.Size = new System.Drawing.Size(140, 29);
            this.btnCloseApp.TabIndex = 1;
            this.btnCloseApp.Text = "Quit";
            this.btnCloseApp.UseVisualStyleBackColor = true;
            this.btnCloseApp.Click += new System.EventHandler(this.btnCloseApp_Click);
            // 
            // TodoListMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCloseApp;
            this.ClientSize = new System.Drawing.Size(822, 384);
            this.Controls.Add(this.btnCloseApp);
            this.Controls.Add(this.tabs);
            this.Name = "TodoListMainForm";
            this.Text = "TodoList";
            this.Load += new System.EventHandler(this.TodoListMainForm_Load);
            this.tabs.ResumeLayout(false);
            this.tabAllTasks.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabAllTasks;
        private System.Windows.Forms.Button btnCloseApp;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnNewTask;
        private System.Windows.Forms.ListView listViewAllTasks;
        private System.Windows.Forms.Button btnOpenSelectedTask;
    }
}

