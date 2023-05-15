using Caliburn.Micro;
using System.Collections.Generic;
using WPFUI.Models;

namespace WPFUI.ViewModels
{
    /// <summary>
    /// Used to present the stats popup and populate all the fields
    /// </summary>
    public class StatsPopupViewModel : Screen
    {
        /// <summary>
        /// Holds the bindable collection of stats
        /// </summary>
        private BindableCollection<GameSaveClass> _stats;

        /// <summary>
        /// Gets and sets the bindable collection of stats
        /// </summary>
        public BindableCollection<GameSaveClass> Stats
        {
            get { return _stats; }
            set
            {
                _stats = value;
                NotifyOfPropertyChange(() => Stats);
            }
        }

        /// <summary>
        /// Used to update the stats
        /// </summary>
        /// <param name="stats"></param>
        public void UpdateStats(List<GameSaveClass> stats)
        {
            _stats = new BindableCollection<GameSaveClass>(stats);
        }

        /// <summary>
        /// Close the stats popup
        /// </summary>
        public void Close()
        {
            TryClose();
        }
    }
}