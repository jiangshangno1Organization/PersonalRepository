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


function ifLogin() {
    var userKey = GetUserKey();
    return !isEmpty(userKey);
}

function GetUserKey()
{
    return getCookie('userkey');
}

function ReMoveUserKey() {
    delCookie('userkey');
}

function GetApiUrlPrefix() {
    var url1 = 'http://localhost:8012/api/';
      //var url1 = 'http://shopapitest.wicp.vip/api/';
    return url1;
}

function GetUrlPrefix() {
    var url1 = 'http://localhost:8012/';
    //var url1 = 'http://shopapitest.wicp.vip/';
    return url1;
}