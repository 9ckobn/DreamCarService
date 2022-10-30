using System.IO;
using Newtonsoft.Json;
using UnityEngine;
public class Game
{
    public static IInputService InputService;

    public static ItemPrefabData itemData;
    public static PlayerUpgrades playerUpgradesData;
    public static WishSpritesData wishSpritesData;

    private static string dataPath;

    public Game()
    {
        RegisterInputService();
        InitializeAssets();
    }

    private static void RegisterInputService()
    {
        if (Application.isEditor)
            InputService = new StandaloneInputModule();
        else
            InputService = new MobileInputModule();
    }

    private static void InitializeAssets()
    {
        playerUpgradesData = UnityEditor.AssetDatabase.LoadAssetAtPath(@"Assets/Data/Upgrades.asset", typeof(PlayerUpgrades)) as PlayerUpgrades;
        itemData = UnityEditor.AssetDatabase.LoadAssetAtPath(@"Assets/Data/ItemData.asset", typeof(ItemPrefabData)) as ItemPrefabData;
        wishSpritesData = UnityEditor.AssetDatabase.LoadAssetAtPath(@"Assets/Data/SpriteData.asset", typeof(WishSpritesData)) as WishSpritesData;

        if (playerUpgradesData == null)
        {
            byte[] bytes = File.ReadAllBytes(dataPath);

            string json = System.Text.Encoding.ASCII.GetString(bytes);

            var decodedData = CoderDecoder.DeShifrovka(json, InGameConstants.password);
            Debug.Log(decodedData);

            playerUpgradesData = JsonConvert.DeserializeObject<PlayerUpgrades>(decodedData);
        }

    }

}