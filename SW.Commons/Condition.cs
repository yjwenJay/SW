using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SW.Commons
{
    [Serializable]
    /// <summary>
    /// 条件
    /// </summary>
    public class Condition : ConditionBase
    {
        /// <summary>
        /// 列名称
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 数据存储格式是否为NameId格式
        /// </summary>
        public bool IsNameId { get; set; }

        /// <summary>
        /// 是否查询的是NameId的Name, 如果为NameId格式的列, 该值才有效
        /// </summary>
        public bool IsQueryName { get; set; }

        /// <summary>
        /// 符号
        /// </summary>
        public Symbol Symbol { get; set; }
        /// <summary>
        /// 比较的值
        /// </summary>
        public List<string> Values { get; set; }


        /// <summary>
        /// 构造实例,根据指定的参数
        /// </summary>
        /// <param name="pColumnName">数据库列名称</param>
        /// <param name="pSymbol">比较关系</param>
        /// <param name="pValues">值的集合</param>
        /// <param name="pRalation">和下一组条件之间的关系</param>
        public Condition(string pColumnName, Symbol pSymbol, List<string> pValues, Ralation pRalation)
        {
            ColumnName = pColumnName;
            Symbol = pSymbol;
            Values = pValues;
            Ralation = pRalation;
            IsNameId = false;
            IsQueryName = false;
        }


        /// <summary>
        /// 构造实例,根据指定的参数
        /// </summary>
        /// <param name="pColumnName">数据库列名称</param>
        /// <param name="pSymbol">比较关系</param>
        /// <param name="value">值</param>
        /// <param name="pRalation">和下一组条件之间的关系</param>
        public Condition(string pColumnName, Symbol pSymbol, string value, Ralation pRalation)
        {
            ColumnName = pColumnName;
            Symbol = pSymbol;

            //
            List<string> lst = new List<string>();
            lst.Add(value);
            Values = lst;

            //
            Ralation = pRalation;
            IsNameId = false;
            IsQueryName = false;
        }




        /// <summary>
        /// 构造实例,根据指定的参数
        /// </summary>
        /// <param name="pColumnName">数据库列名称</param>
        /// <param name="pSymbol">比较关系</param>
        /// <param name="pValues">值的集合</param>
        /// <param name="pRalation">和下一组条件之间的关系</param>
        /// <param name="pIsNameId">是否为NameId格式的存储</param>
        /// <param name="pIsQueryName">要查询的是否为Name, 值为false表示根据Id查询, 否则,表示根据Name查询</param>
        public Condition(string pColumnName, Symbol pSymbol, List<string> pValues, Ralation pRalation,
            bool pIsNameId, bool pIsQueryName)
        {
            ColumnName = pColumnName;
            Symbol = pSymbol;
            Values = pValues;
            Ralation = pRalation;
            IsNameId = pIsNameId;
            IsQueryName = pIsQueryName;
        }


        /// <summary>
        /// 构造实例,根据指定的参数
        /// </summary>
        /// <param name="pColumnName">数据库列名称</param>
        /// <param name="pSymbol">比较关系</param>
        /// <param name="values">值</param>
        /// <param name="pRalation">和下一组条件之间的关系</param>
        /// <param name="pIsNameId">是否为NameId格式的存储</param>
        /// <param name="pIsQueryName">要查询的是否为Name, 值为false表示根据Id查询, 否则,表示根据Name查询</param>
        public Condition(string pColumnName, Symbol pSymbol, string value, Ralation pRalation,
            bool pIsNameId, bool pIsQueryName)
        {
            ColumnName = pColumnName;
            Symbol = pSymbol;

            //
            List<string> lst = new List<string>();
            lst.Add(value);
            Values = lst;

            //
            Ralation = pRalation;
            IsNameId = pIsNameId;
            IsQueryName = pIsQueryName;
        }


        /// <summary>
        /// 转化为Sql查询字符串
        /// </summary>
        /// <returns></returns>
        public override string GenerateQueryString()
        {
            StringBuilder s = new StringBuilder();
            if (Values == null || Values.Count == 0 || Values[0] == null) return "";
            //获取值
            string val1 = Values[0].ToString();
            string val2 = val1;
            if (Values.Count > 1)
            {
                val2 = Values[1].ToString();
            }

            s.Append("(");

            if (IsNameId)
            {
                NameId ni1 = NameId.From(Values[0]);
                NameId ni2 = ni1;
                if (Values.Count > 1)
                {
                    ni2 = NameId.From(Values[1]);
                }

                //查询的是Name
                if (IsQueryName)
                {
                    val1 = ni1.Name;
                    val2 = ni2.Name;
                    s.Append("substring([" + ColumnName + "],  charindex(':',[" + ColumnName + "]) + 1,len([" + ColumnName + "]))");
                }
                else
                {
                    val1 = ni1.Id.ToString();
                    val2 = ni2.Id.ToString();

                    s.Append("substring([" + ColumnName + "], 0 ,charindex(':',[" + ColumnName + "]))");
                }


            }
            else
            {
                s.Append(ColumnName);
            }



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
                        s.Append("'" + Values[i].ToString() + "'");
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
                        s.Append("'" + Values[i].ToString() + "'");
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
    }
}
