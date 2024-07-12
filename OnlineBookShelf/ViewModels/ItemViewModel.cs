using Azure.Storage.Blobs.Models;
using OnlineBookShelf.Dal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookShelf.ViewModels
{
    internal class ItemsViewModel
    {
        public const string ForwardSlash = "/";

        public ObservableCollection<BlobItem> Items { get; }
        public ObservableCollection<String> Directories { get; }

        private string? directory;
        public string? Directory
        {
            get => directory;
            set
            {
                directory = value;
                Refresh();
            }
        }
        public ItemsViewModel()
        {
            Items = new ObservableCollection<BlobItem>();
            Directories = new ObservableCollection<string> { "ALL" };
            Refresh();

        }


        private void Refresh()
        {
            Directories.Clear();
            Items.Clear();
            Directories.Add("ALL"); 

            var allItems = Repository.Container.GetBlobs().ToList();

            foreach (var item in allItems)
            {
                if (item.Name.Contains(ForwardSlash))
                {
                    var dir = item.Name[..item.Name.LastIndexOf(ForwardSlash)];
                    if (!Directories.Contains(dir))
                    {
                        Directories.Add(dir);
                    }
                }
            }

           
            if (Directory == "ALL")
            {
                foreach (var item in allItems)
                {
                    Items.Add(item);
                }
            }
            else
            {
                foreach (var item in allItems)
                {
                    if (string.IsNullOrEmpty(Directory) && !item.Name.Contains(ForwardSlash))
                    {
                        Items.Add(item);
                    }
                    else if (item.Name.StartsWith($"{Directory}{ForwardSlash}"))
                    {
                        Items.Add(item);
                    }
                }
            }
        }
        public async Task DownloadAllAsync(List<BlobItem> items, string filePath)
        {
            ProgressWindow progressWindow = new ProgressWindow();
            progressWindow.Show();

            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    foreach (var item in items)
                    {
                        var blobClient = Repository.Container.GetBlobClient(item.Name);
                       
                        var entryName = item.Name.Replace(ForwardSlash, Path.DirectorySeparatorChar.ToString());
                        var entry = archive.CreateEntry(entryName);

                        using (var entryStream = entry.Open())
                        using (var blobStream = await blobClient.OpenReadAsync())
                        {
                            await blobStream.CopyToAsync(entryStream);
                        }
                    }
                }

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    await memoryStream.CopyToAsync(fileStream);
                }
            }
        }

        public async Task UploadAsync(string path, string directory)
        {
            var filename = path[(path.LastIndexOf(Path.DirectorySeparatorChar) + 1)..];
            if (!string.IsNullOrEmpty(directory))
            {
                filename = $"{directory}{ForwardSlash}{filename}";
            }
            using var fs = File.OpenRead(path);
            await Repository.Container.GetBlobClient(filename).UploadAsync(fs, true);
            Refresh();
        }
        public async Task DownloadAsync(BlobItem item, string path)
        {
            using var fs = File.OpenWrite(path);
            await Repository.Container.GetBlobClient(item.Name).DownloadToAsync(fs);
        }
        public async Task DeleteAsync(BlobItem item)
        {
            await Repository.Container.GetBlobClient(item.Name).DeleteAsync();
            Refresh();
        }

    }
}
