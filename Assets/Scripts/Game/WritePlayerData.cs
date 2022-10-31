using System.IO;
using Newtonsoft.Json;

public static class WritePlayerData
{
    public static void WriteObject(IWrittable objectToWrite, string _fileName)
    {
        var playerJsonData = JsonConvert.SerializeObject(objectToWrite);

        var codedData = CoderDecoder.Shifrovka(playerJsonData, InGameConstants.password);

        byte[] bytesA = System.Text.Encoding.ASCII.GetBytes(codedData);

        File.WriteAllBytesAsync(InGameConstants.dataPath(_fileName), bytesA);
    }

    public static string DecodedString(string _fileName)
    {
        byte[] bytes = File.ReadAllBytes(InGameConstants.dataPath(_fileName));

        string json = System.Text.Encoding.ASCII.GetString(bytes);

        return CoderDecoder.DeShifrovka(json, InGameConstants.password);

        //return JsonConvert.DeserializeObject<IWrittable>(json);
    }
}
