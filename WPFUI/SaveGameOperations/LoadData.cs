using Newtonsoft.Json;
using System.Collections.Generic;
using WPFUI.Models;

namespace WPFUI.SaveGameOperations
{
    /// <summary>
    /// Contains a method for parsing the text from load game files into a GameSaveClass object list
    /// </summary>
    public class LoadData
    {
        /// <summary>
        /// Creates a list of GameSaveClass for populating the game values
        /// </summary>
        /// <param name="json">The string of text from the save game file</param>
        /// <returns>List<GameSaveClass></returns>
        public static List<GameSaveClass> CreateData(string json)
        {
            List<GameSaveClass> list = new List<GameSaveClass>();

            try
            {
                list = JsonConvert.DeserializeObject<List<GameSaveClass>>(json);
            }
            catch
            {
                list.Add(new GameSaveClass { ID = "Dummy data", Value = 0 });
            }

            return list;
        }
    }
}