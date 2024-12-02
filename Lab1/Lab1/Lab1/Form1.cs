using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load_dtGV_Sach();
        }
        Service service = new Service();
        public void Clear_Sach()
        {
            txtMaSach.Clear();
            txtTenSach.Clear();
            txtDonGia.Clear();
            ckb_100Trang.Checked=true;
        }
        public void Load_dtGV_Sach()
        {
            dtGV_Sach.Rows.Clear();
            dtGV_Sach.ColumnCount = 5;
            dtGV_Sach.Columns[0].HeaderText = "Số thứ tự";
            dtGV_Sach.Columns[1].HeaderText = "Mã Sách";
            dtGV_Sach.Columns[2].HeaderText = "Tên Sach";
            dtGV_Sach.Columns[3].HeaderText = "Số Trang";
            dtGV_Sach.Columns[4].HeaderText = "Đơn Giá";
            var listSach= service.Get_ListSach();
            int stt = 0;
            foreach( var item in listSach )
            {
                stt++;
                dtGV_Sach.Rows.Add(stt, item.Ma, item.Ten, item.SoTrang, item.DonGia);
            }
        }
        private void ckb_100Trang_CheckedChanged(object sender, EventArgs e)
        {
            if (ckb_100Trang.Checked)
            {
                ckb_1000Trang.Checked=false;
            }
            else
            {
                ckb_1000Trang.Checked = true;
            }
        }
        private void dtGV_Sach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dtGV_Sach.Rows.Count - 1)
            {
                txtMaSach.Text = dtGV_Sach.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtTenSach.Text = dtGV_Sach.Rows[e.RowIndex].Cells[2].Value.ToString();
                if (dtGV_Sach.Rows[e.RowIndex].Cells[3].Value.ToString() == "100")
                {
                    ckb_100Trang.Checked=true;
                    ckb_1000Trang.Checked = false;
                }
                else
                {
                    ckb_100Trang.Checked = false;
                    ckb_1000Trang.Checked = true;
                }
                txtDonGia.Text = dtGV_Sach.Rows[e.RowIndex].Cells[4].Value.ToString();
            }
        }
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            dtGV_Sach.Rows.Clear();
            dtGV_Sach.ColumnCount = 5;
            dtGV_Sach.Columns[0].HeaderText = "Số thứ tự";
            dtGV_Sach.Columns[1].HeaderText = "Mã Sách";
            dtGV_Sach.Columns[2].HeaderText = "Tên Sach";
            dtGV_Sach.Columns[3].HeaderText = "Số Trang";
            dtGV_Sach.Columns[4].HeaderText = "Đơn Giá";
            var listSach = service.Get_ListSach().Where(x=>x.Ten.Contains(txtTimKiem.Text));
            int stt = 0;
            foreach (var item in listSach)
            {
                stt++;
                dtGV_Sach.Rows.Add(stt, item.Ma, item.Ten, item.SoTrang, item.DonGia);
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            this.Clear_Sach();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            Sach sach = new Sach();
            sach.Ma = txtMaSach.Text;
            sach.Ten = txtTenSach.Text;
            sach.DonGia = int.Parse(txtDonGia.Text);
            sach.SoTrang = "100";
            if (ckb_1000Trang.Checked)
            {
                sach.SoTrang = "1000";
            }
            bool check = service.Get_ListSach().Any(x => x.Ma == sach.Ma);
            if (!check && sach.DonGia>0)
            {
                service.Add_Sach(sach);
                this.Load_dtGV_Sach();
                this.Clear_Sach();
            }
            else
            {
                MessageBox.Show("Sách Đã Tồn Tại!!!");
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            bool check = service.Get_ListSach().Any(x => x.Ma == txtMaSach.Text);
            if (check)
            {
                service.Delete_Sach(txtMaSach.Text);
                this.Load_dtGV_Sach();
                this.Clear_Sach();
            }
            else
            {
                MessageBox.Show($"Không Tìm Thấy Sách:{txtMaSach.Text}!!!");
            }
        }
        private void ckb_1000Trang_CheckedChanged(object sender, EventArgs e)
        {
            if (ckb_1000Trang.Checked)
            {
                ckb_100Trang.Checked = false;
            }
            else
            {
                ckb_100Trang.Checked = true;
            }
        }
        private void dtGV_Sach_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

    }
}
