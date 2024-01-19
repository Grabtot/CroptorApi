using Croptor.Application.Common.Interfaces;

namespace Croptor.Api.Services
{
    public class ArchiveRemover : BackgroundService
    {
        private readonly IFilesRemover _filesRemover;
        private readonly IHostEnvironment _environment;

        public ArchiveRemover(IFilesRemover filesRemover, IHostEnvironment environment)
        {
            _filesRemover = filesRemover ?? throw new ArgumentNullException(nameof(filesRemover));
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                string archivePath = Path.Combine(_environment.ContentRootPath, "wwwroot/archives");
                _filesRemover.RemoveFilesIn(archivePath);

                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            }
        }
    }



}
