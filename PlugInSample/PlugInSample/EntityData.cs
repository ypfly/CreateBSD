using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlugInSample_AutoBSD
{
    class EntityData
    {
        /// <summary>
        /// 随工单号
        /// </summary>
        public string lotsn = "";
        /// <summary>
        /// 落料号
        /// </summary>
        public int PunchPCS = 0;
        /// <summary>
        /// 盒子个数
        /// </summary>
        public int BoxNum = 0;
        /// <summary>
        /// 标箱数量
        /// </summary>
        public int box_pcs = 0;

        /// <summary>
        /// 已经更换随工单
        /// </summary>
        public bool isover = false;
        /// <summary>
        /// 临时文件路径
        /// </summary>
        public string Filepath = Environment.GetEnvironmentVariable("TEMP") + "\\";
    }
}
