using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace SaveSystem
{
    public class SaveManager : MonoBehaviour
    {
        public static SaveManager instance;

        public SaveFile[] SaveFiles;

        public SaveFile CurrentFile;
        
        private void Awake()
        {
            if (instance == null)
                instance = this;
            else if(instance != this)
                Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
            this.ManageSaveData();
        }

        private void ManageSaveData()
        {
            //TODO - fix saving system
            //this.LoadSaveFiles();
            //this.UpsertSaveFile();
        }

        public void ExecuteSave()
        {
            if (this.CurrentFile == null)
            {
                
            }
            
        }


        private SaveFile[] LoadSaveFiles()
        {
            List<SaveFile> returnData;
            if (File.Exists(Application.persistentDataPath + "/saves/" + "savegame.save"))
            {
                
            }
            else
            {
                return null;
            }
            return null;
        }

        private SaveFile UpsertSaveFile()
        {
            SaveFile saveFile = new SaveFile();
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = null;

            if (!Directory.Exists(Application.persistentDataPath + "/saves"))
                Directory.CreateDirectory(Application.persistentDataPath + "/saves");


            //string[] files = Directory.GetFiles(Application.persistentDataPath + "/saves");
            
            if (File.Exists(Application.persistentDataPath + "/saves/savegame.save"))
            {
                file = File.OpenWrite(Application.persistentDataPath + "/saves/savegame.save");
            }
            else
                file = File.Create(Application.persistentDataPath + "/saves/savegame.save");
            
            
            //this.CurrentFile.FileStream = file;
            
            GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>() ;

            string mainJson = string.Empty;
            
            foreach (GameObject go in allObjects)
            {
                Debug.Log(go.name);
                Debug.Log(go.transform.position);
                Debug.Log(go.gameObject.transform.childCount);
                //bf.Serialize(file, go);
            }
            
           //File.WriteAllText(file.Name, mainJson);

            return null;
        }

    }
}