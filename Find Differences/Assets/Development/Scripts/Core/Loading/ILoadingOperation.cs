using System;
using Cysharp.Threading.Tasks;

public interface ILoadingOperation
{
    string Description { get; }

    UniTask Load(Action<float> onProgress);
}
