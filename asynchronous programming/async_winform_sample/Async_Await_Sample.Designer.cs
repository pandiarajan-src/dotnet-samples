namespace async_winform_sample
{
    partial class Async_Await_Sample
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Async_Await_Sample));
            this.Start = new System.Windows.Forms.Button();
            this.ProgressGIF = new System.Windows.Forms.PictureBox();
            this.lbInput = new System.Windows.Forms.Label();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.pgCards = new System.Windows.Forms.ProgressBar();
            this.btCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ProgressGIF)).BeginInit();
            this.SuspendLayout();
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(64, 98);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(112, 34);
            this.Start.TabIndex = 0;
            this.Start.Text = "Start";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // ProgressGIF
            // 
            this.ProgressGIF.Image = ((System.Drawing.Image)(resources.GetObject("ProgressGIF.Image")));
            this.ProgressGIF.Location = new System.Drawing.Point(64, 152);
            this.ProgressGIF.Name = "ProgressGIF";
            this.ProgressGIF.Size = new System.Drawing.Size(223, 234);
            this.ProgressGIF.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.ProgressGIF.TabIndex = 1;
            this.ProgressGIF.TabStop = false;
            this.ProgressGIF.Visible = false;
            // 
            // lbInput
            // 
            this.lbInput.AutoSize = true;
            this.lbInput.Location = new System.Drawing.Point(70, 45);
            this.lbInput.Name = "lbInput";
            this.lbInput.Size = new System.Drawing.Size(68, 25);
            this.lbInput.TabIndex = 2;
            this.lbInput.Text = "Input : ";
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(146, 45);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(231, 31);
            this.txtInput.TabIndex = 3;
            // 
            // pgCards
            // 
            this.pgCards.Location = new System.Drawing.Point(64, 404);
            this.pgCards.Name = "pgCards";
            this.pgCards.Size = new System.Drawing.Size(223, 34);
            this.pgCards.TabIndex = 4;
            this.pgCards.Visible = false;
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(187, 98);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(112, 34);
            this.btCancel.TabIndex = 5;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // Async_Await_Sample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.pgCards);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.lbInput);
            this.Controls.Add(this.ProgressGIF);
            this.Controls.Add(this.Start);
            this.Name = "Async_Await_Sample";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.ProgressGIF)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button Start;
        private PictureBox ProgressGIF;
        private Label lbInput;
        private TextBox txtInput;
        private ProgressBar pgCards;
        private Button btCancel;
    }
}