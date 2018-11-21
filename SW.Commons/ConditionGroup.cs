using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SW
{
    [Serializable]
    /// <summary>
    /// 条件组
    /// </summary>
    public class ConditionGroup : ConditionBase
    {
        public List<ConditionBase> Conditions { get; set; }


        /// <summary>
        /// 构造一个条件组,初始化Conditon为空
        /// </summary>
        public ConditionGroup()
        {
            Conditions = new List<ConditionBase>();
        }


        /// <summary>
        /// 构造一个条件组,初始化Conditon为空,给予指定和下一组条件的关系
        /// </summary>
        /// <param name="ralation">和下一组条件的关系</param>
        public ConditionGroup(Ralation ralation)
        {
            Conditions = new List<ConditionBase>();
            this.Ralation = ralation;
        }


        /// <summary>
        /// 增加条件
        /// </summary>
        /// <param name="condition"></param>
        public void AddCondition(ConditionBase condition)
        {
            Conditions.Add(condition);
        }

        /// <summary>
        /// 增加条件
        /// </summary>
        /// <param name="condition"></param>
        public void AddCondition(Condition condition)
        {
            Conditions.Add(condition);
        }


        /// <summary>
        /// 增加条件组
        /// </summary>
        /// <param name="group"></param>
        public void AddConditionGroup(ConditionGroup group)
        {
            Conditions.Add(group);
        }

        /// <summary>
        /// 条件一个条件组,给定的数组将全部作为自条件
        /// </summary>
        /// <param name="conditions"></param>
        public void AddConditionGroup(Ralation ralation, Condition[] conditions)
        {
            ConditionGroup lst = new ConditionGroup(ralation);
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
        public void AddRange(Condition[] conditions)
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
        public void InsertCondition(int index, ConditionBase condition)
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

                foreach (ConditionBase c in Conditions)
                {
                    s.Append(c.GenerateQueryString());
                }

                s.Append(")");
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
