namespace Play.Models
{
    public class SettingPath
    {
        //视频文件路径
        public string VideoFilePath { get; set; }
        //ffmpeg路径
        public string FfmpegPath { get; set; }
        //ftp路径
        public string FtpPath { get; set; }
        //网站虚拟目录
        public string VirtualPath { get; set; }
    }
}
