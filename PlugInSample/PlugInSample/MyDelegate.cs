using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PlugInSample_AutoBSD
{
    class MyDelegate
    {


        /// <summary>
        /// 委托修改ui
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ed"></param>
        delegate void DelegateSetDataGridView(object[] sender, EntityData ed);

        delegate void DelegateSetRichTextBox(object[] sender, EntityData ed, string msg, bool b);

        delegate void DelegateSetDGVDT(object[] sender, DataTable dt, string msg);

        delegate void DelegateSetDGVDTIsYC(object[] sender, DataTable dt, bool b, string msg);

        delegate void DelegateSetLab5(object sender, string msg, bool b);

        

        public void setLab5(object sender, string msg, bool bl)
        {
            if (sender == null)
                return;
            Label l5 = (Label)sender;

            if (l5.InvokeRequired)
            {
                DelegateSetLab5 d5 = new DelegateSetLab5(DelegateSetLab5Text);
                l5.Invoke(d5, new object[] { l5, msg, bl });
            }
            else
            {
                l5.Text = msg;
                l5.Visible = bl;

            }
        }

        void DelegateSetLab5Text(object sender, string msg, bool b)
        {
            Label l5 = (Label)sender;
            l5.Text = msg;
            l5.Visible = b;
        }

        /// <summary>
        /// 给DataGridView控件设置数据源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="dt"></param>
        public void SetDGVUIClic(object[] sender, DataTable dt, string msg)
        {
            DataGridView dgv = (DataGridView)sender[0];
            Label label = null;
            if (sender.Length > 1)
                label = (Label)sender[1];
            if (dgv.InvokeRequired)
            {
                DelegateSetDGVDT dsdt = new DelegateSetDGVDT(SetDGVDT);
                dgv.Invoke(dsdt, new object[] { new object[] { dgv, label }, dt, msg });
            }
            else
            {
                dgv.DataSource = null;
                dgv.DataSource = dt;
                if (label != null)
                    label.Text = msg;
            }

        }

        /// <summary>
        /// 委托设置DataGridView数据源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="dt"></param>
        void SetDGVDT(object[] sender, DataTable dt, string msg)
        {
            DataGridView dgv = (DataGridView)sender[0];
            Label label = null;
            if (sender.Length > 1)
                label = (Label)sender[1];
            dgv.DataSource = null;
            dgv.DataSource = dt;
            if (label != null)
                label.Text = msg;

        }


        /// <summary>
        /// 给DataGridView控件设置数据源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="dt"></param>
        public void SetDGVIsyc(object[] sender, DataTable dt, bool b, string msg)
        {
            DataGridView dgv = (DataGridView)sender[0];
            Label label = null;
            if (sender.Length > 1)
                label = (Label)sender[1];
            if (dgv.InvokeRequired)
            {
                DelegateSetDGVDTIsYC dgvyc = new DelegateSetDGVDTIsYC(SetDGVIsYC);
                dgv.Invoke(dgvyc, new object[] { new object[] { dgv, label }, dt, b, msg });
            }
            else
            {
                label.Visible = dgv.Visible = b;

                dgv.DataSource = null;
                if (dt != null)
                    dgv.DataSource = dt;

            }

        }

        /// <summary>
        /// 委托设置DataGridView数据源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="dt"></param>
        void SetDGVIsYC(object[] sender, DataTable dt, bool b, string msg)
        {
            DataGridView dgv = (DataGridView)sender[0];
            Label label = null;
            if (sender.Length > 1)
                label = (Label)sender[1];
            label.Visible = dgv.Visible = b;
            dgv.DataSource = null;
            if (dt != null)
                dgv.DataSource = dt;


        }

        /// <summary>
        /// 修改随工单信息UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ed"></param>
        public void SetUIControlValue(object[] sender, EntityData ed)
        {
            if (ed == null)
                return;
            DataGridView dgv = (DataGridView)sender[0];
            RichTextBox rtb = (RichTextBox)sender[1];
            if (dgv.InvokeRequired)
            {
                DelegateSetDataGridView dsdgv = new DelegateSetDataGridView(DelegateSetDGVValue);
                dgv.Invoke(dsdgv, new Object[] { new object[] { dgv, rtb }, ed });
            }
            else
            {
                if (dgv.Columns["落料数"] == null)
                    return;
                dgv.Rows[0].Cells["落料数"].Value = ed.PunchPCS;
                dgv.Rows[0].Cells["盒数"].Value = ed.BoxNum;
                if (dgv.Rows[0].Cells["标箱数量"].Value.ToString() != ed.box_pcs.ToString())
                {
                    ShowMessage(rtb, ed, dgv.Rows[0].Cells["随工单号"].Value.ToString() + "的标箱数量由" + dgv.Rows[0].Cells["标箱数量"].Value.ToString()
                        + "个更改为" + ed.box_pcs.ToString() + "个\n", true);

                }
                dgv.Rows[0].Cells["标箱数量"].Value = ed.box_pcs;
            }
        }

        public void SetUIControlValue(object sender, EntityData ed, string msg, bool b)
        {
            RichTextBox rtb = (RichTextBox)sender;
            //if (ed == null)
            //    return;
            if (rtb.InvokeRequired)
            {
                DelegateSetRichTextBox dsrtb = new DelegateSetRichTextBox(DelegateSetValue);
                rtb.Invoke(dsrtb, new Object[] { new object[] { rtb }, ed, msg, b });
            }
            else
            {
                ShowMessage(rtb, ed, msg, b);

            }
        }
        void DelegateSetDGVValue(object[] sender, EntityData ed)
        {
            if (ed == null)
                return;
            DataGridView dgv = (DataGridView)sender[0];
            RichTextBox rtb = (RichTextBox)sender[1];
            if (dgv.Columns["落料数"] == null)
                return;
            dgv.Rows[0].Cells["落料数"].Value = ed.PunchPCS;
            dgv.Rows[0].Cells["盒数"].Value = ed.BoxNum;
            if (dgv.Rows[0].Cells["标箱数量"].Value.ToString() != ed.box_pcs.ToString())
            {
                ShowMessage(rtb, ed, dgv.Rows[0].Cells["随工单号"].Value.ToString() + "的标箱数量由" + dgv.Rows[0].Cells["标箱数量"].Value.ToString()
                    + "个更改为" + ed.box_pcs.ToString() + "个\n", true);

            }
            dgv.Rows[0].Cells["标箱数量"].Value = ed.box_pcs;
        }
        /// <summary>
        /// 修改提示界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ed"></param>
        /// <param name="MSG"></param>
        void DelegateSetValue(object[] sender, EntityData ed, string MSG, bool b)
        {
            ShowMessage(sender[0], ed, MSG, b);
        }

        void ShowMessage(object sender, EntityData ed, string MSG, bool b)
        {
            RichTextBox rtb = (RichTextBox)sender;
            UserControl1 u = new UserControl1();
            string strmes = "";
            if (ed == null)
            {
                strmes = DateTime.Now.ToString("yyyy-MM-dd hh:mm ") + MSG;
                sqlhelp.GetDataSetWithSQLString(string.Format(sqlhelp.InsertAuto, u.ResourceName, u.OrBitUserName,strmes, ""), u.DatabaseServer, u.DatabaseName, u.DatabasePassword, u.DatabaseUser);

            }
            else
            {
                strmes = DateTime.Now.ToString("yyyy-MM-dd hh:mm ") + "随工单号" + ed.lotsn + ":" + MSG ;
                sqlhelp.GetDataSetWithSQLString(string.Format(sqlhelp.InsertAuto, u.ResourceName, u.OrBitUserName,strmes, ed.lotsn), u.DatabaseServer, u.DatabaseName, u.DatabasePassword, u.DatabaseUser);

            }
            if (b)
                rtb.AppendText(strmes+"\n");
            else
            {
                rtb.AppendText(strmes+"\n");
                rtb.ForeColor = System.Drawing.Color.Red;
            }

            rtb.Select(rtb.TextLength, 0);
            rtb.ScrollToCaret();
        }


    }



}
