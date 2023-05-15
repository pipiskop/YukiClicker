using Caliburn.Micro;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WPFUI.EventModels;
using WPFUI.Helpers;
using WPFUI.Models;
using WPFUI.SaveGameOperations;

namespace WPFUI.ViewModels
{
    /// <summary>
    /// Includes logic for the loading screen
    /// </summary>
    public class LoadViewModel : Screen
    {
        /// <summary>
        /// Holds the version for the program
        /// </summary>
        private string _version;

        /// <summary>
        /// Updates and gets the version
        /// </summary>
        public string Version
        {
            get { return _version; }
            set
            {
                _version = value;
                NotifyOfPropertyChange(() => Version);
            }
        }

        /// <summary>
        /// Private variable to hold the events
        /// </summary>
        private readonly IEventAggregator _events;

        /// <summary>
        /// Event when the LoadViewModel is loaded
        /// </summary>
        /// <param name="events">The events</param>
        public LoadViewModel(IEventAggregator events)
        {
            //Clear the saved location
            FileOperations.SaveLocation = string.Empty;
            //Updates the events private variables
            _events = events;
            //Populate the version info
            Version = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString();
        }

        /// <summary>
        /// The event for when the NewGame button is pressed
        /// </summary>
        public void NewGame()
        {
            //Launches the LoadGameEvent
            GameViewModel.LoadList = new List<GameSaveClass>() { new GameSaveClass { ID = "DummyFromNewGame", Value = 1.0 } };
            _events.PublishOnUIThread(new LoadGameEvent());
        }

        /// <summary>
        /// The event for when the LoadGame button is pressed
        /// </summary>
        public async Task LoadGame()
        {
            FileOperations.SelectLoadLocation();

            /*
             At this point the value of SaveLocation could either be a valid path or "Cancelled"
            */

            //Checking the save location isn't cancelled, if it is then do nothing
            if (FileOperations.SaveLocation != "Cancelled")
            {
                //If the save location isn't a txt file, return and do nothing
                if (!FileOperations.SaveLocation.EndsWith(".txt"))
                {
                    PopupHelper.ShowPopup("FAILED", "Nice try, the save file must be a text file");
                    return;
                }
                string loadResponse = await FileOperations.LoadData();
                //Try to load the file
                if (loadResponse.StartsWith("Failed"))
                {
                    //Loading the file failed
                    PopupHelper.ShowPopup("FAILED", loadResponse.Replace("Failed", ""));
                }
                else
                {
                    //Try to parse the loadResponse
                    List<GameSaveClass> parseResponse = LoadData.CreateData(loadResponse);

                    if (parseResponse.Count() == 1)
                    {
                        //The parsed data only contains one entry, the load has failed
                        PopupHelper.ShowPopup("FAILED", "Beep Boop, data not recognised");
                    }
                    else
                    {
                        //Load checks have passed, launch the game
                        GameViewModel.LoadList = parseResponse;
                        _events.PublishOnUIThread(new LoadGameEvent());
                        PopupHelper.ShowPopup("LOADED", "Woop! Your game has been loaded");
                    }
                }
            }
        }

        /// <summary>
        /// Used to show the settings popup
        /// </summary>
        public static void Settings()
        {
            PopupHelper.ShowSettingsPopup();
        }
    }
}