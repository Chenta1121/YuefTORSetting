
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

        [HarmonyPostfix]
        [HarmonyPatch(typeof(TORConfig), "get_MaximumNumberOfCareerPerkPoints")]
        static void Postfix_MaximumNumberOfCareerPerkPoints(ref int __result)
        {

            __result = GlobalSettings<MCMSetting>.Instance.Yuef_MaximumNumberOfCareerPerkPoints;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(TORConfig), "get_MaximumCustomResourceValue")]
        static void Postfix_MaximumCustomResourceValue(ref int __result)
        {

            __result = GlobalSettings<MCMSetting>.Instance.Yuef_MaximumCustomResourceValue;
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(TORCustomResourceModel), "GetCultureSpecificCustomResourceChange")]
        static void Postfix_GetCultureSpecificCustomResourceChange(ref ExplainedNumber __result)
        {
            if (__result.ResultNumber < 0f)
            {
                // 应用减免逻辑：计算减免的值
                float costReductionAmount = - __result.ResultNumber * GlobalSettings<MCMSetting>.Instance.Yuef_CustomResource_DailyCostReduction / 100f;

                // 应用结果：将减免的值加到结果中，并给出描述
                __result.Add((float)((int)costReductionAmount), new TextObject("消耗减免", null), null);
            }
            else
            {
                // 应用倍率逻辑：计算额外获取的值
                float resourceGainAmount = __result.ResultNumber * (GlobalSettings<MCMSetting>.Instance.Yuef_CustomResource_DailyMultiplier / 100f - 1f);

                // 应用结果：将额外获取的值加到结果中，并给出描述
                __result.Add((float)((int)resourceGainAmount), new TextObject("获取倍率", null), null);
            }
        }


        //兵种升阶减免
        [HarmonyPostfix]
        [HarmonyPatch(typeof(CharacterObjectExtensions), "GetCustomResourceRequiredForUpgrade")]
        static void Postfix_GetCustomResourceRequiredForUpgrade(ref Tuple<CustomResource, int> __result)
        {
            // 获取减免倍率
            float upgradeCostReduction = GlobalSettings<MCMSetting>.Instance.Yuef_CustomResource_UpgradeCostReduction;

            // 确保 __result 不为空，并且包含有效的第二项（升级资源成本）
            if (__result != null && __result.Item2 > 0)
            {
                // 修改第二项的 int 值（升级资源成本），应用减免倍率
                int reducedCost = (int)(__result.Item2 *(1- upgradeCostReduction/100));

                // 确保修改后的成本不为负数
                if (reducedCost < 0)
                {
                    reducedCost = 0;  // 将成本设置为0，或者根据需求设置为其他最小值
                }

                // 更新 __result，将修改后的成本存回
                __result = new Tuple<CustomResource, int>(__result.Item1, reducedCost);
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(CharacterObjectExtensions), "GetCustomResourceRequiredForUpkeep")]
        static void Postfix_GetCustomResourceRequiredForUpkeep(ref Tuple<CustomResource, int> __result)
        {
            // 获取减免倍率
            float KeepCostReduction = GlobalSettings<MCMSetting>.Instance.Yuef_CustomResource_KeepCostReduction;

            // 确保 __result 不为空，并且包含有效的第二项（维持资源成本）
            if (__result != null && __result.Item2 > 0)
            {
                // 修改第二项的 int 值（维持资源成本），应用减免倍率
                int reducedCost = (int)(__result.Item2 *(1 - KeepCostReduction/100));

                // 确保修改后的成本不为负数
                if (reducedCost < 0)
                {
                    reducedCost = 0;
                }
                // 更新 __result，将修改后的成本存回
                __result = new Tuple<CustomResource, int>(__result.Item1, reducedCost);
            }
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(TORConfig), "get_NumberOfInitialHideoutsAtEachBanditFaction")]
        static void Postfix_NumberOfInitialHideoutsAtEachBanditFaction(ref int __result)
        {

            __result = GlobalSettings<MCMSetting>.Instance.Yuef_NumberOfInitialHideoutsAtEachBanditFaction;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(TORConfig), "get_NumberOfMaximumHideoutsAtEachBanditFaction")]
        static void Postfix_NumberOfMaximumHideoutsAtEachBanditFaction(ref int __result)
        {

            __result = GlobalSettings<MCMSetting>.Instance.Yuef_NumberOfMaximumHideoutsAtEachBanditFaction;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(TORConfig), "get_NumberOfMaximumBanditPartiesAroundEachHideout")]
        static void Postfix_NumberOfMaximumBanditPartiesAroundEachHideout(ref int __result)
        {

            __result = GlobalSettings<MCMSetting>.Instance.Yuef_NumberOfMaximumBanditPartiesAroundEachHideout;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(TORConfig), "get_NumberOfMaximumBanditPartiesInEachHideout")]
        static void Postfix_NumberOfMaximumBanditPartiesInEachHideout(ref int __result)
        {

            __result = GlobalSettings<MCMSetting>.Instance.Yuef_NumberOfMaximumBanditPartiesInEachHideout;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(TORConfig), "get_MinPeaceDays")]
        static void Postfix_MinPeaceDays(ref float __result)
        {

            __result = GlobalSettings<MCMSetting>.Instance.Yuef_MinPeaceDays;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(TORConfig), "get_MinWarDays")]
        static void Postfix_MinWarDays(ref float __result)
        {
            __result = GlobalSettings<MCMSetting>.Instance.Yuef_MinWarDays;
        }

    }
}

