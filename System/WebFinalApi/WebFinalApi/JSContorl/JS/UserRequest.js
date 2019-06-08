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
        success: function (data)
        {
            if (data.requestIfSuccess)
            {
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
                    ToIndexFromLogin();
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


