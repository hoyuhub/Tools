# 注意

由于一些原因不得不放弃这个项目，以后所有的工具包移致MyTools

---





# 存放朕的一些工具


1. VideoPlay加载视频实现在线播放
2. PythonLearn学习Python
3. DatabaseUtils数据库工具类

### VideoPlay

* 视频文件存放在本地磁盘中。
* ffmpeg获取视频的第一帧图片，用于展示视频封面。
* html5 video绑定视频源。
* bootstrap 控制整个页面布局。
* swagger 实现WebApi help page （暂时需要手动配置虚拟目录位置以确保swagger ui能够访问swagger.json）
* npm获取video js 项目添加到本项目中，播放页面使用video js作为播放器使用

**使用方法**

1. 需要.net core 2.1的运行环境
2. 修改配置
```
{
  "SettingPath": {
    "VideoFilePath": "C:\\Users\\89275\\Desktop\\Projects\\mv",
    "FfmpegPath": "C:/Users/89275/Desktop/Projects/mv/ffmpeg.exe",
    "FtpPath": "http://192.168.254.1/videofile",
    "VirtualPath": "/videoplay"
  },
  "RedisPath":"192.168.0.108:6379"
}
```
**Windows**

* VideoFilePath：本地视频文件路径
* FfmpegPath：ffmpeg.exe路径 
* FtpPath：视频访问地址，IIS创建网站目录和VideoFilePath一样
* VirtualPath：如果有虚拟目录
* RedisPath：如果不写默认localhost

**Linux**
利用Nginx实现

### PythonLearn

> 这是一个Python的学习项目，对于Python 爬虫很感兴趣，打算搞懂Python 并能粗略的写一两个小爬虫，作为其他项目的数据之源。

### DatabaseUtils

> 这是一个数据库工具类，将积累数据库通用方法
