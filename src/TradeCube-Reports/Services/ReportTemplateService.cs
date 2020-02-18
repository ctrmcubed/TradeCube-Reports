using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;
using TradeCube_Reports.Constants;
using TradeCube_Reports.Messages;
using TradeCube_Reports.Models;

namespace TradeCube_Reports.Services
{
    public class ReportTemplateService : IReportTemplateService
    {
        private readonly IHostEnvironment hostEnvironment;
        private readonly ILogger<ReportTemplateService> logger;

        public ReportTemplateService(IHostEnvironment hostEnvironment, ILogger<ReportTemplateService> logger)
        {
            this.hostEnvironment = hostEnvironment;
            this.logger = logger;
        }

        public async Task<ApiResponseWrapper<ReportTemplate>> ReportTemplate(string templateType)
        {
            var separatorChar = Path.DirectorySeparatorChar;

            async Task<ApiResponseWrapper<ReportTemplate>> ReadFile(string fileName)
            {
                try
                {
                    var file = await this.ReadFile($"{hostEnvironment.ContentRootPath}{separatorChar}Templates{separatorChar}{fileName}");

                    return new ApiResponseWrapper<ReportTemplate>(ApiConstants.SuccessResult, new ReportTemplate { Html = file });
                }
                catch (Exception e)
                {
                    logger.LogError($"The template could not be read ({templateType})", e.Message);
                    return new ApiResponseWrapper<ReportTemplate>(ApiConstants.FailedResult, new ReportTemplate()) { Message = e.Message };
                }
            }

            return templateType switch
            {
                "confirmation" => await ReadFile(TemplateConstants.ConfirmationTemplateFile),
                _ => new ApiResponseWrapper<ReportTemplate> { Status = ApiConstants.FailedResult, Message = $"Unknown template type ({templateType})" }
            };
        }

        private async Task<string> ReadFile(string filename)
        {
            try
            {
                using var sr = new StreamReader(filename);

                return await sr.ReadToEndAsync();
            }
            catch (IOException e)
            {
                logger.LogError($"The file could not be read ({filename})", e.Message);
                throw;
            }
        }
    }
}
