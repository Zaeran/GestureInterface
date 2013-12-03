namespace ImgTest
{
    partial class GestureTrackerForm
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
            this.PicBox = new System.Windows.Forms.PictureBox();
            this.GestureCreatorTitleText = new System.Windows.Forms.Label();
            this.GestureNameLabel = new System.Windows.Forms.Label();
            this.GestureMethodLabel = new System.Windows.Forms.Label();
            this.GestureSequenceLabel = new System.Windows.Forms.Label();
            this.GestureNameBox = new System.Windows.Forms.TextBox();
            this.GestureMethodBox = new System.Windows.Forms.TextBox();
            this.GestureCreateButton = new System.Windows.Forms.Button();
            this.GestureCreatorSequenceVar = new System.Windows.Forms.Label();
            this.GestureCreatorButtonUp = new System.Windows.Forms.Button();
            this.GestureCreatorButtonDown = new System.Windows.Forms.Button();
            this.GestureCreatorButtonRight = new System.Windows.Forms.Button();
            this.GestureCreatorButtonLeft = new System.Windows.Forms.Button();
            this.GestureCreatorButtonUpLeft = new System.Windows.Forms.Button();
            this.GestureCreatorButtonUpRight = new System.Windows.Forms.Button();
            this.GestureCreatorButtonDownLeft = new System.Windows.Forms.Button();
            this.GestureCreatorButtonDownRight = new System.Windows.Forms.Button();
            this.GestureCreatorButtonBack = new System.Windows.Forms.Button();
            this.GestureListBox = new System.Windows.Forms.ListBox();
            this.CurrentGesturesLabel = new System.Windows.Forms.Label();
            this.GestureCurrentRemoveButton = new System.Windows.Forms.Button();
            this.GestureCurrentNameLabel = new System.Windows.Forms.Label();
            this.GestureCurrentMethodLabel = new System.Windows.Forms.Label();
            this.GestureCurrentSequenceLabel = new System.Windows.Forms.Label();
            this.GestureCurrentNameVar = new System.Windows.Forms.Label();
            this.GestureCurrentMethodVar = new System.Windows.Forms.Label();
            this.GestureCurrentSequenceVar = new System.Windows.Forms.Label();
            this.GestureInfoLabel = new System.Windows.Forms.Label();
            this.GestureTrackerTitleLabel = new System.Windows.Forms.Label();
            this.GestureMovementSequenceLabel = new System.Windows.Forms.Label();
            this.GestureDescriptionLabel = new System.Windows.Forms.Label();
            this.GestureCurrentDescriptionVar = new System.Windows.Forms.Label();
            this.GestureCurrentDescriptionLabel = new System.Windows.Forms.Label();
            this.GestureDescriptionBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.GestureSaveBtn = new System.Windows.Forms.Button();
            this.GestureLoadBtn = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.PicBox)).BeginInit();
            this.SuspendLayout();
            // 
            // PicBox
            // 
            this.PicBox.Location = new System.Drawing.Point(12, 53);
            this.PicBox.Name = "PicBox";
            this.PicBox.Size = new System.Drawing.Size(600, 480);
            this.PicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicBox.TabIndex = 1;
            this.PicBox.TabStop = false;
            this.PicBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.PicBox_MouseDoubleClick);
            // 
            // GestureCreatorTitleText
            // 
            this.GestureCreatorTitleText.AutoSize = true;
            this.GestureCreatorTitleText.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GestureCreatorTitleText.Location = new System.Drawing.Point(654, 56);
            this.GestureCreatorTitleText.Name = "GestureCreatorTitleText";
            this.GestureCreatorTitleText.Size = new System.Drawing.Size(200, 24);
            this.GestureCreatorTitleText.TabIndex = 3;
            this.GestureCreatorTitleText.Text = "CREATE A GESTURE";
            // 
            // GestureNameLabel
            // 
            this.GestureNameLabel.AutoSize = true;
            this.GestureNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GestureNameLabel.Location = new System.Drawing.Point(654, 113);
            this.GestureNameLabel.Name = "GestureNameLabel";
            this.GestureNameLabel.Size = new System.Drawing.Size(66, 24);
            this.GestureNameLabel.TabIndex = 4;
            this.GestureNameLabel.Text = "Name:";
            // 
            // GestureMethodLabel
            // 
            this.GestureMethodLabel.AutoSize = true;
            this.GestureMethodLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GestureMethodLabel.Location = new System.Drawing.Point(654, 157);
            this.GestureMethodLabel.Name = "GestureMethodLabel";
            this.GestureMethodLabel.Size = new System.Drawing.Size(79, 24);
            this.GestureMethodLabel.TabIndex = 5;
            this.GestureMethodLabel.Text = "Method:";
            // 
            // GestureSequenceLabel
            // 
            this.GestureSequenceLabel.AutoSize = true;
            this.GestureSequenceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GestureSequenceLabel.Location = new System.Drawing.Point(654, 242);
            this.GestureSequenceLabel.Name = "GestureSequenceLabel";
            this.GestureSequenceLabel.Size = new System.Drawing.Size(103, 24);
            this.GestureSequenceLabel.TabIndex = 6;
            this.GestureSequenceLabel.Text = "Sequence:";
            // 
            // GestureNameBox
            // 
            this.GestureNameBox.Location = new System.Drawing.Point(784, 118);
            this.GestureNameBox.Name = "GestureNameBox";
            this.GestureNameBox.Size = new System.Drawing.Size(166, 20);
            this.GestureNameBox.TabIndex = 7;
            // 
            // GestureMethodBox
            // 
            this.GestureMethodBox.Location = new System.Drawing.Point(784, 162);
            this.GestureMethodBox.Name = "GestureMethodBox";
            this.GestureMethodBox.Size = new System.Drawing.Size(166, 20);
            this.GestureMethodBox.TabIndex = 8;
            // 
            // GestureCreateButton
            // 
            this.GestureCreateButton.Location = new System.Drawing.Point(644, 381);
            this.GestureCreateButton.Name = "GestureCreateButton";
            this.GestureCreateButton.Size = new System.Drawing.Size(328, 42);
            this.GestureCreateButton.TabIndex = 9;
            this.GestureCreateButton.Text = "Create Gesture";
            this.GestureCreateButton.UseVisualStyleBackColor = true;
            this.GestureCreateButton.Click += new System.EventHandler(this.GestureCreateButton_Click);
            // 
            // GestureCreatorSequenceVar
            // 
            this.GestureCreatorSequenceVar.AccessibleDescription = "";
            this.GestureCreatorSequenceVar.AutoSize = true;
            this.GestureCreatorSequenceVar.Location = new System.Drawing.Point(786, 250);
            this.GestureCreatorSequenceVar.Name = "GestureCreatorSequenceVar";
            this.GestureCreatorSequenceVar.Size = new System.Drawing.Size(0, 13);
            this.GestureCreatorSequenceVar.TabIndex = 10;
            // 
            // GestureCreatorButtonUp
            // 
            this.GestureCreatorButtonUp.Location = new System.Drawing.Point(789, 268);
            this.GestureCreatorButtonUp.Name = "GestureCreatorButtonUp";
            this.GestureCreatorButtonUp.Size = new System.Drawing.Size(30, 23);
            this.GestureCreatorButtonUp.TabIndex = 11;
            this.GestureCreatorButtonUp.Text = "U";
            this.GestureCreatorButtonUp.UseVisualStyleBackColor = true;
            this.GestureCreatorButtonUp.Click += new System.EventHandler(this.GestureCreatorButtonUp_Click);
            // 
            // GestureCreatorButtonDown
            // 
            this.GestureCreatorButtonDown.Location = new System.Drawing.Point(789, 352);
            this.GestureCreatorButtonDown.Name = "GestureCreatorButtonDown";
            this.GestureCreatorButtonDown.Size = new System.Drawing.Size(30, 23);
            this.GestureCreatorButtonDown.TabIndex = 12;
            this.GestureCreatorButtonDown.Text = "D";
            this.GestureCreatorButtonDown.UseVisualStyleBackColor = true;
            this.GestureCreatorButtonDown.Click += new System.EventHandler(this.GestureCreatorButtonDown_Click);
            // 
            // GestureCreatorButtonRight
            // 
            this.GestureCreatorButtonRight.Location = new System.Drawing.Point(846, 310);
            this.GestureCreatorButtonRight.Name = "GestureCreatorButtonRight";
            this.GestureCreatorButtonRight.Size = new System.Drawing.Size(30, 23);
            this.GestureCreatorButtonRight.TabIndex = 13;
            this.GestureCreatorButtonRight.Text = "R";
            this.GestureCreatorButtonRight.UseVisualStyleBackColor = true;
            this.GestureCreatorButtonRight.Click += new System.EventHandler(this.GestureCreatorButtonRight_Click);
            // 
            // GestureCreatorButtonLeft
            // 
            this.GestureCreatorButtonLeft.Location = new System.Drawing.Point(725, 310);
            this.GestureCreatorButtonLeft.Name = "GestureCreatorButtonLeft";
            this.GestureCreatorButtonLeft.Size = new System.Drawing.Size(30, 23);
            this.GestureCreatorButtonLeft.TabIndex = 14;
            this.GestureCreatorButtonLeft.Text = "L";
            this.GestureCreatorButtonLeft.UseVisualStyleBackColor = true;
            this.GestureCreatorButtonLeft.Click += new System.EventHandler(this.GestureCreatorButtonLeft_Click);
            // 
            // GestureCreatorButtonUpLeft
            // 
            this.GestureCreatorButtonUpLeft.Location = new System.Drawing.Point(749, 281);
            this.GestureCreatorButtonUpLeft.Name = "GestureCreatorButtonUpLeft";
            this.GestureCreatorButtonUpLeft.Size = new System.Drawing.Size(30, 23);
            this.GestureCreatorButtonUpLeft.TabIndex = 15;
            this.GestureCreatorButtonUpLeft.Text = "UL";
            this.GestureCreatorButtonUpLeft.UseVisualStyleBackColor = true;
            this.GestureCreatorButtonUpLeft.Click += new System.EventHandler(this.GestureCreatorButtonUpLeft_Click);
            // 
            // GestureCreatorButtonUpRight
            // 
            this.GestureCreatorButtonUpRight.Location = new System.Drawing.Point(825, 281);
            this.GestureCreatorButtonUpRight.Name = "GestureCreatorButtonUpRight";
            this.GestureCreatorButtonUpRight.Size = new System.Drawing.Size(33, 23);
            this.GestureCreatorButtonUpRight.TabIndex = 16;
            this.GestureCreatorButtonUpRight.Text = "UR";
            this.GestureCreatorButtonUpRight.UseVisualStyleBackColor = true;
            this.GestureCreatorButtonUpRight.Click += new System.EventHandler(this.GestureCreatorButtonUpRight_Click);
            // 
            // GestureCreatorButtonDownLeft
            // 
            this.GestureCreatorButtonDownLeft.Location = new System.Drawing.Point(749, 339);
            this.GestureCreatorButtonDownLeft.Name = "GestureCreatorButtonDownLeft";
            this.GestureCreatorButtonDownLeft.Size = new System.Drawing.Size(30, 23);
            this.GestureCreatorButtonDownLeft.TabIndex = 17;
            this.GestureCreatorButtonDownLeft.Text = "DL";
            this.GestureCreatorButtonDownLeft.UseVisualStyleBackColor = true;
            this.GestureCreatorButtonDownLeft.Click += new System.EventHandler(this.GestureCreatorButtonDownLeft_Click);
            // 
            // GestureCreatorButtonDownRight
            // 
            this.GestureCreatorButtonDownRight.Location = new System.Drawing.Point(825, 339);
            this.GestureCreatorButtonDownRight.Name = "GestureCreatorButtonDownRight";
            this.GestureCreatorButtonDownRight.Size = new System.Drawing.Size(33, 23);
            this.GestureCreatorButtonDownRight.TabIndex = 18;
            this.GestureCreatorButtonDownRight.Text = "DR";
            this.GestureCreatorButtonDownRight.UseVisualStyleBackColor = true;
            this.GestureCreatorButtonDownRight.Click += new System.EventHandler(this.GestureCreatorButtonDownRight_Click);
            // 
            // GestureCreatorButtonBack
            // 
            this.GestureCreatorButtonBack.Location = new System.Drawing.Point(775, 310);
            this.GestureCreatorButtonBack.Name = "GestureCreatorButtonBack";
            this.GestureCreatorButtonBack.Size = new System.Drawing.Size(55, 23);
            this.GestureCreatorButtonBack.TabIndex = 19;
            this.GestureCreatorButtonBack.Text = "BACK";
            this.GestureCreatorButtonBack.UseVisualStyleBackColor = true;
            this.GestureCreatorButtonBack.Click += new System.EventHandler(this.GestureCreatorButtonBack_Click);
            // 
            // GestureListBox
            // 
            this.GestureListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GestureListBox.FormattingEnabled = true;
            this.GestureListBox.ItemHeight = 20;
            this.GestureListBox.Location = new System.Drawing.Point(17, 567);
            this.GestureListBox.Name = "GestureListBox";
            this.GestureListBox.Size = new System.Drawing.Size(258, 164);
            this.GestureListBox.TabIndex = 20;
            this.GestureListBox.SelectedIndexChanged += new System.EventHandler(this.GestureListBox_SelectedIndexChanged);
            // 
            // CurrentGesturesLabel
            // 
            this.CurrentGesturesLabel.AutoSize = true;
            this.CurrentGesturesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentGesturesLabel.Location = new System.Drawing.Point(13, 540);
            this.CurrentGesturesLabel.Name = "CurrentGesturesLabel";
            this.CurrentGesturesLabel.Size = new System.Drawing.Size(208, 24);
            this.CurrentGesturesLabel.TabIndex = 21;
            this.CurrentGesturesLabel.Text = "CURRENT GESTURES";
            // 
            // GestureCurrentRemoveButton
            // 
            this.GestureCurrentRemoveButton.Location = new System.Drawing.Point(330, 708);
            this.GestureCurrentRemoveButton.Name = "GestureCurrentRemoveButton";
            this.GestureCurrentRemoveButton.Size = new System.Drawing.Size(118, 23);
            this.GestureCurrentRemoveButton.TabIndex = 23;
            this.GestureCurrentRemoveButton.Text = "REMOVE GESTURE";
            this.GestureCurrentRemoveButton.UseVisualStyleBackColor = true;
            this.GestureCurrentRemoveButton.Click += new System.EventHandler(this.GestureCurrentRemoveButton_Click);
            // 
            // GestureCurrentNameLabel
            // 
            this.GestureCurrentNameLabel.AutoSize = true;
            this.GestureCurrentNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GestureCurrentNameLabel.Location = new System.Drawing.Point(326, 566);
            this.GestureCurrentNameLabel.Name = "GestureCurrentNameLabel";
            this.GestureCurrentNameLabel.Size = new System.Drawing.Size(59, 20);
            this.GestureCurrentNameLabel.TabIndex = 24;
            this.GestureCurrentNameLabel.Text = "NAME:";
            // 
            // GestureCurrentMethodLabel
            // 
            this.GestureCurrentMethodLabel.AutoSize = true;
            this.GestureCurrentMethodLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GestureCurrentMethodLabel.Location = new System.Drawing.Point(326, 596);
            this.GestureCurrentMethodLabel.Name = "GestureCurrentMethodLabel";
            this.GestureCurrentMethodLabel.Size = new System.Drawing.Size(82, 20);
            this.GestureCurrentMethodLabel.TabIndex = 25;
            this.GestureCurrentMethodLabel.Text = "METHOD:";
            // 
            // GestureCurrentSequenceLabel
            // 
            this.GestureCurrentSequenceLabel.AutoSize = true;
            this.GestureCurrentSequenceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GestureCurrentSequenceLabel.Location = new System.Drawing.Point(326, 626);
            this.GestureCurrentSequenceLabel.Name = "GestureCurrentSequenceLabel";
            this.GestureCurrentSequenceLabel.Size = new System.Drawing.Size(103, 20);
            this.GestureCurrentSequenceLabel.TabIndex = 26;
            this.GestureCurrentSequenceLabel.Text = "SEQUENCE:";
            // 
            // GestureCurrentNameVar
            // 
            this.GestureCurrentNameVar.AutoSize = true;
            this.GestureCurrentNameVar.Location = new System.Drawing.Point(486, 571);
            this.GestureCurrentNameVar.Name = "GestureCurrentNameVar";
            this.GestureCurrentNameVar.Size = new System.Drawing.Size(24, 13);
            this.GestureCurrentNameVar.TabIndex = 27;
            this.GestureCurrentNameVar.Text = "text";
            // 
            // GestureCurrentMethodVar
            // 
            this.GestureCurrentMethodVar.AutoSize = true;
            this.GestureCurrentMethodVar.Location = new System.Drawing.Point(486, 601);
            this.GestureCurrentMethodVar.Name = "GestureCurrentMethodVar";
            this.GestureCurrentMethodVar.Size = new System.Drawing.Size(24, 13);
            this.GestureCurrentMethodVar.TabIndex = 28;
            this.GestureCurrentMethodVar.Text = "text";
            // 
            // GestureCurrentSequenceVar
            // 
            this.GestureCurrentSequenceVar.AutoSize = true;
            this.GestureCurrentSequenceVar.Location = new System.Drawing.Point(486, 631);
            this.GestureCurrentSequenceVar.Name = "GestureCurrentSequenceVar";
            this.GestureCurrentSequenceVar.Size = new System.Drawing.Size(24, 13);
            this.GestureCurrentSequenceVar.TabIndex = 29;
            this.GestureCurrentSequenceVar.Text = "text";
            // 
            // GestureInfoLabel
            // 
            this.GestureInfoLabel.AutoSize = true;
            this.GestureInfoLabel.BackColor = System.Drawing.SystemColors.Control;
            this.GestureInfoLabel.Location = new System.Drawing.Point(518, 25);
            this.GestureInfoLabel.MinimumSize = new System.Drawing.Size(100, 0);
            this.GestureInfoLabel.Name = "GestureInfoLabel";
            this.GestureInfoLabel.Size = new System.Drawing.Size(109, 13);
            this.GestureInfoLabel.TabIndex = 2;
            this.GestureInfoLabel.Text = "MoveSequenceLabel";
            // 
            // GestureTrackerTitleLabel
            // 
            this.GestureTrackerTitleLabel.AutoSize = true;
            this.GestureTrackerTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GestureTrackerTitleLabel.Location = new System.Drawing.Point(13, 13);
            this.GestureTrackerTitleLabel.Name = "GestureTrackerTitleLabel";
            this.GestureTrackerTitleLabel.Size = new System.Drawing.Size(181, 29);
            this.GestureTrackerTitleLabel.TabIndex = 30;
            this.GestureTrackerTitleLabel.Text = "GestureTracker";
            // 
            // GestureMovementSequenceLabel
            // 
            this.GestureMovementSequenceLabel.AutoSize = true;
            this.GestureMovementSequenceLabel.Location = new System.Drawing.Point(363, 25);
            this.GestureMovementSequenceLabel.Name = "GestureMovementSequenceLabel";
            this.GestureMovementSequenceLabel.Size = new System.Drawing.Size(149, 13);
            this.GestureMovementSequenceLabel.TabIndex = 31;
            this.GestureMovementSequenceLabel.Text = "Current movement sequence: ";
            // 
            // GestureDescriptionLabel
            // 
            this.GestureDescriptionLabel.AutoSize = true;
            this.GestureDescriptionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GestureDescriptionLabel.Location = new System.Drawing.Point(654, 201);
            this.GestureDescriptionLabel.Name = "GestureDescriptionLabel";
            this.GestureDescriptionLabel.Size = new System.Drawing.Size(109, 24);
            this.GestureDescriptionLabel.TabIndex = 32;
            this.GestureDescriptionLabel.Text = "Description:";
            // 
            // GestureCurrentDescriptionVar
            // 
            this.GestureCurrentDescriptionVar.AutoSize = true;
            this.GestureCurrentDescriptionVar.Location = new System.Drawing.Point(486, 661);
            this.GestureCurrentDescriptionVar.Name = "GestureCurrentDescriptionVar";
            this.GestureCurrentDescriptionVar.Size = new System.Drawing.Size(24, 13);
            this.GestureCurrentDescriptionVar.TabIndex = 35;
            this.GestureCurrentDescriptionVar.Text = "text";
            // 
            // GestureCurrentDescriptionLabel
            // 
            this.GestureCurrentDescriptionLabel.AutoSize = true;
            this.GestureCurrentDescriptionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GestureCurrentDescriptionLabel.Location = new System.Drawing.Point(326, 656);
            this.GestureCurrentDescriptionLabel.Name = "GestureCurrentDescriptionLabel";
            this.GestureCurrentDescriptionLabel.Size = new System.Drawing.Size(122, 20);
            this.GestureCurrentDescriptionLabel.TabIndex = 34;
            this.GestureCurrentDescriptionLabel.Text = "DESCRIPTION:";
            // 
            // GestureDescriptionBox
            // 
            this.GestureDescriptionBox.Location = new System.Drawing.Point(784, 205);
            this.GestureDescriptionBox.Name = "GestureDescriptionBox";
            this.GestureDescriptionBox.Size = new System.Drawing.Size(166, 20);
            this.GestureDescriptionBox.TabIndex = 33;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(229, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 36;
            this.label1.Text = "label1";
            // 
            // GestureSaveBtn
            // 
            this.GestureSaveBtn.Location = new System.Drawing.Point(18, 740);
            this.GestureSaveBtn.Name = "GestureSaveBtn";
            this.GestureSaveBtn.Size = new System.Drawing.Size(118, 23);
            this.GestureSaveBtn.TabIndex = 37;
            this.GestureSaveBtn.Text = "SAVE GESTURES";
            this.GestureSaveBtn.UseVisualStyleBackColor = true;
            this.GestureSaveBtn.Click += new System.EventHandler(this.GestureSaveBtn_Click);
            // 
            // GestureLoadBtn
            // 
            this.GestureLoadBtn.Location = new System.Drawing.Point(157, 740);
            this.GestureLoadBtn.Name = "GestureLoadBtn";
            this.GestureLoadBtn.Size = new System.Drawing.Size(118, 23);
            this.GestureLoadBtn.TabIndex = 38;
            this.GestureLoadBtn.Text = "LOAD GESTURES";
            this.GestureLoadBtn.UseVisualStyleBackColor = true;
            this.GestureLoadBtn.Click += new System.EventHandler(this.GestureLoadBtn_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.textBoxTimerTick);
            // 
            // GestureTrackerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(981, 775);
            this.Controls.Add(this.GestureLoadBtn);
            this.Controls.Add(this.GestureSaveBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.GestureCurrentDescriptionVar);
            this.Controls.Add(this.GestureCurrentDescriptionLabel);
            this.Controls.Add(this.GestureDescriptionBox);
            this.Controls.Add(this.GestureDescriptionLabel);
            this.Controls.Add(this.GestureMovementSequenceLabel);
            this.Controls.Add(this.GestureTrackerTitleLabel);
            this.Controls.Add(this.GestureCurrentSequenceVar);
            this.Controls.Add(this.GestureCurrentMethodVar);
            this.Controls.Add(this.GestureCurrentNameVar);
            this.Controls.Add(this.GestureCurrentSequenceLabel);
            this.Controls.Add(this.GestureCurrentMethodLabel);
            this.Controls.Add(this.GestureCurrentNameLabel);
            this.Controls.Add(this.GestureCurrentRemoveButton);
            this.Controls.Add(this.CurrentGesturesLabel);
            this.Controls.Add(this.GestureListBox);
            this.Controls.Add(this.GestureCreatorButtonBack);
            this.Controls.Add(this.GestureCreatorButtonDownRight);
            this.Controls.Add(this.GestureCreatorButtonDownLeft);
            this.Controls.Add(this.GestureCreatorButtonUpRight);
            this.Controls.Add(this.GestureCreatorButtonUpLeft);
            this.Controls.Add(this.GestureCreatorButtonLeft);
            this.Controls.Add(this.GestureCreatorButtonRight);
            this.Controls.Add(this.GestureCreatorButtonDown);
            this.Controls.Add(this.GestureCreatorButtonUp);
            this.Controls.Add(this.GestureCreatorSequenceVar);
            this.Controls.Add(this.GestureCreateButton);
            this.Controls.Add(this.GestureMethodBox);
            this.Controls.Add(this.GestureNameBox);
            this.Controls.Add(this.GestureSequenceLabel);
            this.Controls.Add(this.GestureMethodLabel);
            this.Controls.Add(this.GestureNameLabel);
            this.Controls.Add(this.GestureCreatorTitleText);
            this.Controls.Add(this.GestureInfoLabel);
            this.Controls.Add(this.PicBox);
            this.Name = "GestureTrackerForm";
            this.Text = "GestureTracker";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GestureTrackerForm_FormClosed);
            this.Load += new System.EventHandler(this.GestureTrackerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PicBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PicBox;
        private System.Windows.Forms.Label GestureCreatorTitleText;
        private System.Windows.Forms.Label GestureNameLabel;
        private System.Windows.Forms.Label GestureMethodLabel;
        private System.Windows.Forms.Label GestureSequenceLabel;
        private System.Windows.Forms.TextBox GestureNameBox;
        private System.Windows.Forms.TextBox GestureMethodBox;
        private System.Windows.Forms.Button GestureCreateButton;
        private System.Windows.Forms.Label GestureCreatorSequenceVar;
        private System.Windows.Forms.Button GestureCreatorButtonUp;
        private System.Windows.Forms.Button GestureCreatorButtonDown;
        private System.Windows.Forms.Button GestureCreatorButtonRight;
        private System.Windows.Forms.Button GestureCreatorButtonLeft;
        private System.Windows.Forms.Button GestureCreatorButtonUpLeft;
        private System.Windows.Forms.Button GestureCreatorButtonUpRight;
        private System.Windows.Forms.Button GestureCreatorButtonDownLeft;
        private System.Windows.Forms.Button GestureCreatorButtonDownRight;
        private System.Windows.Forms.Button GestureCreatorButtonBack;
        private System.Windows.Forms.ListBox GestureListBox;
        private System.Windows.Forms.Label CurrentGesturesLabel;
        private System.Windows.Forms.Button GestureCurrentRemoveButton;
        private System.Windows.Forms.Label GestureCurrentNameLabel;
        private System.Windows.Forms.Label GestureCurrentMethodLabel;
        private System.Windows.Forms.Label GestureCurrentSequenceLabel;
        private System.Windows.Forms.Label GestureCurrentNameVar;
        private System.Windows.Forms.Label GestureCurrentMethodVar;
        private System.Windows.Forms.Label GestureCurrentSequenceVar;
        private System.Windows.Forms.Label GestureInfoLabel;
        private System.Windows.Forms.Label GestureTrackerTitleLabel;
        private System.Windows.Forms.Label GestureMovementSequenceLabel;
        private System.Windows.Forms.Label GestureDescriptionLabel;
        private System.Windows.Forms.Label GestureCurrentDescriptionVar;
        private System.Windows.Forms.Label GestureCurrentDescriptionLabel;
        private System.Windows.Forms.TextBox GestureDescriptionBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button GestureSaveBtn;
        private System.Windows.Forms.Button GestureLoadBtn;
        private System.Windows.Forms.Timer timer1;
    }
}

