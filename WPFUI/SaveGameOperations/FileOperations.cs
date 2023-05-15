using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using WPFUI.Helpers;

namespace WPFUI.SaveGameOperations
{
    /// <summary>
    /// Contains methods and properties for saving game data to disk
    /// </summary>
    public static class FileOperations
    {
        /// <summary>
        /// Holds the default name for the game save
        /// </summary>
        private const string _defaultSaveName = "YukiClicker.txt";

        /// <summary>
        /// Holds the default location for the game save
        /// </summary>
        private static readonly string _defaultSaveLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), _defaultSaveName);

        /// <summary>
        /// Privately holds the save location
        /// </summary>
        private static string _saveLocation;

        /// <summary>
        /// Public get/set for the save location
        /// </summary>
        public static string SaveLocation
        {
            get { return _saveLocation; }
            set
            {
                _saveLocation = value;
            }
        }

        /// <summary>
        /// Used to get a new save location and populate SaveLocation
        /// </summary>
        /// <returns></returns>
        public static void SelectSaveLocation()
        {
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Title = "Save your game",
                InitialDirectory = _defaultSaveLocation,
                Filter = "TXT Files|*.txt"
            };

            saveDialog.ShowDialog();

            if (!string.IsNullOrEmpty(saveDialog.FileName))
            {
                SaveLocation = saveDialog.FileName;
            }
            else
            {
                SaveLocation = "Cancelled";
            }

            saveDialog.Dispose();
        }

        /// <summary>
        /// Used to select a pre-existing save file and populate SaveLocation
        /// </summary>
        public static void SelectLoadLocation()
        {
            OpenFileDialog openDialog = new OpenFileDialog
            {
                Title = "Select your save",
                Filter = "TXT Files|*.txt",
                InitialDirectory = _defaultSaveLocation
            };

            var dialogResult = openDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                //Update the save location
                SaveLocation = openDialog.FileName;
            }
            else
            {
                SaveLocation = "Cancelled";
            }
            openDialog.Dispose();
        }

        /// <summary>
        /// Used to write the data to the file
        /// </summary>
        /// <param name="json"></param>
        public static async Task SaveDataAsync(string json)
        {
            //Checks if the save location is empty, cancelled or not a txt file
            if (string.IsNullOrEmpty(SaveLocation) || SaveLocation == "Cancelled" || !SaveLocation.EndsWith(".txt"))
            {
                //No save exists, ask to make one
                SelectSaveLocation();
            }

            if (SaveLocation == "Cancelled")
            {
                //User cancelled the prompt, don't save
                PopupHelper.ShowPopup("NOT SAVED!", "Suit yourself, your game hasn't been saved");
            }
            else if (!SaveLocation.EndsWith(".txt"))
            {
                //The user selected a file that isn't a TXT file
                PopupHelper.ShowPopup("NOT SAVED!", "Nice try, the save file must be a text file");
                SaveLocation = string.Empty;
            }
            else
            {
                try
                {
                    //Try to save to disk
                    await SaveGameDataToDiskAsync(json);
                    PopupHelper.ShowPopup("SAVED!", $"Congrats, your game has been saved to: {SaveLocation}");
                }
                catch (Exception e)
                {
                    //Saving failed
                    PopupHelper.ShowPopup("FAILED", e.Message);
                }
            }
        }

        /// <summary>
        /// Loads the game data from the file and returns it as a string
        /// </summary>
        /// <returns></returns>
        public static async Task<string> LoadData()
        {
            string output;

            try
            {
                using (var sr = new StreamReader(SaveLocation))
                {
                    output = await sr.ReadToEndAsync();
                }
                if (string.IsNullOrEmpty(output))
                {
                    output = "Errrr, that file was empty";
                }
            }
            catch (Exception e)
            {
                output = $"Failed {e.Message}";
            }

            return output;
        }

        /// <summary>
        /// Saves the game data to the save file
        /// </summary>
        /// <returns></returns>
        private static async Task SaveGameDataToDiskAsync(string json)
        {
            using (var sw = new StreamWriter(SaveLocation))
            {
                await sw.WriteAsync(json);
            }
        }
    }
}