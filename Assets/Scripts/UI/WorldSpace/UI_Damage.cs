using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Damage : MonoBehaviour
{
    public TextMeshProUGUI damageText;
    public float initialSpeed = 5.0f;
    public float minSpeed = 1.0f;
    public float destroyTime = 1.0f;

    public void Init(int damage, bool isCritical)
    {
        damageText.enabled = true;
        damageText.text = damage.ToString();
        Debug.Log($"isCritical : {isCritical}");
        if(isCritical)
        {
            //damageText.color = Color.red;
            //damageText.fontStyle = FontStyles.Bold;
            damageText.text = "<b><color=\"red\">" + damage + "</color></b>";
        }
        /*else
        {
            damageText.color = Color.white;
            damageText.fontStyle = FontStyles.Normal;
        }*/


        StartCoroutine(MoveAndDestroy());
    }

    IEnumerator MoveAndDestroy()
    {
        float elapsedTime = 0;
        float currentSpeed = initialSpeed;

        while (elapsedTime < destroyTime)
        {
            // 위로 올라가는 효과를 생성합니다.
            Vector3 moveDirection = transform.up * currentSpeed * Time.deltaTime;
            transform.Translate(moveDirection);

            // 이동 속도를 점점 줄입니다.
            currentSpeed = Mathf.Lerp(initialSpeed, minSpeed, elapsedTime / destroyTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // UI 요소를 제거합니다.
        Destroy(gameObject);
    }
}
