using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector))]
public class GameResultIntroAnimation : MonoBehaviour
{
    [SerializeField] private List<GameResultSetting> _settings;

    private PlayableDirector _director;
    private UniTaskCompletionSource<bool> _playAwater;

    private void Awake() => _director = GetComponent<PlayableDirector>();

    public async UniTask Play(GameResultType result)
    {
        foreach (var s in _settings)
        {
            foreach (var obj in s.Objects)
                obj.SetActive(false);
        }

        foreach (var s in _settings)
            if (s.Type == result)
                foreach (var obj in s.Objects)
                    obj.SetActive(true);


        _playAwater = new UniTaskCompletionSource<bool>();
        _director.stopped -= OnTimelineFinished;
        _director.stopped += OnTimelineFinished;

        _director.Play();
        await _playAwater.Task;
    }

    private void OnTimelineFinished(PlayableDirector _) => _playAwater.TrySetResult(true);

    [Serializable]
    private class GameResultSetting
    {
        [field: SerializeField] public GameResultType Type { get; private set; }
        [field: SerializeField] public List<GameObject> Objects { get; private set; }
    }
}
