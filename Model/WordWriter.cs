using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Word = Microsoft.Office.Interop.Word;

namespace ModuleFour.Model
{
    public class WordWriter
    {
        public void WriteToWordTable(string filePath, int startRowIndex, int columnIndex, string[] data)
        {
            Word.Application wordApp = new Word.Application();
            wordApp.Visible = false;
            Word.Document wordDoc = null;

            try
            {
                wordDoc = wordApp.Documents.Open(filePath);
                Word.Table table = wordDoc.Tables[1];

                while (table.Rows.Count <= startRowIndex - 1)
                {
                    table.Rows.Add(); 
                }

                // Заполняем данные в указанные ячейки
                for (int i = 0; i < data.Length; i += 2)
                {
                    int currentRow = startRowIndex + (i / 2);

                    // Записываем первое значение в столбец columnIndex
                    table.Cell(currentRow, columnIndex).Range.Text = data[i];

                    // Если есть второе значение, записываем его в columnIndex + 2
                    if (i + 1 < data.Length)
                    {
                        table.Cell(currentRow, columnIndex + 2).Range.Text = data[i + 1];
                    }
                }

                wordDoc.Save();
                wordDoc.Close();
                wordApp.Quit();
                MessageBox.Show("Данные успешно записаны в таблицу");
            }
            catch (Exception ex)
            {
                try { wordDoc?.Close(); } catch { }
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
            finally
            {
                if (wordApp != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(wordApp);
                }
            }
        }
    }
}
