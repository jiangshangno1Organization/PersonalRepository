function isEmpty(obj) {
    if (typeof obj == "undefined" || obj == null || obj == "") {
        return true;
    } else {
        return false;
    }
}

function isPoneAvailable(mobile) {
    var myreg = /^[1][3,4,5,7,8][0-9]{9}$/;
    if (!myreg.test(mobile)) {
        return false;
    } else {
        return true;
    }
}

function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) {
        return unescape(r[2]);
    } else {
        return null;
    }
}

function ifLogin() {
    var userKey = GetUserKey();
    return !isEmpty(userKey);
}

function GetUserKey()
{
    return getCookie('userkey');
}


function GetAction()
{
    var data = getCookie('action');
    delCookie('action');
    return data;
}

function SetNextAction(val) {
    setCookie('action', val, 's600');
}

//锚点
function SetScrollTop(val)
{
    setCookie('scrolltop', val, 's600');
}

function GetSetScrollTop() {
    var val = getCookie('scrolltop');
    if (isEmpty(val) || val === 'null' || val === 'NULL')
    {
        return 0;
    }
    else
    {
        delCookie('scrolltop');
        return val;
    }
}

function SetFlashScreenSign()
{
    setCookie('flashscreen', 'have' , 'h1');
}


function VerificationFlashScreenIfShow() {
    var result = false;
    var data = getCookie('flashscreen');
    if (isEmpty(data) || data === 'null' || data === 'NULL') {
        result = true;
    }
    if (data === 'have') {
        result = false;
    }
    return result;
}

function ReMoveUserKey() {
    delCookie('userkey');
}

function GetApiUrlPrefix()
{
    // var url1 = 'http://localhost:8012/api/';
    var url1 = 'http://shopapitest.wicp.vip/api/';
    return url1;
}

function GetUrlPrefix()
{
    //var url1 = 'http://localhost:8012/';
    var url1 = 'http://shopapitest.wicp.vip/';
    return url1;
}