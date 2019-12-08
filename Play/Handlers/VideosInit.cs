using DatabaseUtils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Play.Controllers;
using Play.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace Play.Handlers
{
    public class VideosInit
    {
        private SettingPath settingPath;
        private readonly RedisCommon _redis;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ILogger<ToolsController> _logger = null;


        public VideosInit(SettingPath settingPath, RedisCommon client, IWebHostEnvironment hostingEnvironment, ILogger<ToolsController> logger)
        {
            this.settingPath = settingPath;
            this._redis = client;
            this._hostingEnvironment = hostingEnvironment;
            this._logger = logger;
        }

        //初始化，将获取到的视频数据存放到redis中
        public bool Init()
        {
            try
            {
                List<VideoInfoModel> listVideoInfo = GetVideoList();
                _redis.FlushAll();
                listVideoInfo.ForEach(d =>
                {
                    int videoInfoId = Convert.ToInt32(_redis.GetData().StringIncrement("VIDEOINFO:ID", 1));
                    d.Id = videoInfoId.ToString();
                    _redis.GetData().HashSet(string.Format("VIDEOINFO:{0}", videoInfoId), Extenions.ModelToHashEntry(d));
                });
                return true;
            }
            catch (System.Exception e)
            {
                throw e;
            }

        }


        //获取所有视频信息
        public List<VideoInfoModel> GetVideoList()
        {
            try
            {
                string path = settingPath.VideoFilePath;
                string[] files = Directory.GetFiles(path, "*.mp4");
                List<VideoInfoModel> listVideoInfo = new List<VideoInfoModel>();
                for (int i = 0; i < files.Length; i++)
                {
                    try
                    {
                        string imgPath = Path.GetDirectoryName(files[i]) + "/img/" + Path.GetFileName(files[i]) + ".jpg";
                        //imgPath = imgPath.Replace(@"\", "/");
                        string videoPath = files[i].Replace(settingPath.VideoFilePath, settingPath.FtpPath);
                        videoPath = videoPath.Replace(@"\", "/");
                        if (System.IO.File.Exists(imgPath))
                        {
                            imgPath = imgPath.Replace(settingPath.VideoFilePath, settingPath.FtpPath).ToString();
                        }
                        else
                        {
                            imgPath = GetPicFromVideo(files[i]).ToString();
                        }
                        GetMovInfo(settingPath.FfmpegPath, files[i], out int? width, out int? heigth, out string duration);

                        listVideoInfo.Add(new VideoInfoModel()
                        {
                            ImgPath = imgPath,
                            VideoPath = videoPath,
                            Width = width.ToString(),
                            Height = heigth.ToString(),
                            Duration = duration
                        });
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e.Message);
                    }
                }
                return listVideoInfo;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //给视频文件创建缩略图（按照原始比例）
        public string GetPicFromVideo(string VideoName)
        {
            try
            {
                string imgPath = Path.GetDirectoryName(VideoName) + "/img";
                if (!Directory.Exists(imgPath))
                {
                    Directory.CreateDirectory(imgPath);
                }
                string PicName = string.Format("{0}/{1}.jpg", imgPath, Path.GetFileName(VideoName));//视频图片的名字，绝对路径

                //ProcessStartInfo startInfo = new ProcessStartInfo(settingPath.FfmpegPath)
                var path = _hostingEnvironment.WebRootPath + "/ffmpeg.exe";
                ProcessStartInfo startInfo = new ProcessStartInfo(path)
                {
                    CreateNoWindow = true,

                    // startInfo.Arguments = " -i " + VideoName                    //视频路径
                    //                 + " -r 1"                               //提取图片的频率
                    //                 + " -y -f image2 -ss " + CutTimeFrame   //设置开始获取帧的视频时间
                    //                 + " -t 513 -s " + WidthAndHeight     //设置图片的分辨率
                    //                 + " " + PicName + ""; //输出的图片文件名，路径前必须有空格

                    Arguments = string.Format(" -i  \"{0}\"  -ss 00:00:10 -f image2  \"{1}\" ", VideoName, PicName)
                };

                try
                {
                    Process pro = Process.Start(startInfo);

                    //Thread.Sleep(5000);//线程挂起，等待ffmpeg截图完毕
                }
                catch (Exception e)
                {
                    throw e;
                }


                //返回视频图片完整路径
                for (int i = 0; i < 10; i++)
                {
                    if (System.IO.File.Exists(PicName))
                        return PicName.Replace(settingPath.VideoFilePath, settingPath.FtpPath);
                    else
                        Thread.Sleep(1000);
                }
                return PicName.Replace(settingPath.VideoFilePath, settingPath.FtpPath);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //获取视频信息
        public void GetMovInfo(string ffmpegPath, string videoFilePath, out int? width, out int? height, out string time)
        {
            try
            {
                //判断文件是否存在
                if (!System.IO.File.Exists(videoFilePath))
                {
                    width = null;
                    height = null;
                }

                //执行命令获取该文件的一些信息 
                //string ffmpegPath = new FileInfo(Process.GetCurrentProcess().MainModule.FileName).DirectoryName + @"\ffmpeg.exe";
                var path = _hostingEnvironment.WebRootPath + "/ffmpeg.exe";
                ProcessStartInfo startInfo = new ProcessStartInfo(path)
                {
                    CreateNoWindow = true,
                    Arguments = string.Format(" -i  \"{0}\" ", videoFilePath)
                };

                ExecuteCommand(startInfo, out string output, out string error);
                if (string.IsNullOrEmpty(error))
                {
                    width = null;
                    height = null;
                }

                //通过正则表达式获取信息里面的宽度信息
                Regex regex = new Regex("(\\d{2,4})x(\\d{2,4})", RegexOptions.Compiled);
                Match m = regex.Match(error);
                if (m.Success)
                {
                    width = int.Parse(m.Groups[1].Value);
                    height = int.Parse(m.Groups[2].Value);
                }
                else
                {
                    width = null;
                    height = null;
                }

                regex = new Regex(" ([0-1][0-9]|2[0-3]):([0-5][0-9]):([0-5][0-9]).(\\d{2})", RegexOptions.Compiled);
                m = regex.Match(error);
                if (m.Success)
                {
                    time = m.Value.Substring(1, m.Value.Length - 4);
                }
                else
                {
                    time = null;
                }
            }
            catch (Exception e)
            {
                width = null;
                height = null;
                time = null;
                throw e;
            }
        }

        //执行ffmpeg 获取结果
        public static void ExecuteCommand(ProcessStartInfo startInfo, out string output, out string error)
        {
            try
            {
                //创建一个进程
                Process pc = new Process();
                pc.StartInfo = startInfo;
                pc.StartInfo.UseShellExecute = false;
                pc.StartInfo.RedirectStandardOutput = true;
                pc.StartInfo.RedirectStandardError = true;
                pc.StartInfo.CreateNoWindow = true;
                //启动进程
                pc.Start();

                //准备读出输出流和错误流
                string outputData = string.Empty;
                string errorData = string.Empty;
                pc.BeginOutputReadLine();
                pc.BeginErrorReadLine();

                pc.OutputDataReceived += (ss, ee) =>
                {
                    outputData += ee.Data;
                };

                pc.ErrorDataReceived += (ss, ee) =>
                {
                    errorData += ee.Data;
                };

                //等待退出
                pc.WaitForExit();

                //关闭进程
                pc.Close();

                //返回流结果
                output = outputData;
                error = errorData;
            }
            catch (Exception e)
            {
                output = null;
                error = null;
                throw e;
            }
        }

    }
}