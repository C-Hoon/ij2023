using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableTest : MonoBehaviour
{
    LanguageData ld;


    // Start is called before the first frame update
    void Start()
    {
        ld = GameCore.Managers.Resource.Load<LanguageData>("Data/Language/ko_KR/GameUI");


        ld.entries[0].text = "¹Ù²ñ";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
