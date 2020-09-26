using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace AmberKO.Manager
{
    public class SerializeManager
    {

        //Should be called in case MONO_REFLECTION_SERIALIZER should be active.
        public static void Init()
        {
            System.Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
        }

        public static void Save(string fileName, string data)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(fileName, FileMode.Create)))
            {
                writer.Write(data);
            }
        }


        public static string Load(string fileName)
        {
            if (File.Exists(fileName))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
                {
                    string data = reader.ReadString();
                    return data;
                }
            } else
            {
                File.Create(fileName);
                return null;
            }

        }

        public static void DeleteFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }

    }
}