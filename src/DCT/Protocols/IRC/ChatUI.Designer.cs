namespace DCT.Protocols.IRC
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
            this.txtChat = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblChatOnline
            // 
            this.lblChatOnline.AutoSize = true;
            this.lblChatOnline.Location = new System.Drawing.Point(322, 213);
            this.lblChatOnline.Name = "lblChatOnline";
            this.lblChatOnline.Size = new System.Drawing.Size(78, 13);
            this.lblChatOnline.TabIndex = 7;
            this.lblChatOnline.Text = "Not connected";
            // 
            // txtChatType
            // 
            this.txtChatType.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChatType.Location = new System.Drawing.Point(3, 210);
            this.txtChatType.MaxLength = 250;
            this.txtChatType.Name = "txtChatType";
            this.txtChatType.Size = new System.Drawing.Size(313, 21);
            this.txtChatType.TabIndex = 6;
            this.txtChatType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtChatType_KeyDown);
            // 
            // lstChat
            // 
            this.lstChat.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstChat.FormattingEnabled = true;
            this.lstChat.Location = new System.Drawing.Point(322, 3);
            this.lstChat.Name = "lstChat";
            this.lstChat.Size = new System.Drawing.Size(85, 199);
            this.lstChat.TabIndex = 5;
            // 
            // txtChat
            // 
            this.txtChat.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtChat.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChat.Location = new System.Drawing.Point(3, 3);
            this.txtChat.Multiline = true;
            this.txtChat.Name = "txtChat";
            this.txtChat.ReadOnly = true;
            this.txtChat.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtChat.Size = new System.Drawing.Size(313, 201);
            this.txtChat.TabIndex = 4;
            // 
            // ChatUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblChatOnline);
            this.Controls.Add(this.txtChatType);
            this.Controls.Add(this.lstChat);
            this.Controls.Add(this.txtChat);
            this.Name = "ChatUI";
            this.Size = new System.Drawing.Size(414, 234);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblChatOnline;
        private System.Windows.Forms.TextBox txtChatType;
        private System.Windows.Forms.ListBox lstChat;
        private System.Windows.Forms.TextBox txtChat;
    }
}