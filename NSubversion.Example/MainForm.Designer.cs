/*******************************************************************************

  NSubversion (C) 2010 Krzysztof Arkadiusz Prusik
  SVN, CVS tags in Your application, by the reflection SvnIdAttribute
  Latest version: http://NSubversion.codeplex.com/

  $Id$

  This library is free software; you can redistribute it and/or
  modify it under the terms of the GNU Lesser General Public
  License as published by the Free Software Foundation; either
  version 2.1 of the License, or (at your option) any later version.

  This library is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
  Lesser General Public License for more details.

*******************************************************************************/
namespace NSubversion.Example
{
    [SvnId("$Id$")]
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
            this.btnShowAsText = new System.Windows.Forms.Button();
            this.btnShowSnvRepositoryDialog = new System.Windows.Forms.Button();
            this.svnDialog1 = new NSubversion.SvnDialog(this.components);
            this.SuspendLayout();
            // 
            // btnShowAsText
            // 
            this.btnShowAsText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowAsText.Location = new System.Drawing.Point(44, 42);
            this.btnShowAsText.Name = "btnShowAsText";
            this.btnShowAsText.Size = new System.Drawing.Size(196, 23);
            this.btnShowAsText.TabIndex = 0;
            this.btnShowAsText.Text = "Show SubVersion Info As Text";
            this.btnShowAsText.UseVisualStyleBackColor = true;
            this.btnShowAsText.Click += new System.EventHandler(this.btnShowSubVersionInfoAsText_Click);
            // 
            // btnShowSnvRepositoryDialog
            // 
            this.btnShowSnvRepositoryDialog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowSnvRepositoryDialog.Location = new System.Drawing.Point(44, 71);
            this.btnShowSnvRepositoryDialog.Name = "btnShowSnvRepositoryDialog";
            this.btnShowSnvRepositoryDialog.Size = new System.Drawing.Size(196, 23);
            this.btnShowSnvRepositoryDialog.TabIndex = 2;
            this.btnShowSnvRepositoryDialog.Text = "Show SvnRepository dialog";
            this.btnShowSnvRepositoryDialog.UseVisualStyleBackColor = true;
            this.btnShowSnvRepositoryDialog.Click += new System.EventHandler(this.btnShowSnvRepositoryDialog_Click);
            // 
            // svnDialog1
            // 
            this.svnDialog1.HeaderText = "";
            this.svnDialog1.TitleText = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 138);
            this.Controls.Add(this.btnShowSnvRepositoryDialog);
            this.Controls.Add(this.btnShowAsText);
            this.Name = "MainForm";
            this.Text = "Example MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnShowAsText;
        private System.Windows.Forms.Button btnShowSnvRepositoryDialog;
        private SvnDialog svnDialog1;
    }
}

