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

///�״ν���ҳ
function FirstShow(timer) {
    //��������
    $("#flashscreen").hide();
    EndTime(timer);
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

//�û�����
function ToUserCenter()
{
    //�ж��Ƿ��¼
    if (ifLogin()) {
        $(".controled").hide();
        $('#usercenter').show();
        //�û��������ݻ�ȡ
        GetUserCenterData();
    }
    else {
        alert('���ȵ�¼');
        var baseurl = GetUrlPrefix();
        //��ת��¼
        window.location.href = baseurl+'Login.html';
    }
}

//���ﳵ
function ToUserCart()
{
    //�ж��Ƿ��¼
    if (ifLogin()) {
        $(".controled").hide();
        $('#usercart').show();
        //���ﳵ���ݻ�ȡ
        var goodsData = GetUserCart();
        if (!isEmpty(goodsData)) {
            ShowGoodsForUserCart(goodsData);
        }
    }
    else {
        alert('���ȵ�¼');
        var baseurl = GetUrlPrefix();
        //��ת��¼
        window.location.href = baseurl + 'Login.html?action=tousercart';
    }
}

//��ҳ
function ToIndex()
{
    $(".controled").hide();
    $('#indexbody').show();
}

//��¼��ת��ҳ
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
