using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ADO_Task1.MVVM.ViewModel
{
    public class ViewModel
    {
        public ObservableCollection<TextBox> TextBoxess { get; set; }
        public ObservableCollection<Button> Buttons { get; set; }
        public RichTextBox ShowData { get; set; }


        public ViewModel()
        {
            TextBoxess = new ObservableCollection<TextBox>();
            Buttons = new ObservableCollection<Button>();
            ShowData = new RichTextBox();
            TextBoxess.Add(new TextBox() { Height = 25, ToolTip = "Book Name", VerticalContentAlignment = System.Windows.VerticalAlignment.Center, Margin = new System.Windows.Thickness(5, 0, 0, 0) });
            TextBoxess.Add(new TextBox() { Height = 25, ToolTip = "Book Pages", VerticalContentAlignment = System.Windows.VerticalAlignment.Center, Margin = new System.Windows.Thickness(5, 0, 0, 0) });
            TextBoxess.Add(new TextBox() { Height = 25, ToolTip = "Book YearPress", VerticalContentAlignment = System.Windows.VerticalAlignment.Center, Margin = new System.Windows.Thickness(5, 0, 0, 0) });
            TextBoxess.Add(new TextBox() { Height = 25, ToolTip = "ID Themes", VerticalContentAlignment = System.Windows.VerticalAlignment.Center, Margin = new System.Windows.Thickness(5, 0, 0, 0) });
            TextBoxess.Add(new TextBox() { Height = 25, ToolTip = "ID Category", VerticalContentAlignment = System.Windows.VerticalAlignment.Center, Margin = new System.Windows.Thickness(5, 0, 0, 0) });
            TextBoxess.Add(new TextBox() { Height = 25, ToolTip = "ID Author", VerticalContentAlignment = System.Windows.VerticalAlignment.Center, Margin = new System.Windows.Thickness(5, 0, 0, 0) });
            TextBoxess.Add(new TextBox() { Height = 25, ToolTip = "ID Press", VerticalContentAlignment = System.Windows.VerticalAlignment.Center, Margin = new System.Windows.Thickness(5, 0, 0, 0) });
            TextBoxess.Add(new TextBox() { Height = 25, ToolTip = "Comment", VerticalContentAlignment = System.Windows.VerticalAlignment.Center, Margin = new System.Windows.Thickness(5, 0, 0, 0) });
            TextBoxess.Add(new TextBox() { Height = 25, ToolTip = "Quantity", VerticalContentAlignment = System.Windows.VerticalAlignment.Center, Margin = new System.Windows.Thickness(5, 0, 5, 0) });
            TextBoxess.Add(new TextBox() { Height = 25, ToolTip = "The id of the book that will be deleted", VerticalContentAlignment = System.Windows.VerticalAlignment.Center, Margin = new System.Windows.Thickness(5, 0, 5, 0) });
            TextBoxess[1].TextChanged += CheckNumberValue;
            TextBoxess[2].TextChanged += CheckNumberValue;
            TextBoxess[3].TextChanged += CheckNumberValue;
            TextBoxess[4].TextChanged += CheckNumberValue;
            TextBoxess[5].TextChanged += CheckNumberValue;
            TextBoxess[6].TextChanged += CheckNumberValue;
            TextBoxess[8].TextChanged += CheckNumberValue;
            Buttons.Add(new Button() { Width = 130, Background = new SolidColorBrush(Colors.Green), Foreground = new SolidColorBrush(Colors.White), Content = "Insert", FontSize = 15, Padding = new System.Windows.Thickness(0, 0, 0, 5), Margin = new System.Windows.Thickness(0, 5, 0, 0) });
            Buttons.Add(new Button() { Width = 130, Background = new SolidColorBrush(Colors.Red), Foreground = new SolidColorBrush(Colors.White), Content = "Delete", FontSize = 15, Padding = new System.Windows.Thickness(-25, 0, 0, 0)});
            Buttons.Add(new Button() { Width = 230, Background = new SolidColorBrush(Colors.DodgerBlue), Foreground = new SolidColorBrush(Colors.White), Content = "Show", FontSize = 45, Margin = new System.Windows.Thickness(5, 5, 5, 5), Padding = new System.Windows.Thickness(-30, 0, 0, 0)});
            ShowData.Background = new SolidColorBrush(Color.FromRgb(30, 38, 41));
            ShowData.Foreground = new SolidColorBrush(Colors.DodgerBlue);
            ShowData.IsReadOnly = true;
            ShowData.Cursor = Cursors.Arrow;
            Buttons[0].Click += InsertNewBook;
            Buttons[1].Click += DeleteBook;
            Buttons[2].Click += ShowBookData;
        }

        private void ShowBookData(object sender, RoutedEventArgs e)
        {
            ShowData.Document.Blocks.Clear();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            conn.Open();
            SqlCommand command = conn.CreateCommand();
            command.CommandText = "select * from Books";

            using (var reader = command.ExecuteReader())
            {
                bool hasShowAdded = false;
                while (reader.Read())
                {
                    string temp = null;
                    if (!hasShowAdded)
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            if (i == 0)
                                temp = reader.GetName(i) + '\t';
                            else
                                temp += reader.GetName(i) + '\t';
                        }
                        ShowData.AppendText(temp + "\n\n");
                        hasShowAdded = true;
                    }
                    temp = (reader[0] + "\t" + reader[1] + "\t" + reader[2] + "\t" + reader[3] + "\t" + reader[4] + "\t" + reader[5] + "\t" + reader[6] + "\t" + reader[7] + "\t" + reader[8] + "\t" + reader[9]);
                    ShowData.AppendText(temp + "\n");
                }
            }
        }

        private void DeleteBook(object sender, RoutedEventArgs e)
        {
            if (TextBoxess[9].Text == null || Convert.ToInt32(TextBoxess[9].Text) <= 0 || TextBoxess[9].Text == "" )
                MessageBox.Show("Please enter a large id from scratch.");
            else
            {
                try
                {
                    using (var conn = new SqlConnection())
                    {
                        conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
                        conn.Open();
                        SqlParameter book_id = new SqlParameter();
                        book_id.ParameterName = "@book_id";
                        book_id.SqlDbType = SqlDbType.Int;
                        book_id.Value = Convert.ToInt32(TextBoxess[9].Text);
                        SqlCommand command = conn.CreateCommand();
                        command.CommandText = "sp_DeleteBook";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(book_id);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Book deleted succssesfully.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    string[] result = ex.Message.Split('\n');

                    MessageBox.Show(result[1], "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void CheckNumberValue(object sender, TextChangedEventArgs e)
        {
            TextBox temp = (TextBox)sender;
            if (Convert.ToString(temp.ToolTip) == "Book Name")
                work(0);
            else if (Convert.ToString(temp.ToolTip) == "Book Pages")
                work(1);
            else if (Convert.ToString(temp.ToolTip) == "Book YearPress")
                work(2);
            else if (Convert.ToString(temp.ToolTip) == "ID Themes")
                work(3);
            else if (Convert.ToString(temp.ToolTip) == "ID Category")
                work(4);
            else if (Convert.ToString(temp.ToolTip) == "ID Author")
                work(5);
            else if (Convert.ToString(temp.ToolTip) == "ID Press")
                work(6);
            else if (Convert.ToString(temp.ToolTip) == "Comment")
                work(7);
            else if (Convert.ToString(temp.ToolTip) == "Quantity")
                work(8);
        }
        private void work(int index)
        {
            TextBoxess[index].TextChanged -= CheckNumberValue;
            char[] tempInfo = new char[TextBoxess[index].Text.Length];
            int j = 0;
            for (int i = 0; i < TextBoxess[index].Text.Length; i++)
            {
                if (TextBoxess[index].Text[i] >= '0' && TextBoxess[index].Text[i] <= '9')
                {
                    tempInfo[j] = TextBoxess[index].Text[i];
                    j++;
                }
            }
            TextBoxess[index].Text = new string(tempInfo);
            TextBoxess[index].TextChanged += CheckNumberValue;
            TextBoxess[index].SelectionStart = TextBoxess[index].Text.Length;
            TextBoxess[index].SelectionLength = 0;
        }

        private void InsertNewBook(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBoxResult result;

            if (TextBoxess[0].Text == null || TextBoxess[0].Text == "")
            {
                result = MessageBox.Show("Please book name set.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                if (result == MessageBoxResult.OK)
                    TextBoxess[0].Focus();
            }
            else if (TextBoxess[1].Text == null || TextBoxess[1].Text == "" || Convert.ToInt32(TextBoxess[1].Text) < 20)
            {
                result = MessageBox.Show("Please book pages set.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                if (result == MessageBoxResult.OK)
                    TextBoxess[1].Focus();
            }
            else if (TextBoxess[2].Text == null || TextBoxess[2].Text == "" || Convert.ToInt32(TextBoxess[2].Text) < 2000)
            {
                result = MessageBox.Show("Please book year press set.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                if (result == MessageBoxResult.OK)
                    TextBoxess[2].Focus();
            }
            else if (TextBoxess[3].Text == null || TextBoxess[3].Text == "" || Convert.ToInt32(TextBoxess[3].Text) <= 0)
            {
                result = MessageBox.Show("Please book id themes set.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                if (result == MessageBoxResult.OK)
                    TextBoxess[3].Focus();
            }
            else if (TextBoxess[4].Text == null || TextBoxess[4].Text == "" || Convert.ToInt32(TextBoxess[4].Text) <= 0)
            {
                result = MessageBox.Show("Please book id category set.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                if (result == MessageBoxResult.OK)
                    TextBoxess[4].Focus();
            }
            else if (TextBoxess[5].Text == null || TextBoxess[5].Text == "" || Convert.ToInt32(TextBoxess[5].Text) <= 0)
            {
                result = MessageBox.Show("Please book id author set.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                if (result == MessageBoxResult.OK)
                    TextBoxess[5].Focus();
            }
            else if (TextBoxess[6].Text == null || TextBoxess[6].Text == "" || Convert.ToInt32(TextBoxess[6].Text) <= 0)
            {
                result = MessageBox.Show("Please book id press set.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                if (result == MessageBoxResult.OK)
                    TextBoxess[6].Focus();
            }
            else if (TextBoxess[7].Text == null || TextBoxess[7].Text == "")
            {
                result = MessageBox.Show("Please book comment set.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                if (result == MessageBoxResult.OK)
                    TextBoxess[7].Focus();
            }
            else if (TextBoxess[8].Text == null || TextBoxess[8].Text == "" || Convert.ToInt32(TextBoxess[8].Text) <= 0)
            {
                result = MessageBox.Show("Please book quantity set.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                if (result == MessageBoxResult.OK)
                    TextBoxess[8].Focus();
            }
            else
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;

                try
                {
                    connection.Open();
                    SqlParameter bookName = new SqlParameter();
                    bookName.ParameterName = "@bookName";
                    bookName.SqlDbType = SqlDbType.NVarChar;
                    bookName.Value = TextBoxess[0].Text;
                    SqlParameter pages = new SqlParameter();
                    pages.ParameterName = "@pages";
                    pages.SqlDbType = SqlDbType.Int;
                    pages.Value = Convert.ToInt32(TextBoxess[1].Text);
                    SqlParameter yearPress = new SqlParameter();
                    yearPress.ParameterName = "@yearPress";
                    yearPress.SqlDbType = SqlDbType.Int;
                    yearPress.Value = Convert.ToInt32(TextBoxess[2].Text);
                    SqlParameter id_themes = new SqlParameter();
                    id_themes.ParameterName = "@id_themes";
                    id_themes.SqlDbType = SqlDbType.Int;
                    id_themes.Value = Convert.ToInt32(TextBoxess[3].Text);
                    SqlParameter id_category = new SqlParameter();
                    id_category.ParameterName = "@id_category";
                    id_category.SqlDbType = SqlDbType.Int;
                    id_category.Value = Convert.ToInt32(TextBoxess[4].Text);

                    SqlParameter id_author = new SqlParameter();
                    id_author.ParameterName = "@id_author";
                    id_author.SqlDbType = SqlDbType.Int;
                    id_author.Value = Convert.ToInt32(TextBoxess[5].Text);

                    SqlParameter id_press = new SqlParameter();
                    id_press.ParameterName = "@id_press";
                    id_press.SqlDbType = SqlDbType.Int;
                    id_press.Value = Convert.ToInt32(TextBoxess[6].Text);

                    SqlParameter comment = new SqlParameter();
                    comment.ParameterName = "@comment";
                    comment.SqlDbType = SqlDbType.NVarChar;
                    comment.Value = TextBoxess[7].Text;

                    SqlParameter quantity = new SqlParameter();
                    quantity.ParameterName = "@quantity";
                    quantity.SqlDbType = SqlDbType.Int;
                    quantity.Value = Convert.ToInt32(TextBoxess[8].Text);


                    SqlCommand command = new SqlCommand("sp_AddBook", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(bookName);
                    command.Parameters.Add(pages);
                    command.Parameters.Add(yearPress);
                    command.Parameters.Add(id_themes);
                    command.Parameters.Add(id_category);
                    command.Parameters.Add(id_author);
                    command.Parameters.Add(id_press);
                    command.Parameters.Add(comment);
                    command.Parameters.Add(quantity);

                    command.ExecuteNonQuery();

                    MessageBox.Show("Book successfully added.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    string[] resultMessage = ex.Message.Split('\n');
                    MessageBox.Show(resultMessage[1]);
                }
            }
        }
    } 
} 