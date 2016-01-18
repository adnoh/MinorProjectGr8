using System.Collections;
using System;
using System.IO;
using UnityEngine;
using Ionic.Zip;



    public class Zippingscript : MonoBehaviour
    {

    void Start()
    {
       // Compress();
      //  Decompress();

    }


    public static void Decompress()
    {
        using (ZipFile zip = ZipFile.Read(Application.dataPath+ "/saves/Package.zip"))
            {
              foreach (ZipEntry e in zip)
                  {
                e.Extract(Application.dataPath + "/saves/Package.zip", ExtractExistingFileAction.OverwriteSilently);
            }
            }

    }
        
       public static void Compress()
        {
       
        using (ZipFile zip = new ZipFile())
            {
              zip.AddFile(Application.dataPath + "/saves/base.xml","");
              zip.AddFile(Application.dataPath + "/saves/monsters.xml", "");
             zip.AddFile(Application.dataPath + "/saves/moon.xml", "");
            zip.AddFile(Application.dataPath + "/saves/outside.xml", "");
            zip.AddFile(Application.dataPath + "/saves/Player.xml", "");
            zip.AddFile(Application.dataPath + "/saves/sun.xml", "");
            zip.AddFile(Application.dataPath + "/saves/turrets.xml", "");
            zip.AddFile(Application.dataPath + "/saves/world.xml", "");
            zip.Save(Application.dataPath + "/saves/Package.zip");
        }
    }
    }