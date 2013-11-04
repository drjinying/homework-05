namespace AsyncTcpServer
{
    partial class mainForm
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
            this.tb_svr_IP = new System.Windows.Forms.TextBox();
            this.btn_start = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_stop = new System.Windows.Forms.Button();
            this.tb_id = new System.Windows.Forms.TextBox();
            this.tb_passwd = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lb_log = new System.Windows.Forms.ListBox();
            this.tb_svr_port = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tb_svr_IP
            // 
            this.tb_svr_IP.Location = new System.Drawing.Point(133, 14);
            this.tb_svr_IP.Name = "tb_svr_IP";
            this.tb_svr_IP.Size = new System.Drawing.Size(120, 20);
            this.tb_svr_IP.TabIndex = 0;
            this.tb_svr_IP.Text = "IP";
            this.tb_svr_IP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btn_start
            // 
            this.btn_start.BackColor = System.Drawing.Color.LightGreen;
            this.btn_start.Location = new System.Drawing.Point(133, 188);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(120, 23);
            this.btn_start.TabIndex = 2;
            this.btn_start.Text = "START";
            this.btn_start.UseVisualStyleBackColor = false;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(63, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Server";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(65, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Log";
            // 
            // btn_stop
            // 
            this.btn_stop.BackColor = System.Drawing.Color.Crimson;
            this.btn_stop.Location = new System.Drawing.Point(269, 188);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(120, 23);
            this.btn_stop.TabIndex = 5;
            this.btn_stop.Text = "STOP";
            this.btn_stop.UseVisualStyleBackColor = false;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // tb_id
            // 
            this.tb_id.Location = new System.Drawing.Point(133, 42);
            this.tb_id.Name = "tb_id";
            this.tb_id.Size = new System.Drawing.Size(120, 20);
            this.tb_id.TabIndex = 6;
            this.tb_id.Text = "ID";
            this.tb_id.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tb_passwd
            // 
            this.tb_passwd.Location = new System.Drawing.Point(269, 42);
            this.tb_passwd.Name = "tb_passwd";
            this.tb_passwd.Size = new System.Drawing.Size(120, 20);
            this.tb_passwd.TabIndex = 7;
            this.tb_passwd.Text = "PASSWORD";
            this.tb_passwd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(63, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Account";
            // 
            // lb_log
            // 
            this.lb_log.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lb_log.FormattingEnabled = true;
            this.lb_log.HorizontalScrollbar = true;
            this.lb_log.Location = new System.Drawing.Point(133, 78);
            this.lb_log.Name = "lb_log";
            this.lb_log.Size = new System.Drawing.Size(256, 95);
            this.lb_log.TabIndex = 9;
            // 
            // tb_svr_port
            // 
            this.tb_svr_port.Location = new System.Drawing.Point(269, 14);
            this.tb_svr_port.Name = "tb_svr_port";
            this.tb_svr_port.Size = new System.Drawing.Size(120, 20);
            this.tb_svr_port.TabIndex = 10;
            this.tb_svr_port.Text = "PORT";
            this.tb_svr_port.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MintCream;
            this.ClientSize = new System.Drawing.Size(464, 231);
            this.Controls.Add(this.tb_svr_port);
            this.Controls.Add(this.lb_log);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tb_passwd);
            this.Controls.Add(this.tb_id);
            this.Controls.Add(this.btn_stop);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.tb_svr_IP);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "mainForm";
            this.Opacity = 0.95D;
            this.ShowIcon = false;
            this.Text = "GP Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_svr_IP;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_stop;
        private System.Windows.Forms.TextBox tb_id;
        private System.Windows.Forms.TextBox tb_passwd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lb_log;
        private System.Windows.Forms.TextBox tb_svr_port;
    }
}

