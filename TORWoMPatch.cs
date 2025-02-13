using TOR_Core.Models;
using TaleWorlds.TwoDimension;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using MCM.Abstractions.Base.Global;
using Helpers;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Localization;
using TOR_Core.CampaignMechanics.CustomResources;
using TOR_Core.CharacterDevelopment.CareerSystem;
using TOR_Core.CharacterDevelopment;
using TOR_Core.Extensions;
using TOR_Core.Utilities;
namespace YuefTORSetting
{
    [Harmony]
    internal class TORWoMPatch
    {
        //魔法之风上限加成
        [HarmonyPostfix]
        [HarmonyPatch(typeof(TORAbilityModel), "GetMaximumWindsOfMagic")]
        static void Postfix_GetMaximumWindsOfMagic(CharacterObject baseCharacter, ref float __result)
        {
            // 提前获取 HeroObject 和 PartyBelongedTo 以减少重复的访问
            var heroObject = baseCharacter?.HeroObject;
            var party = heroObject?.PartyBelongedTo;

            // 检查是否有效并且属于主英雄或属于主英雄的队伍
            if (heroObject != null &&
                (heroObject == Hero.MainHero || (party != null && party.IsMainParty && Hero.MainHero != null)))
            {
                __result += GlobalSettings<MCMSetting>.Instance.Yuef_WOM_ValueToAdd;
            }
        }

        [HarmonyPatch(typeof(TORAbilityModel), "GetWindsRechargeRate")]
        [HarmonyPrefix]
        public static bool Prefix_GetWindsRechargeRate(CharacterObject baseCharacter, ref float __result)
        {
            // 基本的 null 和占位符检查
            if (baseCharacter?.HeroObject == null)
                return true; // 继续调用原始方法

            // 如果角色是一个施法者（SpellCaster）且为领主（Lord），并且不是主角Hero.MainHero，执行原始方法
            if (baseCharacter.HeroObject != Hero.MainHero && baseCharacter.HeroObject.Occupation == Occupation.Lord && baseCharacter.HeroObject.IsSpellCaster())
            {
                return true; // 继续调用原始方法
            }

            // 创建ExplainedNumber对象，用于存储计算后的Winds Recharge Rate，初始值为1
            ExplainedNumber explainedNumber = new ExplainedNumber(1f, false, null);

            // 计算技能加成
            SkillHelper.AddSkillBonusForCharacter(TORSkills.SpellCraft, TORSkillEffects.WindsRechargeRate, baseCharacter, ref explainedNumber, -1, true, 0);

            // 判断是否属于主派系
            if (baseCharacter.HeroObject.PartyBelongedTo?.IsMainParty == true)
            {
                // Cult of Isha 祝福加成
                if (MobileParty.MainParty.HasAnyActiveBlessing() && MobileParty.MainParty.HasBlessing("cult_of_isha"))
                {
                    explainedNumber.AddFactor(0.25f, null);
                }

                // 职业被动加成
                CareerHelper.ApplyBasicCareerPassives(baseCharacter.HeroObject, ref explainedNumber, PassiveEffectType.WindsRegeneration, false, null);

                // 装备负重对恢复速率的影响
                float weightPenalty = baseCharacter.Equipment.GetTotalWeightOfArmor(true) / 25f;
                weightPenalty = Mathf.Min(weightPenalty, 0.85f); // 限制最大负重惩罚为0.85

                // 处理特定职业/全局设置的负重免除
                if (Hero.MainHero.HasCareerChoice("ArkaynePassive1") || GlobalSettings<MCMSetting>.Instance.Yuef_RemoveArmorWeightPenalty)
                {
                    weightPenalty = 0f; // 免除负重影响
                }

                // 应用负重对恢复速率的影响
                explainedNumber.AddFactor(-weightPenalty, null);
            }

            // 如果角色的文化为Battania
            if (baseCharacter.Culture.StringId == "battania" && (baseCharacter.HeroObject.PartyBelongedTo?.IsMainParty == true || baseCharacter.HeroObject == Hero.MainHero))
            {
                // 森林和谐等级影响
                if (!Hero.MainHero.HasAttribute("WEWandererSymbol"))
                {
                    var harmonyLevel = Hero.MainHero.GetForestHarmonyLevel();
                    float harmonyDebuff = harmonyLevel switch
                    {
                        ForestHarmonyLevel.Unbound => ForestHarmonyHelper.WindsDebuffUnbound,
                        ForestHarmonyLevel.Bound => ForestHarmonyHelper.WindsDebuffBound,
                        _ => 0f
                    };
                    explainedNumber.AddFactor(harmonyDebuff, new TextObject(harmonyLevel.ToString(), null));
                }

                // WEArielSymbol符号与Oak of the Ages加成
                if (Hero.MainHero.HasAttribute("WEArielSymbol"))
                {
                    var nearestOak = TORCommon.FindNearestSettlement(MobileParty.MainParty, 500f, x => x.IsOakOfTheAges());
                    if (nearestOak != null)
                    {
                        explainedNumber.AddFactor(1f, ForestHarmonyHelper.TreeSymbolText("WEArielSymbol"));
                    }
                }
            }

            // 添加全局设置中的恢复速率加成
            if (baseCharacter.HeroObject.PartyBelongedTo?.IsMainParty == true)
            {
                explainedNumber.AddFactor(GlobalSettings<MCMSetting>.Instance.Yuef_WOM_RechargeSpeedBonus, null);
            }

            // 计算最终恢复速率并赋值给__result
            __result = explainedNumber.ResultNumber;

            // 阻止原方法的执行
            return false;
        }

    }
}
