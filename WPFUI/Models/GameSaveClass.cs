namespace WPFUI.Models
{
    /// <summary>
    /// Used to hold information about singular game data i.e Balance, PointsPerSmack
    /// </summary>
    public class GameSaveClass
    {
        /// <summary>
        /// The name or identifier of the game data type
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// The value of the game data type
        /// </summary>
        public double Value { get; set; }
    }
}