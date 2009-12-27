namespace DCT.UI
{
    partial class ChatUI
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblChatOnline = new System.Windows.Forms.Label();
            this.txtChatType = new System.Windows.Forms.TextBox();
            this.lstChat = new System.Windows.Forms.ListBox();
            this.split = new System.Windows.Forms.SplitContainer();
            this.split1 = new System.Windows.Forms.SplitContainer();
            this.txtChat = new System.Windows.Forms.TextBox();
            this.split.Panel1.SuspendLayout();
            this.split.Panel2.SuspendLayout();
            this.split.SuspendLayout();
            this.split1.Panel1.SuspendLayout();
            this.split1.Panel2.SuspendLayout();
            this.split1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblChatOnline
            // 
            this.lblChatOnline.AutoSize = true;
            this.lblChatOnline.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblChatOnline.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChatOnline.Location = new System.Drawing.Point(0, 0);
            this.lblChatOnline.Name = "lblChatOnline";
            this.lblChatOnline.Size = new System.Drawing.Size(65, 12);
            this.lblChatOnline.TabIndex = 7;
            this.lblChatOnline.Text = "Not connected";
            // 
            // txtChatType
            // 
            this.txtChatType.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtChatType.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChatType.Location = new System.Drawing.Point(0, 0);
            this.txtChatType.MaxLength = 250;
            this.txtChatType.Name = "txtChatType";
            this.txtChatType.Size = new System.Drawing.Size(333, 21);
            this.txtChatType.TabIndex = 6;
            this.txtChatType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtChatType_KeyDown);
            // 
            // lstChat
            // 
            this.lstChat.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lstChat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lstChat.FormattingEnabled = true;
            this.lstChat.HorizontalScrollbar = true;
            this.lstChat.Location = new System.Drawing.Point(0, 11);
            this.lstChat.Name = "lstChat";
            this.lstChat.Size = new System.Drawing.Size(89, 251);
            this.lstChat.TabIndex = 5;
            this.lstChat.Click += new System.EventHandler(this.lstChat_Click);
            // 
            // split
            // 
            this.split.Dock = System.Windows.Forms.DockStyle.Fill;
            this.split.Location = new System.Drawing.Point(0, 0);
            this.split.Name = "split";
            // 
            // split.Panel1
            // 
            this.split.Panel1.Controls.Add(this.split1);
            // 
            // split.Panel2
            // 
            this.split.Panel2.Controls.Add(this.lstChat);
            this.split.Panel2.Controls.Add(this.lblChatOnline);
            this.split.Size = new System.Drawing.Size(426, 262);
            this.split.SplitterDistance = 333;
            this.split.TabIndex = 8;
            // 
            // split1
            // 
            this.split1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.split1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.split1.Location = new System.Drawing.Point(0, 0);
            this.split1.Name = "split1";
            this.split1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // split1.Panel1
            // 
            this.split1.Panel1.Controls.Add(this.txtChat);
            // 
            // split1.Panel2
            // 
            this.split1.Panel2.Controls.Add(this.txtChatType);
            this.split1.Size = new System.Drawing.Size(333, 262);
            this.split1.SplitterDistance = 232;
            this.split1.TabIndex = 7;
            // 
            // txtChat
            // 
            this.txtChat.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtChat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtChat.Location = new System.Drawing.Point(0, 0);
            this.txtChat.Multiline = true;
            this.txtChat.Name = "txtChat";
            this.txtChat.ReadOnly = true;
            this.txtChat.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtChat.Size = new System.Drawing.Size(333, 232);
            this.txtChat.TabIndex = 0;
            // 
            // ChatUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.split);
            this.Name = "ChatUI";
            this.Size = new System.Drawing.Size(426, 262);
            this.split.Panel1.ResumeLayout(false);
            this.split.Panel2.ResumeLayout(false);
            this.split.Panel2.PerformLayout();
            this.split.ResumeLayout(false);
            this.split1.Panel1.ResumeLayout(false);
            this.split1.Panel1.PerformLayout();
            this.split1.Panel2.ResumeLayout(false);
            this.split1.Panel2.PerformLayout();
            this.split1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblChatOnline;
        private System.Windows.Forms.TextBox txtChatType;
        private System.Windows.Forms.ListBox lstChat;
        private System.Windows.Forms.SplitContainer split;
        private System.Windows.Forms.SplitContainer split1;
        private System.Windows.Forms.TextBox txtChat;
    }
}