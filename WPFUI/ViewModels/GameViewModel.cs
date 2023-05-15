using Caliburn.Micro;
using System;
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
    /// Includes the logic for the game screen
    /// </summary>
    public class GameViewModel : Screen
    {
        #region User Control properties and methods

        /// <summary>
        /// Private variable to hold the events
        /// </summary>
        private readonly IEventAggregator _events;

        /// <summary>
        /// Used to store the values when loading a game
        /// </summary>
        public static List<GameSaveClass> LoadList;

        /// <summary>
        /// Runs when the GameViewModel is loaded
        /// </summary>
        public GameViewModel(IEventAggregator events)
        {
            //Assigning the events variable
            _events = events;

            //Loading a save
            List<GameSaveClass> _tempList = LoadList;

            //Check if the templist count is greater than zero
            if (_tempList?.Count() > 0)
            {
                //Run the functions that loads the game save
                LoadGame();
            }
        }

        #endregion User Control properties and methods

        #region Private backing fields

        #region Game private fields

        /// <summary>
        /// Holds the points per smack value
        /// </summary>
        private double _pointPerSmack = 1;

        /// <summary>
        /// Holds the current balance for the user
        /// </summary>
        private double _balance = 0;

        /// <summary>
        /// Holds the total points ever earned
        /// </summary>
        private double _totalBalance = 0;

        /// <summary>
        /// Holds the current total of clicks
        /// </summary>
        private int _totalClicks;

        #endregion Game private fields

        #region ExtraHand private fields

        /// <summary>
        /// Holds the amount of extra hand upgrades
        /// </summary>
        private double _extraHandQTY = 0;

        /// <summary>
        /// Holds the reward added to the per smack value when
        /// buying an extra hand
        /// </summary>
        private readonly int _extraHandReward = 1;

        /// <summary>
        /// The price of an extra hand when first starting the game
        /// </summary>
        private double _extraHandPrice = 20;

        #endregion ExtraHand private fields

        #region Slipper private fields

        /// <summary>
        /// Holds the amount of slipper upgrades
        /// </summary>
        private double _SlipperQTY = 0;

        /// <summary>
        /// Holds the reward added to the per smack value
        /// when buying a slipper
        /// </summary>
        private readonly int _slipperReward = 3;

        /// <summary>
        /// The price of a slipper when first starting the game
        /// </summary>
        private double _slipperPrice = 150;

        #endregion Slipper private fields

        #region Shoe private fields

        /// <summary>
        /// Holds the amount of shoe upgrades
        /// </summary>
        private double _shoeQTY = 0;

        /// <summary>
        /// Holds the reward added to the per smack values
        /// when buying a shoe
        /// </summary>
        private readonly double _shoeReward = 5;

        /// <summary>
        /// The price of a shoe when first starting the game
        /// </summary>
        private double _shoePrice = 400;

        #endregion Shoe private fields

        #region PhoneBook private fields

        /// <summary>
        /// Holds the amount of phonebook upgrades
        /// </summary>
        private double _phoneBookQTY = 0;

        /// <summary>
        /// Holds the reward added to the per smack values
        /// when buying a phonebook
        /// </summary>
        private readonly double _phoneBookReward = 8;

        /// <summary>
        /// The price of a phoneBook when first starting the game
        /// </summary>
        private double _phoneBookPrice = 2000;

        #endregion PhoneBook private fields

        #region Keyboard private fields

        /// <summary>
        /// Holds the amount of keyboard upgrades
        /// </summary>
        private double _keyboardQTY = 0;

        /// <summary>
        /// Holds the reward added to the per smack values
        /// when buying a keyboard
        /// </summary>
        private readonly double _keyboardReward = 12;

        /// <summary>
        /// The price of a keyboard when first starting the game
        /// </summary>
        private double _keyboardPrice = 15000;

        #endregion Keyboard private fields

        #region Stick private fields

        /// <summary>
        /// Holds the amount of stick upgrades
        /// </summary>
        private double _stickQTY = 0;

        /// <summary>
        /// Holds the reward added to the per smack values
        /// when buying a stick
        /// </summary>
        private readonly double _stickReward = 20;

        /// <summary>
        /// The price of a stick when first starting the game
        /// </summary>
        private double _stickPrice = 30000;

        #endregion Stick private fields

        #region Hammer private fields

        /// <summary>
        /// Holds the amount of hammer upgrades
        /// </summary>
        private double _hammerQTY = 0;

        /// <summary>
        /// Holds the reward added to the per smack values
        /// when buying a hammer
        /// </summary>
        private readonly double _hammerReward = 50;

        /// <summary>
        /// The price of a hammer when first starting the game
        /// </summary>
        private double _hammerPrice = 100000;

        #endregion Hammer private fields

        #region Microwave private fields

        /// <summary>
        /// Holds the amount of microwave upgrades
        /// </summary>
        private double _microwaveQTY = 0;

        /// <summary>
        /// Holds the reward added to the per smack values
        /// when buying a microwave
        /// </summary>
        private readonly double _microwaveReward = 80;

        /// <summary>
        /// The price of a microwave when first starting the game
        /// </summary>
        private double _microwavePrice = 500000;

        #endregion Microwave private fields

        #endregion Private backing fields

        #region Game public fields

        /// <summary>
        /// Used to hold the total balance
        /// </summary>
        public double Balance
        {
            get { return _balance; }
            set
            {
                //Update balance
                _balance = value;
                //Notify all upgrade buttons
                NotifyOfPropertyChange(() => Balance);
                NotifyOfPropertyChange(() => CanExtraHand);
                NotifyOfPropertyChange(() => CanSlipper);
                NotifyOfPropertyChange(() => CanShoe);
                NotifyOfPropertyChange(() => CanPhoneBook);
                NotifyOfPropertyChange(() => CanKeyboard);
                NotifyOfPropertyChange(() => CanStick);
                NotifyOfPropertyChange(() => CanHammer);
                NotifyOfPropertyChange(() => CanMicrowave);
            }
        }

        /// <summary>
        /// Holds and updates the total of points ever earned
        /// </summary>
        public double TotalBalance
        {
            get { return _totalBalance; }
            set
            {
                _totalBalance = value;
            }
        }

        /// <summary>
        /// Used to hold the amount of pointed that are rewarded per click
        /// </summary>
        public double PointPerSmack
        {
            get { return _pointPerSmack; }
            set
            {
                _pointPerSmack = value;
                NotifyOfPropertyChange(() => PointPerSmack);
            }
        }

        /// <summary>
        /// Used to hold the amount of clicks that have happened
        /// </summary>
        public int TotalClicks
        {
            get { return _totalClicks; }
            set
            {
                _totalClicks = value;
                NotifyOfPropertyChange(() => TotalClicks);
            }
        }

        /// <summary>
        /// Used to return the total amount of upgrades that are owned
        /// </summary>
        public double TotalUpgrades
        {
            get
            {
                return ExtraHandQTY +
                    SlipperQTY +
                    ShoeQTY +
                    PhoneBookQTY +
                    KeyboardQTY +
                    StickQTY +
                    HammerQTY +
                    MicrowaveQTY;
            }
        }

        #endregion Game public fields

        #region ExtraHand public fields

        /// <summary>
        /// Holds to quantity of extra hand's that are owned
        /// </summary>
        public double ExtraHandQTY
        {
            get { return _extraHandQTY; }
            set
            {
                _extraHandQTY = value;
                NotifyOfPropertyChange(() => ExtraHandQTY);
            }
        }

        /// <summary>
        /// Holds the current price of an extra hand
        /// </summary>
        public double ExtraHandPrice
        {
            get { return _extraHandPrice; }
            set
            {
                _extraHandPrice = value;
                NotifyOfPropertyChange(() => ExtraHandPrice);
            }
        }

        /// <summary>
        /// Decides whether the buy button for an extra hand is
        /// enabled or disabled
        /// </summary>
        public bool CanExtraHand => Balance > ExtraHandPrice;

        #endregion ExtraHand public fields

        #region Slipper public fields

        /// <summary>
        /// Holds to quantity of slippers that are owned
        /// </summary>
        public double SlipperQTY
        {
            get { return _SlipperQTY; }
            set
            {
                _SlipperQTY = value;
                NotifyOfPropertyChange(() => SlipperQTY);
            }
        }

        /// <summary>
        /// Holds the current price of a slipper
        /// </summary>
        public double SlipperPrice
        {
            get { return _slipperPrice; }
            set
            {
                _slipperPrice = value;
                NotifyOfPropertyChange(() => SlipperPrice);
            }
        }

        /// <summary>
        /// Decides whether the buy button for a slipper is
        /// enabled or disabled
        /// </summary>
        public bool CanSlipper => Balance > SlipperPrice;

        #endregion Slipper public fields

        #region Shoe public fields

        /// <summary>
        /// Holds the quantity of shoes that are owned
        /// </summary>
        public double ShoeQTY
        {
            get { return _shoeQTY; }
            set
            {
                _shoeQTY = value;
                NotifyOfPropertyChange(() => ShoeQTY);
            }
        }

        /// <summary>
        /// Holds the current price of a shoe
        /// </summary>
        public double ShoePrice
        {
            get { return _shoePrice; }
            set
            {
                _shoePrice = value;
                NotifyOfPropertyChange(() => ShoePrice);
            }
        }

        /// <summary>
        /// Decides whether the buy button for a shoe is enabled
        /// or disabled
        /// </summary>
        public bool CanShoe => Balance > ShoePrice;

        #endregion Shoe public fields

        #region PhoneBook public fields

        /// <summary>
        /// Holds the quantity of phonebooks that are owned
        /// </summary>
        public double PhoneBookQTY
        {
            get { return _phoneBookQTY; }
            set
            {
                _phoneBookQTY = value;
                NotifyOfPropertyChange(() => PhoneBookQTY);
            }
        }

        /// <summary>
        /// Holds the current price of a phonebook
        /// </summary>
        public double PhoneBookPrice
        {
            get { return _phoneBookPrice; }
            set
            {
                _phoneBookPrice = value;
                NotifyOfPropertyChange(() => PhoneBookPrice);
            }
        }

        /// <summary>
        /// Decides whether the buy button for a phonebook is enabled
        /// or disabled
        /// </summary>
        public bool CanPhoneBook => Balance > PhoneBookPrice;

        #endregion PhoneBook public fields

        #region Keyboard public fields

        /// <summary>
        /// Holds the quantity of keyboards that are owned
        /// </summary>
        public double KeyboardQTY
        {
            get { return _keyboardQTY; }
            set
            {
                _keyboardQTY = value;
                NotifyOfPropertyChange(() => KeyboardQTY);
            }
        }

        /// <summary>
        /// Holds the current price of a keyboard
        /// </summary>
        public double KeyboardPrice
        {
            get { return _keyboardPrice; }
            set
            {
                _keyboardPrice = value;
                NotifyOfPropertyChange(() => KeyboardPrice);
            }
        }

        /// <summary>
        /// Decides whether the buy button for a keyboard is enabled
        /// or disabled
        /// </summary>
        public bool CanKeyboard => Balance > KeyboardPrice;

        #endregion Keyboard public fields

        #region Stick public fields

        /// <summary>
        /// Holds the quantity of sticks that are owned
        /// </summary>
        public double StickQTY
        {
            get { return _stickQTY; }
            set
            {
                _stickQTY = value;
                NotifyOfPropertyChange(() => StickQTY);
            }
        }

        /// <summary>
        /// Holds the current price of a stick
        /// </summary>
        public double StickPrice
        {
            get { return _stickPrice; }
            set
            {
                _stickPrice = value;
                NotifyOfPropertyChange(() => StickPrice);
            }
        }

        /// <summary>
        /// Decides whether the buy button for a stick is enabled
        /// or disabled
        /// </summary>
        public bool CanStick => Balance > StickPrice;

        #endregion Stick public fields

        #region Hammer public fields

        /// <summary>
        /// Holds the quantity of hammers that are owned
        /// </summary>
        public double HammerQTY
        {
            get { return _hammerQTY; }
            set
            {
                _hammerQTY = value;
                NotifyOfPropertyChange(() => HammerQTY);
            }
        }

        /// <summary>
        /// Holds the current price of a hammer
        /// </summary>
        public double HammerPrice
        {
            get { return _hammerPrice; }
            set
            {
                _hammerPrice = value;
                NotifyOfPropertyChange(() => HammerPrice);
            }
        }

        /// <summary>
        /// Decides whether the buy button for a hammer is enabled
        /// or disabled
        /// </summary>
        public bool CanHammer => Balance > HammerPrice;

        #endregion Hammer public fields

        #region Microwave public fields

        /// <summary>
        /// Holds the quantity of microwaves that are owned
        /// </summary>
        public double MicrowaveQTY
        {
            get { return _microwaveQTY; }
            set
            {
                _microwaveQTY = value;
                NotifyOfPropertyChange(() => MicrowaveQTY);
            }
        }

        /// <summary>
        /// Holds the current price of a microwave
        /// </summary>
        public double MicrowavePrice
        {
            get { return _microwavePrice; }
            set
            {
                _microwavePrice = value;
                NotifyOfPropertyChange(() => MicrowavePrice);
            }
        }

        /// <summary>
        /// Decides whether the buy button for a microwave is enabled
        /// or disabled
        /// </summary>
        public bool CanMicrowave => Balance > MicrowavePrice;

        #endregion Microwave public fields

        #region Game public methods

        /// <summary>
        /// Performes the actions when the player presses the buy button for
        /// an extra hand
        /// </summary>
        public void ExtraHand()
        {
            Sounds.PlayPurchaseSound();
            //Take off the price of the extra hand
            Balance -= ExtraHandPrice;
            //Increase the quantity owned
            ExtraHandQTY++;
            //Update the points per smack
            PointPerSmack += _extraHandReward;
            //Update the price for an extra hand
            ExtraHandPrice += Math.Round((ExtraHandQTY * 2));
            //Update if the user can purchase another hand
            NotifyOfPropertyChange(() => CanExtraHand);
        }

        /// <summary>
        /// Performes the actions when the player presses the buy button for
        /// a slipper
        /// </summary>
        public void Slipper()
        {
            Sounds.PlayPurchaseSound();
            //Take off the price of the slipper
            Balance -= SlipperPrice;
            //Increase the quantity owned
            SlipperQTY++;
            //Update the points per slipper
            PointPerSmack += _slipperReward;
            //Update the price for a slipper
            SlipperPrice += Math.Round((SlipperQTY * 5));
            //Update if the user can purchase another slipper
            NotifyOfPropertyChange(() => CanSlipper);
        }

        /// <summary>
        /// Performs the actions when the player presses the buy button for
        /// a shoe
        /// </summary>
        public void Shoe()
        {
            Sounds.PlayPurchaseSound();
            //Take off the price of the shoe
            Balance -= ShoePrice;
            //Increase the quantity owned
            ShoeQTY++;
            //Update the points per shoe
            PointPerSmack += _shoeReward;
            //Update the price for a shoe
            ShoePrice += Math.Round((ShoeQTY * 15));
            //Update if the user can purchase a shoe
            NotifyOfPropertyChange(() => CanShoe);
        }

        /// <summary>
        /// Performs the actions when the player presses the buy button for
        /// a phonebook
        /// </summary>
        public void PhoneBook()
        {
            Sounds.PlayPurchaseSound();
            //Take off the price of the shoe
            Balance -= PhoneBookPrice;
            //Increase the quantity owned
            PhoneBookQTY++;
            //Update the points per phonebook
            PointPerSmack += _phoneBookReward;
            //Update the price for a phonebook
            PhoneBookPrice += Math.Round((PhoneBookQTY * 15));
            //Update if the user can purchase a phonebook
            NotifyOfPropertyChange(() => CanPhoneBook);
        }

        /// <summary>
        /// Performs the actions when the player presses the buy button for
        /// a keyboard
        /// </summary>
        public void Keyboard()
        {
            Sounds.PlayPurchaseSound();
            //Take off the price of the keyboard
            Balance -= KeyboardPrice;
            //Increase the quantity owned
            KeyboardQTY++;
            //Update the points per keyboard
            PointPerSmack += _keyboardReward;
            //Update the price for a keyboard
            KeyboardPrice += Math.Round((KeyboardQTY * 15));
            //Update if the user can purchase a keyboard
            NotifyOfPropertyChange(() => CanKeyboard);
        }

        /// <summary>
        /// Performs the actions when the player presses the buy button for
        /// a stick
        /// </summary>
        public void Stick()
        {
            Sounds.PlayPurchaseSound();
            //Take off the price of the stick
            Balance -= StickPrice;
            //Increase the quantity owned
            StickQTY++;
            //Update the points per stick
            PointPerSmack += _stickReward;
            //Update the price for a stick
            StickPrice += Math.Round((StickQTY * 15));
            //Update if the user can purchase a stick
            NotifyOfPropertyChange(() => CanStick);
        }

        /// <summary>
        /// Performs the actions when the player presses the buy button for
        /// a hammer
        /// </summary>
        public void Hammer()
        {
            Sounds.PlayPurchaseSound();
            //Take off the price of the hammer
            Balance -= HammerPrice;
            //Increase the quantity owned
            HammerQTY++;
            //Update the points per hammer
            PointPerSmack += _hammerReward;
            //Update the price for a hammer
            HammerPrice += Math.Round((HammerQTY * 15));
            //Update if the user can purchase a hammer
            NotifyOfPropertyChange(() => CanHammer);
        }

        /// <summary>
        /// Performs the actions when the player presses the buy button for
        /// a microwave
        /// </summary>
        public void Microwave()
        {
            Sounds.PlayPurchaseSound();
            //Take off the price of the microwave
            Balance -= MicrowavePrice;
            //Increase the quantity owned
            MicrowaveQTY++;
            //Update the points per microwave
            PointPerSmack += _microwaveReward;
            //Update the price for a microwave
            MicrowavePrice += Math.Round((MicrowaveQTY * 15));
            //Update if the user can purchase a microwave
            NotifyOfPropertyChange(() => CanMicrowave);
        }

        /// <summary>
        /// Performs actions when the player presses
        /// the smack button
        /// </summary>
        public void Smack()
        {
            Balance += PointPerSmack;
            TotalBalance += PointPerSmack;
            TotalClicks++;
            Sounds.PlayRandomSmack();
        }

        /// <summary>
        /// Saves the current progress
        /// </summary>
        public async Task SaveAsync()
        {
            string data = SaveData.CreateData(PointPerSmack, Balance, TotalBalance, TotalClicks,
                                ExtraHandQTY, ExtraHandPrice,
                                SlipperQTY, SlipperPrice,
                                ShoeQTY, ShoePrice,
                                PhoneBookQTY, PhoneBookPrice,
                                KeyboardQTY, KeyboardPrice,
                                StickQTY, StickPrice,
                                HammerQTY, HammerPrice,
                                MicrowaveQTY, MicrowavePrice);

            await FileOperations.SaveDataAsync(data);
        }

        /// <summary>
        /// Loads the game progrss from a file and updates all the fields
        /// </summary>
        public void LoadGame()
        {
            foreach (GameSaveClass i in LoadList)
            {
                switch (i.ID)
                {
                    case "PointsPerSmack":
                        PointPerSmack = i.Value;
                        break;

                    case "Balance":
                        Balance = i.Value;
                        break;

                    case "TotalBalance":
                        TotalBalance = i.Value;
                        break;

                    case "TotalClicks":
                        TotalClicks = (int)i.Value;
                        break;

                    case "ExtraHandQTY":
                        ExtraHandQTY = i.Value;
                        break;

                    case "ExtraHandPrice":
                        ExtraHandPrice = i.Value;
                        break;

                    case "SlipperQTY":
                        SlipperQTY = i.Value;
                        break;

                    case "SlipperPrice":
                        SlipperPrice = i.Value;
                        break;

                    case "ShoeQTY":
                        ShoeQTY = i.Value;
                        break;

                    case "ShoePrice":
                        ShoePrice = i.Value;
                        break;

                    case "PhoneBookQTY":
                        PhoneBookQTY = i.Value;
                        break;

                    case "PhoneBookPrice":
                        PhoneBookPrice = i.Value;
                        break;

                    case "KeyboardQTY":
                        KeyboardQTY = i.Value;
                        break;

                    case "KeyboardPrice":
                        KeyboardPrice = i.Value;
                        break;

                    case "StickQTY":
                        StickQTY = i.Value;
                        break;

                    case "StickPrice":
                        StickPrice = i.Value;
                        break;

                    case "HammerQTY":
                        HammerQTY = i.Value;
                        break;

                    case "HammerPrice":
                        HammerPrice = i.Value;
                        break;

                    case "MicrowaveQTY":
                        MicrowaveQTY = i.Value;
                        break;

                    case "MicrowavePrice":
                        MicrowavePrice = i.Value;
                        break;
                }
            }
        }

        /// <summary>
        /// Used to show the stats popup
        /// </summary>
        public void Stats()
        {
            //Building a temporary list with the required stats
            List<GameSaveClass> _tempList = new List<GameSaveClass>
            {
                new GameSaveClass { ID = "Total Clicks", Value = TotalClicks },
                new GameSaveClass { ID = "Total Upgrades", Value = TotalUpgrades },
                new GameSaveClass { ID = "Total Balance", Value = TotalBalance }
            };

            //Updating the popup and displaying it
            PopupHelper.ShowStatsPopup(_tempList);
        }

        /// <summary>
        /// Used to exit the application
        /// </summary>
        public async Task ExitAsync()
        {
            //Saving before exiting
            await SaveAsync();
            //Launch the load screen
            _events.BeginPublishOnUIThread(new LoadEvent());
        }

        /// <summary>
        /// Show the settings
        /// </summary>
        public static void Settings()
        {
            PopupHelper.ShowSettingsPopup();
        }

        #endregion Game public methods
    }
}