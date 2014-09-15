namespace TiledMapDemo1
{
    partial class ImportForm
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
            this.btnBrowse = new System.Windows.Forms.Button();
            this.tbImageSource = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.picBoxMapReview = new System.Windows.Forms.PictureBox();
            this.tblUploadSize = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grpTileSetReview = new System.Windows.Forms.GroupBox();
            this.picBoxTileSetImage = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbWidthTileSize = new System.Windows.Forms.TextBox();
            this.tbHeightTileSize = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnScan = new System.Windows.Forms.Button();
            this.pgBarScanProgess = new System.Windows.Forms.ProgressBar();
            this.btnImportOK = new System.Windows.Forms.Button();
            this.btnImportCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxMapReview)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.grpTileSetReview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxTileSetImage)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(347, 9);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(2);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(60, 21);
            this.btnBrowse.TabIndex = 4;
            this.btnBrowse.Text = "Browse..";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // tbImageSource
            // 
            this.tbImageSource.Location = new System.Drawing.Point(101, 9);
            this.tbImageSource.Margin = new System.Windows.Forms.Padding(2);
            this.tbImageSource.Name = "tbImageSource";
            this.tbImageSource.Size = new System.Drawing.Size(226, 20);
            this.tbImageSource.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 12);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Image";
            // 
            // picBoxMapReview
            // 
            this.picBoxMapReview.Location = new System.Drawing.Point(6, 27);
            this.picBoxMapReview.Name = "picBoxMapReview";
            this.picBoxMapReview.Size = new System.Drawing.Size(617, 176);
            this.picBoxMapReview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBoxMapReview.TabIndex = 7;
            this.picBoxMapReview.TabStop = false;
            // 
            // tblUploadSize
            // 
            this.tblUploadSize.AutoSize = true;
            this.tblUploadSize.Location = new System.Drawing.Point(432, 13);
            this.tblUploadSize.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.tblUploadSize.Name = "tblUploadSize";
            this.tblUploadSize.Size = new System.Drawing.Size(27, 13);
            this.tblUploadSize.TabIndex = 8;
            this.tblUploadSize.Text = "Size";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.picBoxMapReview);
            this.groupBox1.Location = new System.Drawing.Point(36, 83);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(629, 204);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Review";
            // 
            // grpTileSetReview
            // 
            this.grpTileSetReview.Controls.Add(this.picBoxTileSetImage);
            this.grpTileSetReview.Location = new System.Drawing.Point(36, 328);
            this.grpTileSetReview.Name = "grpTileSetReview";
            this.grpTileSetReview.Size = new System.Drawing.Size(629, 204);
            this.grpTileSetReview.TabIndex = 12;
            this.grpTileSetReview.TabStop = false;
            this.grpTileSetReview.Text = "TileSet";
            // 
            // picBoxTileSetImage
            // 
            this.picBoxTileSetImage.Location = new System.Drawing.Point(6, 27);
            this.picBoxTileSetImage.Name = "picBoxTileSetImage";
            this.picBoxTileSetImage.Size = new System.Drawing.Size(617, 176);
            this.picBoxTileSetImage.TabIndex = 7;
            this.picBoxTileSetImage.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 46);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Tile size";
            // 
            // tbWidthTileSize
            // 
            this.tbWidthTileSize.Location = new System.Drawing.Point(101, 43);
            this.tbWidthTileSize.Margin = new System.Windows.Forms.Padding(2);
            this.tbWidthTileSize.Name = "tbWidthTileSize";
            this.tbWidthTileSize.Size = new System.Drawing.Size(68, 20);
            this.tbWidthTileSize.TabIndex = 14;
            this.tbWidthTileSize.Text = "16";
            // 
            // tbHeightTileSize
            // 
            this.tbHeightTileSize.Location = new System.Drawing.Point(205, 43);
            this.tbHeightTileSize.Margin = new System.Windows.Forms.Padding(2);
            this.tbHeightTileSize.Name = "tbHeightTileSize";
            this.tbHeightTileSize.Size = new System.Drawing.Size(68, 20);
            this.tbHeightTileSize.TabIndex = 15;
            this.tbHeightTileSize.Text = "16";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(181, 46);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "x";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(286, 46);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(18, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "px";
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(347, 43);
            this.btnScan.Margin = new System.Windows.Forms.Padding(2);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(60, 21);
            this.btnScan.TabIndex = 18;
            this.btnScan.Text = "Scan";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // pgBarScanProgess
            // 
            this.pgBarScanProgess.Location = new System.Drawing.Point(36, 292);
            this.pgBarScanProgess.Margin = new System.Windows.Forms.Padding(2);
            this.pgBarScanProgess.Name = "pgBarScanProgess";
            this.pgBarScanProgess.Size = new System.Drawing.Size(629, 21);
            this.pgBarScanProgess.TabIndex = 19;
            // 
            // btnImportOK
            // 
            this.btnImportOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnImportOK.Location = new System.Drawing.Point(511, 554);
            this.btnImportOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnImportOK.Name = "btnImportOK";
            this.btnImportOK.Size = new System.Drawing.Size(60, 21);
            this.btnImportOK.TabIndex = 20;
            this.btnImportOK.Text = "OK";
            this.btnImportOK.UseVisualStyleBackColor = true;
            this.btnImportOK.Click += new System.EventHandler(this.btnImportOK_Click);
            // 
            // btnImportCancel
            // 
            this.btnImportCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnImportCancel.Location = new System.Drawing.Point(599, 554);
            this.btnImportCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnImportCancel.Name = "btnImportCancel";
            this.btnImportCancel.Size = new System.Drawing.Size(60, 21);
            this.btnImportCancel.TabIndex = 21;
            this.btnImportCancel.Text = "Cancel";
            this.btnImportCancel.UseVisualStyleBackColor = true;
            this.btnImportCancel.Click += new System.EventHandler(this.btnImportCancel_Click);
            // 
            // ImportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(696, 586);
            this.Controls.Add(this.btnImportCancel);
            this.Controls.Add(this.btnImportOK);
            this.Controls.Add(this.pgBarScanProgess);
            this.Controls.Add(this.btnScan);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbHeightTileSize);
            this.Controls.Add(this.tbWidthTileSize);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grpTileSetReview);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tblUploadSize);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.tbImageSource);
            this.Controls.Add(this.label2);
            this.Name = "ImportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Import";
            ((System.ComponentModel.ISupportInitialize)(this.picBoxMapReview)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.grpTileSetReview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxTileSetImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox tbImageSource;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox picBoxMapReview;
        private System.Windows.Forms.Label tblUploadSize;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox grpTileSetReview;
        private System.Windows.Forms.PictureBox picBoxTileSetImage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbWidthTileSize;
        private System.Windows.Forms.TextBox tbHeightTileSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.ProgressBar pgBarScanProgess;
        private System.Windows.Forms.Button btnImportOK;
        private System.Windows.Forms.Button btnImportCancel;
    }
}