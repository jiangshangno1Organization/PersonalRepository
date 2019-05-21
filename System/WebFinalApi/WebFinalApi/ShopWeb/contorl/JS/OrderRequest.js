
//获取购物车
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
                alert('请求失败.');
            }
            //window.location.href = furl+id;
        }
    });
}

//购物车添加
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
                alert('请求失败.');
            }
        }
    });
}

//删除购物车
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
                alert('请求失败.');
            }
        }
    });
}

//用户订单列表
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
                alert('请求失败.');
            }
        }
    });
}

//订单详情
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
                alert('请求失败.');
            }
        }
    });
}