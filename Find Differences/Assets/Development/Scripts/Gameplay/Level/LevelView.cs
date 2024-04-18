using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class LevelView : MonoBehaviour
{
    [field: SerializeField] public List<Difference> Difficulties { get; private set; } = new();
    [field: SerializeField] public List<Difference> OriginalDifficulties { get; private set; } = new();

    private UniTaskCompletionSource<bool> _taskCompletion;

    public async UniTask<bool> Init()
    {
        _taskCompletion = new UniTaskCompletionSource<bool>();

        ReleaceDifficulties(Difficulties);
        ReleaceDifficulties(OriginalDifficulties);

        var result = await _taskCompletion.Task;

        return result;
    }

    private void ClickDifficulte(Difference difference)
    {
        int id = difference.ID;

        AnimateDifficultie(Difficulties, id);
        AnimateDifficultie(OriginalDifficulties, id);

        CheckWin();
    }

    private void ReleaceDifficulties(List<Difference> differences)
    {
        int id = 0;
        differences.ForEach(difficultie =>
        {
            difficultie.Init(ClickDifficulte, id);
            id++;
        });
    }

    private void AnimateDifficultie(List<Difference> differences, int id)
    {
        for (int i = 0; i < differences.Count; i++)
        {
            if (differences[i].ID == id)
            {
                differences[i].Animate();
                differences.Remove(differences[i]);
                return;
            }
        }
    }

    private void CheckWin()
    {
        if (Difficulties.Count == 0)
            _taskCompletion.TrySetResult(true);
    }
}
