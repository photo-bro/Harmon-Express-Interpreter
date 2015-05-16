namespace HarmonExpressInterpretor
{
    partial class HarmonExpressInterpreter
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
            this.tb_Console = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menu_Main = new System.Windows.Forms.ToolStripMenuItem();
            this.openTomFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayTokenListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayParseTreeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearConsoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleConsoleBoxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_Go = new System.Windows.Forms.Button();
            this.tb_Expression = new System.Windows.Forms.TextBox();
            this.btn_Tokenize = new System.Windows.Forms.Button();
            this.btn_PrintParseTree = new System.Windows.Forms.Button();
            this.aboutInformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tb_Console
            // 
            this.tb_Console.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Console.Location = new System.Drawing.Point(12, 115);
            this.tb_Console.Multiline = true;
            this.tb_Console.Name = "tb_Console";
            this.tb_Console.ReadOnly = true;
            this.tb_Console.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tb_Console.Size = new System.Drawing.Size(760, 312);
            this.tb_Console.TabIndex = 3;
            this.tb_Console.Visible = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Black;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_Main});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menu_Main
            // 
            this.menu_Main.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutInformationToolStripMenuItem,
            this.openTomFileToolStripMenuItem,
            this.openToolStripMenuItem,
            this.displayTokenListToolStripMenuItem,
            this.displayParseTreeToolStripMenuItem,
            this.clearConsoleToolStripMenuItem,
            this.toggleConsoleBoxToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.menu_Main.ForeColor = System.Drawing.Color.White;
            this.menu_Main.Name = "menu_Main";
            this.menu_Main.Size = new System.Drawing.Size(37, 20);
            this.menu_Main.Text = "File";
            // 
            // openTomFileToolStripMenuItem
            // 
            this.openTomFileToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.openTomFileToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.openTomFileToolStripMenuItem.Name = "openTomFileToolStripMenuItem";
            this.openTomFileToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.openTomFileToolStripMenuItem.Text = "Open Tom File";
            this.openTomFileToolStripMenuItem.Click += new System.EventHandler(this.openTomFileToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.openToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.openToolStripMenuItem.Text = "Open Expression File";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // displayTokenListToolStripMenuItem
            // 
            this.displayTokenListToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.displayTokenListToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.displayTokenListToolStripMenuItem.Name = "displayTokenListToolStripMenuItem";
            this.displayTokenListToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.displayTokenListToolStripMenuItem.Text = "Display Token List";
            this.displayTokenListToolStripMenuItem.Visible = false;
            this.displayTokenListToolStripMenuItem.Click += new System.EventHandler(this.displayTokenListToolStripMenuItem_Click);
            // 
            // displayParseTreeToolStripMenuItem
            // 
            this.displayParseTreeToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.displayParseTreeToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.displayParseTreeToolStripMenuItem.Name = "displayParseTreeToolStripMenuItem";
            this.displayParseTreeToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.displayParseTreeToolStripMenuItem.Text = "Display Parse Tree";
            this.displayParseTreeToolStripMenuItem.Visible = false;
            this.displayParseTreeToolStripMenuItem.Click += new System.EventHandler(this.displayParseTreeToolStripMenuItem_Click);
            // 
            // clearConsoleToolStripMenuItem
            // 
            this.clearConsoleToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.clearConsoleToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.clearConsoleToolStripMenuItem.Name = "clearConsoleToolStripMenuItem";
            this.clearConsoleToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.clearConsoleToolStripMenuItem.Text = "Clear Console";
            this.clearConsoleToolStripMenuItem.Visible = false;
            this.clearConsoleToolStripMenuItem.Click += new System.EventHandler(this.clearConsoleToolStripMenuItem_Click);
            // 
            // toggleConsoleBoxToolStripMenuItem
            // 
            this.toggleConsoleBoxToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.toggleConsoleBoxToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.toggleConsoleBoxToolStripMenuItem.Name = "toggleConsoleBoxToolStripMenuItem";
            this.toggleConsoleBoxToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.toggleConsoleBoxToolStripMenuItem.Text = "Toggle Console Box";
            this.toggleConsoleBoxToolStripMenuItem.Click += new System.EventHandler(this.toggleConsoleBoxToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.quitToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // btn_Go
            // 
            this.btn_Go.Location = new System.Drawing.Point(577, 43);
            this.btn_Go.Name = "btn_Go";
            this.btn_Go.Size = new System.Drawing.Size(71, 69);
            this.btn_Go.TabIndex = 1;
            this.btn_Go.Text = "Go";
            this.btn_Go.UseVisualStyleBackColor = true;
            this.btn_Go.Click += new System.EventHandler(this.btn_Go_Click);
            // 
            // tb_Expression
            // 
            this.tb_Expression.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Expression.Location = new System.Drawing.Point(12, 67);
            this.tb_Expression.Name = "tb_Expression";
            this.tb_Expression.Size = new System.Drawing.Size(551, 22);
            this.tb_Expression.TabIndex = 0;
            this.tb_Expression.Text = "Expression ( ex:  3+4/(2*11) )";
            // 
            // btn_Tokenize
            // 
            this.btn_Tokenize.Location = new System.Drawing.Point(654, 43);
            this.btn_Tokenize.Name = "btn_Tokenize";
            this.btn_Tokenize.Size = new System.Drawing.Size(110, 32);
            this.btn_Tokenize.TabIndex = 2;
            this.btn_Tokenize.Text = "Print Token List";
            this.btn_Tokenize.UseVisualStyleBackColor = true;
            this.btn_Tokenize.Visible = false;
            this.btn_Tokenize.Click += new System.EventHandler(this.btn_Tokenize_Click);
            // 
            // btn_PrintParseTree
            // 
            this.btn_PrintParseTree.Location = new System.Drawing.Point(654, 77);
            this.btn_PrintParseTree.Name = "btn_PrintParseTree";
            this.btn_PrintParseTree.Size = new System.Drawing.Size(110, 32);
            this.btn_PrintParseTree.TabIndex = 4;
            this.btn_PrintParseTree.Text = "Print Parse Tree";
            this.btn_PrintParseTree.UseVisualStyleBackColor = true;
            this.btn_PrintParseTree.Visible = false;
            this.btn_PrintParseTree.Click += new System.EventHandler(this.btn_PrintParseTree_Click);
            // 
            // aboutInformationToolStripMenuItem
            // 
            this.aboutInformationToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.aboutInformationToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.aboutInformationToolStripMenuItem.Name = "aboutInformationToolStripMenuItem";
            this.aboutInformationToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.aboutInformationToolStripMenuItem.Text = "About / Information";
            this.aboutInformationToolStripMenuItem.Click += new System.EventHandler(this.aboutInformationToolStripMenuItem_Click);
            // 
            // HarmonExpressInterpreter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::HarmonExpressInterpretor.Properties.Resources.BG_Logo_a1;
            this.ClientSize = new System.Drawing.Size(784, 442);
            this.Controls.Add(this.btn_PrintParseTree);
            this.Controls.Add(this.btn_Tokenize);
            this.Controls.Add(this.tb_Expression);
            this.Controls.Add(this.btn_Go);
            this.Controls.Add(this.tb_Console);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "HarmonExpressInterpreter";
            this.Text = "Harmon Express Interpreter";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_Console;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menu_Main;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.Button btn_Go;
        private System.Windows.Forms.TextBox tb_Expression;
        private System.Windows.Forms.ToolStripMenuItem displayTokenListToolStripMenuItem;
        private System.Windows.Forms.Button btn_Tokenize;
        private System.Windows.Forms.Button btn_PrintParseTree;
        private System.Windows.Forms.ToolStripMenuItem openTomFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displayParseTreeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearConsoleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toggleConsoleBoxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutInformationToolStripMenuItem;
    }
}

