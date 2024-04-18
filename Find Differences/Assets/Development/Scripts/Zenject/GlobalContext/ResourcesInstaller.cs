using UnityEngine;
using Zenject;

public class ResourcesInstaller : MonoInstaller
{
    [SerializeField] private LevelDatabase _levelDatabase;

    public override void InstallBindings()
    {
        BindLevelFactory();
    }

    private void BindLevelFactory()
    {
        Container.BindInstances(_levelDatabase);
    }
}
