using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SW.Model
{
    public static class CONSTSTATICENUM
    {
        //导航节点ID
        public const string LAYOUTMEUNITEMID = "menu-item-";
        //导航首节点Class
        public const string LAYOUTMEUNITEMFCLASS = "menu-item menu-item-type-custom menu-item-object-custom current-menu-item current_page_item menu-item-home ";
        //导航一般节点Class
        public const string LAYOUTMEUNITEMNCLASS = "menu-item menu-item-type-post_type menu-item-object-page ";
        //导航带子节点Class
        public const string LAYOUTMEUNITEMSCLASS = "menu-item menu-item-type-post_type menu-item-object-page menu-item-has-children ";
        //导航子节点Class
        public const string LAYOUTMEUNITEMSSCLASS = "menu-item menu-item-type-taxonomy menu-item-object-category ";

        public const string PORTALIMGAGELINK = "../../PortalDemo/statics/images/";
    }
    public  enum Authorizations
    {
        Create = 1,         //0001
        Retrieve = 2,       //0010
        Update = 4,         //0100
        Delete = 8,         //1000
        CRUD = 15,          //1111
        CRU = 7,            //0111
        RU = 6              //0110
    }
  
}