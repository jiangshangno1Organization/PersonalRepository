// JavaScript source code

//�û���������
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
                alert('����ʧ��.');
            }
            //window.location.href = furl+id;
        }
    });
}

//�û���¼
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
                //cookies���
                setCookie('userkey', '428808564252227614', 'd30');
            }
            else {
                alert('����ʧ��.');
            }
        }
    });
}

//������֤��
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
                alert("���ͳɹ�.");
            }
            else {
                alert('����ʧ��.');
            }
        }
    });
}


window.onload = function () {
    setCookie('userkey', '428808564252227614', 'd30');
    GetUserCenterData();
    GetAllGoods();
};

