using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using BepInEx;
using BepInEx.Bootstrap;
using Steamworks.Data;

public class OnLobbyCreatedPatch
{
    static void Postfix(ref Lobby lobby)
    {
        var loadedPlugins = Chainloader.PluginInfos;
        var pluginList = MakeJSONPluginList(loadedPlugins.Values.ToList());

        lobby.SetData("plugins", pluginList);
        LethalSync.Plugin.Log.LogInfo(lobby.GetData("plugins"));
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

        return JsonSerializer.Serialize(plugins);
    }
}
