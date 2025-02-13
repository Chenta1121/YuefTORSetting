using System;
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
                return "YuefTORSetting";
            }
        }
        public override string DisplayName
        {
            get
            {
                string text = new TextObject("{=YueF_TORSetting_Name}TOR: Settings", null).ToString();
                
                Version version = typeof(MCMSetting).Assembly.GetName().Version;
                return text + ((version != null) ? version.ToString(3) : null);
            }
        }

        public override string FolderName
        {
            get
            {
                return "Yuef_TOR_Setting";
            }
        }

        public override string FormatType
        {
            get
            {
                return "json2";
            }
        }

        [SettingProperty("{=Yuef_MaxCareerPoints}Max Career Perk Points", 0, 40, RequireRestart = false, HintText = "{=Yuef_Hint_MaxCareerPoints}Set the maximum career perk points (35 to max out all skills)", Order = 1)]
        [SettingPropertyGroup("{=Yuef_Group_CareerPoints}Career Perk Points Adjustment", GroupOrder = 0)]
        public int Yuef_MaximumNumberOfCareerPerkPoints { get; set; } = 30;

        [SettingProperty("{=Yuef_MaxCustomResource}Max Custom Resource Value", 0, 100000, RequireRestart = false, HintText = "{=Yuef_Hint_MaxCustomResource}Set the upper limit for custom resource values", Order = 11)]
        [SettingPropertyGroup("{=Yuef_Group_CustomResource}Custom Resource Settings", GroupOrder = 1)]
        public int Yuef_MaximumCustomResourceValue { get; set; } = 2500;

        [SettingProperty("{=Yuef_CustomResourceDailyMult}Custom Resource Daily Multiplier (%)", 0, 1000, RequireRestart = false, HintText = "{=Yuef_Hint_CustomResourceDailyMult}Set the daily percentage multiplier for custom resources (effective when the daily settlement value is positive)", Order = 12)]
        [SettingPropertyGroup("{=Yuef_Group_CustomResource}Custom Resource Settings", GroupOrder = 1)]
        public int Yuef_CustomResource_DailyMultiplier { get; set; } = 100;

        [SettingProperty("{=Yuef_CustomResourceDailyCostRed}Custom Resource Daily Cost Reduction (%)", 0, 100, RequireRestart = false, HintText = "{=Yuef_Hint_CustomResourceDailyCostRed}Set the percentage reduction for daily consumption of custom resources (effective when the daily settlement value is negative)", Order = 13)]
        [SettingPropertyGroup("{=Yuef_Group_CustomResource}Custom Resource Settings", GroupOrder = 1)]
        public int Yuef_CustomResource_DailyCostReduction { get; set; } = 100;

        [SettingProperty("{=Yuef_CustomResourceUpgradeRed}Custom Resource Upgrade Cost Reduction (%)", -100, 100, RequireRestart = false, HintText = "{=Yuef_Hint_CustomResourceUpgradeRed}Set the percentage reduction for resource cost during upgrades (negative values increase cost)", Order = 14)]
        [SettingPropertyGroup("{=Yuef_Group_CustomResource}Custom Resource Settings", GroupOrder = 1)]
        public int Yuef_CustomResource_UpgradeCostReduction { get; set; } = 30;

        [SettingProperty("{=Yuef_CustomResourceKeepRed}Custom Resource Maintenance Cost Reduction (%)", -100, 100, RequireRestart = false, HintText = "{=Yuef_Hint_CustomResourceKeepRed}Set the percentage reduction for maintenance resource costs (negative values increase cost)", Order = 15)]
        [SettingPropertyGroup("{=Yuef_Group_CustomResource}Custom Resource Settings", GroupOrder = 1)]
        public int Yuef_CustomResource_KeepCostReduction { get; set; } = 30;

        [SettingProperty("{=Yuef_WOMMaxAdd}Wind of Magic Max Bonus", 0, 1000, RequireRestart = false, HintText = "{=Yuef_Hint_WOMMaxAdd}Set additional max bonus for Wind of Magic (fixed value)", Order = 16)]
        [SettingPropertyGroup("{=Yuef_Group_WOM}Wind of Magic Settings", GroupOrder = 1)]
        public int Yuef_WOM_ValueToAdd { get; set; } = 0;

        [SettingProperty("{=Yuef_WOMRechargeSpeed}Wind of Magic Recharge Speed Bonus (fixed value)", -10, 10, RequireRestart = false, HintText = "{=Yuef_Hint_WOMRechargeSpeed}Set additional increment for recharge speed of Wind of Magic (negative values slow down)", Order = 17)]
        [SettingPropertyGroup("{=Yuef_Group_WOM}Wind of Magic Settings", GroupOrder = 1)]
        public int Yuef_WOM_RechargeSpeedBonus { get; set; } = 0;

        [SettingProperty("{=Yuef_RemoveArmorPenalty}Remove Armor Weight Penalty", RequireRestart = false, HintText = "{=Yuef_Hint_RemoveArmorPenalty}When selected, the recovery speed of Wind of Magic is no longer affected by armor weight (including NPC companions)", Order = 18)]
        [SettingPropertyGroup("{=Yuef_Group_WOM}Wind of Magic Settings", GroupOrder = 1)]
        public bool Yuef_RemoveArmorWeightPenalty { get; set; } = false;

        [SettingProperty("{=Yuef_BanditFactionInitialHideouts}Initial Hideouts Per Bandit Faction", 1, 50, RequireRestart = false, HintText = "{=Yuef_Hint_BanditFactionInitialHideouts}Set the number of initial hideouts for each bandit faction", Order = 21)]
        [SettingPropertyGroup("{=Yuef_Group_BanditFaction}Custom Bandit Teams", GroupOrder = 2)]
        public int Yuef_NumberOfInitialHideoutsAtEachBanditFaction { get; set; } = 10;

        [SettingProperty("{=Yuef_BanditFactionMaxHideouts}Max Hideouts Per Bandit Faction", 1, 100, RequireRestart = false, HintText = "{=Yuef_Hint_BanditFactionMaxHideouts}Set the maximum number of hideouts for each bandit faction", Order = 22)]
        [SettingPropertyGroup("{=Yuef_Group_BanditFaction}Custom Bandit Teams", GroupOrder = 2)]
        public int Yuef_NumberOfMaximumHideoutsAtEachBanditFaction { get; set; } = 60;

        [SettingProperty("{=Yuef_MaxBanditPartiesAroundHideout}Max Bandit Parties Around Each Hideout", 1, 50, RequireRestart = false, HintText = "{=Yuef_Hint_MaxBanditPartiesAroundHideout}Set the maximum number of bandit parties around each hideout", Order = 23)]
        [SettingPropertyGroup("{=Yuef_Group_BanditFaction}Custom Bandit Teams", GroupOrder = 2)]
        public int Yuef_NumberOfMaximumBanditPartiesAroundEachHideout { get; set; } = 10;

        [SettingProperty("{=Yuef_MaxBanditPartiesInHideout}Max Bandit Parties Inside Each Hideout", 1, 15, RequireRestart = false, HintText = "{=Yuef_Hint_MaxBanditPartiesInHideout}Set the maximum number of bandit parties inside each hideout", Order = 24)]
        [SettingPropertyGroup("{=Yuef_Group_BanditFaction}Custom Bandit Teams", GroupOrder = 2)]
        public int Yuef_NumberOfMaximumBanditPartiesInEachHideout { get; set; } = 3;

        [SettingProperty("{=Yuef_MinPeaceDays}Minimum Peace Days", 1, 50, RequireRestart = false, HintText = "{=Yuef_Hint_MinPeaceDays}Set the minimum number of peace days", Order = 31)]
        [SettingPropertyGroup("{=Yuef_Group_AIWarConfig}AI War Configuration Settings", GroupOrder = 3)]
        public float Yuef_MinPeaceDays { get; set; } = 20;

        [SettingProperty("{=Yuef_MinWarDays}Minimum War Days", 1, 50, RequireRestart = false, HintText = "{=Yuef_Hint_MinWarDays}Set the minimum number of war days", Order = 32)]
        [SettingPropertyGroup("{=Yuef_Group_AIWarConfig}AI War Configuration Settings", GroupOrder = 3)]
        public float Yuef_MinWarDays { get; set; } = 20;

        [SettingProperty("{=Yuef_BUGFix_AICast}AI Casting Targeting Bug Fix", RequireRestart = false, HintText = "{=Yuef_Hint_BUGFix_AICast}Enabling this will remove the infinite targeting of AI mages and impose a targeting distance limit", Order = 41)]
        [SettingPropertyGroup("{=Yuef_Group_TORBugFix}TOR Bug Fixes", GroupOrder = 4)]
        public bool Yuef_BUGFix_AICast { get; set; } = true;

        [SettingProperty("{=Yuef_BUGFix_CastDistance}Max Casting Distance for AI", 0, 500, RequireRestart = false, HintText = "{=Yuef_Hint_BUGFix_CastDistance}Effective when AI Casting Targeting Bug Fix is enabled", Order = 42)]
        [SettingPropertyGroup("{=Yuef_Group_TORBugFix}TOR Bug Fixes", GroupOrder = 4)]
        public float Yuef_BUGFix_CastDistance { get; set; } = 100f;
    }
}

