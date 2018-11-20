using System;
using System.Collections.Generic;
using System.Text;

namespace Easpnet
{
    [Serializable]
    /// <summary>
    /// 条件组
    /// </summary>
    public class QueryConditionGroup : QueryConditionBase
    {
        public List<QueryConditionBase> Conditions { get; set; }


        /// <summary>
        /// 构造一个条件组,初始化Conditon为空
        /// </summary>
        public QueryConditionGroup()
        {
            Conditions = new List<QueryConditionBase>();            
        }


        /// <summary>
        /// 构造一个条件组,初始化Conditon为空,给予指定和下一组条件的关系
        /// </summary>
        /// <param name="ralation">和下一组条件的关系</param>
        public QueryConditionGroup(Ralation ralation)
        {
            Conditions = new List<QueryConditionBase>();
            this.Ralation = ralation;
        }


        /// <summary>
        /// 增加条件
        /// </summary>
        /// <param name="condition"></param>
        public void AddCondition(QueryConditionBase condition)
        {
            Conditions.Add(condition);
        }

        /// <summary>
        /// 增加条件
        /// </summary>
        /// <param name="condition"></param>
        public void AddCondition(QueryCondition condition)
        {
            Conditions.Add(condition);
        }


        /// <summary>
        /// 条件一个条件组,给定的数组将全部作为自条件
        /// </summary>
        /// <param name="conditions"></param>
        public void AddConditionGroup(Ralation ralation, params QueryConditionBase[] conditions)
        {
            QueryConditionGroup lst = new QueryConditionGroup(ralation);
            for (int i = 0; i < conditions.Length; i++)
            {
                lst.AddCondition(conditions[i]);
            }

            AddCondition(lst);
        }




        /// <summary>
        /// 一次性添加多个条件
        /// </summary>
        /// <param name="conditions"></param>
        public void AddRange(QueryCondition[] conditions)
        {
            for (int i = 0; i < conditions.Length; i++)
            {
                AddCondition(conditions[i]);
            }
        }


        /// <summary>
        /// 插入条件
        /// </summary>
        /// <param name="condition"></param>
        public void InsertCondition(int index, QueryConditionBase condition)
        {
            Conditions.Insert(index, condition);
        }


        /// <summary>
        /// 转化为Sql查询字符串
        /// </summary>
        /// <returns></returns>
        public override string GenerateQueryString()
        {
            StringBuilder s = new StringBuilder();

            if (Conditions.Count > 0)
            {
                s.Append("(");
                Conditions[Conditions.Count - 1].Ralation = Easpnet.Ralation.End;   //将最后一组条件改为结束
                foreach (QueryConditionBase c in Conditions)
                {
                    s.Append(c.GenerateQueryString());
                }

                s.Append(")");
            }
            else
            {
                s.Append("(1=1)");
            }


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
