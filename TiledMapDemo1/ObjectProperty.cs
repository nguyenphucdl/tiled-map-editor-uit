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
using TiledMapDemo1.Model;

namespace TiledMapDemo1
{
    public partial class ObjectProperty : Form
    {

        private IEnumerable<ObjectTypes> objectTypes;

        private TileObject tileObjectBound;

        private bool removeState = false;

        public bool RemoveState
        {
            get { return removeState; }
            set { removeState = value; }
        }

        public TileObject TileObject
        {
            get { return tileObjectBound; }
            set { tileObjectBound = value; LoadTileObjectProperties(); }
        }

        public ObjectProperty()
        {
            InitializeComponent();

            objectTypes = EnumUtil.GetValues<ObjectTypes>();
            cmbObjectTypes.DataSource = objectTypes;
        }

        private void LoadTileObjectProperties()
        {
            txtBoxId.Text = tileObjectBound.Id.ToString();
            txtBoxName.Text = tileObjectBound.Name;
            txtObjectType.Text = tileObjectBound.Type;
            if (txtObjectType.Text.Equals(""))
            {
                cmbObjectTypes_SelectionChangeCommitted(cmbObjectTypes, null);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

            String newName = txtBoxName.Text;
            String newObjType = txtObjectType.Text;

            tileObjectBound.Name = newName;
            tileObjectBound.Type = newObjType;
        }

        private void cmbObjectTypes_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void cmbObjectTypes_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            string selectedText = cb.SelectedItem.ToString();
            foreach (var value in Enum.GetValues(typeof(ObjectTypes)))
            {
                if (value.ToString().Equals(selectedText))
                {
                    txtObjectType.Text = ((int)value).ToString();
                    break;
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            RemoveState = true;
        }
    }
}
