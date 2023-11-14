using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundFollow : MonoBehaviour
{
    public float smoothSpeed = 5f; // 따라가는 속도 조절

    private void LateUpdate()
    {
        if (Camera.main != null)
        {
            /*// 현재 위치에서 대상 오브젝트의 위치로 부드럽게 이동
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
