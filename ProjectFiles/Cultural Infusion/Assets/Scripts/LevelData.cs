using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Level Data", menuName = "CI/Level Data", order = 1)]
public class LevelData : ScriptableObject
{
    [System.Serializable]
    public class IncludedObject
    {
        public GameObject go;

        [Range(0, 10)]
        public int weight = 0;

        public string Name;
        public string Description;
    }

    public List<IncludedObject> Objects;
}
