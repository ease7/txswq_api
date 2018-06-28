using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Txs.Core;
using Txs.Core.Models;
using TxsWeb.Models;
using System.Text;
using System.Text.RegularExpressions;

namespace TxsWeb.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            ResultValue res = new ResultValue()
            {
                Status = 0,
                Message = "success",
                Value = DateTime.Now
            };

            return new JsonRes(res);
        }

        // GET: User
        public ActionResult Add(UserInfo info)
        {
            ResultValue res = CheckInfo(info);

            if(res.Status == 0)
            {
                UserService service = new UserService();

                service.AddUser(info);

                res.Status = 0;
                res.Message = "success";

                return new JsonRes(res);
            }
            else
            {
                return new JsonRes(res);
            }
        }

        private ResultValue CheckInfo(UserInfo info)
        {
            ResultValue res = new ResultValue();

            if(info == null)
            {
                res.Status = -1;
                res.Message = "数据异常";
                return res;
            }

            info.ID = Utility.GetValue(info.ID);
            info.Name = Utility.GetValue(info.Name);
            info.Province = Utility.GetValue(info.Province);

            if (string.IsNullOrEmpty(info.ID))
            {
                res.Status = -1;
                res.Message = "身份证号码不能为空";
                return res;
            }
            else
            {
                if (!CheckId(info.ID))
                {
                    res.Status = -1;
                    res.Message = "无效的身份证号码";

                    return res;
                }
            }

            if (info.Amount <=0 || info.Amount> 100000000)
            {
                res.Status = -1;
                res.Message = "无效的金额";

                return res;
            }

            if (string.IsNullOrEmpty(info.Name))
            {
                res.Status = -1;
                res.Message = "无效的姓名";

                return res;
            }
            else
            {
                if(info.Name.Length > 100)
                {
                    res.Status = -1;
                    res.Message = "姓名非法";
                    return res;
                }
            }

            if (string.IsNullOrEmpty(info.Province))
            {
                res.Status = -1;
                res.Message = "无效的省份";

                return res;
            }
            else
            {
                if (info.Province.Length > 100)
                {
                    res.Status = -1;
                    res.Message = "省份数据非法";
                    return res;
                }
            }

            res.Status = 0;
            res.Message = "";

            return res;
        }

        private bool CheckId(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                id = id.Trim();

                if(id.Length == 18)
                {
                    Regex re = new Regex(@"^[1-9]\d{5}(18|19|([23]\d))\d{2}((0[1-9])|(10|11|12))(([0-2][1-9])|10|20|30|31)\d{3}[0-9Xx]$");

                    if (re.IsMatch(id))
                    {

                        if (IdCardChecker.Check(id))
                        {
                            return true;
                        }
                    
                    }
                }else
                {
                    return false;
                }
            }

            return false;
        }

        
    }
}