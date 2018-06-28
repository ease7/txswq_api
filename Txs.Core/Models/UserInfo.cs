using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Txs.Core.Models
{
    public class UserInfo
    {
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 投资金额
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 所属省份
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public long CreateTime { get; set; }
    }
}
