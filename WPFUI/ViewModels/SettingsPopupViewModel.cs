using Caliburn.Micro;
using WPFUI.Helpers;

namespace WPFUI.ViewModels
{
    /// <summary>
    /// Logic for the settings popup
    /// </summary>
    public class SettingsPopupViewModel : Screen
    {
        /// <summary>
        /// Controls whether the purchase sound is played or not
        /// </summary>
        /// <param name="i"></param>
        public static void PurchaseSound(string i)
        {
            if (i == "true")
            {
                Sounds.PlayPurchaseSoundBool = true;
            }
            else
            {
                Sounds.PlayPurchaseSoundBool = false;
            }
        }

        /// <summary>
        /// Controls whether the smack sound is played or not
        /// </summary>
        /// <param name="i"></param>
        public static void SmackSound(string i)
        {
            if (i == "true")
            {
                Sounds.PlaySmackSoundBool = true;
            }
            else
            {
                Sounds.PlaySmackSoundBool = false;
            }
        }

        /// <summary>
        /// Updates the smack toggle button depending on the boolean value
        /// </summary>
        public bool SmackIsChecked { get { return Sounds.PlaySmackSoundBool; } }

        /// <summary>
        /// Updates the purchase toggle button depending on the boolean value
        /// </summary>
        public bool PurchaseIsChecked { get { return Sounds.PlayPurchaseSoundBool; } }

        /// <summary>
        /// Holds the value for the background music volume
        /// </summary>
        private int _backgroundMusicVolume = 100;

        /// <summary>
        /// Accessor and modifier for the background music volume
        /// </summary>
        public int BackgroundMusicVolume
        {
            get { return _backgroundMusicVolume; }
            set
            {
                _backgroundMusicVolume = value;
                NotifyOfPropertyChange(() => BackgroundMusicVolume);
            }
        }

        /// <summary>
        /// The event for when the volume slider is changed
        /// </summary>
        public void BackgroundMusicVolumeChanged()
        {
            Sounds.SetBackgroundMusicVolume(BackgroundMusicVolume / 100.0f);
        }

        /// <summary>
        /// The close button for the settings
        /// </summary>
        public void Close()
        {
            TryClose();
        }
    }
}