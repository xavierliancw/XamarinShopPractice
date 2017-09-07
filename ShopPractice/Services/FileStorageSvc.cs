using System;
using PCLStorage;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace ShopPractice
{
    public static class FileStorageSvc
    {
        private static IFolder root = FileSystem.Current.LocalStorage;

        /// <summary>
        /// Obtains a folder from the root file. It'll create and return a
        /// new folder if the one specified doesn't exist.
        /// </summary>
        /// <returns>A reference to the folder async.</returns>
        /// <param name="folderName">Folder name.</param>
        public async static Task<IFolder> GetFolderAsync(string folderName)
        {
            return await root.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);
        }

        /// <summary>
        /// Obtains the specified file within the specified folder. If none
        /// exist, then a new file is created with the name specified.
        /// </summary>
        /// <returns>The new file async.</returns>
        /// <param name="fileName">File name.</param>
        public async static Task<IFile> GetFileAsync(string fileName, IFolder fromFolder)
        {
            return await fromFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
        }

        public async static Task<bool> FolderExistsAsync(string folderName)
        {
            ExistenceCheckResult exists = await root.CheckExistsAsync(folderName);
            if (exists == ExistenceCheckResult.FolderExists)
            {
                return true;
            }
            return false;
        }

        public async static Task<bool> FileExistsAsync(string fileName)
        {
            ExistenceCheckResult exists = await root.CheckExistsAsync(fileName);
            if (exists == ExistenceCheckResult.FileExists)
            {
                return true;
            }
            return false;
        }
    }
}
