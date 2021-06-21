using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class SampleSave : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
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

    public class Save
    {
    
    }
}
