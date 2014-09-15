using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TiledMapDemo1.MapTools;
using TiledMapDemo1.Utils;

namespace TiledMapDemo1
{
    public partial class ImportForm : Form
    {
        #region Fields
        Image m_ImageUpload;
        string m_imageUploadName;
        string m_imageUploadPath;
        Size m_tileSize;

        public string MapPath { get; private set; }

        public System.Windows.Forms.ProgressBar ProgessBar
        {
            get { return pgBarScanProgess; }
            set { pgBarScanProgess = value; }
        }

        private void progessBarIncrease(int percentage)
        {
            if(percentage != ProgessBar.Value)
                ProgessBar.Value = percentage;
        }
        #endregion

        #region Contructors
        public ImportForm()
        {
            InitializeComponent();

            // Default progress value is 100%
            ProgessBar.Maximum = 100;
        }
        #endregion

        #region Form functions
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog fDialog = new OpenFileDialog();
            fDialog.Title = "Select file to be upload";
            fDialog.Filter = "PNG Image(*.png)|*.png*";
            if (fDialog.ShowDialog() == DialogResult.OK)
            {
                tbImageSource.Text = fDialog.FileName.ToString();
                m_ImageUpload = Image.FromFile(tbImageSource.Text);
                m_imageUploadPath = tbImageSource.Text;
                m_imageUploadName = FileUtility.GetFileNameWithoutExtension(m_imageUploadPath);

                
                tblUploadSize.Text = "Size: " + m_ImageUpload.Size.Width + " x " + m_ImageUpload.Size.Height + " px";

                // Review image upload
                picBoxMapReview.Image = m_ImageUpload;


            }
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            picBoxTileSetImage.Image = null;

            if (!isAbleToScan())
                return;
            ProgessBar.Visible = true;

            ImageToMap mapScan = new ImageToMap(m_tileSize.Width, m_tileSize.Height, m_ImageUpload, m_imageUploadName);
            mapScan.process(new Action<int>(progessBarIncrease));
            MapPath = mapScan.MapSavePath;
            Image review =  Image.FromFile(mapScan.TileMap.Path + "\\" + mapScan.TileMap.TileSets[0].Image.Name);



            picBoxTileSetImage.Image = review;
            grpTileSetReview.Text = "Tile Set (" + mapScan.TileSetList.Count + ")";

            ProgessBar.Visible = false;
        }

        private bool isAbleToScan()
        {
            if (String.IsNullOrEmpty(tbWidthTileSize.Text) || String.IsNullOrEmpty(tbHeightTileSize.Text) || String.IsNullOrEmpty(tbImageSource.Text))
                return false;
            int tileSizeWidth, tileSizeHeight;
            if (!Int32.TryParse(tbWidthTileSize.Text, out tileSizeWidth) || !Int32.TryParse(tbHeightTileSize.Text, out tileSizeHeight))
                return false;
            m_tileSize.Width = tileSizeWidth;
            m_tileSize.Height = tileSizeHeight;
            if(!ImageToMap.isTiledMapValid(m_tileSize, m_ImageUpload, m_imageUploadName))
            {
                return false;
            }
            return true;
        }
        #endregion

        private void btnImportOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImportCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
