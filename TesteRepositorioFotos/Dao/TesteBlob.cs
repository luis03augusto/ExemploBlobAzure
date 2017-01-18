using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using TesteRepositorioFotos.Models;

namespace TesteRepositorioFotos.Dao
{
    public class TesteBlob
    {
        public CloudStorageAccount storageAccount;
        public TesteBlob(string AccountNome, string AccountKey)
        {
            string UserConnection = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", AccountNome, AccountKey);
            storageAccount = CloudStorageAccount.Parse(UserConnection);
            //< add key = "StorageBlob" value = "DefaultEndpointsProtocol=https;AccountName=foodtruckimg;AccountKey=8EdkctIIc1w/00L+2YFVIPhn27SDrDHk/NZLAxa0m7pKFeh5EQUkUe6Ud7bhhYplavJpFdyrGv5r01V0KJBQsQ==" />
            //cloudstorageaccount storageaccount = cloudstorageaccount.parse(
            //cloudconfigurationmanager.getsetting("storageblob"));

        }        

       public CloudBlockBlob UploadBlob(string BlobName, string ContainerName, Stream strem)
        {
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(ContainerName.ToLower());
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(BlobName);
            //blockBlob.UploadFromStream(strem);

            try
            {
                blockBlob.UploadFromStream(strem);
                return blockBlob;
            }
            catch (Exception e)
            {
                var r = e.Message;
                return null;
            }
        }
        public void DeletaBlob(string BlobNome, string ContainerNome)
        {
            CloudBlobClient blobCLiente = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobCLiente.GetContainerReference(ContainerNome);
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(BlobNome);
            blockBlob.Delete();
        }
    }
}