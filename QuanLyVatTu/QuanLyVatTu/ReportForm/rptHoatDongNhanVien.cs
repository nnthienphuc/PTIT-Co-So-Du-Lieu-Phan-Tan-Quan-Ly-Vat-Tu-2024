﻿using DevExpress.Mvvm.Native;
using DevExpress.Utils.Extensions;
using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyVatTu.ReportForm
{
    public partial class rptHoatDongNhanVien : DevExpress.XtraReports.UI.XtraReport
    {
        public rptHoatDongNhanVien()
        {
            InitializeComponent();
        }

        public rptHoatDongNhanVien(string maNhanVien, DateTime fromDate, DateTime toDate)
        {
            InitializeComponent();
            this.sqlDataSource1.Connection.ConnectionString = Program.connstr;
            this.sqlDataSource1.Queries[0].Parameters[0].Value = maNhanVien;
            this.sqlDataSource1.Queries[0].Parameters[1].Value = fromDate;
            this.sqlDataSource1.Queries[0].Parameters[2].Value = toDate;
            this.sqlDataSource1.Fill();
            txtTienChu.BeforePrint += ConvertMoney_BeforePrint;
        }

        private void ConvertMoney_BeforePrint(object sender, CancelEventArgs e)
        {
            if (txtTongTriGia.Text.ToString().Equals(""))
            {
                txtTienChu.Text = "đồng  (không đồng)";
            }
            else
            {
                String currencyToWords = ConvertMoney.ConvertToText(int.Parse(txtTongTriGia.Summary.GetResult().ToString().Replace(".00", "")));
                txtTienChu.Text = "đồng  (" + currencyToWords + ")";
            }
        }
    }
}
