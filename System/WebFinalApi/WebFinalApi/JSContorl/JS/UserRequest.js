// JavaScript source code

//用户中心数据
function GetUserCenterData() {
    var url = GetUrlPrefix();
    var userKey = getCookie('userkey');
    $.ajax({
        url: url + 'User/GetUserData',
        type: "Get",
        headers: { 'Authorization': userKey },
        dataType: 'JSON',

        //beforeSend: function (xhr)
        //{
        //    xhr.setRequestHeader("Authorization", "Basic " + btoa(userKey + ":"));
        //},
        success: function (data) {
            if (data.requestIfSuccess) {
                alert(data);
            }
            else
            {
                alert('请求失败.');
            }
            //window.location.href = furl+id;
        }
    });
}

//用户登录
function UserLogin() {
    var url = GetUrlPrefix();
    var userKey = getCookie('userkey');
    $.ajax({
        url: url + 'User/UserLogin',
        type: "Post",
        dataType: 'JSON',
        success: function (data) {
            if (data.requestIfSuccess) {
                alert(data);
                //cookies添加
                setCookie('userkey', '428808564252227614', 'd30');
            }
            else {
                alert('请求失败.');
            }
        }
    });
}

//发送验证码
function SendVerificationCode(mobile) {
    var url = GetUrlPrefix();
    var userKey = getCookie('userkey');
    $.ajax({
        url: url + 'User/SendVerificationCode',
        type: "Put",
        dataType: 'JSON',
        data: { "mobile": mobile },
        success: function (data)
        {
            if (data.requestIfSuccess)
            {
                alert("发送成功.");
            }
            else {
                alert('请求失败.');
            }
        }
    });
}


window.onload = function () {
    setCookie('userkey', '428808564252227614', 'd30');
    GetUserCenterData();
    GetAllGoods();
};

