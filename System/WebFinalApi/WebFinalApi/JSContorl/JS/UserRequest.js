// JavaScript source code

//用户中心数据
function GetUserCenterData() {
    var userData;
    var url = GetApiUrlPrefix();
    var userKey = getCookie('userkey');
    $.ajax({
        url: url + 'User/GetUserData',
        type: "Get",
        headers: { 'Authorization': userKey },
        dataType: 'JSON',
        async: false,
        success: function (data)
        {
            if (data.requestIfSuccess)
            {
                userData = data.data;
            }
            else {
                userData = null;
                alert('请求失败.');
            }
            //window.location.href = furl+id;
        }
    });
    return userData;
}


//用户登录
function UserLogin(mobile, code) {

    if (!isPoneAvailable(mobile)) {
        alert('手机号码不合法.');
        return;
    }

    if (isEmpty(code)) {
        alert('请输入验证码.');
        return;
    }

    var url = GetApiUrlPrefix();
    var userKey = getCookie('userkey');
    $.ajax({
        url: url + 'User/UserLogin',
        type: 'Put',
        dataType: 'JSON',
        data: { mobile: mobile, code: code },
        success: function (data) {
            if (data.requestIfSuccess) {
                if (data.data.ifSuccess) {
                    var userkey = data.data.key;
                    //cookies添加
                    setCookie('userkey', userkey, 'd30');
                    //跳转用户中心
                    ToIndexFromLogin();
                }
                else {
                    alert(data.data.remindMsg);
                }
            }
            else {
                alert('请求失败.');
            }
        }
    });
}

//用户退出
function UserQuit() {
    ReMoveUserKey();
}

//发送验证码
function SendVerificationCode(mobile) {
    if (!isPoneAvailable(mobile)) {
        alert('手机号码不合法.');
        return;
    }

    var url = GetApiUrlPrefix();
    var userKey = getCookie('userkey');
    $.ajax({
        url: url + 'System/SendVerificationCode?mobile=' + mobile,
        type: "PUT",
        dataType: 'JSON',
        data: { "mobile": mobile },
        success: function (data) {
            if (data.requestIfSuccess) {
                if (data.data) {
                    alert('验证码发送成功.');
                }
                else
                {
                    alert(data.errMeassage);
                }
              
            }
            else {
                alert(data.errMeassage);
            }
        }
    });
}


