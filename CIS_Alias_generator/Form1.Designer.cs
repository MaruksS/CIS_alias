﻿namespace CIS_Alias_generator
{
    partial class Form1
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
            this.lbl_name = new System.Windows.Forms.Label();
            this.txt_input = new System.Windows.Forms.TextBox();
            this.txt_output = new System.Windows.Forms.TextBox();
            this.lbl_alias = new System.Windows.Forms.Label();
            this.btn_generate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.chb_Person = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lbl_name
            // 
            this.lbl_name.AutoSize = true;
            this.lbl_name.Location = new System.Drawing.Point(24, 85);
            this.lbl_name.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbl_name.Name = "lbl_name";
            this.lbl_name.Size = new System.Drawing.Size(64, 25);
            this.lbl_name.TabIndex = 0;
            this.lbl_name.Text = "Name";
            // 
            // txt_input
            // 
            this.txt_input.Location = new System.Drawing.Point(152, 85);
            this.txt_input.Margin = new System.Windows.Forms.Padding(6);
            this.txt_input.Name = "txt_input";
            this.txt_input.Size = new System.Drawing.Size(516, 29);
            this.txt_input.TabIndex = 1;
            // 
            // txt_output
            // 
            this.txt_output.Location = new System.Drawing.Point(152, 177);
            this.txt_output.Margin = new System.Windows.Forms.Padding(6);
            this.txt_output.Multiline = true;
            this.txt_output.Name = "txt_output";
            this.txt_output.Size = new System.Drawing.Size(803, 366);
            this.txt_output.TabIndex = 2;
            // 
            // lbl_alias
            // 
            this.lbl_alias.AutoSize = true;
            this.lbl_alias.Location = new System.Drawing.Point(29, 177);
            this.lbl_alias.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbl_alias.Name = "lbl_alias";
            this.lbl_alias.Size = new System.Drawing.Size(55, 25);
            this.lbl_alias.TabIndex = 3;
            this.lbl_alias.Text = "Alias";
            // 
            // btn_generate
            // 
            this.btn_generate.Location = new System.Drawing.Point(839, 76);
            this.btn_generate.Margin = new System.Windows.Forms.Padding(6);
            this.btn_generate.Name = "btn_generate";
            this.btn_generate.Size = new System.Drawing.Size(138, 42);
            this.btn_generate.TabIndex = 4;
            this.btn_generate.Text = "Generate";
            this.btn_generate.UseVisualStyleBackColor = true;
            this.btn_generate.Click += new System.EventHandler(this.onBtnClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(152, 638);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "label1";
            // 
            // chb_Person
            // 
            this.chb_Person.AutoSize = true;
            this.chb_Person.Location = new System.Drawing.Point(677, 87);
            this.chb_Person.Name = "chb_Person";
            this.chb_Person.Size = new System.Drawing.Size(86, 29);
            this.chb_Person.TabIndex = 6;
            this.chb_Person.Text = "Entity";
            this.chb_Person.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AcceptButton = this.btn_generate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1032, 823);
            this.Controls.Add(this.chb_Person);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_generate);
            this.Controls.Add(this.lbl_alias);
            this.Controls.Add(this.txt_output);
            this.Controls.Add(this.txt_input);
            this.Controls.Add(this.lbl_name);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Form1";
            this.Text = "CIS Alias Generator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_name;
        private System.Windows.Forms.TextBox txt_input;
        private System.Windows.Forms.TextBox txt_output;
        private System.Windows.Forms.Label lbl_alias;
        private System.Windows.Forms.Button btn_generate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chb_Person;
    }
}

