using System.Diagnostics;

// TO DOCUMENT:
// Form.Show() - non-blocking diy of the form, allows the main thread to continue processing other events
// Form.ShowDialog() - blocking diy of the form, waits for the form to be closed before continuing processing other events
// Form.Activate() - brings the form to the front and gives it focus, ONLY WORKS if the form is already open
// Form.Focus() - sets the input focus to the form, does not bring it to the front, ONLY WORKS if the form is already open

namespace CSharpAppPlayground.Classes
{
    public class FormFactory
    {
        private Form _instance;
        private string _qualifiedFormName;
        private bool _isOpen = false;

        public bool IsOpen
        {
            get { return _isOpen; }
            // set { _isOpen = value; }
        }

        public FormFactory(string QualifiedFormName)
        {
            _qualifiedFormName = QualifiedFormName;
            CreateInstance(QualifiedFormName);
        }

        protected void CreateInstance(string QualifiedFormName)
        {
            Type t = Type.GetType(QualifiedFormName);
            if (t == null)
            {
                throw new ArgumentException($"Type '{QualifiedFormName}' not found.");
            }
            _instance = (Form) Activator.CreateInstance(t);
            Debug.Print($"Created instance of: {QualifiedFormName}");
        }

        // Create a new instance of FormUIs every time the button is clicked
        // Because Closing the form will dispose the form and we need a new instance
        // ALTERNATIVELY, you can keep a single instance of FormUIs and just hide it instead of closing it
        // BUT to do this, you need to subscribe to the FormClosing event and set e.Cancel = true; to prevent it from closing
        //
        // EXAMPLE:
        // private void Event_FormClosing(object sender, FormClosingEventArgs e)
        // {
        //    e.Cancel = true;      // Cancel the close
        //    ((Form)sender).Hide(); // Hide the form instead
        // }
        //
        protected void Event_FormClosed(object sender, FormClosedEventArgs e)
        {
            Debug.Print("Form Closed");
            _isOpen = false;
        }

        public void Open()
        {
            if (_instance == null || _instance.IsDisposed)
            {
                CreateInstance(_qualifiedFormName);
            }
            if(_isOpen)
            {
                Debug.Print($"Form {_qualifiedFormName} is already open, bringing to front.");
                _instance.BringToFront();
                return;
            }
            _instance.FormClosed -= Event_FormClosed; // Unsubscribe if already attached
            _instance.FormClosed += Event_FormClosed; // Subscribe
            _instance.Show(); // this does not block the main thread (UI thread)
            // _instance.ShowDialog(); // this blocks the main thread until the form is closed
            _isOpen = true;
        }

        /// <summary>
        /// This method checks if there are any subscribers to the FormClosed event of the form instance.
        /// Just example of using reflection to inspect the event subscribers.
        /// Look into FormHelpers.GetFormClosedSubscribers() for more details.
        /// </summary>
        public void CheckFormHasClosedEventHandler()
        {
            var subscribers = FormHelpers.GetFormClosedSubscribers(_instance);
            if (subscribers.Length > 0)
            {
                Debug.Print($"Subscribers to FormClosed: {subscribers.Length}");
                foreach (var subscriber in subscribers)
                {
                    Debug.Print($"Subscriber: {subscriber.Method.Name}");
                }
            }
            else
            {
                Debug.Print("No subscribers to FormClosed event.");
            }
        }
    }
}
