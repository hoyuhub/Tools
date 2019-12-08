$(function () {
    Init();
});

function Init() {
    HeaderInit();
    FirstRowInit();
    GetPage(pageIndex);
    GetPageLastIndex();
    GetVideoCount();
}

var pageIndex = 1;
var pageLastIndex = 0;
//加载header-slide
function HeaderInit() {
    var html = '';
    $.post(heard_init_url, function (v) {
        if (v !== null) {
            $.each(JSON.parse(v), function () {
                html += '<div class="item">' + LoadHtml(this['Width'] + 'x' + this['Height'], this['Duration'], this['ImgPath'], this['Id']) + '</div>';
            });
            $('#owl-demo').append(html);

            $("#owl-demo").owlCarousel({
                autoPlay: 3000,
                items: 5,
                itemsDesktop: [1199, 4],
                itemsDesktopSmall: [979, 4]
            });

        }
    });

}
//加载第一行视频
function FirstRowInit() {
    $.post(first_row_url, function (v) {
        if (v !== null) {
            var htmlArray = new Array();
            $.each(JSON.parse(v), function (index, value) {
                htmlArray.push(LoadHtml(this['Width'] + 'x' + this['Height'], this['Duration'], this['ImgPath'], this['Id']));
            });
            $('.featured .main-vid .col-md-6').append(htmlArray.pop());
            var subVid = $('.featured .sub-vid .col-md-3');
            $.each(subVid, function () {
                if (htmlArray.length > 0) {
                    $(this).append(htmlArray.pop());
                }
                if (htmlArray.length > 0) {
                    $(this).append(htmlArray.pop());
                }
                if (htmlArray.length === 0) {
                    return false;
                }
            });
        } else {
            alert('FirstRow Error');
        }
    });

}

//获取视频数量
function GetVideoCount() {
    $.post(videos_count, function (v) {
        $('video-count').val(v)
    })

}


//加载分页视频
function GetPage(index) {
    if (index === 0) {
        index = 1;
    }
    if (index > pageLastIndex) {
        index = pageLastIndex;
    }
    var html = '';
    $.post(get_page_url, {
        pageIndex: index
    }, function (v) {
        $.each(JSON.parse(v), function () {
            html += '  <div class="col-md-4"><div class="wrap-vid">';
            html += LoadHtml(this['Width'] + 'x' + this['Height'], this['Duration'], this['ImgPath'], this['Id']);
            html += '</div></div>';
        });
        if (index === 1) {
            $('.box-content .row').empty();
        }
        $('.box-content .row').append(html);
        pageIndex = index;
    });
}

//翻页
function GetNextPage(index) {
    pageIndex += index;
    if (pageIndex < 1) {
        pageIndex = 1;
    }
    if (pageIndex > pageLastIndex) {
        pageIndex = pageLastIndex;
    }
    GetPage(pageIndex);
}

//获取尾页
function GetLastPage() {
    GetPage(pageLastIndex);
}

//获取尾页码
function GetPageLastIndex() {
    $.post(page_last_index_url, function (v) {
        pageLastIndex = v;
    });
}