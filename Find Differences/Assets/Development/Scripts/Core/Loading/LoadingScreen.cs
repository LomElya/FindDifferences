using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Canvas))]
public class LoadingScreen : MonoBehaviour
{

    [SerializeField] private Canvas _canvas;
    [SerializeField] private Slider _progressFill;
    [SerializeField] private TMP_Text _loadingInfo;
    [SerializeField] private float _barSpeed;

    private Camera _mainCamera => Camera.main;

    private float _targetProgress;

    private void OnValidate()
    {
        if (_canvas == null)
            _canvas = GetComponent<Canvas>();
    }

    public async UniTask Load(Queue<ILoadingOperation> loadingOperations)
    {
        _canvas.worldCamera = _mainCamera;
        _canvas.enabled = true;
        StartCoroutine(UpdateProgressBar());

        foreach (var operation in loadingOperations)
        {
            ResetFill();
            _loadingInfo.text = operation.Description;

            await operation.Load(OnProgress);
            await WaitForBarFill();
        }
    }

    private void ResetFill()
    {
        _progressFill.value = 0;
        _targetProgress = 0;
    }

    private void OnProgress(float progress)
    {
        _targetProgress = progress;
    }

    private async UniTask WaitForBarFill()
    {
        while (_progressFill.value < _targetProgress)
            await UniTask.Yield();

        await UniTask.Delay(TimeSpan.FromSeconds(0.15f));
    }

    private IEnumerator UpdateProgressBar()
    {
        while (_canvas.enabled)
        {
            if (_progressFill.value < _targetProgress)
                _progressFill.value += Time.deltaTime * _barSpeed;

            yield return null;
        }
    }
}
