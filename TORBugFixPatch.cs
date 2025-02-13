using HarmonyLib;
using MCM.Abstractions.Base.Global;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TOR_Core.CharacterDevelopment;
using TOR_Core.Extensions;
using TOR_Core.Models;
using TOR_Core.BattleMechanics.AI.CommonAIFunctions;
using TOR_Core.AbilitySystem;
using TOR_Core.BattleMechanics.AI.CastingAI.Components;


namespace YuefTORSetting
{
    [HarmonyPatch]
    internal class TORBugFixPatch
    {       
        // 使用 Harmony 后置补丁修复 PiercingShots
        [HarmonyPostfix]
        [HarmonyPatch(typeof(TORAgentApplyDamageModel), "DecideMissileWeaponFlags")]
        static void Postfix_DecideMissileWeaponFlags(Agent attackerAgent, MissionWeapon missileWeapon, ref WeaponFlags missileWeaponFlags)
        {
            // 首先检查攻击者是否有领袖角色，且该角色具备 PiercingShots 特性
            var leaderCharacter = attackerAgent.GetPartyLeaderCharacter();
            if (leaderCharacter == null)
            {
                // 如果没有领袖角色，直接返回
                return;
            }

            // 检查当前使用的武器是否是子弹类，并且攻击者的领袖具有 PiercingShots 特性
            if (missileWeapon.CurrentUsageItem.WeaponClass == WeaponClass.Cartridge &&
                leaderCharacter.GetPerkValue(TORPerks.GunPowder.PiercingShots))
            {
                // 如果满足条件，则允许武器穿透护盾
                missileWeaponFlags |= WeaponFlags.CanPenetrateShield;
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Ability), "CanCast")]
        static void Postfix_CanCast(Agent casterAgent, ref bool __result)
        {
            // 判断是否启用修复
            if (GlobalSettings<MCMSetting>.Instance.Yuef_BUGFix_AICast)
            {
                // 获取 WizardAIComponent 组件，确保施法者（casterAgent）包含该组件
                WizardAIComponent wizardAIComponent = casterAgent.GetComponent<WizardAIComponent>();

                // 确保 wizardAIComponent 不为 null 且其当前施法行为（CurrentCastingBehavior）也有效
                if (wizardAIComponent?.CurrentCastingBehavior != null)
                {
                    // 获取当前目标（CurrentTarget），该目标为施法行为的目标
                    Target target = wizardAIComponent.CurrentCastingBehavior.CurrentTarget;

                    // 检查目标是否有效（非 null）
                    if (target != null)
                    {
                        // 使用提供的 Func<Vec3> 获取施法者位置并计算与目标的距离
                        var distanceFunc = CommonAIDecisionFunctions.DistanceToTarget(() => casterAgent.Position);
                        float distanceToTarget = distanceFunc(target);
                        // 处理距离信息
                        if (distanceToTarget > GlobalSettings<MCMSetting>.Instance.Yuef_BUGFix_CastDistance)
                        {
                            __result = false;
                        }
                    }
                }
            }
        }

    }
}

