using DatabaseUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Play.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;

namespace Play.Controllers
{

    public class VideosController : Controller
    {
        private readonly IDatabase _redis;
        private readonly int _pageSize = 14;
        private int _start = 1, _end = 0;
        public VideosController(RedisCommon client)
        {
            _redis = client.GetData();
        }


        //视频网站首页
        //[HttpGet]
        public IActionResult HomePage(int pageIndex)
        {
            try
            {
                int videoInfoId = GetVideoInfoId();
                int maxPageIndex = videoInfoId / _pageSize;
                if (maxPageIndex - 1 == pageIndex)
                    pageIndex--;
                if (pageIndex < 0)
                    pageIndex = 0;
                if (pageIndex != 0)
                {
                    _start = videoInfoId - (pageIndex * _pageSize);
                }
                _end = _start + _pageSize;
                var model = GetVideoInfo(_start, _end);
                ViewBag.pageIndex = pageIndex;
                return View(model);
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        //播放页面
        public IActionResult Play(string imgPath, string playPath)
        {
            ViewData["imgPath"] = imgPath;
            ViewData["playPath"] = playPath;
            return View();
        }
        private static string imgPath, playPath;
        public IActionResult VideoPlay(string videoId)
        {
            VideoInfoModel videoInfo = GetVideoInfoById(videoId);
            imgPath = videoInfo.ImgPath;
            playPath = videoInfo.VideoPath;
            return RedirectToAction("Play", "Videos");
        }

        public List<VideoInfoModel> HeaderInit()
        {
            try
            {
                int videoinfoId = GetVideoInfoId();
                return GetVideoInfo(videoinfoId, videoinfoId - 8);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //firstRow
        public List<VideoInfoModel> FirstRowInit()
        {
            try
            {
                int videoInfoId = GetVideoInfoId();
                return GetVideoInfo(videoInfoId - 8, videoInfoId - 8 - 5);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //分页查询数据，每页默认十二条数据
        //参数
        //pageIndex 默认为1
        public List<VideoInfoModel> GetPage(int pageIndex)
        {

            try
            {
                int start = 0, end = 0, pageSize = 12;
                int videoInfoId = GetVideoInfoId();
                start = videoInfoId - 12 - ((pageIndex - 1) * pageSize);
                end = start - 11;
                return GetVideoInfo(start, end);
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }


        //获取视频信息
        public List<VideoInfoModel> GetVideoInfo(int start, int end)
        {
            try
            {
                List<VideoInfoModel> listVideoInfo = new List<VideoInfoModel>();
                for (int i = start; i <= end; i++)
                {
                    string key = string.Format("VIDEOINFO:{0}", i);
                    if (_redis.KeyExists(key))
                    {
                        listVideoInfo.Add(Extenions.HashEntryToModel<VideoInfoModel>(_redis.HashGetAll(key)));
                    }
                    else { break; }


                }
                return listVideoInfo;

            }
            catch (Exception e)
            {

                throw e;
            }

        }

        //获取视频信息
        public VideoInfoModel GetVideoInfoById(string videoId)
        {
            try
            {
                string key = string.Format("VIDEOINFO:{0}", videoId);
                VideoInfoModel videoInfo = Extenions.HashEntryToModel<VideoInfoModel>(_redis.HashGetAll(key));
                return videoInfo;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //获取视频ID
        public int GetVideoInfoId()
        {
            try
            {
                return Convert.ToInt32(_redis.StringGet("VIDEOINFO:ID"));
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        //获取视频尾页编码
        public int GetPageLastIndex()
        {
            try
            {
                int pageLastIndex = 0;
                int totalCount = GetVideoInfoId() - 12 - 1;
                pageLastIndex = totalCount % 12 == 0 ? totalCount / 12 : totalCount / 12 + 1;
                return pageLastIndex;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //获取视频数量
        public int GetVideoCount()
        {
            try
            {
                return Convert.ToInt32(_redis.StringGet("VIDEOINFO:ID"));
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }

}