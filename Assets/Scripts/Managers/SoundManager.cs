using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ���ҽ� ����
/// </summary>
namespace GameCore.Resource
{
    /// <summary>
    /// ���� ����� �����ϴ� SoundManagerŬ����
    /// </summary>
    public class SoundManager
    {
        AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount];
        Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

        public void Init()
        {
            GameObject _sound = GameObject.Find("@Sound");
            if (_sound == null)
            {
                _sound = new GameObject("@Sound");
                Object.DontDestroyOnLoad(_sound);

                string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));
                for (int i = 0; i < soundNames.Length - 1; i++)
                {
                    GameObject go = new GameObject { name = soundNames[i] };
                    _audioSources[i] = go.AddComponent<AudioSource>();
                    go.transform.parent = _sound.transform;
                }

                _audioSources[(int)Define.Sound.Bgm].loop = true;
            }
        }
        public void Clear()
        {
            foreach (AudioSource audioSource in _audioSources)
            {
                audioSource.clip = null;
                audioSource.Stop();
            }
            _audioClips.Clear();
        }

        /// <summary>
        /// audioClip�� �Ű������� �ϴ� ���� ��� Play�Լ�
        /// </summary>
        /// <param name="audioClip">AudioClip������ ������ ��������</param>
        /// <param name="type">Define.Sound�� ��������, ��������� Define.Sound.BGM, ����Ʈ����� Define.Sound.Effect</param>
        /// <param name="pitch">������ ����ӵ�, ����Ʈ�� 1.0f</param>
        /// 
        /// <example>
        /// ���带 ����ϴ� �����Դϴ�.
        /// <code>
        /// //��������� ����ϱ� ����
        /// AudioClip BGM;
        /// Managers.Sound.Play(BGM, Define.Sound.Bgm);
        /// //�Ǵ� ����Ʈ���带 ����ϱ� ����
        /// AudioClip Effect;
        /// Managers.Sound.Play(Effect);
        /// </code>
        /// </example>
        public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
        {
            if (audioClip == null)
                return;

            if (type == Define.Sound.Bgm) // BGM ������� ���
            {
                AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];
                if (audioSource.isPlaying)
                    audioSource.Stop();

                audioSource.pitch = pitch;
                audioSource.clip = audioClip;
                audioSource.Play();
            }
            else // Effect ȿ���� ���
            {
                AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
                audioSource.pitch = pitch;
                audioSource.PlayOneShot(audioClip);
            }
        }

        /// <summary>
        /// audioClip������ �ּҸ� �Ű������� �ϴ� ���� ��� Play�Լ�
        /// </summary>
        /// <param name="path">AudioClip������ �ּ�, Assets/Resources/Sounds/���� �ּҸ� �ۼ�</param>
        /// <param name="type">Define.Sound�� ��������, ��������� Define.Sound.BGM, ����Ʈ����� Define.Sound.Effect, ����Ʈ���� ����Ʈ�����Դϴ�.</param>
        /// <param name="pitch">������ ����ӵ�, ����Ʈ�� 1.0f</param>
        /// 
        /// <example>
        /// ���带 ����ϴ� �����Դϴ�.
        /// <code>
        /// //��������� ����ϱ� ����
        /// Managers.Sound.Play("BGM/BGM_01", Define.Sound.Bgm);
        /// //�Ǵ� ����Ʈ���带 ����ϱ� ����
        /// Managers.Sound.Play("Effect/Effect_01");
        /// </code>
        /// </example>
        public void Play(string path, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
        {
            AudioClip audioClip = GetOrAddAudioClip(path, type);
            Play(audioClip, type, pitch);
        }


        AudioClip GetOrAddAudioClip(string path, Define.Sound type = Define.Sound.Effect)
        {
            if (path.Contains("Sounds/") == false)
                path = $"Sounds/{path}"; // Sound ���� �ȿ� ����

            AudioClip audioClip = null;

            if (type == Define.Sound.Bgm) // BGM ������� Ŭ�� ���̱�
            {
                audioClip = Managers.Resource.Load<AudioClip>(path);
            }
            else // Effect ȿ���� Ŭ�� ���̱�
            {
                if (_audioClips.TryGetValue(path, out audioClip) == false)
                {
                    audioClip = Managers.Resource.Load<AudioClip>(path);
                    _audioClips.Add(path, audioClip);
                }
            }

            if (audioClip == null)
                Debug.Log($"AudioClip Missing ! {path}");

            return audioClip;
        }

        public void SetVolume(float value)
        {
            _audioSources[(int)Define.Sound.Bgm].volume = value;
        }
    }
}