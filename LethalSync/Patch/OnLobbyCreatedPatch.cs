using System.Collections.Generic;
using System.Linq;
using BepInEx;
using BepInEx.Bootstrap;
using HarmonyLib;
using Steamworks.Data;
using Newtonsoft.Json;

[HarmonyPatch(typeof(GameNetworkManager))]
[HarmonyPatch(nameof(GameNetworkManager.SteamMatchmaking_OnLobbyCreated))]
public class OnLobbyCreatedPatch
{
    static void Postfix(ref Lobby lobby)
    {
        try
        {
            var loadedPlugins = Chainloader.PluginInfos;
            var pluginList = MakeJSONPluginList(loadedPlugins.Values.ToList());

            lobby.SetData("plugins", pluginList);
            LethalSync.Plugin.Log.LogInfo(lobby.GetData("plugins"));
        }
        catch (System.Exception error)
        {
            LethalSync.Plugin.Log.LogError(error);
            throw;
        }
    }

    private static string MakeJSONPluginList(List<PluginInfo> pluginInfos)
    {
        List<PlayerPlugin> plugins = new List<PlayerPlugin> { };

        foreach (var pluginInfo in pluginInfos)
        {
            var plugin = pluginInfo.Metadata;
            plugins.Add(new PlayerPlugin
            {
                Id = plugin.GUID,
                Name = plugin.Name,
                Version = plugin.Version.ToString()
            });
        }

        return JsonConvert.SerializeObject(plugins); 
    }
}
