using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Localization.Settings;

using DaggerfallWorkshop.Game;
using DaggerfallWorkshop.Game.Utility.ModSupport;

namespace RoleplayRealismRu
{
    internal class RuRoleplayRealismLoader : MonoBehaviour
    {
        private static Mod mod;
        static Dictionary<string, string> textDataBase = null;

        [Invoke(StateManager.StateTypes.Start, 0)]
        public static void Init(InitParams initParams)
        {
            mod = initParams.Mod;

            var go = new GameObject(mod.Title);
            go.AddComponent<RuRoleplayRealismLoader>();
        }

        void Awake()
        {
            LoadTextData();

            mod.IsReady = true;
        }

        static void LoadTextData()
        {
            string FortLocationID = "51938";
            string FortName = null;
            const string csvFilename = "RoleplayRealismModData.csv";

            if (textDataBase == null)
            {
                textDataBase = StringTableCSVParser.LoadDictionary(csvFilename);
            }

            if (textDataBase.ContainsKey(FortLocationID))
            {
                FortName = textDataBase[FortLocationID];
            }

            if (FortName == null)
            {
                return;
            }

            var locationsStrings = LocalizationSettings.StringDatabase.GetTable(TextManager.Instance.runtimeLocationsStrings);

            if (locationsStrings == null)
            {
                return;
            }

            locationsStrings.AddEntry(FortLocationID, FortName);

            return;
        }
    }
}
