using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundFollow : MonoBehaviour
{
    public float smoothSpeed = 5f; // ���󰡴� �ӵ� ����

    private void LateUpdate()
    {
        if (Camera.main != null)
        {
            /*// ���� ��ġ���� ��� ������Ʈ�� ��ġ�� �ε巴�� �̵�
            Vector3 temp = Camera.main.transform.position;

            Vector3 desiredPosition = new Vector3(temp.x, temp.y, 1);

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;*/

            Vector3 temp = Camera.main.transform.position;

            Vector3 desiredPosition = new Vector3(temp.x, temp.y, 1);
            transform.position = desiredPosition;
        }
    }
}
