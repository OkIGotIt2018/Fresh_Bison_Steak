using BepInEx;
using BepInEx.Configuration;
using RoR2;
using System.IO;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using UnityEngine;

[module: UnverifiableCode]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]

namespace Fresh_Bison_Steak
{
    [BepInPlugin("com.OkIGotIt.Fresh_Bison_Steak", "Fresh_Bison_Steak", "1.0.1")]
    public class Fresh_Bison_Steak : BaseUnityPlugin
    {
        public void Awake()
        {
            On.RoR2.GlobalEventManager.OnCharacterDeath += GlobalEventManager_OnCharacterDeath;
        }

        private void GlobalEventManager_OnCharacterDeath(On.RoR2.GlobalEventManager.orig_OnCharacterDeath orig, GlobalEventManager self, DamageReport damageReport)
        {
            orig(self, damageReport);
            if (!damageReport.attacker || !damageReport.attackerBody)
                return;
            CharacterBody body = PlayerCharacterMasterController.instances[0].master.GetBody();
            CharacterBody attacker = damageReport.attackerBody;
            if (body != attacker)
                return;
            int itemCount = attacker.inventory.GetItemCount(ItemCatalog.FindItemIndex("FlatHealth"));
            if (itemCount > 0)
                attacker.AddTimedBuff(RoR2Content.Buffs.MeatRegenBoost, 3f * (float)itemCount);
        }
    }
}
