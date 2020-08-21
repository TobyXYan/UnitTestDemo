using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UnitTestDemo.Common
{
     public class PreviewTextBox:TextBox
    {
        public PreviewTextBox()
        {
            PreviewTextInput += TextBox_PreviewTextInput;
             Unloaded += PreviewTextBoxUnloaded;
        }

        public virtual void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
        }

        private void PreviewTextBoxUnloaded(object sender, RoutedEventArgs e)
        {
            Unloaded -= PreviewTextBoxUnloaded;
            PreviewTextInput -= TextBox_PreviewTextInput;
        }
    }
}
