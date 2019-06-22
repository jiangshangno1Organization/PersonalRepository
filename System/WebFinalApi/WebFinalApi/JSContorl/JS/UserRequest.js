// JavaScript source code

//�û���������
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
                alert('����ʧ��.');
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

//�û���¼
function UserLogin(mobile, code) {

    if (!isPoneAvailable(mobile)) {
        alert('�ֻ����벻�Ϸ�.');
        return;
    }

    if (isEmpty(code)) {
        alert('��������֤��.');
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
                    //cookies���
                    setCookie('userkey', userkey, 'd30');
                    //��ת�û�����
                    AfterLoginGo();
                }
                else {
                    alert(data.data.remindMsg);
                }
            }
            else {
                alert('����ʧ��.');
            }
        }
    });
}

//�û��˳�
function UserQuit() {
    ReMoveUserKey();
    var baseurl = GetUrlPrefix();
    //��ת��¼
    window.location.href = baseurl + 'Index.html';
}

//������֤��
function SendVerificationCode(mobile) {
    if (!isPoneAvailable(mobile)) {
        alert('�ֻ����벻�Ϸ�.');
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
                    alert('��֤�뷢�ͳɹ�.');
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

//��ȡ�û��ջ���ַ
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
                alert('����ʧ��.');
            }
            //window.location.href = furl+id;
        }
    });
    return userAddressData;
}

//�û��ջ���ַչʾ
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

//html ƴ��
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

//�����û��ջ���ַ
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
                    alert('�������');
                }
                else
                {
                    alert('����ʧ��');
                }
            }
            else {
                userAddressData = null;
                alert('����ʧ��.');
            }
            //window.location.href = furl+id;
        }
    });
}