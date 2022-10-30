using System.IO;
using Newtonsoft.Json;

public class WritePlayerData
{
    public WritePlayerData(IWrittable objectToWrite)
    {
        var playerJsonData = JsonConvert.SerializeObject(objectToWrite);

        var codedData = CoderDecoder.Shifrovka(playerJsonData, InGameConstants.password);

        byte[] bytesA = System.Text.Encoding.ASCII.GetBytes(codedData);

        File.WriteAllBytesAsync(InGameConstants.dataPath("Upgrades.json"), bytesA);
    }
}
