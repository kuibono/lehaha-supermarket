using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using NSH.Core.WPF;

namespace NSH.Core.WPF
{
    public class DoubleClickBehavior
    {
        #region DoubleClickCommand
        public static DelegateCommand GetDoubleClickCommand(DependencyObject obj)
        {
            return (DelegateCommand)obj.GetValue(DoubleClickCommandProperty);
        }

        public static void SetDoubleClickCommand(DependencyObject obj, DelegateCommand value)
        {
            obj.SetValue(DoubleClickCommandProperty, value);
        }

        // Using a DependencyProperty as the backing store for DoubleClickCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DoubleClickCommandProperty =
            DependencyProperty.RegisterAttached("DoubleClickCommand", typeof(DelegateCommand), typeof(DoubleClickBehavior), new UIPropertyMetadata(PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            var element = target as Control;

            if ((e.NewValue != null) && (e.OldValue == null))
            {

                element.PreviewMouseDoubleClick += PreviewMouseDoubleClick;
            }
            else if ((e.NewValue == null) && (e.OldValue != null))
            {
                element.PreviewMouseDoubleClick -= PreviewMouseDoubleClick;
            }
        }

        public static void PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UIElement element = (UIElement)sender;
            ICommand command = (ICommand)element.GetValue(DoubleClickBehavior.DoubleClickCommandProperty);
            object parameter = element.GetValue(DoubleClickBehavior.DoubleClickCommandParameterProperty);
            command.Execute(parameter);
        } 
        #endregion

        #region DoubleClickCommandParameter
        public static object GetDoubleClickCommandParameter(DependencyObject obj)
        {
            return (object)obj.GetValue(DoubleClickCommandParameterProperty);
        }

        public static void SetDoubleClickCommandParameter(DependencyObject obj, object value)
        {
            obj.SetValue(DoubleClickCommandParameterProperty, value);
        }

        // Using a DependencyProperty as the backing store for DoubleClickCommandParameter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DoubleClickCommandParameterProperty =
            DependencyProperty.RegisterAttached("DoubleClickCommandParameter", typeof(object), typeof(DoubleClickBehavior), new UIPropertyMetadata(null));
        #endregion
    }
}
