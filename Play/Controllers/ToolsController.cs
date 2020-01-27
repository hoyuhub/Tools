using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DatabaseUtils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Play.Handlers;
using Play.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Play.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ToolsController : ControllerBase
    {
        private SettingPath _settingPath;
        private IConfiguration _configuration;
        private readonly RedisCommon _redis;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ILogger<ToolsController> _logger = null;
        public ToolsController(IConfiguration configuration, RedisCommon client, IWebHostEnvironment hostingEnvironment, ILogger<ToolsController> logger)
        {
            //settingPath = new ConfigurationHelper().GetAppSettings<SettingPath>("SettingPath");
            _configuration = configuration;
            var settingPath = new SettingPath();
            configuration.GetSection("SettingPath").Bind(settingPath);
            _settingPath = settingPath;
            _redis = client;
            this._hostingEnvironment = hostingEnvironment;
            this._logger = logger;

        }


        [HttpGet(Name = "RenameAllVideo")]
        //重命名所有视频文件
        public void RenameAllVideo()
        {
            try
            {
                string path = _settingPath.VideoFilePath;
                string[] files = Directory.GetFiles(path, "*.mp4");
                foreach (var item in files)
                {
                    string directory = Path.GetDirectoryName(item);
                    //string extension = Path.GetExtension(item);
                    string fileName = Path.GetFileName(item);
                    fileName = fileName.Replace(" ", "-");
                    System.IO.File.Move(item, string.Format("{0}/{1}", directory, fileName));
                }
            }
            catch (Exception e ) {
                _logger.LogError(e.Message);
                throw e;
            }
        }
        [HttpGet(Name = "DelImg")]
        //删除所有视频缩略图
        public void DelImg()
        {
            try
            {
                string imgPath = _settingPath.VideoFilePath;
                if (Directory.Exists(string.Format("{0}/img", imgPath)))
                    Directory.Delete(string.Format("{0}/img", imgPath), true);
            }
            catch (Exception e) {
                _logger.LogError(e.Message);
                throw e;
            }
        }

        [HttpGet(Name = "VideoInit")]
        public void VideoInit()
        {
            try
            {
                new VideosInit(_settingPath, _redis,_logger).Init();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw e;
            }
        }

    }
}
