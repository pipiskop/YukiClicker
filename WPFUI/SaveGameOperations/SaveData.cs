using Newtonsoft.Json;
using System.Collections.Generic;
using WPFUI.Models;

namespace WPFUI.SaveGameOperations
{
    /// <summary>
    /// Contains a single method for collecting the data and putting it into a string
    /// </summary>
    public static class SaveData
    {
        /// <summary>
        /// Used to build a list of the game save data
        /// </summary>
        public static string CreateData(double pointsPerSmack, double balance, double totalBalance, int totalClicks,
                                                     double extraHandQTY, double extraHandPrice,
                                                     double slipperQTY, double slipperPrice,
                                                     double shoeQTY, double shoePrice,
                                                     double phoneBookQTY, double phoneBookPrice,
                                                     double keyboardQTY, double keyboardPrice,
                                                     double stickQTY, double stickPrice,
                                                     double hammerQTY, double hammerPrice,
                                                     double microwaveQTY, double microwavePrice)
        {
            List<GameSaveClass> SaveDataList = new List<GameSaveClass>
            {
                //Adding all the fields into the list
                new GameSaveClass { ID = "PointsPerSmack", Value = pointsPerSmack },
                new GameSaveClass { ID = "Balance", Value = balance },
                new GameSaveClass { ID = "TotalBalance", Value = totalBalance },
                new GameSaveClass { ID = "TotalClicks", Value = totalClicks },
                new GameSaveClass { ID = "ExtraHandQTY", Value = extraHandQTY },
                new GameSaveClass { ID = "ExtraHandPrice", Value = extraHandPrice },
                new GameSaveClass { ID = "SlipperQTY", Value = slipperQTY },
                new GameSaveClass { ID = "SlipperPrice", Value = slipperPrice },
                new GameSaveClass { ID = "ShoeQTY", Value = shoeQTY },
                new GameSaveClass { ID = "ShoePrice", Value = shoePrice },
                new GameSaveClass { ID = "PhoneBookQTY", Value = phoneBookQTY },
                new GameSaveClass { ID = "PhoneBookPrice", Value = phoneBookPrice },
                new GameSaveClass { ID = "KeyboardQTY", Value = keyboardQTY },
                new GameSaveClass { ID = "KeyboardPrice", Value = keyboardPrice },
                new GameSaveClass { ID = "StickQTY", Value = stickQTY },
                new GameSaveClass { ID = "StickPrice", Value = stickPrice },
                new GameSaveClass { ID = "HammerQTY", Value = hammerQTY },
                new GameSaveClass { ID = "HammerPrice", Value = hammerPrice },
                new GameSaveClass { ID = "MicrowaveQTY", Value = microwaveQTY },
                new GameSaveClass { ID = "MicrowavePrice", Value = microwavePrice }
            };

            return JsonConvert.SerializeObject(SaveDataList.ToArray());
        }
    }
}