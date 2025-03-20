using Zenject;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<InputManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlayerController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlatformObjectPooling>().FromComponentInHierarchy().AsSingle();
        Container.Bind<StartManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlatformSpawner>().FromComponentInHierarchy().AsSingle();
        Container.Bind<BoostTimer>().FromComponentInHierarchy().AsSingle();
        Container.Bind<DifficultyManager>().AsSingle();
        Container.Bind<UIManager>().FromComponentInHierarchy().AsSingle();
    }
}
