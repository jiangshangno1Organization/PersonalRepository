// JavaScript source code

///a : current loaction   b: target loaction
function ShowController(a, b) {
    var currentID;
    var targetID;
    //A
    var indexBody = '#indexbody';
    //B
    var goodsList = '#goodslist';
    //C
    var userCenter = '#usercenter';
    currentID = GetID(a);
    targetID = GetID(b);
    //切换
    function SwitchView() {
        $(this.currentID).hide();
        $(this.targetID).show();
    }
}

function ScollPostion() {
    var t, l, w, h;
    if (document.documentElement && document.documentElement.scrollTop) {
        t = document.documentElement.scrollTop;
        l = document.documentElement.scrollLeft;
        w = document.documentElement.scrollWidth;
        h = document.documentElement.scrollHeight;
    } else if (document.body) {
        t = document.body.scrollTop;
        l = document.body.scrollLeft;
        w = document.body.scrollWidth;
        h = document.body.scrollHeight;
    }
    return {
        top: t,
        left: l,
        width: w,
        height: h
    };
}

///首次进主页
function FirstShow(timer) {
    EndTime(timer);
    //闪屏结束
    $("#flashscreen").hide();
    //商品列表
    $('#goodsmodular').show();
    //获取商品信息
    var goodsData = GetAllGoods();
    //展示
    if (!isEmpty(goodsData)) {
        ShowGoodsForIndex(goodsData);
    }
    //块广告
    //$('advertisementblock').show();
    //底部标题
    $('#bottomhead').show();
}




//登录跳转
function AfterLoginGo() {
    //获取 source
    var baseurl = GetUrlPrefix();
    var source = getQueryString('source');
    SetNextAction(source);
    window.location.href = baseurl + 'Index.html';
}

//用户中心
function ToUserCenter(o)
{
    // miao alert( ScollPostion());
    //判断是否登录
    if (ifLogin()) {
        $(".controled").hide();
        $('#usercenter').show();
        UserCenterDataInit();
        NavigationShowControl($(o));
    }
    else {
        alert('请先登录');
        var baseurl = GetUrlPrefix();
        //跳转登录
        window.location.href = baseurl + 'Login.html?source=ToUserCenter';
    }
}

//购物车
function ToUserCart(o)
{
    //判断是否登录
    if (ifLogin()) {
        $(".controled").hide();
        $('#usercart').show();
        //购物车数据获取
        var goodsData = GetUserCart();
        if (!isEmpty(goodsData))
        {
            ShowGoodsForUserCart(goodsData);
        } else
        {
            OrderCarClear();
        }
    }
    else {
        alert('请先登录');
        var baseurl = GetUrlPrefix();
        //跳转登录
        window.location.href = baseurl + 'Login.html?source=ToUserCart';
    }
    NavigationShowControl($(o));
}

//购物车 0 刷新显示
function OrderCarClear()
{
    $('#cartbody').html('');
    AfterCartReview();
}

//主页
function ToIndex(o)
{
    $(".controled").hide();
    $('#indexbody').show();
    //$("body,html").scrollTop(691);
    NavigationShowControl($(o));
}

function ToIndexRef() {
    var baseurl = GetUrlPrefix();
    window.location.href = baseurl + 'Index.html';
}

//返回用户中心
function BackUserCenterFromAddress()
{
    SetNextAction("ToUserCenter");
    ToIndexRef();
}

//订单列表
function ToOrderIndex(a) {
    var baseurl = GetUrlPrefix();
    window.location.href = baseurl + 'OrderData.html?action=tousercart';
}

//商品详情
function ToGoodsDetail(o)
{
    var goodsid = $(o).find('.goodsid').text();
    var baseurl = GetUrlPrefix();
    //记录锚点
    var top = $("body,html").scrollTop();
    SetScrollTop(top);
    window.location.href = baseurl + 'GoodsDetail.html?goodsid=' + goodsid;
}

//订单提交
function ToOrderCommit(orderBaseID)
{
    var baseurl = GetUrlPrefix();
    window.location.href = baseurl + 'OrderCommit.html?baseID=' + orderBaseID;
}

//用户收货地址
function ToUserAddress() {
    var baseurl = GetUrlPrefix();
    window.location.href = baseurl + 'UserAddress.html';
}

//收货地址新增
function ToUserAddressAdd(source)
{
    var baseurl = GetUrlPrefix();
    var url = baseurl + 'UserAdressAdd.html';
    if (!isEmpty(source)) {
        url += '?source=' + source;
    }
    window.location.href = url;
}

//导航图标控制
function NavigationShowControl(o)
{
    // 清除
    $(".icon").removeClass("chose");
    $(".aui-tabBar-item-text").removeClass("chose");

    // 添加样式
    var clonedNode = $(o)[0];
    var pic = clonedNode.getElementsByClassName("icon iconfont")[0];
    var text = clonedNode.getElementsByClassName("aui-tabBar-item-text")[0];
    pic.setAttribute("class","icon iconfont chose");
    text.setAttribute("class", "aui-tabBar-item-text chose");
}

function GetID(a) {
    switch (a) {
        case 'A':
            a = indexBody;
            break;
        case 'B':
            a = goodsList;
            break;
        case 'C':
            a = userCenter;
            break;
        default: '';
            return a;
    }
}

function EndTime(timer) {
    clearInterval(timer);
}

function Controller() {
    var timer;
    Second = 0;
    function StartTime(timer) {
        timer = setInterval(function () {
            Second++;
            if (Second >= 3) {
                FirstShow(timer);
            }
        }, 1000);
    }
    function EndTime() {
        Second = 3;
    }
    Controller.prototype.Start = StartTime;
    Controller.prototype.End = EndTime;
}

var cc;

function EndPictureShow()
{
    cc.End();
}

///闪屏展示
function ShowFlashScreen() {
    //添加1小时 cookie
    SetFlashScreenSign();
    //底部导航隐藏
    $('#bottomhead').hide();
    //商品隐藏
    $('#goodsmodular').hide();
    //闪屏图展示
    $('#flashscreen').show();

    setTimeout('EndShowFlashScreen()', 500);
}

function EndShowFlashScreen() {
    //判断已隐藏
    if ($('#flashscreen').css("display") === "block") {
        //闪屏图隐藏
        $('#flashscreen').hide();
        //商品展示
        $('#goodsmodular').show();
        //底部导航展示
        $('#bottomhead').show();
    }
}

window.onload = function myfunction() {
    var strUrl = window.location.href;
    var arrUrl = strUrl.split("/");
    var strPage = arrUrl[arrUrl.length - 1];
    var action = GetAction();
    var dd = true;
    IndexGoodsInitAndShow();
    if (!isEmpty(action)) {
        switch (action) {
            case 'ToUserCart':
                $("#userCart").click();
                //ToUserCart();
                dd = false;
                break;
            case 'ToUserCenter':
                $("#userCenter").click();
                //ToUserCenter();
                dd = false;
                break;
            default:
        }
        if (!dd) {
            return;
        }
    }
    var d = GetSetScrollTop();
    alert(d);
    $("body,html").scrollTop(d);
    //获取闪屏
    if (VerificationFlashScreenIfShow())
    {
        ShowFlashScreen();
    }

  

    //if (strPage.includes("Index") || strPage.includes("index") || isEmpty(strPage)) {
    //    cc = new Controller();
    //    cc.Start();
    //}
   
};
