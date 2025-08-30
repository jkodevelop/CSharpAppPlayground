using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Reflection;

namespace CSharpAppPlayground.Classes
{
    public static class FormHelpers
    {
        /// <summary>
        /// This method retrieves the subscribers to the FormClosed event of a given Form.
        ///
        /// Usage:
        /// 
        /// Delegate[] subscribers = FormHelpers.GetFormClosedSubscribers(formUIs);
        /// if (subscribers.Length > 0){
        ///   foreach (var subscriber in subscribers){
        ///     Debug.Print($"Subscriber: {subscriber.Method.Name}");
        ///   }
        /// }
        /// 
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public static Delegate[] GetFormClosedSubscribers(Form form)
        {
            // Get the type of the form
            var type = typeof(Form);

            // Find the event key for FormClosed
            var eventField = type.GetField("EVENT_FORMCLOSED", BindingFlags.Static | BindingFlags.NonPublic);
            if (eventField == null) return Array.Empty<Delegate>();

            var eventKey = eventField.GetValue(null);

            // Get the EventHandlerList from the form
            var eventsProp = typeof(Component).GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Instance);
            var eventList = eventsProp.GetValue(form);

            // Get the delegate for the FormClosed event
            var getHandlerMethod = eventList.GetType().GetMethod("get_Item", new[] { typeof(object) });
            var handler = getHandlerMethod.Invoke(eventList, new[] { eventKey }) as Delegate;

            return handler?.GetInvocationList() ?? Array.Empty<Delegate>();
        }

        public static object GetInstance(string strFullyQualifiedName)
        {
            Type t = Type.GetType(strFullyQualifiedName);
            return Activator.CreateInstance(t);
        }

        /// <summary>
        /// This allow you to switch the enabled state of two buttons.
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        public static void FlipButtons(Button A, Button B)
        {
            A.Enabled = !A.Enabled;
            B.Enabled = !B.Enabled;
        }

        public static void ToggleButtonsPauseAndStop(bool reset, Form form, 
            Button btnPause, Button btnStop)
        {
            if (form.InvokeRequired)
            {
                form.BeginInvoke(() =>
                {
                    btnPause.Enabled = !btnPause.Enabled;
                    btnStop.Enabled = !btnStop.Enabled;
                    if (reset)
                    {
                        btnPause.Text = "Pause";
                    }
                });
            }
            else
            {
                btnPause.Enabled = !btnPause.Enabled;
                btnStop.Enabled = !btnStop.Enabled;
            }
        }
    }
}
