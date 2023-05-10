﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace CustomControl
{
    [TemplatePart(Name = "textBox", Type = typeof(TextBox))]
    [TemplatePart(Name = "textBlock", Type = typeof(TextBlock))]
    [TemplatePart(Name = "errorMessage", Type = typeof(TextBox))]
    public partial class InputBox : UserControl
    {
        public string TextName
        {
            get { return (string)GetValue(TextNameProperty); }
            set { SetValue(TextNameProperty, value); }
        }

        public static readonly DependencyProperty TextNameProperty =
            DependencyProperty.Register("TextName", typeof(string), typeof(InputBox),
                new PropertyMetadata(string.Empty));



        public new string TextInput
        {
            get { return (string)GetValue(TextInputProperty); }
            set { SetValue(TextInputProperty, value); }
        }

        public static readonly DependencyProperty TextInputProperty =
            DependencyProperty.Register("TextInput", typeof(string), typeof(InputBox),
                new PropertyMetadata(string.Empty));


        public string ErrorMessage
        {
            get {
               
                return (string)GetValue(ErrorMessageProperty);
            }
            set 
            {
                //errorMessage.Text = value;

                SetValue(ErrorMessageProperty, value);
            }
        }

        public static readonly DependencyProperty ErrorMessageProperty =
            DependencyProperty.Register("ErrorMessage", typeof(string), typeof(InputBox),
                new PropertyMetadata(string.Empty));


        private TextBox textBox;
        private TextBlock textBlock;
        private TextBox errorMessage;


        static InputBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(InputBox),
                new FrameworkPropertyMetadata(typeof(InputBox)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (this.Template != null)
            {
                textBox = this.Template.FindName("textBox", this) as TextBox;
                textBlock = this.Template.FindName("textBlock", this) as TextBlock;
                errorMessage = this.Template.FindName("errorMessage", this) as TextBox;

                textBox.TextChanged += textBox_TextChanged;
                textBlock.Text = TextName;
                ErrorMessage = errorMessage.Text;

                errorMessage.TextChanged += ErrorMessage_TextChanged; ;
               
            }
        }

        private void ErrorMessage_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((string)GetValue(ErrorMessageProperty) != string.Empty)
            {
                errorMessage.Visibility = Visibility.Visible;
            }
            else
            {
                errorMessage.Visibility = Visibility.Collapsed;
            }
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextInput = textBox.Text;
           // errorMessage.Text = ErrorMessage;
        }
    }
}