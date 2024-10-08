﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyVatTu.SubForm
{
    public partial class frmChonDonDatHang : Form
    {
        public frmChonDonDatHang()
        {
            InitializeComponent();
        }

        private void frmChonDonDatHang_Load(object sender, EventArgs e)
        {
            dataSet.EnforceConstraints = false;
            this.datHangTableAdapter.Connection.ConnectionString = Program.connstr;
            this.datHangTableAdapter.Fill(this.dataSet.DatHang);
        }

       

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private int kiemTraDonHangCoPhieuNhap(String maDonHang)
        {


            String cauTruyVan =
                    "DECLARE	@result int " +
                    "EXEC @result = sp_DonHangCoPhieuNhapChua '" +
                    maDonHang + "' " +
                    "SELECT 'Value' = @result";
            SqlCommand sqlCommand = new SqlCommand(cauTruyVan, Program.conn);
            try
            {
                Program.myReader = Program.ExecSqlDataReader(cauTruyVan);
                if (Program.myReader == null)
                {
                    return 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể thực hiện hành động này: \n" + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex.Message);
                return 1;
            }
            Program.myReader.Read();
            int result = int.Parse(Program.myReader.GetValue(0).ToString());
            Program.myReader.Close();

            return result;
        }


        private void btnChon_Click(object sender, EventArgs e)
        {
            DataRowView drv = ((DataRowView)(bdsDonDatHang.Current));
            string maNhanVien = drv["MANV"].ToString().Trim();
            string maDonHang = drv["MasoDDH"].ToString().Trim();
            string maKho = drv["MaKho"].ToString().Trim();

            if (Program.userName != maNhanVien)
            {
                MessageBox.Show("Bạn không thể lập phiếu trên đơn đặt hàng do người khác tạo", "Thông báo", MessageBoxButtons.OK);
                return;
            }

            int ketQua = kiemTraDonHangCoPhieuNhap(maDonHang);
            if (ketQua == 1)
            {
                MessageBox.Show("Đơn hàng này đã có phiếu nhập không thể tạo thêm", "Thông báo", MessageBoxButtons.OK);
                return;
            }

            Program.maDonDatHangDuocChon = maDonHang;
            Program.maKhoDuocChon = maKho;

            this.Close();
        }

    }
}
