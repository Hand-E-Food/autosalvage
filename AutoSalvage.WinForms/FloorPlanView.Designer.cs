namespace AutoSalvage.WinForms
{
    partial class FloorPlanView
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
            this.SuspendLayout();
            // 
            // FloorPlanView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.DoubleBuffered = true;
            this.Name = "FloorPlanView";
            this.MouseCaptureChanged += new System.EventHandler(this.FloorPlanView_MouseCaptureChanged);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FloorPlanView_MouseDown);
            this.MouseEnter += new System.EventHandler(this.FloorPlanView_MouseEnter);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FloorPlanView_MouseMove);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.FloorPlanView_MouseWheel);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FloorPlanView_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
