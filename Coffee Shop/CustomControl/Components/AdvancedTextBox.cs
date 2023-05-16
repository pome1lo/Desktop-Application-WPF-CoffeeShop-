using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CustomControl
{
    [TemplatePart(Name = "PartTextBox", Type = typeof(TextBox))]
    [TemplatePart(Name = "PartIcon", Type = typeof(Image))]
    public class AdvancedTextBox : Control
    {
        #region Constructor

        static AdvancedTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AdvancedTextBox),
                new FrameworkPropertyMetadata(typeof(AdvancedTextBox)));
        }

        #endregion

        #region Dependency property

        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(TextBoxType), typeof(AdvancedTextBox),
                new FrameworkPropertyMetadata(TextBoxType.Clear, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(string), typeof(AdvancedTextBox),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty FullPathProperty =
            DependencyProperty.Register("FullPath", typeof(string), typeof(AdvancedTextBox),
                new PropertyMetadata(string.Empty));

        #endregion

        #region Property

        public TextBoxType Type
        {
            get { return (TextBoxType)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }
        public string Content
        {
            get { return (string)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        public string FullPath
        {
            get { return (string)GetValue(FullPathProperty); }
            set { SetValue(FullPathProperty, value); }
        }

        #endregion

        #region Fields

        private TextBox textBox;
        private Image image;

        #endregion

        #region Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (this.Template != null)
            {
                textBox = this.Template.FindName("PartTextBox", this) as TextBox;
                image = this.Template.FindName("PartIcon", this) as Image;
               
                if (textBox != null)
                {
                    if (Type == TextBoxType.BrowseFile && image != null)
                    {
                        image.PreviewMouseDown += BrowseFile;
                    }
                    else if (Type == TextBoxType.BrowseFolder)
                    {
                        image.PreviewMouseDown += BrowseFolder;
                    }
                    else
                    {
                        image.PreviewMouseDown += Clear;
                    }
                }
            }
        }

        private void Clear(object sender, MouseButtonEventArgs e)
        {
            textBox.Clear();
        }

        private void BrowseFolder(object sender, MouseButtonEventArgs e)
        {
            var folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            if (folderBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox.Text = folderBrowser.SelectedPath;
            }
        }

        private void BrowseFile(object sender, MouseButtonEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                textBox.Text = openFileDialog.FileName;
                FullPath= openFileDialog.FileName;

                string fileName = textBox.Text.Split(new char[] { '\\' }).Last();

                try
                {
                    File.Copy(textBox.Text, $@"../../../StaticFiles/Img/{fileName}");
                }
                catch { }
                Content = fileName;
            }
        }

        #endregion
    }

    public enum TextBoxType
    {
        Clear,
        BrowseFile,
        BrowseFolder,
    }
}
