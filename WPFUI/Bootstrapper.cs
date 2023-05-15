using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WPFUI.Helpers;
using WPFUI.ViewModels;

namespace WPFUI
{
    /// <summary>
    /// Contains the base methods for the bootstrapper class
    /// </summary>
    public class Bootstrapper : BootstrapperBase
    {
        /// <summary>
        /// The container for folding ViewModels
        /// </summary>
        private readonly SimpleContainer _container = new SimpleContainer();

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            _container.Instance(_container);

            _container
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>();

            GetType().Assembly.GetTypes()
                .Where(type => type.IsClass)
                .Where(type => type.Name.EndsWith("ViewModel"))
                .ToList()
                .ForEach(viewModelType => _container.RegisterPerRequest(
                    viewModelType, viewModelType.ToString(), viewModelType));
        }

        /// <summary>
        /// Controls what happens on application startup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override async void OnStartup(object sender, StartupEventArgs e)
        {
            //save and start the music
            await DiskOperations.SaveMusicToDisk();
            Sounds.StartBackgroundMusic();

            DisplayRootViewFor<ShellViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        /// <summary>
        /// Controls what happens on application close
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override async void OnExit(object sender, EventArgs e)
        {
            //stop and delete the music
            Sounds.StopBackgroundMusic();
            await DiskOperations.DeleteMusicFromDisk();
        }
    }
}