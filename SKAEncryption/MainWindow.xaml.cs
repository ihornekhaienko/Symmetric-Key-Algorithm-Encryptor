using Microsoft.Win32;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SKAEncryption
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DESEncryptor des;
        AESEncryptor aes;
        TripleDESEncryptor tdes;

        public MainWindow()
        {
            InitializeComponent();

            des = new DESEncryptor();
            aes = new AESEncryptor();
            tdes = new TripleDESEncryptor();
        }
        private void open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
                fileTB.Text = File.ReadAllText(openFileDialog.FileName);
        }

        private void saveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, fileTB.Text);
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void about_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Created by\nihornekhaienko\non 01.24.21");
        }

        private void encrypt_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(keyTB.Text) || string.IsNullOrEmpty(ivTB.Text))
                return;

            string key = keyTB.Text;
            string iv = ivTB.Text;
            string selectedMethod = (string)methodPanel.Children.OfType<RadioButton>()
                 .FirstOrDefault(r => r.IsChecked.HasValue && r.IsChecked.Value).Content;

            switch (selectedMethod)
            {
                case "DES":
                    {                    
                        des.GenerateKey(key, iv);

                        fileTB.Text = des.Encrypt(fileTB.Text);
                        break;
                    }
                case "AES":
                    {
                        aes.GenerateKey(key, iv);

                        fileTB.Text = aes.Encrypt(fileTB.Text);
                        break;
                    }
                case "Triple DES":
                    {
                        tdes.GenerateKey(key, iv);

                        fileTB.Text = tdes.Encrypt(fileTB.Text);
                        break;
                    }
            }
        }

        private void decrypt_Click(object sender, RoutedEventArgs e)
        {
            string selectedMethod = (string)methodPanel.Children.OfType<RadioButton>()
                 .FirstOrDefault(r => r.IsChecked.HasValue && r.IsChecked.Value).Content;

            switch (selectedMethod)
            {
                case "DES":
                    {
                        fileTB.Text = des.Decrypt(fileTB.Text);
                        break;
                    }
                case "AES":
                    {
                        fileTB.Text = aes.Decrypt(fileTB.Text);
                        break;
                    }
                case "Triple DES":
                    {
                        fileTB.Text = tdes.Decrypt(fileTB.Text);
                        break;
                    }
            }
        }

        private void random_Click(object sender, RoutedEventArgs e)
        {
            string selectedMethod = (string)methodPanel.Children.OfType<RadioButton>()
                 .FirstOrDefault(r => r.IsChecked.HasValue && r.IsChecked.Value).Content;

            switch (selectedMethod)
            {
                case "DES":
                    {
                        var keys = des.RandomKey();
                        keyTB.Text = keys.Item1;
                        ivTB.Text = keys.Item2;
                        break;
                    }
                case "AES":
                    {
                        var keys = aes.RandomKey();
                        keyTB.Text = keys.Item1;
                        ivTB.Text = keys.Item2;
                        break;
                    }
                case "Triple DES":
                    {
                        var keys = tdes.RandomKey();
                        keyTB.Text = keys.Item1;
                        ivTB.Text = keys.Item2;
                        break;
                    }
            }
        }
    }
}
