using Terraria;
using Terraria.ModLoader;
using Terraria.GameInput;
using Terraria.Audio;
using Terraria.ID;
using Microsoft.Xna.Framework;
using LockHotbarScrollWheel.Config;
using LockHotbarScrollWheel.Content.UI;
using LockHotbarScrollWheel.Keybind;

namespace LockHotbarScrollWheel.Content.Players
{
    /// <summary>
    /// This class defines a ModPlayer that handles the logic for locking the hotbar scroll wheel functionality based on the state 
    /// of the player and the ScrollWheelLockUI toggle.
    /// </summary>
	public class LockScrollWheelPlayer : ModPlayer
	{
		private int lastCombatTextIndex = -1;

		// Detects if the keybind for toggling the hotbar scroll wheel lock has been pressed and toggles the state of the ScrollWheelLockUI accordingly
		// while displaying the status above the player via CombatText.
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
			if (KeybindSystem.ToggleScrollWheelLockKeybind.JustPressed)
			{
				var scrollWheelLockUI = ModContent.GetInstance<ScrollWheelLockUI>();
				if (scrollWheelLockUI != null)
				{
					scrollWheelLockUI.CurrentState = (scrollWheelLockUI.CurrentState == 0) ? 1 : 0;
					SoundEngine.PlaySound(SoundID.Unlock);
					
					string msg = scrollWheelLockUI.CombatTextValue();
					
					// Check if the previous text index is active and if so turn it off
            		if (lastCombatTextIndex >= 0 && lastCombatTextIndex < Main.combatText.Length && Main.combatText[lastCombatTextIndex].active)
            		{
            		    Main.combatText[lastCombatTextIndex].active = false;
            		}
					
					// Sets color and displays combat text
					Color combatTextColor = (scrollWheelLockUI.CurrentState == 0) ? new Color(0, 255, 0) : new Color(255, 0, 0);
            		lastCombatTextIndex = CombatText.NewText(Player.getRect(), combatTextColor, msg, dramatic: false);
				}
			}

            base.ProcessTriggers(triggersSet);
        }

		// Focuses on checking if conditions are met to lock the hotbar scroll wheel functionality,
		// if the ScrollWheelLockUI toggle is in the "Locked" state.
		public override void PreUpdate()
		{
			var scrollWheelLockUI = ModContent.GetInstance<ScrollWheelLockUI>();
            var lockBarConfig = ModContent.GetInstance<LockHotBarScrollWheelConfig>();          

            bool isEventActive = Main.bloodMoon || Main.eclipse || 
                                 Main.pumpkinMoon || Main.snowMoon ||
                                 Main.slimeRain || Main.invasionType != 0;
            bool isBossActive = Main.CurrentFrameFlags.AnyActiveBossNPC || NPC.AnyNPCs(NPCID.EaterofWorldsTail) || NPC.AnyNPCs(NPCID.MoonLordCore) ||
								NPC.AnyNPCs(NPCID.DungeonGuardian) || NPC.AnyNPCs(NPCID.TheDestroyer);
            bool isNonBossEventActive = !isEventActive && !isBossActive;

			// If the toggle exists and "CurrentState" is 1 (Locked), wipe out scroll inputs
			if (scrollWheelLockUI != null && scrollWheelLockUI.CurrentState == 1)
			{
                // If any of these UI menus are open, does not run the lock logic
				if (Main.playerInventory || Main.mapFullscreen || Main.LocalPlayer.mouseInterface)
				{
					return;
				}

                if (isEventActive && !lockBarConfig.LockDuringEvents) return; // Ignore lock during events if config is disabled
                if (isBossActive && !lockBarConfig.LockDuringBossBattles) return; // Ignore lock during bosses if config is disabled
                if (isNonBossEventActive && !lockBarConfig.LockDuringNonBossEvents) return; // Ignore lock during peace times if config is disabled

				// Clear mouse scroll values for this frame
				PlayerInput.ScrollWheelDelta = 0;
				PlayerInput.ScrollWheelDeltaForUI = 0;

				// Lock the selected item slot in place so the hotbar can't cycle
				Player.selectedItem = Player.selectedItem;
			}
		}
		NPC npc = Main.npc[0];
		
	}
}