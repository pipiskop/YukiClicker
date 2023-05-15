using Caliburn.Micro;

namespace WPFUI.ViewModels
{
    public class PopupViewModel : Screen
    {
        /// <summary>
        /// Holds the header for the popup
        /// </summary>
        private string _header;

        /// <summary>
        /// Holds the message for the popup
        /// </summary>
        private string _message;

        /// <summary>
        /// Gets and sets the header for the popup
        /// </summary>
        public string Header
        {
            get { return _header; }
            set
            {
                _header = value;
                NotifyOfPropertyChange(() => Header);
            }
        }

        /// <summary>
        /// Gets and sets the message for the popup
        /// </summary>
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                NotifyOfPropertyChange(() => Message);
            }
        }

        /// <summary>
        /// Update the popup's header and message
        /// </summary>
        /// <param name="header">what the header will be updated to</param>
        /// <param name="message">What the message will be updated to</param>
        public void UpdatePopup(string header, string message)
        {
            Header = header;
            Message = message;
        }

        /// <summary>
        /// Close the popup
        /// </summary>
        public void Close()
        {
            TryClose();
        }
    }
}