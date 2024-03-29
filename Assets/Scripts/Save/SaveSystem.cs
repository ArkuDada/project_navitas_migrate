using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveGame(LevelCondition[] lc, int id)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save" + id + ".dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        ProgressData data = new ProgressData(lc);

        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static ProgressData LoadGame(int id)
    {
        string path = Application.persistentDataPath + "/save" + id + ".dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            ProgressData data = (ProgressData)formatter.Deserialize(stream);
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("BRUH no shit");
            return null;
        }
    }
}