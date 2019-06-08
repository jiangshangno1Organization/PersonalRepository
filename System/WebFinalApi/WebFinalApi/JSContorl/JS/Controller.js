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

///首次进主页
function FirstShow(timer) {
    //闪屏结束
    $("#flashscreen").hide();
    EndTime(timer);
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

//用户中心
function ToUserCenter()
{
    //判断是否登录
    if (ifLogin()) {
        $(".controled").hide();
        $('#usercenter').show();
        //用户中心数据获取
        GetUserCenterData();
    }
    else {
        alert('请先登录');
        var baseurl = GetUrlPrefix();
        //跳转登录
        window.location.href = baseurl+'Login.html';
    }
}

//购物车
function ToUserCart()
{
    //判断是否登录
    if (ifLogin()) {
        $(".controled").hide();
        $('#usercart').show();
        //购物车数据获取
        var goodsData = GetUserCart();
        if (!isEmpty(goodsData)) {
            ShowGoodsForUserCart(goodsData);
        }
    }
    else {
        alert('请先登录');
        var baseurl = GetUrlPrefix();
        //跳转登录
        window.location.href = baseurl + 'Login.html?action=tousercart';
    }
}

//主页
function ToIndex()
{
    $(".controled").hide();
    $('#indexbody').show();
}

//登录跳转主页
function ToIndexFromLogin()
{
    var baseurl = GetUrlPrefix();
    window.location.href = baseurl + 'Index.html?action=tousercart';
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
            if (Second === 3)
            {
                FirstShow(timer);
            }
        }, 1000);
    }
    Controller.prototype.Start = StartTime;
}

window.onload = function myfunction() {
    var cc = new Controller();
    cc.Start();
};
