using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace LethalSync
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        internal static new ManualLogSource Log;

        private void Awake()
        {
            Log = base.Logger;
            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");

            var harmony = new Harmony("com.lethalsync");
            harmony.PatchAll();
        }
    }
}
