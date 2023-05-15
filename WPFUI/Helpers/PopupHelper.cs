using Caliburn.Micro;
using System.Collections.Generic;
using System.Dynamic;
using System.Windows;
using WPFUI.Models;
using WPFUI.ViewModels;

namespace WPFUI.Helpers
{
    /// <summary>
    /// Various methods and properties that allow popups to be display when using the application
    /// </summary>
    public class PopupHelper : Screen
    {
        /// <summary>
        /// Holds the passed in window manager object
        /// </summary>
        private static IWindowManager _window;

        /// <summary>
        /// Holds the passed in PopupViewModel object
        /// </summary>
        private static PopupViewModel _popup;

        /// <summary>
        /// Holds the passed in StatsPopupViewModel object
        /// </summary>
        private static StatsPopupViewModel _statsPopup;

        /// <summary>
        /// Holds the passed in SettingsPopupViewModel object
        /// </summary>
        private static SettingsPopupViewModel _settingsPopup;

        /// <summary>
        /// Used to setup the settings for the showdialog popup
        /// </summary>
        /// <returns>The settings for the popup</returns>
        private static dynamic GetSettings()
        {
            dynamic settings = new ExpandoObject();
            settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            settings.ResizeMode = ResizeMode.NoResize;
            settings.WindowStyle = WindowStyle.None;
            return settings;
        }

        /// <summary>
        /// Displays a popup using the header and message input
        /// </summary>
        /// <param name="header">The header of the popup</param>
        /// <param name="message">The message of the popup</param>
        public static void ShowPopup(string header, string message)
        {
            _window = ShellViewModel.GetWindowManager();
            _popup = ShellViewModel.GetPopup();

            _popup.UpdatePopup(header, message);
            _window.ShowDialog(_popup, null, GetSettings());
        }

        /// <summary>
        /// Displays the stats popup using a List fo GameSaveClass
        /// </summary>
        /// <param name="stats">A list of all the stats that have been passed in</param>
        public static void ShowStatsPopup(List<GameSaveClass> stats)
        {
            _window = ShellViewModel.GetWindowManager();
            _statsPopup = ShellViewModel.GetStatsPopup();

            _statsPopup.UpdateStats(stats);
            _window.ShowDialog(_statsPopup, null, GetSettings());
        }

        /// <summary>
        /// Displays the stats popup
        /// </summary>
        public static void ShowSettingsPopup()
        {
            _window = ShellViewModel.GetWindowManager();
            _settingsPopup = ShellViewModel.GetSettingsPopup();

            _window.ShowDialog(_settingsPopup, null, GetSettings());
        }
    }
}