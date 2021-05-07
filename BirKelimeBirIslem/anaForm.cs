using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yazilimYapimi
{
    public partial class anaForm : Form
    {
        public anaForm()
        {
            InitializeComponent();
        }
        private void formYukleyici(Form yuklenenForm)
        {
            panel2.Controls.Clear();
            yuklenenForm.MdiParent = this;
            yuklenenForm.FormBorderStyle = FormBorderStyle.None;
            panel2.Controls.Add(yuklenenForm);
            yuklenenForm.Show();
        }

        private void formSecici(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                birKelime kelime = new birKelime();
                formYukleyici(kelime);
            }
            else
            {
                birIslem islem = new birIslem();
                formYukleyici(islem);
            }
        }

        internal void puanDeğistirici(int puan)
        {
            label2.Text = puan.ToString();
        }

        internal int puanDöndürücü()
        {
            return Convert.ToInt32(label2.Text);
        }
    }
}
