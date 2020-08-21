using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestDemo.Common
{
    public class IntPreviewTextBox:PreviewTextBox
    {
        public override void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
             do
            {
                if (e.Text == "-")
                {
                    var curText = this.Text;
                    e.Handled = (curText.Length != 0);
                    break;
                }
                e.Handled = !(int.TryParse(e.Text, out int i));
            } while (false);
        }
    }
}
