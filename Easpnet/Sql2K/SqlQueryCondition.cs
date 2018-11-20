using System;
using System.Collections.Generic;
using System.Text;

namespace Easpnet.Sql2K
{
    /// <summary>
    /// SQL Server数据库查询构造
    /// </summary>
    public class SqlQueryCondition : QueryCondition
    {
        public override string GenerateQueryString()
        {
            //
            StringBuilder s = new StringBuilder();
            //if (Values == null || Values.Count == 0 || Values[0] == null) return "";
            //获取值
            string val1 = "";
            if (Values != null && Values.Count >= 1)
            {
                val1 = GetValue(Values[0]);
            }
            
            string val2 = val1;
            if (Values != null && Values.Count > 1)
            {
                val2 = GetValue(Values[1]);
            }

            s.Append("(");

            s.Append("[" + ColumnName + "]");

            //
            switch (this.Symbol)
            {
                case Symbol.Between:
                    s.Append(" Between '" + val1 + "' And '" + val2 + "'");
                    break;
                case Symbol.EqualTo:
                    s.Append(" = '" + val1 + "'");
                    break;
                case Symbol.GreaterThan:
                    s.Append(" > '" + val1 + "'");
                    break;
                case Symbol.GreaterThanOrEqualTo:
                    s.Append(" >= '" + val1 + "'");
                    break;
                case Symbol.In:
                    s.Append(" In(");
                    for (int i = 0; i < Values.Count; i++)
                    {
                        s.Append("'" + GetValue(Values[i]) + "'");
                        //最后一个值后面不加","号
                        if (i < Values.Count - 1)
                        {
                            s.Append(",");
                        }
                    }
                    s.Append(")");
                    break;
                case Symbol.IsNotNull:
                    s.Append(" IS NOT NULL");
                    break;
                case Symbol.IsNull:
                    s.Append(" IS NULL");
                    break;
                case Symbol.LessThan:
                    s.Append(" < '" + val1 + "'");
                    break;
                case Symbol.LessThanOrEqualTo:
                    s.Append(" <= '" + val1 + "'");
                    break;
                case Symbol.Like:
                    s.Append(" Like '%" + val1 + "%'");
                    break;
                case Symbol.NotBetween:
                    s.Append(" Not Between '" + val1 + "' And '" + val2 + "'");
                    break;
                case Symbol.NotEqualTo:
                    s.Append(" <> '" + val1 + "'");
                    break;
                case Symbol.NotIn:
                    s.Append(" Not In(");
                    for (int i = 0; i < Values.Count; i++)
                    {
                        s.Append("'" + GetValue(Values[i]) + "'");
                        //最后一个值后面不加","号
                        if (i < Values.Count - 1)
                        {
                            s.Append(",");
                        }
                    }
                    s.Append(")");
                    break;
                case Symbol.NotLike:
                    s.Append(" Not Like '%" + val1 + "%'");
                    break;
            }

            s.Append(")");

            //下一组条件关系
            if (this.Ralation == Ralation.And)
            {
                s.Append(" And ");
            }
            else if (this.Ralation == Ralation.Or)
            {
                s.Append(" Or ");
            }

            return s.ToString();
        }



        private string GetValue(object obj)
        {
            if (obj != null)
            {
                Type type = obj.GetType();
                if (type == typeof(DateTime))
                {
                    return DateTimeUtility.FullString(TypeConvert.ToDateTime(obj));
                }
                else if (type == typeof(bool))
                {
                    return TypeConvert.ToBoolean(obj) ? "1" : "0";
                }
                else if (type.IsEnum)
                {
                    return TypeConvert.ToInt32(obj).ToString();
                }
                else
                {

                    string str = obj.ToString();
                    //sql中两个单引号代表一个单引号
                    str = str.Replace("'", "''");
                    return str;
                }
            }
            else
            {
                return "";
            }
        }
    }
}
