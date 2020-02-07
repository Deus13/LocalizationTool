
using System;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Text.RegularExpressions;

namespace LocalizationTool.src
{
    internal class Utils
    {
        private const string NAME = "LocalizationTool";
        public static string modsFolder;
        public static string modDataFolder;
        public static string settingsFile = "Localization.json";

        public static List<Localization_def> objList = null;
        public static Dictionary<string,string> keys;
        public static void OnLoad()
        {
            Log("[LocalizationTool] Version " + Assembly.GetExecutingAssembly().GetName().Version);

            modsFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            modDataFolder = Path.Combine(modsFolder, "LocalizationTool");
            keys = new Dictionary<string, string>();

            // settings.AddToModSettings("Remove Clutter Settings");
            // options = settings.setOptions;

            LoadDefinitions();
        }
        internal static void LoadDefinitions()
        {
            string defsPath = Path.Combine(modDataFolder, "definitions");
            string[] defFiles = Directory.GetFiles(defsPath, "*.json");
            Log(defsPath);


            if (defFiles.Length > 0)
            {
                objList = new List<Localization_def>();

                for (int i = 0; i < defFiles.Length; i++)
                {
                    string data = File.ReadAllText(defFiles[i]);
                    List<Localization_def> fileObjs = null;

                    try
                    {
                        fileObjs = FastJson.Deserialize<List<Localization_def>>(data);

                        objList.AddRange(fileObjs);

                        Log(" " + Path.GetFileName(defFiles[i]) + " definitions loaded ");
                    }
                    catch (FormatException e)
                    {
                        Log(" ERROR: " + Path.GetFileName(defFiles[i]) + " incorrectly formatted.");
                    }
                }
                if (objList.Count > 0)
                {
                    foreach(var o in objList)
                    {
                        keys.Add(o.id,o.text);
                    }
                    
                }
            }
        }

        internal static void Log(string message)
        {
            Debug.LogFormat("[" + NAME + "] {0}", message);
        }

        internal static void Log(string message, params object[] parameters)
        {
            string preformattedMessage = string.Format("[" + NAME + "] {0}", message);
            Debug.LogFormat(preformattedMessage, parameters);
        }
    }
}
