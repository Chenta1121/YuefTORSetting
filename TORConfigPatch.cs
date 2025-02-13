using HarmonyLib;
using TOR_Core.Utilities;
using MCM.Abstractions.Base.Global;
using TOR_Core.Models;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Localization;
using TOR_Core.Extensions;
using System;
using TOR_Core.CampaignMechanics.CustomResources;
namespace YuefTORSetting
{
    [Harmony]
    public static class TORConfigPatch
    {
        // 获取最大职业Perk点数
        [HarmonyPostfix]
        [HarmonyPatch(typeof(TORConfig), "get_MaximumNumberOfCareerPerkPoints")]
        static void Postfix_MaximumNumberOfCareerPerkPoints(ref int __result)
        {
            var globalSettings = GlobalSettings<MCMSetting>.Instance;
            if (globalSettings != null)
            {
                __result = globalSettings.Yuef_MaximumNumberOfCareerPerkPoints;
            }
            else
            {
                // 处理配置为空的情况，设置为默认值0
                __result = 30;
            }
        }

        // 获取最大自定义资源值
        [HarmonyPostfix]
        [HarmonyPatch(typeof(TORConfig), "get_MaximumCustomResourceValue")]
        static void Postfix_MaximumCustomResourceValue(ref int __result)
        {
            var globalSettings = GlobalSettings<MCMSetting>.Instance;
            if (globalSettings != null)
            {
                __result = globalSettings.Yuef_MaximumCustomResourceValue;
            }
            else
            {
                __result = 2500;
            }
        }

        // 获取文化特定的自定义资源变化
        [HarmonyPostfix]
        [HarmonyPatch(typeof(TORCustomResourceModel), "GetCultureSpecificCustomResourceChange")]
        static void Postfix_GetCultureSpecificCustomResourceChange(ref ExplainedNumber __result)
        {
            if (__result.ResultNumber < 0f)
            {
                // 应用减免逻辑：计算减免的值
                float costReductionAmount = -__result.ResultNumber * GlobalSettings<MCMSetting>.Instance.Yuef_CustomResource_DailyCostReduction / 100f;
                costReductionAmount = (float)Math.Round(costReductionAmount, 2); // 保留两位小数

                // 应用结果：将减免的值加到结果中，并给出描述
                __result.Add(costReductionAmount, new TextObject("{=Yuef_Description_1}Cost Reduction", null), null);
            }
            else
            {
                // 应用倍率逻辑：计算额外获取的值
                float resourceGainAmount = __result.ResultNumber * (GlobalSettings<MCMSetting>.Instance.Yuef_CustomResource_DailyMultiplier / 100f - 1f);
                resourceGainAmount = (float)Math.Round(resourceGainAmount, 2); // 保留两位小数

                // 应用结果：将额外获取的值加到结果中，并给出描述
                __result.Add(resourceGainAmount, new TextObject("{=Yuef_Description_2}Acquisition Rate", null), null);
            }
        }

        // 兵种升阶减免
        [HarmonyPostfix]
        [HarmonyPatch(typeof(CharacterObjectExtensions), "GetCustomResourceRequiredForUpgrade")]
        static void Postfix_GetCustomResourceRequiredForUpgrade(ref Tuple<CustomResource, int> __result)
        {
            var globalSettings = GlobalSettings<MCMSetting>.Instance;

            // 检查全局设置、__result 是否有效，并且资源成本大于0
            if (globalSettings != null && __result != null && __result.Item2 > 0)
            {
                // 获取减免倍率，并限制在0到100之间
                float upgradeCostReduction = Math.Max(0f, Math.Min(100f, globalSettings.Yuef_CustomResource_UpgradeCostReduction));

                // 计算减免后的成本，并保证不低于0
                int reducedCost = Math.Max(0, (int)(__result.Item2 * (1 - upgradeCostReduction / 100f)));

                // 如果减免后的成本为0，考虑返回null或者进行特殊处理
                if (reducedCost == 0)
                {
                    __result = null;  // 返回null，表示资源成本为0
                }
                else
                {
                    // 更新__result，将修改后的成本存回
                    __result = new Tuple<CustomResource, int>(__result.Item1, reducedCost);
                }
            }
        }


