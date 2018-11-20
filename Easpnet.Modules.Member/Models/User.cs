using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;

namespace Easpnet.Modules.Member.Models
{
    /// <summary>
    /// 用户
    /// </summary>
    [Table(PrimaryKey = "UserId", TableName = "MemberUser")]
    public class User : ModelBase
    {
        private static readonly User mUser = new User();

        /// <summary>
        /// 用户id
        /// </summary>
        [TableField(IsTableField = true, IsIdentifier = true)]
        public long UserId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [TableField(25)]
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [TableField(255)]
        public string Password { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [TableField(255)]
        public string Email { get; set; }
        /// <summary>
        /// 注册IP
        /// </summary>
        [TableField(50)]
        public string RegIp { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        [TableField]
        public DateTime RegTime { get; set; }
        /// <summary>
        /// 用户类型
        /// </summary>
        [TableField]
        public UserType UserType { get; set; }
        /// <summary>
        /// 用户分组名称
        /// </summary>
        [TableField]
        public string GroupName { get; set; }
        /// <summary>
        /// 权限数据
        /// </summary>
        [TableField]
        public string PermissionsData { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        [TableField]
        public string RealName { get; set; }
        /// <summary>
        /// 登陆时间
        /// </summary>
        [TableField(50)]
        public DateTime LoginTime { get; set; }
        /// <summary>
        /// 登陆IP
        /// </summary>
        [TableField]
        public string LoginIP { get; set; }
        /// <summary>
        /// 上次登陆时间
        /// </summary>
        [TableField]
        public DateTime LastLoginTime { get; set; }
        /// <summary>
        /// 上次登陆IP
        /// </summary>
        [TableField(50)]
        public string LastLoginIP { get; set; }

        /// <summary>
        /// 权限列表
        /// </summary>
        public List<Permission> Permissions { get; set; }

        /// <summary>
        /// 用户状态
        /// </summary>
        [TableField]
        public UserStatus UserStatus { get; set; }

        /// <summary>
        /// 绑定的物理地址
        /// </summary>
        [TableField]
        public string MacAddress { get; set; }

        /// <summary>
        /// 二级密码
        /// </summary>
        [TableField]
        public string SecondPassword { get; set; }
        /// <summary>
        /// 商户Id
        /// </summary>
        [TableField]
        public long MerchantId { get; set; }
        /// <summary>
        /// 重写方法，构造权限数据
        /// </summary>
        /// <param name="dr"></param>
        public override void MakeSelfFromIDataReader(System.Data.IDataReader dr)
        {
            base.MakeSelfFromIDataReader(dr);
            
            //对员工才进行权限的解析
            if (UserType == UserType.Employee)
            {
                if (PermissionsData != null && PermissionsData != "")
                {
                    try
                    {
                        Permissions = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Permission>>(PermissionsData);
                    }
                    catch (Exception)
                    {
                        Permissions = new List<Permission>();
                    }
                }
                else
                {
                    Permissions = new List<Permission>();
                }
            }
        }

        /// <summary>
        /// 对一个用户设置权限
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="lst"></param>
        /// <returns></returns>
        public static bool UpdatePermission(long uid, List<Permission> lst)
        {
            if (lst == null)
            {
                lst = new List<Permission>();
            }
            string str = Newtonsoft.Json.JsonConvert.SerializeObject(lst);
            Query q = Query.NewQuery();
            q.Where("UserId", Symbol.EqualTo, Ralation.End, uid);
            return mUser.Update("PermissionsData", str, q) > 0;
        }

        //更新
        public override bool Update()
        {
            return base.Update();
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <returns></returns>
        public bool Register()
        {
            string pwd = this.Password;
            long i = Create();
            if (i > 0)
            {
                UserId = i;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        public bool Login()
        {
            string pwd = this.Password;
            MD5Password();
            Query query = Query.NewQuery();
            query.AddCondition(Query.CreateCondition("UserName", Symbol.EqualTo, Ralation.And, UserName));
            query.AddCondition(Query.CreateCondition("Password", Symbol.EqualTo, Ralation.End, Password));
            bool suc = GetModel(query);
            if (suc)
            {
                //更新登陆时间
                this.LastLoginIP = this.LoginIP;
                this.LastLoginTime = this.LoginTime;
                this.LoginIP = Easpnet.Modules.Web.Http.GetClientIP();
                this.LoginTime = DateTime.Now;
                Update();

                //
                MemberContext.LoginUser = this;

                //跟踪登陆
                LogWriter.Track("用户登陆", this.UserName + "登陆成功");
            }
            else
            {
                LogWriter.Warning("用户登陆", "登陆失败，用户：" + this.UserName + "，尝试密码：" + pwd);
            }

            return suc;
        }

        /// <summary>
        /// 根据用户名查找用户，若找到，则返回true，否则返回false
        /// </summary>
        /// <returns></returns>
        public bool GetUserByName()
        {
            Query query = Query.NewQuery();
            query.Where("UserName", Symbol.EqualTo, Ralation.End, UserName);
            return GetModel(query);
        }

        /// <summary>
        /// 查看是否存在用户
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool ExistsUser(string userName)
        {
            Query q = Query.NewQuery();
            Query query = Query.NewQuery();
            query.AddCondition(Query.CreateCondition("UserName", Symbol.EqualTo, Ralation.End, userName));
            return Count(query) > 0;
        }

        /// <summary>
        /// 对密码进行MD5加密
        /// </summary>
        /// <returns></returns>
        public void MD5Password()
        {
            if (!string.IsNullOrEmpty(Password))
            {
                Password = FormsAuthentication.HashPasswordForStoringInConfigFile(Password, "MD5");
            }            
        }


        /// <summary>
        /// 根据用户类型获取所有的用户
        /// </summary>
        /// <param name="userType"></param>
        /// <returns></returns>
        public static List<User> GetUserList(UserType userType)
        {
            List<User> lst = new User().GetList<User>(Query.NewQuery()
                .Where("UserType", Symbol.EqualTo, Ralation.End, userType));
            return lst;
        }


        /// <summary>
        /// 更新用户状态
        /// </summary>
        /// <param name="status">状态</param>
        /// <returns></returns>
        public static bool UpdateUserStatus(long uid, UserStatus status)
        {
            Query query = Query.NewQuery();
            query.AddCondition(Query.CreateCondition("UserId", Symbol.EqualTo, Ralation.End, uid));
            return mUser.Update(query, new NameObject("UserStatus", status)) > 0;
        }



        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        public bool AdminLogin(string mac, out string res)
        {
            res = "";
            string pwd = this.Password;            
            Query query = Query.NewQuery();
            query.AddCondition(Query.CreateCondition("UserName", Symbol.EqualTo, Ralation.And, UserName));
            query.AddCondition(Query.CreateCondition("Password", Symbol.EqualTo, Ralation.And, Password));
            query.AddConditionGroup(Ralation.End,
                Query.CreateCondition("UserType", Symbol.EqualTo, Ralation.Or, UserType.Employee),
                Query.CreateCondition("UserType", Symbol.EqualTo, Ralation.Or, UserType.SystemAdmin),
                Query.CreateCondition("UserType", Symbol.EqualTo, Ralation.End, UserType.WebAdmin));

            bool suc = GetModel(query);
            if (suc)
            {
                //验证Mac地址是否正确
                if (string.IsNullOrEmpty(this.MacAddress) || this.MacAddress == mac)
                {
                    //更新登陆时间
                    this.LastLoginIP = this.LoginIP;
                    this.LastLoginTime = this.LoginTime;
                    this.LoginIP = Easpnet.Modules.Web.Http.GetClientIP();
                    this.LoginTime = DateTime.Now;
                    this.MacAddress = mac;
                    Update();

                    //
                    MemberContext.LoginUser = this;

                    //跟踪登陆
                    LogWriter.Track("用户登陆", this.UserName + "登陆成功");
                    res = "登陆成功";
                }
                else
                {
                    LogWriter.Warning("用户登陆", "硬件验证失败");
                    res = "该用户只能在指定的硬件上登陆！";
                    suc = false;
                }                
            }
            else
            {
                LogWriter.Warning("用户登陆", "登陆失败，用户：" + this.UserName + "，尝试密码：" + pwd + ",IP:" + Easpnet.Modules.Web.Http.GetClientIP());
                res = "用户名或者密码错误！";
            }
                        
            return suc;
        }
    }
}
