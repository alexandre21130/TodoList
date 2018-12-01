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
            this.listViewAllTasks = new System.Windows.Forms.ListView();
            this.btnEditTask = new System.Windows.Forms.Button();
            this.btnOpenSelectedTask = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnNewTask = new System.Windows.Forms.Button();
            this.btnCloseApp = new System.Windows.Forms.Button();
            this.flowButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCloseSelectedTab = new System.Windows.Forms.Button();
            this.tabs.SuspendLayout();
            this.tabAllTasks.SuspendLayout();
            this.flowButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabs
            // 
            this.tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabs.Controls.Add(this.tabAllTasks);
            this.tabs.Location = new System.Drawing.Point(14, 47);
            this.tabs.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(598, 367);
            this.tabs.TabIndex = 0;
            this.tabs.SelectedIndexChanged += new System.EventHandler(this.tabs_SelectedIndexChanged);
            this.tabs.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tabs_MouseDoubleClick);
            // 
            // tabAllTasks
            // 
            this.tabAllTasks.Controls.Add(this.listViewAllTasks);
            this.tabAllTasks.Location = new System.Drawing.Point(4, 25);
            this.tabAllTasks.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabAllTasks.Name = "tabAllTasks";
            this.tabAllTasks.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabAllTasks.Size = new System.Drawing.Size(590, 338);
            this.tabAllTasks.TabIndex = 0;
            this.tabAllTasks.Text = "All tasks";
            this.tabAllTasks.UseVisualStyleBackColor = true;
            // 
            // listViewAllTasks
            // 
            this.listViewAllTasks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewAllTasks.FullRowSelect = true;
            this.listViewAllTasks.GridLines = true;
            this.listViewAllTasks.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewAllTasks.HideSelection = false;
            this.listViewAllTasks.LabelWrap = false;
            this.listViewAllTasks.Location = new System.Drawing.Point(7, 7);
            this.listViewAllTasks.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.listViewAllTasks.MultiSelect = false;
            this.listViewAllTasks.Name = "listViewAllTasks";
            this.listViewAllTasks.Size = new System.Drawing.Size(575, 316);
            this.listViewAllTasks.TabIndex = 0;
            this.listViewAllTasks.UseCompatibleStateImageBehavior = false;
            this.listViewAllTasks.View = System.Windows.Forms.View.List;
            this.listViewAllTasks.SelectedIndexChanged += new System.EventHandler(this.listViewAllTasks_SelectedIndexChanged);
            this.listViewAllTasks.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listViewAllTasks_KeyDown);
            this.listViewAllTasks.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewAllTasks_MouseDoubleClick);
            // 
            // btnEditTask
            // 
            this.btnEditTask.Location = new System.Drawing.Point(134, 0);
            this.btnEditTask.Margin = new System.Windows.Forms.Padding(0);
            this.btnEditTask.Name = "btnEditTask";
            this.btnEditTask.Size = new System.Drawing.Size(67, 26);
            this.btnEditTask.TabIndex = 3;
            this.btnEditTask.Text = "Edit";
            this.btnEditTask.UseVisualStyleBackColor = true;
            this.btnEditTask.Click += new System.EventHandler(this.btnEditTask_Click);
            // 
            // btnOpenSelectedTask
            // 
            this.btnOpenSelectedTask.Location = new System.Drawing.Point(67, 0);
            this.btnOpenSelectedTask.Margin = new System.Windows.Forms.Padding(0);
            this.btnOpenSelectedTask.Name = "btnOpenSelectedTask";
            this.btnOpenSelectedTask.Size = new System.Drawing.Size(67, 26);
            this.btnOpenSelectedTask.TabIndex = 2;
            this.btnOpenSelectedTask.Text = "View";
            this.btnOpenSelectedTask.UseVisualStyleBackColor = true;
            this.btnOpenSelectedTask.Click += new System.EventHandler(this.btnOpenSelectedTask_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(201, 0);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(67, 26);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnNewTask
            // 
            this.btnNewTask.Location = new System.Drawing.Point(0, 0);
            this.btnNewTask.Margin = new System.Windows.Forms.Padding(0);
            this.btnNewTask.Name = "btnNewTask";
            this.btnNewTask.Size = new System.Drawing.Size(67, 26);
            this.btnNewTask.TabIndex = 1;
            this.btnNewTask.Text = "New";
            this.btnNewTask.UseVisualStyleBackColor = true;
            this.btnNewTask.Click += new System.EventHandler(this.btnNewTask_Click);
            // 
            // btnCloseApp
            // 
            this.btnCloseApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCloseApp.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCloseApp.Location = new System.Drawing.Point(335, 0);
            this.btnCloseApp.Margin = new System.Windows.Forms.Padding(0);
            this.btnCloseApp.Name = "btnCloseApp";
            this.btnCloseApp.Size = new System.Drawing.Size(67, 26);
            this.btnCloseApp.TabIndex = 1;
            this.btnCloseApp.Text = "Quit";
            this.btnCloseApp.UseVisualStyleBackColor = true;
            this.btnCloseApp.Click += new System.EventHandler(this.btnCloseApp_Click);
            // 
            // flowButtons
            // 
            this.flowButtons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowButtons.AutoScroll = true;
            this.flowButtons.Controls.Add(this.btnNewTask);
            this.flowButtons.Controls.Add(this.btnOpenSelectedTask);
            this.flowButtons.Controls.Add(this.btnEditTask);
            this.flowButtons.Controls.Add(this.btnDelete);
            this.flowButtons.Controls.Add(this.btnCloseSelectedTab);
            this.flowButtons.Controls.Add(this.btnCloseApp);
            this.flowButtons.Location = new System.Drawing.Point(18, 11);
            this.flowButtons.Name = "flowButtons";
            this.flowButtons.Size = new System.Drawing.Size(590, 36);
            this.flowButtons.TabIndex = 5;
            // 
            // btnCloseSelectedTab
            // 
            this.btnCloseSelectedTab.Location = new System.Drawing.Point(268, 0);
            this.btnCloseSelectedTab.Margin = new System.Windows.Forms.Padding(0);
            this.btnCloseSelectedTab.Name = "btnCloseSelectedTab";
            this.btnCloseSelectedTab.Size = new System.Drawing.Size(67, 26);
            this.btnCloseSelectedTab.TabIndex = 5;
            this.btnCloseSelectedTab.Text = "Close";
            this.btnCloseSelectedTab.UseVisualStyleBackColor = true;
            this.btnCloseSelectedTab.Click += new System.EventHandler(this.buttonCloseSelectedTab_Click);
            // 
            // TodoListMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCloseApp;
            this.ClientSize = new System.Drawing.Size(624, 425);
            this.Controls.Add(this.flowButtons);
            this.Controls.Add(this.tabs);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.RoyalBlue;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "TodoListMainForm";
            this.Text = "TodoList";
            this.Load += new System.EventHandler(this.TodoListMainForm_Load);
            this.tabs.ResumeLayout(false);
            this.tabAllTasks.ResumeLayout(false);
            this.flowButtons.ResumeLayout(false);
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
        private System.Windows.Forms.Button btnEditTask;
        private System.Windows.Forms.FlowLayoutPanel flowButtons;
        private System.Windows.Forms.Button btnCloseSelectedTab;
    }
}