        // 维持资源成本减免
        [HarmonyPostfix]
        [HarmonyPatch(typeof(CharacterObjectExtensions), "GetCustomResourceRequiredForUpkeep")]
        static void Postfix_GetCustomResourceRequiredForUpkeep(ref Tuple<CustomResource, int> __result)
        {
            var globalSettings = GlobalSettings<MCMSetting>.Instance;
            if (globalSettings != null && __result != null && __result.Item2 > 0)
            {
                // 获取减免倍率
                float keepCostReduction = Math.Max(0f, Math.Min(100f, globalSettings.Yuef_CustomResource_KeepCostReduction));

                // 修改第二项的 int 值（维持资源成本），应用减免倍率
                int reducedCost = Math.Max(0, (int)(__result.Item2 * (1 - keepCostReduction / 100f)));

                // 更新 __result，将修改后的成本存回
                __result = new Tuple<CustomResource, int>(__result.Item1, reducedCost);
            }
        }

        // 获取初始盗匪据点数量
        [HarmonyPostfix]
        [HarmonyPatch(typeof(TORConfig), "get_NumberOfInitialHideoutsAtEachBanditFaction")]
        static void Postfix_NumberOfInitialHideoutsAtEachBanditFaction(ref int __result)
        {
            var globalSettings = GlobalSettings<MCMSetting>.Instance;
            if (globalSettings != null)
            {
                __result = globalSettings.Yuef_NumberOfInitialHideoutsAtEachBanditFaction;
            }
            else
            {
                __result = 10;
            }
        }

        // 获取最大盗匪据点数量
        [HarmonyPostfix]
        [HarmonyPatch(typeof(TORConfig), "get_NumberOfMaximumHideoutsAtEachBanditFaction")]
        static void Postfix_NumberOfMaximumHideoutsAtEachBanditFaction(ref int __result)
        {
            var globalSettings = GlobalSettings<MCMSetting>.Instance;
            if (globalSettings != null)
            {
                __result = globalSettings.Yuef_NumberOfMaximumHideoutsAtEachBanditFaction;
            }
            else
            {
                __result = 0;
            }
        }

        // 获取每个据点周围最大盗匪部队数量
        [HarmonyPostfix]
        [HarmonyPatch(typeof(TORConfig), "get_NumberOfMaximumBanditPartiesAroundEachHideout")]
        static void Postfix_NumberOfMaximumBanditPartiesAroundEachHideout(ref int __result)
        {
            var globalSettings = GlobalSettings<MCMSetting>.Instance;
            if (globalSettings != null)
            {
                __result = globalSettings.Yuef_NumberOfMaximumBanditPartiesAroundEachHideout;
            }
            else
            {
                __result = 0;
            }
        }

        // 获取每个盗匪据点的最大盗匪部队数量
        [HarmonyPostfix]
        [HarmonyPatch(typeof(TORConfig), "get_NumberOfMaximumBanditPartiesInEachHideout")]
        static void Postfix_NumberOfMaximumBanditPartiesInEachHideout(ref int __result)
        {
            var globalSettings = GlobalSettings<MCMSetting>.Instance;
            if (globalSettings != null)
            {
                __result = globalSettings.Yuef_NumberOfMaximumBanditPartiesInEachHideout;
            }
            else
            {
                __result = 0;
            }
        }

        // 获取最小和平天数
        [HarmonyPostfix]
        [HarmonyPatch(typeof(TORConfig), "get_MinPeaceDays")]
        static void Postfix_MinPeaceDays(ref float __result)
        {
            var globalSettings = GlobalSettings<MCMSetting>.Instance;
            if (globalSettings != null)
            {
                __result = globalSettings.Yuef_MinPeaceDays;
            }
            else
            {
                __result = 0f;
            }
        }

        // 获取最小战争天数
        [HarmonyPostfix]
        [HarmonyPatch(typeof(TORConfig), "get_MinWarDays")]
        static void Postfix_MinWarDays(ref float __result)
        {
            var globalSettings = GlobalSettings<MCMSetting>.Instance;
            if (globalSettings != null)
            {
                __result = globalSettings.Yuef_MinWarDays;
            }
            else
            {
                __result = 0f;
            }
        }
    }
}


