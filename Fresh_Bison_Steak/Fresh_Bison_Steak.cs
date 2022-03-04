using BepInEx;
using RoR2;
using System.Security;
using System.Security.Permissions;

[module: UnverifiableCode]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]

namespace Fresh_Bison_Steak
{
    [BepInPlugin("com.OkIGotIt.Fresh_Bison_Steak", "Fresh_Bison_Steak", "1.0.4")]
    public class Fresh_Bison_Steak : BaseUnityPlugin
    {
        public void Awake()
        {
            GlobalEventManager.onCharacterDeathGlobal += GlobalEventManager_onCharacterDeathGlobal;
        }

        private void GlobalEventManager_onCharacterDeathGlobal(DamageReport report)
        {
            if (!report.attacker || !report.attackerBody)
                return;
            int itemCount = report.attackerBody.inventory.GetItemCount(ItemCatalog.FindItemIndex("FlatHealth"));
            if (itemCount > 0)
                report.attackerBody.AddTimedBuff(JunkContent.Buffs.MeatRegenBoost, 3f * (float)itemCount);
        }
    }
}
