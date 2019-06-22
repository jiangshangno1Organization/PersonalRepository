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
        success: function (data) {
            if (data.requestIfSuccess) {
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

function UserCenterDataInit() {
    var data = GetUserCenterData();

    $('#username').text(data.name);
    $('#mobile').text(data.mobile);
    

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
                    AfterLoginGo();
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
    var baseurl = GetUrlPrefix();
    //跳转登录
    window.location.href = baseurl + 'Index.html';
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
                else {
                    alert(data.errMeassage);
                }

            }
            else {
                alert(data.errMeassage);
            }
        }
    });
}

//获取用户收货地址
function GetUserAddressData() {
    var userAddressData;
    var url = GetApiUrlPrefix();
    var userKey = getCookie('userkey');
    $.ajax({
        url: url + 'User/GetUserAddress',
        type: "Get",
        headers: { 'Authorization': userKey },
        dataType: 'JSON',
        async: false,
        success: function (data) {
            if (data.requestIfSuccess) {
                userAddressData = data.data.userAddresses;
            }
            else {
                userAddressData = null;
                alert('请求失败.');
            }
            //window.location.href = furl+id;
        }
    });
    return userAddressData;
}

//用户收货地址展示
function UserAddressListShow() {
    var addressData = GetUserAddressData();
    var html = '';
    if (!isEmpty(addressData)) {
        for (var i = 0; i < addressData.length; i++) {
            html += AddressCellHtmlInit(addressData[i]);
        }
    }
    $('#addresslist').html(html);
}

//html 拼接
function AddressCellHtmlInit(address) {
    var html = '<li class="b-line" onclick="">\
        <h2 > detail <span > <i class="icon icon-add"></i></span></h2 >\
            <p>address</p>\
            <p>name mobile</p>\
              </li >';
    html = html.replace(/address/, address.address).replace(/name/, address.userName).replace(/mobile/, address.mobile)
        .replace(/detail/, address.addressde);
    return html;
}

//新增用户收货地址
function AddUserAddress(userName, mobile, address, addressde, ifDefault) {
    var userAddressData;
    var url = GetApiUrlPrefix();
    var userKey = getCookie('userkey');
    $.ajax({
        url: url + 'User/AddUserAddress',
        type: "PUT",
        headers: { 'Authorization': userKey },
        data:
        {
            userName: userName,
            mobile: mobile,
            address: address,
            addressde: addressde,
            ifDefault: ifDefault
        },
        dataType: 'JSON',
        async: false,
        success: function (data) {
            if (data.requestIfSuccess)
            {
                if (data.data) {
                    alert('新增完成');
                }
                else
                {
                    alert('新增失败');
                }
            }
            else {
                userAddressData = null;
                alert('请求失败.');
            }
            //window.location.href = furl+id;
        }
    });
}