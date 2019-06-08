
//获取购物车
function GetUserCart() {
    var res;
    var url = GetApiUrlPrefix();
    var userKey = getCookie('userkey');
    $.ajax({
        url: url + 'Order/GetUserCart',
        type: "Get",
        headers: { 'Authorization': userKey },
        dataType: 'JSON',
        async: false,
        success: function (data) {
            if (data.requestIfSuccess) {
                res = data.data.orderCarts;
            }
            else {
                res = null;
                alert('请求失败.');
            }
            //window.location.href = furl+id;
        }
    });
    return res;
}

//购物车添加
function AddToCart(o)
{
    var goodsid = $(o).parent().children('.goodsid').text();
    return AddToCartBase(goodsid, 1);
}

//购物车添加
function AddToCartBase(gdsid, count) {
    var ifsuccess;
    var url = GetApiUrlPrefix();
    var userKey = getCookie('userkey');
    $.ajax({
        url: url + 'Order/AddToCart',
        type: "Put",
        headers: { 'Authorization': userKey },
        data: { goodsID: gdsid, goodsCount: count },
        dataType: 'JSON',
        async: false,
        success: function (data) {
            if (data.requestIfSuccess) {
                ifsuccess = data.data;
            }
            else {
                alert('请求失败.');
                alert(data.errMeassage);
                ifsuccess = false;
            }
        }
    });
    return ifsuccess;
}

//删除购物车
function RemoveCart(gdsid,ifall) {
    var url = GetApiUrlPrefix();
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
function GetUserOrderList() {
    var url = GetApiUrlPrefix();
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
    var url = GetApiUrlPrefix();
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

///订单提交
function SubmitOrder(orderids)
{
    var ifsuccess;
    var url = GetApiUrlPrefix();
    var userKey = getCookie('userkey');
    $.ajax({
        url: url + 'Order/AddToCart',
        type: "Put",
        headers: { 'Authorization': userKey },
        data: { IDs: orderids, ifSumbitAll: false },
        dataType: 'JSON',
        async: false,
        success: function (data) {
            if (data.requestIfSuccess) {
                ifsuccess = data.data.ifSuccess;

                if (ifsuccess)
                {
                    alert("提交成功");
                }
                else
                {
                    alert(data.data.remindMsg);
                }

            }
            else {
                alert('请求失败.');
                alert(data.errMeassage);
                ifsuccess = false;
            }
        }
    });
    return ifsuccess;
}