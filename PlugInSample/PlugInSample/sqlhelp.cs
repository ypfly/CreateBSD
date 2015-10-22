using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;


namespace PlugInSample_AutoBSD
{
    class sqlhelp
    {
        /// <summary>
        /// 获取机台的随工单
        /// </summary>
        public const string GetLotSN = @"DECLARE @return_value int,
		                                  @I_ReturnMessage nvarchar(max)
                                  EXEC @return_value = [dbo].[GetLotsnS]
                                          @I_ReturnMessage = @I_ReturnMessage OUTPUT,
                                          @FixedAssetNumber = N'{0}'
                                  SELECT @I_ReturnMessage as N'@I_ReturnMessage','Return Value' = @return_value";

        /// <summary>
        /// 获取上一个随工单的落料情况
        /// </summary>
        public const string GetoldLotsn = @"DECLARE	@return_value int,
                                            @I_ReturnMessage nvarchar(max)
                                            EXEC @return_value = [dbo].[GetOldLotsnS]
                                                 @I_ReturnMessage = @I_ReturnMessage OUTPUT,
                                                 @lotsn ='{0}'
                                            SELECT @I_ReturnMessage as N'@I_ReturnMessage','Return Value' = @return_value";

        /// <summary>
        /// 获取随工单对应的标识单
        /// </summary>
        public const string GetIdentitysn = @"SELECT IdentityID as id,Identitysn as 标识单号,BoxNum as 盒序号,CreateDate as 创建时间 FROM IdentityPrintInfo WHERE IdentityDocumentsId='{0}' AND IsDelete=0 AND IsToScrapped=0 AND IDStatus<>4";

        /// <summary>
        /// 获取盒数，标箱数量，落料数
        /// </summary>
        public const string GetLotSNPunchPCSBoxNum = @"select PunchPCS,BoxNum,bp.Box_pcs from FlowWorkNumber 
			                                           join Box_pcsTable bp on bp.ProductNO=FlowWorkNumber.ProductNO 
			                                           and bp.Modelspn=FlowWorkNumber.ModulSN where FlowWorkNO = '{0}'";

        ///// <summary>
        ///// 生成标识单{0}-随工单id1，{1}-用户名，{2}-随工单id1，{3}-电脑名，{4}-打印张数
        ///// </summary>
        //public const string PrintBSD = @"DECLARE	@return_value int,	@I_ReturnMessage nvarchar(max) EXEC	@return_value = [dbo].[_AuToPrintIdentityDocumentsInfo]
        //                       @I_ReturnMessage = @I_ReturnMessage OUTPUT,
        //                 @IdentityDocumentsId = N'{0}',@I_OrBitUserId='{1}', @IdentityDocumentsId1 = N'{2}',@I_ResourceName ='{3}',
        //                       @Effective = '{4}' SELECT	@I_ReturnMessage as N'@I_ReturnMessage','Return Value' = @return_value";
        /// <summary>
        /// 生成标识单{0}-随工单id1，{1}-用户名，{2}-随工单id1，{3}-电脑名，{4}-打印张数
        /// </summary>
        public const string PrintBSD = @"DECLARE	@return_value int,	@I_ReturnMessage nvarchar(max) EXEC	@return_value = [dbo].[_AuToPrintIdentityDocumentsInfo]
                               @I_ReturnMessage = @I_ReturnMessage OUTPUT,
		                       @IdentityDocumentsId = N'{0}',@I_OrBitUserId='{1}', @I_ResourceName ='{2}',
                               @Effective = '{3}' SELECT	@I_ReturnMessage as N'@I_ReturnMessage','Return Value' = @return_value";
        /// <summary>
        /// 获取Ftp服务器信息
        /// </summary>
        public const string FtpInfo = @"select ParameterValueEx from SysParameter where ParameterValue  ='LBD' and ParameterName='[Ftp Server Setting]'";

        /// <summary>
        /// 重新打印指定表示单
        /// </summary>
        public const string ageinPrintBSD = @"select IdentityDocuments.IdentityDocumentsId 
                                            ,f.WorkShopNM,
                                                 IdentityDocuments .ProdStatusQc,
		                                            IdentityDocuments.ProName,
	                                               f.ProductNO as SparePartsPhotoNum,
	                                               IdentityDocuments.FollowWorkNum,
	                                               IdentityDocuments.RawMaterialName,
	                                               IdentityDocuments. RawMaterialLot,
	                                               IdentityDocuments. QCmodelNum,
	                                               EquiCurrentOpreator,
	                                                QCOpreator,
	                                                IdentityPrintInfo.BoxNum,
		                                            IdentityPrintInfo.Identitysn,
		                                            IdentityPrintInfo.pcs,
		                                            f.EquipmentNO,
		                                            IdentityPrintInfo.Cav
		                                             from IdentityPrintInfo		 
	                                            join IdentityDocuments on IdentityPrintInfo.IdentityDocumentsId=IdentityDocuments.IdentityDocumentsId
	                                            join FlowWorkNumber f on  f.FlowWorkNO=IdentityDocuments.FollowWorkNum
	                                            --left join PatrolQCCheck pc on  f.FlowWorkNO= pc.FollowWorkNum and pc.QCType'' and pc.IsDel =0
	                                            where IdentityPrintInfo.IdentityID='{0}'  and IsDelete=0 and IsToScrapped=0 --返回打印数据
	                                            order by IdentityPrintInfo.IdentityDocumentsId,BoxNum ";
        /// <summary>
        /// 获取ftp信息
        /// </summary>
        public const string ftpname = "select FtpFileId+FtpFileName,FtpDirectory from FtpFile where FtpFileName='MetalworkingIdentityLAB1.mrz' order by CreateDate desc";

        public const string InsertAuto = @"EXEC[dbo].[InsertAUTOBSD]
                                           @FixedAssetNumber = N'{0}',
                                           @userName='{1}',
		                                   @MSGinfo = N'{2}',
		                                   @LotSN = N'{3}'";





        /// <summary>
        ///  执行一个指定的SQL字串，并返回一个记录集,在浏览器下执行时，直接调用浏览器的WCF服务器来传送记录集
        /// </summary>
        /// <param name="SQLString">sql</param>
        /// <param name="DatabaseServer">服务器地址</param>
        /// <param name="DatabaseName">数据库名字</param>
        /// <param name="DatabasePassword">密码</param>
        /// <param name="DatabaseUser">用户名</param>
        /// <param name="parentNM">父控件名</param>
        /// <returns></returns>
        public static DataSet GetDataSetWithSQLString(string SQLString, string DatabaseServer, string DatabaseName, string DatabasePassword, string DatabaseUser)
        {
            try
            {

                string ConnectionString = "Data Source=" + DatabaseServer +
                                    ";Initial Catalog=" + DatabaseName +
                                    ";password=" + DatabasePassword +
                                    ";Persist Security Info=True;User ID=" + DatabaseUser;
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = conn;
                    comm.CommandText = SQLString;

                    comm.CommandType = CommandType.Text;
                    comm.CommandTimeout = conn.ConnectionTimeout;

                    DataSet ds = new DataSet("SQLDataSet");
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = comm;
                    adapter.Fill(ds, "SQLDataSet");

                    conn.Close();
                    return ds;
                }
            }
            catch (Exception e)
            {
                // System.Windows.Forms.MessageBox.Show(e.ToString(),"系统提示", System.Windows.Forms.MessageBoxButtons.OK);            
                return null;
            }
        }
    }
}
