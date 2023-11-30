using Data;
using Service;
using Zenject;

namespace Installers
{
    public class AudioInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IAudioDirector>().To<AudioDirector>().AsSingle();
            Container.Bind<AudioDataBase>().AsSingle();
        }
    }
}