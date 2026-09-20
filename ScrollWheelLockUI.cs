using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace LockHotbarScrollWheel
{
    /// <summary>
	/// This class defines a BuilderToggle that allows players to lock or unlock the hotbar scroll wheel functionality.
	/// </summary>
	public class ScrollWheelLockUI : BuilderToggle
	{
		public static LocalizedText UnlockedText { get; private set; }
		public static LocalizedText LockedText { get; private set; }

		public override void SetStaticDefaults()
        {
            UnlockedText = this.GetLocalization(nameof(UnlockedText));
			LockedText = this.GetLocalization(nameof(LockedText));
        }

		public override string Texture => "LockHotbarScrollWheel/Content/UI/MouseWheelToggle";

		public override bool Active() => true;
		
		public override int NumberOfStates => 2;

		// Replaces the default sound with the "Unlock" sound when the toggle is clicked.
		public override bool OnLeftClick(ref SoundStyle? sound)
		{
			SoundEngine.PlaySound(SoundID.Unlock);
			return true;
		}

		// Displays the current state of the Scroll Wheel lock in the UI. Returns "Unlocked" when CurrentState is 0, 
		// and "Locked" when CurrentState is 1, while hovered.
		public override string DisplayValue() 
		{
			return CurrentState == 0 ? UnlockedText.Value : LockedText.Value;
		}

		// Changes the color of the toggle button based on the current state. Green when unlocked, red when locked.
		public override bool Draw(SpriteBatch spriteBatch, ref BuilderToggleDrawParams drawParams) {
			drawParams.Color = CurrentState == 0 ? Color.Green : Color.Red; // Green when Unlocked, Red when Locked
			return true;
		}
	}
}