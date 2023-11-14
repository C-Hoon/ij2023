using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 게임 리소스 관리
/// </summary>
namespace GameCore.Resource
{
    /// <summary>
    /// 프리팹 생성, 파괴등을 관리하는 ResourceManager클래스
    /// </summary>
    public class ResourceManager
    {
        /// <summary>
        /// resource폴더 path주소값의 리소스를 로드하는 Load함수
        /// </summary>
        /// <param name="path">리소스의 주소</param>
        /// <typeparam name="T">리소스의 타입</typeparam>
        /// 
        /// <example>
        /// 리소스의 타입에 대해 @n
        /// <list>
        ///     <item>
        ///         txt, json파일은 TextAsset 타입
        ///     </item>
        ///     <item>
        ///         텍스쳐로 사용할 png파일은 Texture2D 타입
        ///     </item>
        ///     <item>
        ///         sprite로 사용할 png파일은 Sprite 타입
        ///     </item>
        ///     <item>
        ///         사운드 mp3등의 파일은 AudioClip타입
        ///     </item>
        /// </list>
        /// 
        /// 리소스를 로드하는 예시입니다.
        /// <code>
        /// var hitSound = Managers.Resource.Load<AudioClip타입>("sound/hit.mp3");
        /// </code>
        /// </example>
        public T Load<T>(string path) where T : Object
        {
            /*오브젝트 폴링
            if(typeof(T) == typeof(GameObject))
            {
                string name = path;
                int index = name.LastIndexOf('/');
                if (index >= 0)
                    name = name.Substring(index + 1);

                GameObject go = Managers.Pool.GetOriginal(name);
                if (go != null)
                    return go as T;
            }
            */

            return Resources.Load<T>(path);
        }


        /// <summary>
        /// 프리팹 인스턴스(게임오브젝트) 생성 함수
        /// </summary>
        /// <param name="path">프리팹의 주소, ~Prefabs/뒤의 부분만 </param>
        /// <param name="parent">프리팹의 부모 오브젝트 설정</param>
        /// <returns>생성된 GameObject 반환</returns>
        /// 
        /// <example>
        /// 프리팹 인스턴스(게임오브젝트)를 생성하는 예시입니다.
        /// <code>
        /// GameObject go = Managers.Resource.Instantiate("Character/Player");
        /// </code>
        /// </example>
        public GameObject Instantiate(string path, Transform parent = null)
        {
            GameObject original = Load<GameObject>($"Prefabs/{path}");
            if (original == null)
            {
                Debug.Log($"Failed to load prefab : {path}");
                throw new System.Exception($"Failed to load prefab : {path}");
            }
            GameObject go = Object.Instantiate(original, parent);
            go.name = original.name;
            return go;
        }
        public GameObject Instantiate(GameObject stack, Transform parent = null)
        {
            if (stack == null)
            {
                Debug.Log($"Failed to load GameObject");
                return null;
            }
            GameObject go = Object.Instantiate(stack, parent);
            go.name = stack.name;
            return go;
        }

        /// <summary>
        /// 위치 지정 인스턴스(게임오브젝트) 생성 함수
        /// </summary>
        /// <param name="path">프리팹의 주소, ~Prefabs/뒤의 부분만 </param>
        /// <param name="vector">프리팹 인스턴스를 생성할 위치</param>
        /// <param name="rotation">프리팹 인스턴스가 생성될 때의 방향(Quaternion.identity을 입력)</param>
        /// <param name="parent">프리팹의 부모 설정</param>
        /// <returns>생성된 GameObject 반환</returns>
        ///
        /// <example>
        /// 프리팹 인스턴스(게임오브젝트)를 생성하는 예시입니다.\n
        /// Quaternion값은 회전없음을 나타내는 값인 Quaternion.identity을 입력하도록 합니다.
        /// <code>
        /// GameObject go = Managers.Resource.Instantiate("Character/Player", Vector2.zero, Quaternion.identity);
        /// </code>
        /// </example>
        public GameObject Instantiate(string path, Vector2 vector, Quaternion rotation, Transform parent = null)
        {
            GameObject original = Load<GameObject>($"Prefabs/{path}");
            if (original == null)
            {
                Debug.Log($"Failed to load prefab : {path}");
                return null;
            }
            /*오브젝트 폴링
            if (original.GetComponent<Poolable>() != null)
                return Managers.Pool.Pop(original, parent).gameObject;
            */
            GameObject go = Object.Instantiate(original, vector, rotation, parent);
            go.name = original.name;
            return go;
        }


        /// <summary>
        /// 게임 오브젝트 제거 함수
        /// </summary>
        /// <param name="go">제거할 오브젝트</param>
        /// <returns>
        /// 0 정상 작동, 
        /// -1 에러 발생, 
        /// 1 정상 작동(오브젝트 폴링 적용)
        /// </returns>
        ///
        /// <example>
        /// 게임오브젝트를 제거하는 예시입니다.
        /// <code>
        /// GameObject go = new GameObject("ObjectName");
        /// int i = Managers.Resource.Destroy(go);
        /// switch (i)
        /// {
        ///    case -1:
        ///        Debug.Log("에러 발생")
        ///        break;
        ///    case 0:
        ///        Debug.Log("정상 작동")
        ///        break;
        ///    case 1:
        ///        Debug.Log("정상 작동(오브젝트 폴링)")
        ///        break;
        /// }
        /// </code>
        /// 예시이며 리턴값을 받아 체크하지 않아도 됩니다.
        /// </example>
        public int Destroy(GameObject go)
        {
            if (go == null)
            {
                Debug.Log("존재하지 않는 게임 오브젝트입니다.");
                return -1;
            }

            /*오브젝트 폴링
            Poolable poolable = go.GetComponent<Poolable>();
            if (poolable != null)
            {
                Managers.Pool.Push(poolable);
                return 1;
            }
            */

            Object.Destroy(go);
            return 0;
        }
    }
}