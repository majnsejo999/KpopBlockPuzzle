using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace BlockGame.Nova.Conf
{
    public static class DailySpinConfig
    {
        private static int spinRewardCoin = 0;

        private static int spinRewardHintDiscount = 1;

        private static Vector3[] roulettePos = new Vector3[8]
        {
            new Vector3(-75f, 180f),
            new Vector3(-180f, 75f),
            new Vector3(-180f, -75f),
            new Vector3(-75f, -180f),
            new Vector3(75f, -180f),
            new Vector3(180f, -75f),
            new Vector3(180f, 75f),
            new Vector3(75f, 180f)
        };

        private static float[] rouletteRads = new float[8]
        {
            337.5f,
            292.5f,
            247.5f,
            202.5f,
            157.5f,
            112.5f,
            67.5f,
            22.5f
        };

        private static Dictionary<string, int>[] rewards;

        public static int SpinRewardCoin => spinRewardCoin;

        public static int SpinRewardHintDiscount => spinRewardHintDiscount;

        public static float[] RouletteRads => rouletteRads;

        public static Dictionary<string, int>[] Rewards => rewards;

        public static Vector3[] RoulettePos => roulettePos;

        public static void LoadDailySpinConfig()
        {
            string path = "Infos/DailySpinConfig";
            TextAsset textAsset = Resources.Load(path) as TextAsset;
            Dictionary<string, object> dictionary = (Dictionary<string, object>)JsonConvert.DeserializeObject(textAsset.text);
            List<object> list = (List<object>)dictionary["data"];
            rewards = new Dictionary<string, int>[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                Dictionary<string, object> dictionary2 = (Dictionary<string, object>)list[i];
                Dictionary<string, int> dictionary3 = new Dictionary<string, int>();
                foreach (KeyValuePair<string, object> item in dictionary2)
                {
                    string key = item.Key;
                    int value = int.Parse(item.Value.ToString());
                    dictionary3.Add(key, value);
                    rewards[i] = dictionary3;
                }
            }
        }
    }
}
