using Cysharp.Threading.Tasks;
using KKSFramework.SceneLoad;
using Zenject;

public class ProjectInstall : MonoInstaller
{
    public override void InstallBindings()
    {
        SceneLoadProjectManager.Instance.InitManager();
    }

    public override void Start()
    {
        StartAsync().Forget();

        async UniTask StartAsync()
        {
            await TableDataManager.Instance.LoadTableDatas();
            await SceneLoadProjectManager.Instance.ChangeSceneAsync(SceneType.Title);
        }
    }
}