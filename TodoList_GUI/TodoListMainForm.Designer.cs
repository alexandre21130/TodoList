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
            this.components = new System.ComponentModel.Container();
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabAllTasks = new System.Windows.Forms.TabPage();
            this.listViewAllTasks = new System.Windows.Forms.ListView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cxtMenuMainTab = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cxtMenuMainOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.cxtMenuMainEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.cxtMenuMainDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.cxtMenuTaskTab = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cxtMenuTaskEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.cxtMenuTaskSetCompleted = new System.Windows.Forms.ToolStripMenuItem();
            this.cxtMenuTaskSetNotCompleted = new System.Windows.Forms.ToolStripMenuItem();
            this.tabs.SuspendLayout();
            this.tabAllTasks.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.cxtMenuMainTab.SuspendLayout();
            this.cxtMenuTaskTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabs
            // 
            this.tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabs.Controls.Add(this.tabAllTasks);
            this.tabs.Location = new System.Drawing.Point(14, 28);
            this.tabs.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(598, 386);
            this.tabs.TabIndex = 1;
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
            this.tabAllTasks.Size = new System.Drawing.Size(590, 357);
            this.tabAllTasks.TabIndex = 0;
            this.tabAllTasks.Text = "All tasks";
            this.tabAllTasks.UseVisualStyleBackColor = true;
            // 
            // listViewAllTasks
            // 
            this.listViewAllTasks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewAllTasks.ContextMenuStrip = this.cxtMenuMainTab;
            this.listViewAllTasks.FullRowSelect = true;
            this.listViewAllTasks.GridLines = true;
            this.listViewAllTasks.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewAllTasks.HideSelection = false;
            this.listViewAllTasks.LabelWrap = false;
            this.listViewAllTasks.Location = new System.Drawing.Point(7, 7);
            this.listViewAllTasks.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.listViewAllTasks.MultiSelect = false;
            this.listViewAllTasks.Name = "listViewAllTasks";
            this.listViewAllTasks.Size = new System.Drawing.Size(575, 335);
            this.listViewAllTasks.TabIndex = 0;
            this.listViewAllTasks.UseCompatibleStateImageBehavior = false;
            this.listViewAllTasks.View = System.Windows.Forms.View.List;
            this.listViewAllTasks.SelectedIndexChanged += new System.EventHandler(this.listViewAllTasks_SelectedIndexChanged);
            this.listViewAllTasks.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listViewAllTasks_KeyDown);
            this.listViewAllTasks.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewAllTasks_MouseDoubleClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.editToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(624, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // cxtMenuMainTab
            // 
            this.cxtMenuMainTab.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cxtMenuMainOpen,
            this.cxtMenuMainEdit,
            this.cxtMenuMainDelete});
            this.cxtMenuMainTab.Name = "cxtMenuMainTab";
            this.cxtMenuMainTab.Size = new System.Drawing.Size(108, 70);
            // 
            // cxtMenuMainOpen
            // 
            this.cxtMenuMainOpen.Name = "cxtMenuMainOpen";
            this.cxtMenuMainOpen.Size = new System.Drawing.Size(107, 22);
            this.cxtMenuMainOpen.Text = "Open";
            this.cxtMenuMainOpen.Click += new System.EventHandler(this.cxtMenuMainOpen_Click);
            // 
            // cxtMenuMainEdit
            // 
            this.cxtMenuMainEdit.Name = "cxtMenuMainEdit";
            this.cxtMenuMainEdit.Size = new System.Drawing.Size(107, 22);
            this.cxtMenuMainEdit.Text = "Edit";
            this.cxtMenuMainEdit.Click += new System.EventHandler(this.cxtMenuMainEdit_Click);
            // 
            // cxtMenuMainDelete
            // 
            this.cxtMenuMainDelete.Name = "cxtMenuMainDelete";
            this.cxtMenuMainDelete.Size = new System.Drawing.Size(107, 22);
            this.cxtMenuMainDelete.Text = "Delete";
            this.cxtMenuMainDelete.Click += new System.EventHandler(this.cxtMenuMainDelete_Click);
            // 
            // cxtMenuTaskTab
            // 
            this.cxtMenuTaskTab.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cxtMenuTaskEdit,
            this.cxtMenuTaskSetCompleted,
            this.cxtMenuTaskSetNotCompleted});
            this.cxtMenuTaskTab.Name = "cxtMenuTaskTab";
            this.cxtMenuTaskTab.Size = new System.Drawing.Size(198, 92);
            // 
            // cxtMenuTaskEdit
            // 
            this.cxtMenuTaskEdit.Name = "cxtMenuTaskEdit";
            this.cxtMenuTaskEdit.Size = new System.Drawing.Size(197, 22);
            this.cxtMenuTaskEdit.Text = "Switch to edition mode";
            this.cxtMenuTaskEdit.Click += new System.EventHandler(this.cxtMenuTaskEdit_Click);
            // 
            // cxtMenuTaskSetCompleted
            // 
            this.cxtMenuTaskSetCompleted.Name = "cxtMenuTaskSetCompleted";
            this.cxtMenuTaskSetCompleted.Size = new System.Drawing.Size(197, 22);
            this.cxtMenuTaskSetCompleted.Text = "Set completed";
            this.cxtMenuTaskSetCompleted.Click += new System.EventHandler(this.cxtMenuTaskSetCompleted_Click);
            // 
            // cxtMenuTaskSetNotCompleted
            // 
            this.cxtMenuTaskSetNotCompleted.Name = "cxtMenuTaskSetNotCompleted";
            this.cxtMenuTaskSetNotCompleted.Size = new System.Drawing.Size(197, 22);
            this.cxtMenuTaskSetNotCompleted.Text = "Reset (not completed)";
            this.cxtMenuTaskSetNotCompleted.Click += new System.EventHandler(this.cxtMenuTaskSetNotCompleted_Click);
            // 
            // TodoListMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 425);
            this.Controls.Add(this.tabs);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.RoyalBlue;
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "TodoListMainForm";
            this.Text = "TodoList";
            this.Load += new System.EventHandler(this.TodoListMainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TodoListMainForm_KeyDown);
            this.tabs.ResumeLayout(false);
            this.tabAllTasks.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.cxtMenuMainTab.ResumeLayout(false);
            this.cxtMenuTaskTab.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabAllTasks;
        private System.Windows.Forms.ListView listViewAllTasks;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cxtMenuMainTab;
        private System.Windows.Forms.ToolStripMenuItem cxtMenuMainOpen;
        private System.Windows.Forms.ToolStripMenuItem cxtMenuMainEdit;
        private System.Windows.Forms.ToolStripMenuItem cxtMenuMainDelete;
        private System.Windows.Forms.ContextMenuStrip cxtMenuTaskTab;
        private System.Windows.Forms.ToolStripMenuItem cxtMenuTaskEdit;
        private System.Windows.Forms.ToolStripMenuItem cxtMenuTaskSetCompleted;
        private System.Windows.Forms.ToolStripMenuItem cxtMenuTaskSetNotCompleted;
    }
}

