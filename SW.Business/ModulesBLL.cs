using Easpnet;
using SW.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW.Business
{
    public class ModulesBLL
    {
        public List<Authorization_Role_SysUser_Module> GetModules(int owner)
        {
            try
            {
                Db db = new Db("Authorization_Role_SysUser_Module");
                if (owner >0 )
                {
                    db.Where("Id", owner);
                }
                db.Where("ModuleLevel", 0);
                return db.Select<Authorization_Role_SysUser_Module>("DISTINCT(ModuleId),ModuleName"); 
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}
