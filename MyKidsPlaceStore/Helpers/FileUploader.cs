using Domains.Models;
using ImageMagick;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.File;
using Serilog;
using Service.UnitOfWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyKidsPlaceStore.Helpers
{
    public class FileUploader
    {
        private string[] permittedExtensions = { ".jpg", ".jpeg", ".tiff", ".png" };
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IServiceUnitOfWork _serviceUnitOfWork;
        private CloudBlobClient cloudBlobClient;

        public FileUploader(IWebHostEnvironment webHostEnvironmen, IServiceUnitOfWork serviceUnitOfWork)
        {
            _webHostEnvironment = webHostEnvironmen;
            _serviceUnitOfWork = serviceUnitOfWork;
        }

        public async Task<string> PostFile(IFormFile File)
        {
            try
            {
                string randomFileName = Guid.NewGuid().ToString() + Path.GetExtension(File.FileName);
                string directory = @"/home/attachments/appimages";
                //string directory = "\\home\\attachments\\appimages";
                string filePath = Path.Combine(directory, randomFileName);
                string randomFileName2 = "";
                if (File.Length > 0)
                {
                    if (File.Length >= 5242880)
                    {
                        throw new ValidationException("File size is invalid, max file size is 5 MB");
                    }


                    //if (!Directory.Exists(directory))
                    //{
                    //    Directory.CreateDirectory(directory);
                    //}

                    string ext = Path.GetExtension(filePath).ToLowerInvariant();
                    if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
                    {
                        throw new ValidationException("File extention is not valid");
                    }

                    using var ms = new MemoryStream();
                    File.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    string s = Convert.ToBase64String(fileBytes);
                    var stream = System.IO.File.Create(filePath);
                    await File.CopyToAsync(stream);
                    stream.Position = 0;
                    using MagickImage image = new MagickImage(stream);
                    stream.Dispose();
                    image.Format = image.Format; // Get or Set the format of the image.
                    image.Strip();
                    image.Quality = 50; // This is the Compression level.
                    randomFileName2 = Guid.NewGuid().ToString() + Path.GetExtension(File.FileName);
                    string filePath2 = Path.Combine(directory, randomFileName2);
                    image.Write(filePath2);
                    System.IO.File.Delete(filePath);

                }

                return randomFileName2;
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public string CompressAllImages()
        {
            int counter = 0;
            try
            {
                List<ItemImages> itemImages = _serviceUnitOfWork.ItemImages.Value.GetAll().OrderBy(x => x.Id).ToList();
                foreach (var itemImage in itemImages)
                {
                    counter++;
                    string directory = "\\home\\attachments\\appimages";
                    string filePath = Path.Combine(directory, itemImage.ImagePath);
                    string ext = Path.GetExtension(filePath).ToLowerInvariant();
                    if (!File.Exists(filePath))
                    {
                        //Console.WriteLine("File Does not exist Id:"+itemImage.Id+"  Index:"+counter);
                        continue;
                    }
                    //using (var streamx = System.IO.File.OpenRead(filePath))
                    //{
                    //   var xFile = new FormFile(streamx, 0, streamx.Length, null, Path.GetFileName(streamx.Name))

                    //}


                    FileInfo fileInfo = new FileInfo(filePath);
                    //FileStream stream = fileInfo.OpenRead();
                    byte[] data = new byte[fileInfo.Length];
                    FileStream stream = fileInfo.OpenRead();
                    stream.Read(data, 0, data.Length);
                    
                    //var stream = System.IO.File.OpenRead(filePath);
                    var file = ReturnFormFile(stream, itemImage.ImagePath);
                    using var ms = new MemoryStream();
                    //file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    string s = Convert.ToBase64String(fileBytes);

                    stream.Position = 0;
                    using MagickImage image = new MagickImage(stream);
                    stream.Dispose();
                    image.Format = image.Format; // Get or Set the format of the image.
                    image.Strip();
                    image.Quality = 60; // This is the Compression level.
                    string randomFileName2 = Guid.NewGuid().ToString() + Path.GetExtension(filePath);
                    string filePath2 = Path.Combine(directory, randomFileName2);
                    image.Write(filePath2);
                    File.Delete(filePath);
                    itemImage.ImagePath = randomFileName2;
                    _serviceUnitOfWork.ItemImages.Value.UpdateAsync(itemImage);
                }
                return "";
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public IFormFile ReturnFormFile(FileStream result,string fileName)
        {
            var ms = new MemoryStream();
            try
            {
                result.CopyTo(ms);
                return new FormFile(ms, 0, ms.Length, fileName, fileName);
            }
            catch (Exception e)
            {
                ms.Dispose();
                throw;
            }
            finally
            {
                ms.Dispose();
            }
        }
        public static byte[] ReadToEnd(System.IO.Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }

        public bool DeleteFile(string fileName)
        {
            string path = Path.Combine(_webHostEnvironment.WebRootPath + "\\AppImages\\", fileName);
            if (File.Exists(path))
            {
                // If file found, delete it    
                File.Delete(Path.Combine(path));
            }
            return true;
        }
    }
}
