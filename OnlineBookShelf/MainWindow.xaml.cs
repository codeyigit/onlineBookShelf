using Azure.Storage.Blobs.Models;
using Microsoft.Win32;
using OnlineBookShelf.Dal;
using OnlineBookShelf.ViewModels;
using System.IO.Compression;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OnlineBookShelf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ItemsViewModel itemsViewModel;
        public MainWindow()
        {
            InitializeComponent();
            itemsViewModel = new ItemsViewModel();
            Init();
        }

        private void Init()
        {
            var directories = new List<string>();
            directories.AddRange(itemsViewModel.Directories);
            cbDirectories.ItemsSource = directories;
            lbItems.ItemsSource = itemsViewModel.Items;
        }

        private void LbItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
         => DataContext = lbItems.SelectedItem as BlobItem;

        private void CbDirectories_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                itemsViewModel.Directory = cbDirectories.Text.Trim();
                cbDirectories.Text = itemsViewModel.Directory;
            }
        }
        private async void BtnDownloadAll_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Zip files (*.zip)|*.zip",
                FileName = "DownloadedFiles.zip"
            };

       
            if (saveFileDialog.ShowDialog() == true)
            {
                ProgressWindow progressWindow = new ProgressWindow();
                progressWindow.Show();

                try
                {
                    var items = lbItems.Items.Cast<BlobItem>().ToList();
                    int totalItems = items.Count;
                    if (totalItems > 0)
                    {
                        int completedItems = 0;
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                            {
                                foreach (var item in items)
                                {
                                    var blobClient = Repository.Container.GetBlobClient(item.Name);
                                    var entry = archive.CreateEntry(item.Name.Replace(ItemsViewModel.ForwardSlash, System.IO.Path.DirectorySeparatorChar.ToString()));

                                    using (var entryStream = entry.Open())
                                    using (var blobStream = await blobClient.OpenReadAsync())
                                    {
                                        await blobStream.CopyToAsync(entryStream);
                                    }

                                    completedItems++;
                                    int percentage = (int)((completedItems / (float)totalItems) * 100);
                                    progressWindow.UpdateProgress(percentage); 
                                }
                            }

                            using (var fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                            {
                                memoryStream.Seek(0, SeekOrigin.Begin);
                                await memoryStream.CopyToAsync(fileStream);
                            }
                        }
                        MessageBox.Show("All files have been downloaded and zipped successfully.", "Download Complete", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("There are no files to download.", "Download All", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    progressWindow.Close(); 
                }
            }
        }

        private void CbDirectories_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (itemsViewModel.Directories.Contains(cbDirectories.Text))
            {
                itemsViewModel.Directory = cbDirectories.Text;
                cbDirectories.SelectedItem = itemsViewModel.Directory;
            }
           
        }
        private async void BtnUpload_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                await itemsViewModel.UploadAsync(openFileDialog.FileName, cbDirectories.Text);
            }
            cbDirectories.Text = itemsViewModel.Directory;
        }

        private async void BtnDownload_Click(object sender, RoutedEventArgs e)
        {
            if (lbItems.SelectedItem is not BlobItem item)
            {
                return;
            }

            var saveFileDialog = new SaveFileDialog()
            {
                FileName = item.Name[(item.Name.LastIndexOf(ItemsViewModel.ForwardSlash) + 1)..], 
                Filter = "All files (*.*)|*.*" 
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                
                ProgressWindow progressWindow = new ProgressWindow();
                progressWindow.Show();

                try
                {
                    
                    await itemsViewModel.DownloadAsync(item, saveFileDialog.FileName);

                    
                    MessageBox.Show("File has been downloaded successfully.", "Download Complete", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                   
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                { 
                    progressWindow.Close();
                }

               
                cbDirectories.Text = itemsViewModel.Directory;
            }
            
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lbItems.SelectedItem is not BlobItem item)
            {
                return;
            }
            await itemsViewModel.DeleteAsync(item);
            cbDirectories.Text = itemsViewModel.Directory;
        }
    }
}