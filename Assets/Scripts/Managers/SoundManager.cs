using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 게임 리소스 관리
/// </summary>
namespace GameCore.Resource
{
    /// <summary>
    /// 사운드 출력을 관리하는 SoundManager클래스
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
        /// audioClip을 매개변수로 하는 사운드 재생 Play함수
        /// </summary>
        /// <param name="audioClip">AudioClip데이터 형식의 사운드파일</param>
        /// <param name="type">Define.Sound의 열거형값, 배경음악은 Define.Sound.BGM, 이펙트사운드는 Define.Sound.Effect</param>
        /// <param name="pitch">사운드의 재생속도, 디폴트값 1.0f</param>
        /// 
        /// <example>
        /// 사운드를 재생하는 예시입니다.
        /// <code>
        /// //배경을악을 재생하기 위해
        /// AudioClip BGM;
        /// Managers.Sound.Play(BGM, Define.Sound.Bgm);
        /// //또는 이펙트사운드를 재생하기 위해
        /// AudioClip Effect;
        /// Managers.Sound.Play(Effect);
        /// </code>
        /// </example>
        public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
        {
            if (audioClip == null)
                return;

            if (type == Define.Sound.Bgm) // BGM 배경음악 재생
            {
                AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];
                if (audioSource.isPlaying)
                    audioSource.Stop();

                audioSource.pitch = pitch;
                audioSource.clip = audioClip;
                audioSource.Play();
            }
            else // Effect 효과음 재생
            {
                AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
                audioSource.pitch = pitch;
                audioSource.PlayOneShot(audioClip);
            }
        }

        /// <summary>
        /// audioClip파일의 주소를 매개변수로 하는 사운드 재생 Play함수
        /// </summary>
        /// <param name="path">AudioClip파일의 주소, Assets/Resources/Sounds/뒤의 주소만 작성</param>
        /// <param name="type">Define.Sound의 열거형값, 배경음악은 Define.Sound.BGM, 이펙트사운드는 Define.Sound.Effect, 디폴트값은 이펙트사운드입니다.</param>
        /// <param name="pitch">사운드의 재생속도, 디폴트값 1.0f</param>
        /// 
        /// <example>
        /// 사운드를 재생하는 예시입니다.
        /// <code>
        /// //배경을악을 재생하기 위해
        /// Managers.Sound.Play("BGM/BGM_01", Define.Sound.Bgm);
        /// //또는 이펙트사운드를 재생하기 위해
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
                path = $"Sounds/{path}"; // Sound 폴더 안에 저장

            AudioClip audioClip = null;

            if (type == Define.Sound.Bgm) // BGM 배경음악 클립 붙이기
            {
                audioClip = Managers.Resource.Load<AudioClip>(path);
            }
            else // Effect 효과음 클립 붙이기
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