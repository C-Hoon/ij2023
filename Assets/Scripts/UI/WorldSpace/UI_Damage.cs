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
            // ���� �ö󰡴� ȿ���� �����մϴ�.
            Vector3 moveDirection = transform.up * currentSpeed * Time.deltaTime;
            transform.Translate(moveDirection);

            // �̵� �ӵ��� ���� ���Դϴ�.
            currentSpeed = Mathf.Lerp(initialSpeed, minSpeed, elapsedTime / destroyTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // UI ��Ҹ� �����մϴ�.
        Destroy(gameObject);
    }
}
