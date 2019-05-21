
//��ȡ���ﳵ
function myfunction() {
    var url = GetUrlPrefix();
    var userKey = getCookie('userkey');
    $.ajax({
        url: url + 'Order/GetUserCart',
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
            else {
                alert('����ʧ��.');
            }
            //window.location.href = furl+id;
        }
    });
}

//���ﳵ���
function AddToCart(gdsid, count) {
    var url = GetUrlPrefix();
    var userKey = getCookie('userkey');
    $.ajax({
        url: url + 'Order/GetUserCart',
        type: "Put",
        headers: { 'Authorization': userKey },
        data: { goodsID: gdsid, goodsCount: count },
        dataType: 'JSON',
        success: function (data) {
            if (data.requestIfSuccess) {
                alert(data);
            }
            else {
                alert('����ʧ��.');
            }
        }
    });
}

//ɾ�����ﳵ
function RemoveCart(gdsid,ifall) {
    var url = GetUrlPrefix();
    var userKey = getCookie('userkey');
    $.ajax({
        url: url + 'Order/RemoveCart',
        type: "Put",
        headers: { 'Authorization': userKey },
        data: { goodsID: gdsid, ifRemoveAll: ifall },
        dataType: 'JSON',
        success: function (data) {
            if (data.requestIfSuccess) {
                alert(data);
            }
            else {
                alert('����ʧ��.');
            }
        }
    });
}

//�û������б�
function GetUserCenterData() {
    var url = GetUrlPrefix();
    var userKey = getCookie('userkey');
    $.ajax({
        url: url + 'Order/GetGetOrderList',
        type: "Get",
        headers: { 'Authorization': userKey },
        data: { type: '0' },
        dataType: 'JSON',
        success: function (data) {
            if (data.requestIfSuccess) {
                alert(data);
            }
            else {
                alert('����ʧ��.');
            }
        }
    });
}

//��������
function GetOrderDetail(baseid) {
    var url = GetUrlPrefix();
    var userKey = getCookie('userkey');
    $.ajax({
        url: url + 'Order/GetOrderDetail',
        type: "Get",
        headers: { 'Authorization': userKey },
        data: { baseID: baseid },
        dataType: 'JSON',
        success: function (data) {
            if (data.requestIfSuccess) {
                alert(data);
            }
            else {
                alert('����ʧ��.');
            }
        }
    });
}