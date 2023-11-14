using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ���ҽ� ����
/// </summary>
namespace GameCore.Resource
{
    /// <summary>
    /// ������ ����, �ı����� �����ϴ� ResourceManagerŬ����
    /// </summary>
    public class ResourceManager
    {
        /// <summary>
        /// resource���� path�ּҰ��� ���ҽ��� �ε��ϴ� Load�Լ�
        /// </summary>
        /// <param name="path">���ҽ��� �ּ�</param>
        /// <typeparam name="T">���ҽ��� Ÿ��</typeparam>
        /// 
        /// <example>
        /// ���ҽ��� Ÿ�Կ� ���� @n
        /// <list>
        ///     <item>
        ///         txt, json������ TextAsset Ÿ��
        ///     </item>
        ///     <item>
        ///         �ؽ��ķ� ����� png������ Texture2D Ÿ��
        ///     </item>
        ///     <item>
        ///         sprite�� ����� png������ Sprite Ÿ��
        ///     </item>
        ///     <item>
        ///         ���� mp3���� ������ AudioClipŸ��
        ///     </item>
        /// </list>
        /// 
        /// ���ҽ��� �ε��ϴ� �����Դϴ�.
        /// <code>
        /// var hitSound = Managers.Resource.Load<AudioClipŸ��>("sound/hit.mp3");
        /// </code>
        /// </example>
        public T Load<T>(string path) where T : Object
        {
            /*������Ʈ ����
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
        /// ������ �ν��Ͻ�(���ӿ�����Ʈ) ���� �Լ�
        /// </summary>
        /// <param name="path">�������� �ּ�, ~Prefabs/���� �κи� </param>
        /// <param name="parent">�������� �θ� ������Ʈ ����</param>
        /// <returns>������ GameObject ��ȯ</returns>
        /// 
        /// <example>
        /// ������ �ν��Ͻ�(���ӿ�����Ʈ)�� �����ϴ� �����Դϴ�.
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
        /// ��ġ ���� �ν��Ͻ�(���ӿ�����Ʈ) ���� �Լ�
        /// </summary>
        /// <param name="path">�������� �ּ�, ~Prefabs/���� �κи� </param>
        /// <param name="vector">������ �ν��Ͻ��� ������ ��ġ</param>
        /// <param name="rotation">������ �ν��Ͻ��� ������ ���� ����(Quaternion.identity�� �Է�)</param>
        /// <param name="parent">�������� �θ� ����</param>
        /// <returns>������ GameObject ��ȯ</returns>
        ///
        /// <example>
        /// ������ �ν��Ͻ�(���ӿ�����Ʈ)�� �����ϴ� �����Դϴ�.\n
        /// Quaternion���� ȸ�������� ��Ÿ���� ���� Quaternion.identity�� �Է��ϵ��� �մϴ�.
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
            /*������Ʈ ����
            if (original.GetComponent<Poolable>() != null)
                return Managers.Pool.Pop(original, parent).gameObject;
            */
            GameObject go = Object.Instantiate(original, vector, rotation, parent);
            go.name = original.name;
            return go;
        }


        /// <summary>
        /// ���� ������Ʈ ���� �Լ�
        /// </summary>
        /// <param name="go">������ ������Ʈ</param>
        /// <returns>
        /// 0 ���� �۵�, 
        /// -1 ���� �߻�, 
        /// 1 ���� �۵�(������Ʈ ���� ����)
        /// </returns>
        ///
        /// <example>
        /// ���ӿ�����Ʈ�� �����ϴ� �����Դϴ�.
        /// <code>
        /// GameObject go = new GameObject("ObjectName");
        /// int i = Managers.Resource.Destroy(go);
        /// switch (i)
        /// {
        ///    case -1:
        ///        Debug.Log("���� �߻�")
        ///        break;
        ///    case 0:
        ///        Debug.Log("���� �۵�")
        ///        break;
        ///    case 1:
        ///        Debug.Log("���� �۵�(������Ʈ ����)")
        ///        break;
        /// }
        /// </code>
        /// �����̸� ���ϰ��� �޾� üũ���� �ʾƵ� �˴ϴ�.
        /// </example>
        public int Destroy(GameObject go)
        {
            if (go == null)
            {
                Debug.Log("�������� �ʴ� ���� ������Ʈ�Դϴ�.");
                return -1;
            }

            /*������Ʈ ����
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