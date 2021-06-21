using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Net;
using UnityEngine;

namespace Test
{
    public class SampleSave : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            var user = new User();
            user.id = 333;
            user.value = 100;
            SaveSystem.Save(user);

            user = SaveSystem.Load<User>();
            Debug.Log(user.ToJson());
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }

    [System.Serializable]
    public class User : SaveData
    {
        public int id;
        public int value;
    }

    public class SaveData
    {
        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }
    }

    public static class SaveSystem
    {
        private static string SaveRootPath => string.Intern(Application.dataPath);
        private static string SaveFolder => string.Intern("save");
        private static string Salt => string.Intern("2ijowfnmn");

        public static void Save(SaveData saveData)
        {
            var json = saveData.ToJson();
            var saveRootPath = string.Intern($"{SaveRootPath}/{SaveFolder}");
            if (!System.IO.Directory.Exists(saveRootPath))
            {
                System.IO.Directory.CreateDirectory(saveRootPath);
            }
            var savePath = string.Intern($"{saveRootPath}/save");
            System.IO.File.WriteAllLines(savePath, new[] {json, Hash128.Compute(json + Salt).ToString()});
        }

        public static T Load<T>() where T : SaveData
        {
            var saveRootPath = string.Intern($"{SaveRootPath}/{SaveFolder}");
            var savePath = string.Intern($"{saveRootPath}/save");
            var values = System.IO.File.ReadAllLines(savePath);
            if (values[1] != Hash128.Compute(values[0] + Salt).ToString())
            {
                return default(T);
            }
            return JsonUtility.FromJson<T>(values[0]);
        }
    }
}