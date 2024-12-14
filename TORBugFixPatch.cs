using HarmonyLib;

using MCM.Abstractions.Base.Global;

using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

using TOR_Core.CharacterDevelopment;
using TOR_Core.Extensions;
using TOR_Core.Models;


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
            if(!GlobalSettings<MCMSetting>.Instance.Yuef_BUGFix_PiercingShots)return;
            
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

    }
}
