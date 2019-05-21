// JavaScript source code

//用户中心数据
function GetUserCenterData() {
    var url = GetUrlPrefix();
    var userKey = getCookie('userkey');

    //base64编码

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
                alert('请求失败.');
            }
            //window.location.href = furl+id;
        }
    });
}


window.onload = function () {
    setCookie('userkey', '428808564252227614', 'd30');
    GetUserCenterData();
}

