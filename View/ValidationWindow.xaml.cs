
using ModuleFour.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ModuleFour.View
{
    /// <summary>
    /// Логика взаимодействия для ValidationWindow.xaml
    /// </summary>
    public partial class ValidationWindow : Window
    {
        int index = 3;
        ServerRequest request = new ServerRequest();
        WordWriter writer = new WordWriter();
        public ValidationWindow()
        {
            InitializeComponent();
        }

        private async void OnGetRequestButtonClick(object sender, RoutedEventArgs e)
        {
            string url = "http://localhost:4444/TransferSimulator/fullName";
            string result = await request.GetRequestAsync(url);
            FullNameTextBlock.Text = GetFullNameFromString(result);
        }
        private string GetFullNameFromString(string result)
        {
            return result
                .Substring(result.IndexOf(":") + 2)
                .Replace("\"", "")
                .Replace("}", "");
        }
        private bool ContainsUnknownCharacters(string text)
        {
            
            return Regex.IsMatch(FullNameTextBlock.Text, @"[^а-яА-ЯёЁ0-9\s]");
        
        }




        public void ButtonCheckButtonClick(object sender, RoutedEventArgs e)
        {
            bool hasUnknownChars = ContainsUnknownCharacters(FullNameTextBlock.Text);
            ResultTextBlock.Text = hasUnknownChars ? "не успешно" : "успешно";
            string filePath = "C:\\Users\\admin\\Downloads\\ModuleFour-master\\ТестКейс.docx";
            int columnIndex = 1;
            string[] data = {$"{FullNameTextBlock.Text}", $"{ResultTextBlock.Text}"};
            writer.WriteToWordTable(filePath, index, columnIndex, data);
            index++;
        }
    }
}
