using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Level Data", menuName = "CI/Level Data", order = 1)]
public class LevelData : ScriptableObject {

    public int LevelNumber = 0;

    [System.Serializable]
    public class IncludedObject
    {
        public GameObject prefab;

        [Range(0, 100)]
        public int count = 0;
    }

    public List<IncludedObject> Objects;

}
