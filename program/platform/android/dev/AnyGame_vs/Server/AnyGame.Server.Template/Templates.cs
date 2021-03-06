﻿using AnyGame.Server.Template.Item;
using AnyGame.Server.Template.Spell;
using DogSE.Library.Log;
using DogSE.Server.Core.Config;
using System;
using System.Collections.Generic;

namespace AnyGame.Server.Template
{
    public static class Templates
    {
        #region 变量定义

        /// <summary>
        /// 物品模板列表
        /// </summary>
        public static ItemTemplate[] ItemTemplate { get; private set; }
        private static Dictionary<int, ItemTemplate> itemMap = new Dictionary<int, ItemTemplate>();

        /// <summary>
        /// 技能模版列表
        /// </summary>
        public static SpellTemplate[] SpellTemplate { get; private set; }
        private static Dictionary<int, SpellTemplate> spellMap = new Dictionary<int, SpellTemplate>();

        #endregion

        #region 对外方法

        /// <summary>
        /// 获得物品模板
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ItemTemplate GetItemTemplate(int id)
        {
            return itemMap[id];
        }


        /// <summary>
        /// 获得技能模版
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static SpellTemplate GetSpellTemplate(int id)
        {
            return spellMap[id];
        }

        #endregion

        #region ToMap

        /// <summary>
        /// 将一个数组转为一个字典
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="values"></param>
        /// <param name="fun"></param>
        /// <returns></returns>
        private static Dictionary<TKey, TValue> ToMap<TKey, TValue>(this TValue[] values, Func<TValue, TKey> fun)
        {
            Dictionary<TKey, TValue> ret = new Dictionary<TKey, TValue>(values.Length + 1);
            foreach (var v in values)
            {
                var key = fun(v);
                if (ret.ContainsKey(key))
                {
                    Logs.Error(string.Format("To map has same key {0}", key));
                }
                ret[key] = v;
            }
            return ret;
        }

        #endregion

        #region 加载模板数据
        /// <summary>
        /// 加载模板数据
        /// </summary>
        /// <param name="folder">配置文件的目录，不填写时，采用项目的根目录</param>
        public static void LoadTemplate(string folder = null)
        {
            DynamicConfigFileManager.LoadData(folder);

            #region Item

            ItemTemplate = DynamicConfigFileManager.GetConfigData<ItemTemplate>("Item");
            Logs.Debug("Template Item count:{0}", ItemTemplate.Length);

            itemMap = ItemTemplate.ToMap(o => o.Id);

            #endregion


            #region Spell

            SpellTemplate = DynamicConfigFileManager.GetConfigData<SpellTemplate>("Spell");
            Logs.Debug("Template Spell count:{0}", SpellTemplate.Length);

            spellMap = SpellTemplate.ToMap(o => o.Id);

            #endregion
        }
        #endregion
    }
}
