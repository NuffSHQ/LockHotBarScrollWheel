using Terraria.ModLoader;

namespace LockHotbarScrollWheel
{
	/// <summary>
    /// Add keybind to toggle the hotbar scroll wheel lock functionality.
    /// </summary>
	public class KeybindSystem : ModSystem
	{
		public static ModKeybind ToggleScrollWheelLockKeybind { get; private set; }

		public override void Load() 
        {
			ToggleScrollWheelLockKeybind = KeybindLoader.RegisterKeybind(Mod, "ToggleScrollWheelLock", "L");
		}

		public override void Unload() 
        {
			ToggleScrollWheelLockKeybind = null;
		}
	}
}