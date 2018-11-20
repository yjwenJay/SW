using Easpnet;
using Easpnet.Modules;
using SW.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW.Business
{
    public class SysUserAuthorBLL
    {
        /// <summary>
        /// 获取文章list
        /// </summary>
        /// <param name="_owner">权限</param>
        /// <returns></returns>
        public bool CheckPasswordHash(string passwordHash, SysUser_Role_Module_Authorization sysUser)
        {
            try
            {
               
                if (passwordHash.Equals(sysUser.PsswordHash))//账密不为空
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public SysUser_Role_Module_Authorization CheckAccount(string accounts)
        {
            try
            {
                SysUser_Role_Module_Authorization sysUser = new SysUser_Role_Module_Authorization();

                if (!String.IsNullOrEmpty(accounts))//账密不为空
                {
                    Query query = Query.NewQuery();
                    query.Where("Account", Symbol.EqualTo, Ralation.Or, accounts);
                    query.Where("Email", Symbol.EqualTo, Ralation.Or, accounts);
                    query.Where("Phone", Symbol.EqualTo, Ralation.Or, accounts);
                    List<SysUser_Role_Module_Authorization> lists = sysUser.GetList<SysUser_Role_Module_Authorization>(query);
                    if (lists.Count > 0)
                    {
                        return lists[0];
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
