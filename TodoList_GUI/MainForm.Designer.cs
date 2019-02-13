namespace TodoList_GUI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listViewAllTasks = new System.Windows.Forms.ListView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.currentTaskTreeView = new System.Windows.Forms.TreeView();
            this.contextMenuCurrentTask = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cxtMenuCurrentTaskMoveUp = new System.Windows.Forms.ToolStripMenuItem();
            this.cxtMenuCurrentTaskMoveDown = new System.Windows.Forms.ToolStripMenuItem();
            this.cxtMenuCurrentTaskSetCompleted = new System.Windows.Forms.ToolStripMenuItem();
            this.cxtMenuCurrentTaskSetNotCompleted = new System.Windows.Forms.ToolStripMenuItem();
            this.cxtMenuCurrentTaskDeleteSubtask = new System.Windows.Forms.ToolStripMenuItem();
            this.descriptionTextBox = new System.Windows.Forms.RichTextBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuNotify = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnNewTask = new System.Windows.Forms.Button();
            this.btnEditTask = new System.Windows.Forms.Button();
            this.btnDeleteTask = new System.Windows.Forms.Button();
            this.checkHideCompletedSubtasks = new System.Windows.Forms.CheckBox();
            this.cxtMenuCurrentTaskEdit = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.contextMenuCurrentTask.SuspendLayout();
            this.contextMenuNotify.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Location = new System.Drawing.Point(0, 46);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listViewAllTasks);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(725, 297);
            this.splitContainer1.SplitterDistance = 241;
            this.splitContainer1.SplitterWidth = 10;
            this.splitContainer1.TabIndex = 1;
            // 
            // listViewAllTasks
            // 
            this.listViewAllTasks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewAllTasks.Location = new System.Drawing.Point(3, 3);
            this.listViewAllTasks.Name = "listViewAllTasks";
            this.listViewAllTasks.Size = new System.Drawing.Size(233, 289);
            this.listViewAllTasks.TabIndex = 0;
            this.listViewAllTasks.UseCompatibleStateImageBehavior = false;
            this.listViewAllTasks.View = System.Windows.Forms.View.List;
            this.listViewAllTasks.SelectedIndexChanged += new System.EventHandler(this.listViewAllTasks_SelectedIndexChanged);
            this.listViewAllTasks.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listViewAllTasks_KeyDown);
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.currentTaskTreeView);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.descriptionTextBox);
            this.splitContainer2.Size = new System.Drawing.Size(474, 297);
            this.splitContainer2.SplitterDistance = 195;
            this.splitContainer2.SplitterWidth = 10;
            this.splitContainer2.TabIndex = 0;
            // 
            // currentTaskTreeView
            // 
            this.currentTaskTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.currentTaskTreeView.ContextMenuStrip = this.contextMenuCurrentTask;
            this.currentTaskTreeView.Location = new System.Drawing.Point(3, 3);
            this.currentTaskTreeView.Name = "currentTaskTreeView";
            this.currentTaskTreeView.Size = new System.Drawing.Size(466, 187);
            this.currentTaskTreeView.TabIndex = 0;
            this.currentTaskTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.currentTaskTreeView_AfterSelect);
            this.currentTaskTreeView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.currentTaskTreeView_KeyDown);
            // 
            // contextMenuCurrentTask
            // 
            this.contextMenuCurrentTask.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cxtMenuCurrentTaskEdit,
            this.cxtMenuCurrentTaskMoveUp,
            this.cxtMenuCurrentTaskMoveDown,
            this.cxtMenuCurrentTaskSetCompleted,
            this.cxtMenuCurrentTaskSetNotCompleted,
            this.cxtMenuCurrentTaskDeleteSubtask});
            this.contextMenuCurrentTask.Name = "contextMenuCurrentTask";
            this.contextMenuCurrentTask.Size = new System.Drawing.Size(214, 158);
            // 
            // cxtMenuCurrentTaskMoveUp
            // 
            this.cxtMenuCurrentTaskMoveUp.Name = "cxtMenuCurrentTaskMoveUp";
            this.cxtMenuCurrentTaskMoveUp.Size = new System.Drawing.Size(213, 22);
            this.cxtMenuCurrentTaskMoveUp.Text = "Move up (Ctrl + Up)";
            this.cxtMenuCurrentTaskMoveUp.Click += new System.EventHandler(this.moveUpToolStripMenuItem_Click);
            // 
            // cxtMenuCurrentTaskMoveDown
            // 
            this.cxtMenuCurrentTaskMoveDown.Name = "cxtMenuCurrentTaskMoveDown";
            this.cxtMenuCurrentTaskMoveDown.Size = new System.Drawing.Size(213, 22);
            this.cxtMenuCurrentTaskMoveDown.Text = "Move down (Ctrl + down)";
            this.cxtMenuCurrentTaskMoveDown.Click += new System.EventHandler(this.moveDownToolStripMenuItem_Click);
            // 
            // cxtMenuCurrentTaskSetCompleted
            // 
            this.cxtMenuCurrentTaskSetCompleted.Name = "cxtMenuCurrentTaskSetCompleted";
            this.cxtMenuCurrentTaskSetCompleted.Size = new System.Drawing.Size(213, 22);
            this.cxtMenuCurrentTaskSetCompleted.Text = "Set completed (Space)";
            this.cxtMenuCurrentTaskSetCompleted.Click += new System.EventHandler(this.setCompletedToolStripMenuItem_Click);
            // 
            // cxtMenuCurrentTaskSetNotCompleted
            // 
            this.cxtMenuCurrentTaskSetNotCompleted.Name = "cxtMenuCurrentTaskSetNotCompleted";
            this.cxtMenuCurrentTaskSetNotCompleted.Size = new System.Drawing.Size(213, 22);
            this.cxtMenuCurrentTaskSetNotCompleted.Text = "Set not completed (Space)";
            this.cxtMenuCurrentTaskSetNotCompleted.Click += new System.EventHandler(this.setNotCompletedToolStripMenuItem_Click);
            // 
            // cxtMenuCurrentTaskDeleteSubtask
            // 
            this.cxtMenuCurrentTaskDeleteSubtask.Name = "cxtMenuCurrentTaskDeleteSubtask";
            this.cxtMenuCurrentTaskDeleteSubtask.Size = new System.Drawing.Size(213, 22);
            this.cxtMenuCurrentTaskDeleteSubtask.Text = "Delete (Suppr)";
            this.cxtMenuCurrentTaskDeleteSubtask.Click += new System.EventHandler(this.deleteToolStripMenuItem1_Click);
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionTextBox.Location = new System.Drawing.Point(3, 3);
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.Size = new System.Drawing.Size(466, 84);
            this.descriptionTextBox.TabIndex = 0;
            this.descriptionTextBox.Text = "";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuNotify;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "TODO List";
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuNotify
            // 
            this.contextMenuNotify.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuNotify.Name = "contextMenuNotify";
            this.contextMenuNotify.Size = new System.Drawing.Size(104, 48);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.showToolStripMenuItem.Text = "Show";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // btnNewTask
            // 
            this.btnNewTask.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewTask.Location = new System.Drawing.Point(4, 12);
            this.btnNewTask.Name = "btnNewTask";
            this.btnNewTask.Size = new System.Drawing.Size(52, 28);
            this.btnNewTask.TabIndex = 2;
            this.btnNewTask.Text = "New";
            this.btnNewTask.UseVisualStyleBackColor = true;
            this.btnNewTask.Click += new System.EventHandler(this.btnNewTask_Click);
            // 
            // btnEditTask
            // 
            this.btnEditTask.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditTask.Location = new System.Drawing.Point(62, 12);
            this.btnEditTask.Name = "btnEditTask";
            this.btnEditTask.Size = new System.Drawing.Size(52, 28);
            this.btnEditTask.TabIndex = 2;
            this.btnEditTask.Text = "Edit";
            this.btnEditTask.UseVisualStyleBackColor = true;
            this.btnEditTask.Click += new System.EventHandler(this.btnEditTask_Click);
            // 
            // btnDeleteTask
            // 
            this.btnDeleteTask.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteTask.Location = new System.Drawing.Point(120, 12);
            this.btnDeleteTask.Name = "btnDeleteTask";
            this.btnDeleteTask.Size = new System.Drawing.Size(52, 28);
            this.btnDeleteTask.TabIndex = 2;
            this.btnDeleteTask.Text = "Delete";
            this.btnDeleteTask.UseVisualStyleBackColor = true;
            this.btnDeleteTask.Click += new System.EventHandler(this.btnDeleteTask_Click);
            // 
            // checkHideCompletedSubtasks
            // 
            this.checkHideCompletedSubtasks.AutoSize = true;
            this.checkHideCompletedSubtasks.Location = new System.Drawing.Point(576, 12);
            this.checkHideCompletedSubtasks.Name = "checkHideCompletedSubtasks";
            this.checkHideCompletedSubtasks.Size = new System.Drawing.Size(145, 17);
            this.checkHideCompletedSubtasks.TabIndex = 3;
            this.checkHideCompletedSubtasks.Text = "Hide completed subtasks";
            this.checkHideCompletedSubtasks.UseVisualStyleBackColor = true;
            this.checkHideCompletedSubtasks.CheckedChanged += new System.EventHandler(this.checkHideCompletedSubtasks_CheckedChanged);
            // 
            // cxtMenuCurrentTaskEdit
            // 
            this.cxtMenuCurrentTaskEdit.Name = "cxtMenuCurrentTaskEdit";
            this.cxtMenuCurrentTaskEdit.Size = new System.Drawing.Size(213, 22);
            this.cxtMenuCurrentTaskEdit.Text = "Edit (F2)";
            this.cxtMenuCurrentTaskEdit.Click += new System.EventHandler(this.cxtMenuCurrentTaskEdit_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(725, 343);
            this.Controls.Add(this.checkHideCompletedSubtasks);
            this.Controls.Add(this.btnDeleteTask);
            this.Controls.Add(this.btnEditTask);
            this.Controls.Add(this.btnNewTask);
            this.Controls.Add(this.splitContainer1);
            this.ForeColor = System.Drawing.Color.RoyalBlue;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "TodoList";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.contextMenuCurrentTask.ResumeLayout(false);
            this.contextMenuNotify.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView listViewAllTasks;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.RichTextBox descriptionTextBox;
        private System.Windows.Forms.TreeView currentTaskTreeView;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuNotify;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuCurrentTask;
        private System.Windows.Forms.ToolStripMenuItem cxtMenuCurrentTaskMoveUp;
        private System.Windows.Forms.ToolStripMenuItem cxtMenuCurrentTaskMoveDown;
        private System.Windows.Forms.ToolStripMenuItem cxtMenuCurrentTaskSetCompleted;
        private System.Windows.Forms.ToolStripMenuItem cxtMenuCurrentTaskSetNotCompleted;
        private System.Windows.Forms.ToolStripMenuItem cxtMenuCurrentTaskDeleteSubtask;
        private System.Windows.Forms.Button btnNewTask;
        private System.Windows.Forms.Button btnEditTask;
        private System.Windows.Forms.Button btnDeleteTask;
        private System.Windows.Forms.CheckBox checkHideCompletedSubtasks;
        private System.Windows.Forms.ToolStripMenuItem cxtMenuCurrentTaskEdit;
    }
}