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
        //private void OnCollectionChange(object sender, NotifyCollectionChangedEventArgs args)
        //{
        //    DataContext = null;
        //    DataContext = V4MC;
        //    //MessageBox.Show(DataContext.ToString());
        //}

        private V4MainCollection V4MC;

        private void FilterDataCollection(object a,FilterEventArgs b) => b.Accepted = b.Item is V4DataCollection;
        private void FilterDataOnGrid(object a, FilterEventArgs b) => b.Accepted = b.Item is V4DataOnGrid;



        public MainWindow()
        {
            InitializeComponent();
            DataContext = V4MC;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("loaded");
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (ISaved())
                MessageBox.Show("closed");
        }
        //private void ChangeV4MC(V4MainCollection subj,bool fl=false)
        //{
        //    if (V4MC != null)
        //        V4MC.CollectionChanged -= OnCollectionChange;
        //    subj.CollectionChanged += OnCollectionChange;
        //    V4MC = subj;
        //    if (fl)
        //        V4MC.IfChangedCollection = true;
        //    OnCollectionChange(this, null);             
        //}
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
                //            ChangeV4MC(new V4MainCollection());
                V4MC = new V4MainCollection();
                DataContext = V4MC;
                //V4MC = new V4MainCollection();
                //V4MC.IfChangedCollection = true;
            }
                /*V4MC.AddDefaults();
            ListBox.ItemsSource = null;
            ListBox.ItemsSource = V4MC.list;*/
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
                        //            ChangeV4MC(new V4MainCollection());
                        V4MC.Load(dlg.FileName);
                        //            ChangeV4MC(V4MC);

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
                    //            ChangeV4MC(V4MC, true);

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
