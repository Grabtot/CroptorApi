using Croptor.Application.Common.Interfaces;

namespace Croptor.Infrastructure.Files
{
    public class FilesRemover : IFilesRemover
    {
        public void RemoveFilesIn(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(path);

                    foreach (FileInfo file in directoryInfo.GetFiles())
                    {
                        file.Delete();
                    }

                    foreach (DirectoryInfo subDirectory in directoryInfo.GetDirectories())
                    {
                        subDirectory.Delete(true);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while removing files in {path}: {ex.Message}");
            }
        }
    }
}
