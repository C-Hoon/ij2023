using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLanguageData", menuName = "Language/Language Data")]
public class LanguageData : ScriptableObject
{
    public List<LanguageEntry> entries;

    [System.Serializable]
    public class LanguageEntry
    {
        public string id;
        public string text;
    }

}
