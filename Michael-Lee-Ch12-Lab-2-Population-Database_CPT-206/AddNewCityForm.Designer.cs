namespace Michael_Lee_Ch12_Lab_2_Population_Database_CPT_206
{
    partial class AddCityForm
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
            this.cityNameLabel = new System.Windows.Forms.Label();
            this.cityNameTextBox = new System.Windows.Forms.TextBox();
            this.closeButton = new System.Windows.Forms.Button();
            this.populationLabel = new System.Windows.Forms.Label();
            this.populationTextBox = new System.Windows.Forms.TextBox();
            this.addButton = new System.Windows.Forms.Button();
            this.stateComboBox = new System.Windows.Forms.ComboBox();
            this.stateLabel = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // cityNameLabel
            // 
            this.cityNameLabel.AutoSize = true;
            this.cityNameLabel.Location = new System.Drawing.Point(12, 14);
            this.cityNameLabel.Name = "cityNameLabel";
            this.cityNameLabel.Size = new System.Drawing.Size(85, 20);
            this.cityNameLabel.TabIndex = 0;
            this.cityNameLabel.Text = "City Name:";
            // 
            // cityNameTextBox
            // 
            this.cityNameTextBox.Location = new System.Drawing.Point(106, 11);
            this.cityNameTextBox.Name = "cityNameTextBox";
            this.cityNameTextBox.Size = new System.Drawing.Size(256, 26);
            this.cityNameTextBox.TabIndex = 1;
            // 
            // closeButton
            // 
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Location = new System.Drawing.Point(206, 125);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 35);
            this.closeButton.TabIndex = 2;
            this.closeButton.Text = "&Close";
            this.toolTip.SetToolTip(this.closeButton, "Close this window.");
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // populationLabel
            // 
            this.populationLabel.AutoSize = true;
            this.populationLabel.Location = new System.Drawing.Point(12, 84);
            this.populationLabel.Name = "populationLabel";
            this.populationLabel.Size = new System.Drawing.Size(88, 20);
            this.populationLabel.TabIndex = 3;
            this.populationLabel.Text = "Population:";
            // 
            // populationTextBox
            // 
            this.populationTextBox.Location = new System.Drawing.Point(106, 80);
            this.populationTextBox.Name = "populationTextBox";
            this.populationTextBox.Size = new System.Drawing.Size(256, 26);
            this.populationTextBox.TabIndex = 4;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(287, 125);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 35);
            this.addButton.TabIndex = 5;
            this.addButton.Text = "&Add";
            this.toolTip.SetToolTip(this.addButton, "Adds new city data to the database.");
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // stateComboBox
            // 
            this.stateComboBox.FormattingEnabled = true;
            this.stateComboBox.Location = new System.Drawing.Point(106, 46);
            this.stateComboBox.Name = "stateComboBox";
            this.stateComboBox.Size = new System.Drawing.Size(256, 28);
            this.stateComboBox.TabIndex = 6;
            // 
            // stateLabel
            // 
            this.stateLabel.AutoSize = true;
            this.stateLabel.Location = new System.Drawing.Point(12, 49);
            this.stateLabel.Name = "stateLabel";
            this.stateLabel.Size = new System.Drawing.Size(52, 20);
            this.stateLabel.TabIndex = 7;
            this.stateLabel.Text = "State:";
            // 
            // AddCityForm
            // 
            this.AcceptButton = this.addButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeButton;
            this.ClientSize = new System.Drawing.Size(374, 172);
            this.Controls.Add(this.stateLabel);
            this.Controls.Add(this.stateComboBox);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.populationTextBox);
            this.Controls.Add(this.populationLabel);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.cityNameTextBox);
            this.Controls.Add(this.cityNameLabel);
            this.Name = "AddCityForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add New City";
            this.Load += new System.EventHandler(this.AddCityForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label cityNameLabel;
        private System.Windows.Forms.TextBox cityNameTextBox;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Label populationLabel;
        private System.Windows.Forms.TextBox populationTextBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.ComboBox stateComboBox;
        private System.Windows.Forms.Label stateLabel;
        private System.Windows.Forms.ToolTip toolTip;
    }
}