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
        using (ZipFile zip = ZipFile.Read("Assets/saves/Package.zip"))
            {
              foreach (ZipEntry e in zip)
                  {
                e.Extract("Assets/saves1", ExtractExistingFileAction.OverwriteSilently);
            }
            }

    }
        
       public static void Compress()
        {
       
        using (ZipFile zip = new ZipFile())
            {
              zip.AddFile("Assets/saves/base.xml");
              zip.AddFile("Assets/saves/monsters.xml");
             zip.AddFile("Assets/saves/moon.xml");
            zip.AddFile("Assets/saves/outside.xml");
            zip.AddFile("Assets/saves/Player.xml");
            zip.AddFile("Assets/saves/sun.xml");
            zip.AddFile("Assets/saves/turrets.xml");
            zip.AddFile("Assets/saves/world.xml");
            zip.Save("Assets/saves/Package.zip");
           }
    }
    }