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
    //�л�
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

///�״ν���ҳ
function FirstShow(timer) {
    EndTime(timer);
    //��������
    $("#flashscreen").hide();
    //��Ʒ�б�
    $('#goodsmodular').show();
    //��ȡ��Ʒ��Ϣ
    var goodsData = GetAllGoods();
    //չʾ
    if (!isEmpty(goodsData)) {
        ShowGoodsForIndex(goodsData);
    }
    //����
    //$('advertisementblock').show();
    //�ײ�����
    $('#bottomhead').show();
}




//��¼��ת
function AfterLoginGo() {
    //��ȡ source
    var baseurl = GetUrlPrefix();
    var source = getQueryString('source');
    SetNextAction(source);
    window.location.href = baseurl + 'Index.html';
}

//�û�����
function ToUserCenter(o)
{
    // miao alert( ScollPostion());
    //�ж��Ƿ��¼
    if (ifLogin()) {
        $(".controled").hide();
        $('#usercenter').show();
        UserCenterDataInit();
        NavigationShowControl($(o));
    }
    else {
        alert('���ȵ�¼');
        var baseurl = GetUrlPrefix();
        //��ת��¼
        window.location.href = baseurl + 'Login.html?source=ToUserCenter';
    }
}

//���ﳵ
function ToUserCart(o)
{
    //�ж��Ƿ��¼
    if (ifLogin()) {
        $(".controled").hide();
        $('#usercart').show();
        //���ﳵ���ݻ�ȡ
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
        alert('���ȵ�¼');
        var baseurl = GetUrlPrefix();
        //��ת��¼
        window.location.href = baseurl + 'Login.html?source=ToUserCart';
    }
    NavigationShowControl($(o));
}

//���ﳵ 0 ˢ����ʾ
function OrderCarClear()
{
    $('#cartbody').html('');
    AfterCartReview();
}

//��ҳ
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

//�����û�����
function BackUserCenterFromAddress()
{
    SetNextAction("ToUserCenter");
    ToIndexRef();
}

//�����б�
function ToOrderIndex(a) {
    var baseurl = GetUrlPrefix();
    window.location.href = baseurl + 'OrderData.html?action=tousercart';
}

//��Ʒ����
function ToGoodsDetail(o)
{
    var goodsid = $(o).find('.goodsid').text();
    var baseurl = GetUrlPrefix();
    //��¼ê��
    var top = $("body,html").scrollTop();
    SetScrollTop(top);
    window.location.href = baseurl + 'GoodsDetail.html?goodsid=' + goodsid;
}

//�����ύ
function ToOrderCommit(orderBaseID)
{
    var baseurl = GetUrlPrefix();
    window.location.href = baseurl + 'OrderCommit.html?baseID=' + orderBaseID;
}

//�û��ջ���ַ
function ToUserAddress() {
    var baseurl = GetUrlPrefix();
    window.location.href = baseurl + 'UserAddress.html';
}

//�ջ���ַ����
function ToUserAddressAdd(source)
{
    var baseurl = GetUrlPrefix();
    var url = baseurl + 'UserAdressAdd.html';
    if (!isEmpty(source)) {
        url += '?source=' + source;
    }
    window.location.href = url;
}

//����ͼ�����
function NavigationShowControl(o)
{
    // ���
    $(".icon").removeClass("chose");
    $(".aui-tabBar-item-text").removeClass("chose");

    // �����ʽ
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

///����չʾ
function ShowFlashScreen() {
    //���1Сʱ cookie
    SetFlashScreenSign();
    //�ײ���������
    $('#bottomhead').hide();
    //��Ʒ����
    $('#goodsmodular').hide();
    //����ͼչʾ
    $('#flashscreen').show();

    setTimeout('EndShowFlashScreen()', 500);
}

function EndShowFlashScreen() {
    //�ж�������
    if ($('#flashscreen').css("display") === "block") {
        //����ͼ����
        $('#flashscreen').hide();
        //��Ʒչʾ
        $('#goodsmodular').show();
        //�ײ�����չʾ
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
    //��ȡ����
    if (VerificationFlashScreenIfShow())
    {
        ShowFlashScreen();
    }

  

    //if (strPage.includes("Index") || strPage.includes("index") || isEmpty(strPage)) {
    //    cc = new Controller();
    //    cc.Start();
    //}
   
};
