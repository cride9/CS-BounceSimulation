namespace Simulation {
    partial class Simulation {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            Generate = new Button();
            Remove = new Button();
            SuspendLayout();
            // 
            // Generate
            // 
            Generate.BackColor = Color.FromArgb(30, 30, 30);
            Generate.FlatStyle = FlatStyle.Flat;
            Generate.ForeColor = Color.FromArgb(212, 212, 212);
            Generate.Location = new Point(12, 12);
            Generate.Name = "Generate";
            Generate.Size = new Size(75, 23);
            Generate.TabIndex = 0;
            Generate.Text = "Generate";
            Generate.UseVisualStyleBackColor = false;
            Generate.Click += GenerateSquare;
            // 
            // Remove
            // 
            Remove.BackColor = Color.FromArgb(30, 30, 30);
            Remove.FlatStyle = FlatStyle.Flat;
            Remove.ForeColor = Color.FromArgb(212, 212, 212);
            Remove.Location = new Point(447, 12);
            Remove.Name = "Remove";
            Remove.Size = new Size(75, 23);
            Remove.TabIndex = 1;
            Remove.Text = "Remove";
            Remove.UseVisualStyleBackColor = false;
            Remove.Click += RemoveObject;
            // 
            // Simulation
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(22, 22, 22);
            ClientSize = new Size(534, 511);
            Controls.Add(Remove);
            Controls.Add(Generate);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Simulation";
            Text = "Simulation";
            Load += Initialize;
            ResumeLayout(false);
        }

        #endregion

        private Button Generate;
        private Button Remove;
    }
}