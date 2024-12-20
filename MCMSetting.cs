﻿using System;
using MCM.Abstractions.Attributes.v1;

using MCM.Abstractions.Attributes;
using MCM.Abstractions.Base.Global;

using TaleWorlds.Localization;

namespace YuefTORSetting
{
    internal class MCMSetting : AttributeGlobalSettings<MCMSetting>
    {
        public override string Id
        {
            get
            {
                return "YuefTORSetting_Submodule";
            }
        }
        public override string DisplayName
        {
            get
            {
                // 使用本地化字符串为显示名称提供文本
                string text = new TextObject("{=YueF_ModName}中古战锤:设置", null).ToString();
                // 获取当前程序集的版本号
                Version version = typeof(MCMSetting).Assembly.GetName().Version;
                // 返回带版本号的模块名称
                return text + ((version != null) ? version.ToString(3) : null);
            }
        }

        public override string FolderName
        {
            get
            {
                return "Yuef_TOR_Config";
            }
        }

        public override string FormatType
        {
            get
            {
                return "json2";
            }
        }


        // 最大职业技能点
        [SettingProperty("最大职业技能点数", 0, 40, RequireRestart = false, HintText = "设置最大职业技能点数(35即可点满所有技能)", Order = 1)]
        [SettingPropertyGroup("职业技能点数调整", GroupOrder = 0)]
        public int Yuef_MaximumNumberOfCareerPerkPoints { get; set; } = 30;

        // 最大自定义资源值
        [SettingProperty("最大自定义资源值", 0, 100000, RequireRestart = false, HintText = "设置最大自定义资源值上限", Order = 11)]
        [SettingPropertyGroup("自定义资源设置", GroupOrder = 1)]
        public int Yuef_MaximumCustomResourceValue { get; set; } = 2500;

        [SettingProperty("自定义资源值每日获取倍率(%)", 0, 1000, RequireRestart = false, HintText = "设置自定义资源值的每日获取的百分比倍率(当每日结算值为正时生效)", Order = 12)]
        [SettingPropertyGroup("自定义资源设置", GroupOrder = 1)]
        public int Yuef_CustomResource_DailyMultiplier { get; set; } = 100;

        [SettingProperty("自定义资源值每日消耗减免(%)", 0, 100, RequireRestart = false, HintText = "设置自定义资源值的日常消耗的百分比倍率(当每日结算值为负时生效)", Order = 13)]
        [SettingPropertyGroup("自定义资源设置", GroupOrder = 1)]
        public int Yuef_CustomResource_DailyCostReduction { get; set; } = 100;

        [SettingProperty("自定义资源值兵种升阶消耗减免(%)", -100, 100, RequireRestart = false, HintText = "设置升阶的资源消耗减免百分比(负数增加消耗)", Order = 14)]
        [SettingPropertyGroup("自定义资源设置", GroupOrder = 1)]
        public int Yuef_CustomResource_UpgradeCostReduction { get; set; } = 30;

        [SettingProperty("自定义资源值兵种维持消耗减免(%)", -100, 100, RequireRestart = false, HintText = "设置维持时的资源消耗减免百分比(负数增加消耗)", Order = 15)]
        [SettingPropertyGroup("自定义资源设置", GroupOrder = 1)]
        public int Yuef_CustomResource_KeepCostReduction { get; set; } = 30;


        // 每个强盗派系初始的藏身处数量
        [SettingProperty("每个强盗派系初始的藏身处数量", 1, 50, RequireRestart = false, HintText = "设置每个强盗派系初始的藏身处数量", Order = 21)]
        [SettingPropertyGroup("自定义强盗队伍", GroupOrder = 2)]
        public int Yuef_NumberOfInitialHideoutsAtEachBanditFaction { get; set; } = 10;

        // 每个强盗派系最大藏身处数量
        [SettingProperty("每个强盗派系最大藏身处数量", 1, 100, RequireRestart = false, HintText = "设置每个强盗派系最大藏身处数量", Order = 22)]
        [SettingPropertyGroup("自定义强盗队伍", GroupOrder = 2)]
        public int Yuef_NumberOfMaximumHideoutsAtEachBanditFaction { get; set; } = 60;

