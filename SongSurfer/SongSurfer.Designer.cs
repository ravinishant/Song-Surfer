namespace SongSurfer
{
    partial class SongSurfer
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnStopThread = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(65, 37);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(137, 20);
            this.textBox1.TabIndex = 0;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(65, 320);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(137, 20);
            this.textBox2.TabIndex = 1;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(280, 37);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(244, 303);
            this.listBox1.TabIndex = 2;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(33, 175);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 3;
            this.btnLoad.Text = "Load Songs";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(161, 175);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(85, 23);
            this.btnPlay.TabIndex = 4;
            this.btnPlay.Text = "PlaySelected";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(33, 419);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(75, 23);
            this.btnDownload.TabIndex = 5;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnStopThread
            // 
            this.btnStopThread.Location = new System.Drawing.Point(161, 419);
            this.btnStopThread.Name = "btnStopThread";
            this.btnStopThread.Size = new System.Drawing.Size(75, 23);
            this.btnStopThread.TabIndex = 6;
            this.btnStopThread.Text = "Stop Thread";
            this.btnStopThread.UseVisualStyleBackColor = true;
            this.btnStopThread.Click += new System.EventHandler(this.btnStopThread_Click);
            // 
            // SongSurfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 501);
            this.Controls.Add(this.btnStopThread);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Name = "SongSurfer";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnStopThread;
    }
}

