using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TiledMapDemo1.Contants;

namespace TiledMapDemo1
{
    public partial class ViewportForm : Form
    {
        private IEnumerable<ViewportTypes> viewportTypes;

        private int viewportType = 0;

        public int ViewportType
        {
            get { return viewportType; }
            set { viewportType = value; }
        }
        public ViewportForm()
        {
            InitializeComponent();

            viewportTypes = EnumUtil.GetValues<ViewportTypes>();
            cmbViewportTypes.DataSource = viewportTypes;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbViewportTypes_SelectedValueChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            string selectedText = cb.SelectedItem.ToString();
            foreach (var value in Enum.GetValues(typeof(ViewportTypes)))
            {
                if (value.ToString().Equals(selectedText))
                {
                    viewportType = (int)value;
                    break;
                }
            }
        }
    }
}
