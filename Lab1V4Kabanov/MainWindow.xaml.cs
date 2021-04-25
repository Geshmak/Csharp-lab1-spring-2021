using ClassLibrary1;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab1V4Kabanov
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        private V4MainCollection V4MC;
        private PropertyClass PC;


        private void FilterDataCollection(object a,FilterEventArgs b) => b.Accepted = b.Item is V4DataCollection;
        private void FilterDataOnGrid(object a, FilterEventArgs b) => b.Accepted = b.Item is V4DataOnGrid;



        public MainWindow()
        {
            InitializeComponent();
            V4MC  = new V4MainCollection();
            PC = new PropertyClass( V4MC);
            
            //DataContext = V4MC;
            DataContext = PC;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("loaded");
            //DataContext = V4MC;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (ISaved())
                MessageBox.Show("closed");
        }
        
        private bool ISaved()
        {
            if (V4MC == null)
                return true;
            if (V4MC.IfChangedCollection == false)
            {
                return true;
            }
            MessageBoxResult result = MessageBox.Show("Хотите сохранить?", "Данные не сохранены", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                    if (dlg.ShowDialog() == true)
                    {
                        V4MC.Save(dlg.FileName);
                        //return true;
                    }
                    else 
                        MessageBox.Show("Данные не были сохранены");
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
            }
            else 
                return true;
        }
        private void FileNew(object sender, RoutedEventArgs e)
        {
            if (ISaved())
            {
                
                V4MC = new V4MainCollection();
                PC = new PropertyClass( V4MC);
                //DataContext = V4MC;
                //DataContext = PC;
            }
               
        }

        private void COpen(object sender, RoutedEventArgs e)
        {
            if (ISaved())
            {
                try
                {
                    Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                    if (dlg.ShowDialog() == true)
                    {
                        
                        V4MC.Load(dlg.FileName);
                        PC = new PropertyClass(V4MC);
                        DataContext = PC;

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
                
        }
        private void CSave(object sender, RoutedEventArgs e)
        {
            if (V4MC != null)
            {
                try
                {
                    SaveFileDialog dlg = new SaveFileDialog();

                    if (dlg.ShowDialog() == true)
                    {
                        V4MC.Save(dlg.FileName);

                    }
                    else
                        MessageBox.Show("Данные не были сохранены");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
                MessageBox.Show("Нет объекта");
        }

        private void CAddDefaults(object sender, RoutedEventArgs e)
        {
            if (V4MC == null)
                MessageBox.Show("Нет объекта");
            else
            {
                try
                {
                    V4MC.AddDefaults();
                    PC.PCUpt(V4MC);
                    //PC = new PropertyClass(V4MC);
                    DataContext = PC;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void CAddDefaultV4DataCollection(object sender, RoutedEventArgs e)
        {
            if (V4MC == null)
                MessageBox.Show("Нет объекта");
            else
            {
                try
                {
                    V4MC.AddExtrColl();
                    PC = new PropertyClass(V4MC);
                    DataContext = PC;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }
        }

        private void CAddDefaultV4DataOnGrid(object sender, RoutedEventArgs e)
        {
            if (V4MC == null)
                MessageBox.Show("Нет объекта");
            else
            {
                try
                {   
 
                    V4MC.AddExtrGrid();
                    PC = new PropertyClass(V4MC);
                    DataContext = PC;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }
        }

        private void CAddElementfromFile(object sender, RoutedEventArgs e)
        {
            if (V4MC == null)
                MessageBox.Show("Нет объекта");
            else
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                if (dlg.ShowDialog() == true)
                {
                    try
                    {
                        V4DataOnGrid V4MCF = new V4DataOnGrid(dlg.FileName);
                        V4MC.Add(V4MCF);
                        PC = new PropertyClass(V4MC);
                        DataContext = PC;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void CRemove(object sender, RoutedEventArgs e)
        {
            if (ListBox.SelectedItem != null)
            {
                try
                {
                    V4Data Selected = (V4Data)ListBox.SelectedItem;
                    V4MC.Remove(Selected.CInfo, Selected.CFrequency);
                    PC = new PropertyClass(V4MC);
                    DataContext = PC;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }       
        }

        private void Window_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(V4MC.MaxMagnitude.ToString());
        }

        private void AddCustomV4DCClick(object sender, RoutedEventArgs e)
        {
            //DataContext = V4MC;
            //DataContext = PC;
        }



        /*private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void button_Test_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            if (dlg.ShowDialog() == true)
                TextBlock_Echo.Text = dlg.FileName;
        }

        

        

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBox_Echo !=null && TextBlock_Echo != null)
            TextBlock_Echo.Text = TextBox_Echo.Text;
        }*/
    }
}
