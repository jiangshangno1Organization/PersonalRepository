// JavaScript source code

//�û���������
function GetUserCenterData() {
    var url = GetUrlPrefix();
    var userKey = getCookie('userkey');

    //base64����

    $.ajax({
        url: url + 'User/GetUserData',
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


window.onload = function () {
    setCookie('userkey', '428808564252227614', 'd30');
    GetUserCenterData();
}