        // 每个藏身处周围最大强盗队伍数量
        [SettingProperty("每个藏身处周围最大强盗队伍数量", 1, 50, RequireRestart = false, HintText = "设置每个藏身处周围最大强盗队伍数量", Order = 23)]
        [SettingPropertyGroup("自定义强盗队伍", GroupOrder = 2)]
        public int Yuef_NumberOfMaximumBanditPartiesAroundEachHideout { get; set; } = 10;

        // 每个藏身处内最大强盗队伍数量
        [SettingProperty("每个藏身处内最大强盗队伍数量", 1, 15, RequireRestart = false, HintText = "设置每个藏身处内最大强盗队伍数量", Order = 24)]
        [SettingPropertyGroup("自定义强盗队伍", GroupOrder = 2)]
        public int Yuef_NumberOfMaximumBanditPartiesInEachHideout { get; set; } = 3;



        // 最短和平天数
        [SettingProperty("最短和平天数", 1, 50, RequireRestart = false, HintText = "设置最短和平天数", Order = 31)]
        [SettingPropertyGroup("AI战争配置设置", GroupOrder = 3)]
        public float Yuef_MinPeaceDays { get; set; } = 20;

        // 最短战争天数
        [SettingProperty("最短战争天数", 1, 50, RequireRestart = false, HintText = "设置最短战争天数", Order = 32)]
        [SettingPropertyGroup("AI战争配置设置", GroupOrder = 3)]
        public float Yuef_MinWarDays { get; set; } = 20;


        [SettingProperty("火枪BUG修复", RequireRestart = false, HintText = "启用后会修复原版TOR中火枪300熟练度时队伍士兵枪械无法穿盾的BUG;修复法师无限索敌的BUG", Order = 41)]
        [SettingPropertyGroup("TOR原版BUG修复", GroupOrder = 4)]
        public bool Yuef_BUGFix_PiercingShots { get; set; } = true;
        [SettingProperty("AI施法索敌BUG修复", RequireRestart = false, HintText = "启用后会移除法师AI无限索敌,并增加索敌距离限制", Order = 42)]
        [SettingPropertyGroup("TOR原版BUG修复", GroupOrder = 4)]
        public bool Yuef_BUGFix_AICast { get; set; } = true;
        [SettingProperty("AI最大施法距离限制",0,500,RequireRestart = false, HintText = "在启用AI施法索敌BUG修复启用时生效", Order = 43)]
        [SettingPropertyGroup("TOR原版BUG修复", GroupOrder = 4)]
        public float Yuef_BUGFix_CastDistance{ get; set; } = 100f;



        [SettingProperty("诅咒之地机制调整", RequireRestart = false, HintText = "启用后会替换原版TOR的扣血受伤机制,更改为在诅咒之地范围内减速", Order = 51)]
        [SettingPropertyGroup("TOR机制调整", GroupOrder = 5)]
        public bool Yuef_CursedSite_adjustment { get; set; } = false;

        [SettingProperty("AI树人招募调整", RequireRestart = false, HintText = "启用后会强化木精文化的招募机制,当AI位于木精森林时将有更高的概率获得树人的援助", Order = 52)]
        [SettingPropertyGroup("TOR机制调整", GroupOrder = 5)]
        public bool Yuef_Treeman_adjustment { get; set; } = true;

        [SettingProperty("城堡机制调整", RequireRestart = false, HintText = "启用后对城堡添加机制: 当队伍与附近的城堡所属为敌对时会受到移速惩罚,反之则会获得移速奖励", Order = 53)]
        [SettingPropertyGroup("TOR机制调整", GroupOrder = 5)]
        public bool Yuef_Castle_adjustment { get; set; } = true;

        [SettingProperty("战后俘虏机制调整", RequireRestart = false, HintText = "启用后会恢复拯救俘虏的机制", Order = 54)]
        [SettingPropertyGroup("TOR机制调整", GroupOrder = 5)]
        public bool Yuef_BattleReward_adjustment { get; set; } = false;

        [SettingProperty("AI战斗行为逻辑调整", RequireRestart = false, HintText = "(测试项目当前版本暂未实装)启用后会调整TOR原版的AI部队战斗行为逻辑", Order = 60)]
        [SettingPropertyGroup("TOR_AI调整", GroupOrder = 6)]
        public bool Yuef_AIBehavior_adjustment { get; set; } = false;
    }
}
