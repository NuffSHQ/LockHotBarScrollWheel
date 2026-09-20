using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace LockHotbarScrollWheel
{
    /// <summary>
    /// Adds a configuration class for the LockHotbarScrollWheel mod, allowing players to 
    /// customize the behavior of the hotbar scroll wheel lock functionality based on different game events.
    /// </summary>
    public class LockHotBarScrollWheelConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;
        
        [DefaultValue(true)]
        public bool LockDuringNonBossEvents { get; set; }

        [DefaultValue(true)]
        public bool LockDuringEvents { get; set; }

        [DefaultValue(true)]
        public bool LockDuringBossBattles { get; set; }
    }
}