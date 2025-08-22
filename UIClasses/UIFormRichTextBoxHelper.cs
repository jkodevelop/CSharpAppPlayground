using System.Diagnostics;

namespace CSharpAppPlayground.UIClasses
{
    public class UIFormRichTextBoxHelper
    {
        protected Form f;

        protected void RichTextbox(string msg, Color lineColor = default, bool consoleLog = false)
        {
            if (f != null && f is FormWithRichText formWithRichText)
            {
                (f as FormWithRichText).updateRichTextBoxMain(msg, lineColor);
                if(consoleLog)
                    Debug.Print(msg);
            }
            else
                throw new InvalidOperationException("Form is not of type FormWithRichText.");
        }

        protected void Label(string msg, bool consoleLog = false)
        {
            if (f != null && f is FormWithRichText formWithRichText)
            {
                (f as FormWithRichText).updateLabelMain(msg);
                if (consoleLog)
                    Debug.Print(msg);
            }
            else
                throw new InvalidOperationException("Form is not of type FormWithRichText.");
        }
    }
}
