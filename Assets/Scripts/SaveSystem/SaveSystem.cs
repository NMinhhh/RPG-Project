using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveSystem 
{
    public static readonly string SaveFolder = Application.persistentDataPath + "/Saves/";
    public static void Initilize()
    {
        if (!Directory.Exists(SaveFolder))
        {
            Directory.CreateDirectory(SaveFolder);
        }
    }

    public static void SaveFile(string filename, string saveString)
    {
        File.WriteAllText(SaveFolder + filename + ".txt", saveString);
    }

    public static string LoadFile(string filename)
    {
        string file = SaveFolder + filename + ".txt";
        if (File.Exists(file))
        {
            string saveFile = File.ReadAllText(file);
            return saveFile;
        }
        return null;

    }
}
