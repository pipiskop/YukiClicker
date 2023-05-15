using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace WPFUI.Helpers
{
    /// <summary>
    /// Contains methods for file system interactions
    /// </summary>
    public static class DiskOperations
    {
        /// <summary>
        /// Saves the background music to a temp file
        /// </summary>
        public static async Task SaveMusicToDisk()
        {
            using (FileStream fileStream = File.Create(Path.GetTempPath() + Sounds.BackgroundMusicFileName))
            {
                await Assembly.GetExecutingAssembly().GetManifestResourceStream(Sounds.BackgroundMusicResourceName).CopyToAsync(fileStream);
            }
        }

        /// <summary>
        /// Removes the temp background music file
        /// </summary>
        public static async Task DeleteMusicFromDisk()
        {
            await Task.Run(() => File.Delete(Path.Combine(Path.GetTempPath(), Sounds.BackgroundMusicFileName)));
        }
    }
}