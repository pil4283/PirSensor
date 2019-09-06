namespace EntranceSensorForm
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.LogLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.optionComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.videoStatLabel = new System.Windows.Forms.Label();
            this.nextVideoLabel = new System.Windows.Forms.Label();
            this.logTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // LogLabel
            // 
            this.LogLabel.BackColor = System.Drawing.SystemColors.Control;
            this.LogLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LogLabel.Enabled = false;
            this.LogLabel.Location = new System.Drawing.Point(202, 36);
            this.LogLabel.Name = "LogLabel";
            this.LogLabel.Size = new System.Drawing.Size(260, 405);
            this.LogLabel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(314, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Log";
            // 
            // optionComboBox
            // 
            this.optionComboBox.FormattingEnabled = true;
            this.optionComboBox.Location = new System.Drawing.Point(26, 127);
            this.optionComboBox.Name = "optionComboBox";
            this.optionComboBox.Size = new System.Drawing.Size(143, 20);
            this.optionComboBox.TabIndex = 2;
            this.optionComboBox.SelectedIndexChanged += new System.EventHandler(this.OptionComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(161, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "인식하고 난 뒤 \r\n동영상 재생까지 걸리는 시간";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("굴림", 11F);
            this.label3.Location = new System.Drawing.Point(12, 202);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "동영상 재생상태 :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("굴림", 11F);
            this.label4.Location = new System.Drawing.Point(13, 235);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(167, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "다음 동영상 재생여부 : ";
            // 
            // videoStatLabel
            // 
            this.videoStatLabel.AutoSize = true;
            this.videoStatLabel.Font = new System.Drawing.Font("굴림", 11F);
            this.videoStatLabel.Location = new System.Drawing.Point(141, 202);
            this.videoStatLabel.Name = "videoStatLabel";
            this.videoStatLabel.Size = new System.Drawing.Size(0, 15);
            this.videoStatLabel.TabIndex = 6;
            // 
            // nextVideoLabel
            // 
            this.nextVideoLabel.AutoSize = true;
            this.nextVideoLabel.Font = new System.Drawing.Font("굴림", 11F);
            this.nextVideoLabel.Location = new System.Drawing.Point(177, 235);
            this.nextVideoLabel.Name = "nextVideoLabel";
            this.nextVideoLabel.Size = new System.Drawing.Size(0, 15);
            this.nextVideoLabel.TabIndex = 7;
            // 
            // logTextBox
            // 
            this.logTextBox.Location = new System.Drawing.Point(202, 36);
            this.logTextBox.Multiline = true;
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logTextBox.Size = new System.Drawing.Size(260, 405);
            this.logTextBox.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(474, 450);
            this.Controls.Add(this.logTextBox);
            this.Controls.Add(this.nextVideoLabel);
            this.Controls.Add(this.videoStatLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.optionComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LogLabel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LogLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox optionComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label videoStatLabel;
        private System.Windows.Forms.Label nextVideoLabel;
        private System.Windows.Forms.TextBox logTextBox;
    }
}

