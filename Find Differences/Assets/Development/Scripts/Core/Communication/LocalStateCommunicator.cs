using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

public sealed class LocalStateCommunicator : IStateCommunicator
{
    private const string FileName = "playerState";
    private const string SaveFileExtension = ".json";

    private string SavePath => Application.persistentDataPath;
    private string FullPath => Path.Combine(SavePath, $"{FileName}{SaveFileExtension}");

    public UniTask<bool> SaveState(PlayerState playerState)
    {
        File.WriteAllText(FullPath, JsonConvert.SerializeObject(playerState, Formatting.Indented, new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        }));

        return UniTask.FromResult(true);
    }

    public async UniTask<PlayerState> GetState()
    {
        PlayerState result = new PlayerState();

        if (IsDataAlreadyExist())
            result = JsonConvert.DeserializeObject<PlayerState>(ReadAllText());

        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));

        return result;
    }

    public bool IsDataAlreadyExist() => File.Exists(FullPath);

    private string ReadAllText() => File.ReadAllText(FullPath);
}