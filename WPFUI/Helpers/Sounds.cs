using System;
using System.IO;
using System.Media;
using System.Windows.Media;

namespace WPFUI.Helpers
{
    /// <summary>
    /// Contains methods for outputting sound
    /// </summary>
    public static class Sounds
    {
        /// <summary>
        /// Background music for the application
        /// </summary>
        private static readonly MediaPlayer _backgroundMusic = new MediaPlayer();

        /// <summary>
        /// The name for the background music temp file
        /// </summary>
        public static readonly string BackgroundMusicFileName = "YukiClicker.wav";

        /// <summary>
        /// The name for the background music resource
        /// </summary>
        public static readonly string BackgroundMusicResourceName = "WPFUI.Assets.Sounds.backgroundmusic.wav";

        /// <summary>
        /// Smack one sound effect
        /// </summary>
        private static readonly SoundPlayer _smackSoundOne = new SoundPlayer(WPFUI.Properties.Resources.punch1);

        /// <summary>
        /// Smack two sound effect
        /// </summary>
        private static readonly SoundPlayer _smackSoundTwo = new SoundPlayer(WPFUI.Properties.Resources.punch2);

        /// <summary>
        /// Purchase one sound effect
        /// </summary>
        private static readonly SoundPlayer _purchaseSoundOne = new SoundPlayer(WPFUI.Properties.Resources.purchase1);

        /// <summary>
        /// Random number for getting a smack sound effect
        /// </summary>
        private static readonly Random randomNumber = new Random();

        /// <summary>
        /// Decides if the smack sound is played
        /// </summary>
        public static bool PlaySmackSoundBool = true;

        /// <summary>
        /// Decides if the purchase sound is played
        /// </summary>
        public static bool PlayPurchaseSoundBool = true;

        /// <summary>
        /// Starts the background music
        /// </summary>
        public static void StartBackgroundMusic()
        {
            _backgroundMusic.Open(new Uri(Path.Combine(Path.GetTempPath(), BackgroundMusicFileName)));
            _backgroundMusic.MediaEnded += new EventHandler(BackgroundMusic_Ended);
            _backgroundMusic.Play();
        }

        /// <summary>
        /// Used to continuously reset the background music once it's ended
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void BackgroundMusic_Ended(object sender, EventArgs e)
        {
            _backgroundMusic.Position = TimeSpan.Zero;
            _backgroundMusic.Play();
        }

        /// <summary>
        /// Stop the background music and disposes of the object
        /// </summary>
        public static void StopBackgroundMusic()
        {
            _backgroundMusic.Stop();
            _backgroundMusic.Close();
        }

        /// <summary>
        /// Set the volume of the background music
        /// </summary>
        /// <param name="volume"></param>
        public static void SetBackgroundMusicVolume(float volume)
        {
            _backgroundMusic.Volume = volume;
        }

        /// <summary>
        /// Play a random smack sound
        /// </summary>
        public static void PlayRandomSmack()
        {
            if (!PlaySmackSoundBool)
            {
                return;
            }

            if (randomNumber.NextDouble() < 0.5)
            {
                _smackSoundOne.Play();
            }
            else
            {
                _smackSoundTwo.Play();
            }
        }

        /// <summary>
        /// Play the purchase sound
        /// </summary>
        public static void PlayPurchaseSound()
        {
            if (PlayPurchaseSoundBool)
            {
                _purchaseSoundOne.Play();
            }
        }
    }
}