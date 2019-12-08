namespace Play.Models
{
    public class VideoInfoModel
    {
        public string Id { get; set; }
        //图片展示路径
        public string ImgPath { get; set; }
        //视频播放路径
        public string VideoPath { get; set; }
        //视频宽度
        public string Width { get; set; }
        //视频高度
        public string Height { get; set; }
        //视频时长
        public string Duration { get; set; }

    }
}