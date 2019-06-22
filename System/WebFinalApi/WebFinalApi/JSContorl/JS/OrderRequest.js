//��ȡ���ﳵ
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
                alert('����ʧ��.');
            }
            //window.location.href = furl+id;
        }
    });
    return res;
}

//���ﳵ���(��ҳ)
function AddToCart(o) {
    stopevt();
    var goodsid = $(o).prev().children('.goodsid').text();
    if (AddToCartBase(goodsid, 1))
    {
        alert('���ﳵ��ӳɹ�');
    }  
}

//���ﳵ���(����ҳ)
function AddToUserCart() {
    var goodsid = $('#goodsid').text();
    if (isEmpty(goodsid)) {
        alert('��Ʒ�����ڣ���ˢ������');
        return;
    }

    if (AddToCartBase(goodsid, 1)) {
        alert('���ﳵ��ӳɹ�');
    }  
}

//���ﳵ���
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
                if (data.errMeassage === 'δ��¼') {

                    $('#userCart').click();
                }
                else {
                    alert(data.errMeassage);
                }
                ifsuccess = false;
            }
        }
    });
    return ifsuccess;
}

//ɾ�����ﳵ
function RemoveCart(gdsid, cardid) {
    var res = false;
    var url = GetApiUrlPrefix();
    var userKey = getCookie('userkey');
    $.ajax({
        url: url + 'Order/RemoveCart',
        type: "Delete",
        headers: { 'Authorization': userKey },
        data: { goodsID: gdsid, cardid: cardid, ifRemoveAll: false },
        dataType: 'JSON',
        async: false,
        success: function (data)
        {
            if (data.requestIfSuccess)
            {
                res = data.data;
            }
            else
            {
                alert('����ʧ��.');
            }
        }
    });
    return res;
}

//�û������б�
//��ȡ�����б�0��δ���� 1��δ�ջ� 2������� 3: ��ȡ�� 4:ȫ����
function GetUserOrderList(type) {
    var orderData;
    var url = GetApiUrlPrefix();
    var userKey = getCookie('userkey');
    $.ajax({
        url: url + 'Order/GetGetOrderList',
        type: "Get",
        headers: { 'Authorization': userKey },
        data: { type: type },
        dataType: 'JSON',
        async: false,
        success: function (data) {
            if (data.requestIfSuccess) {
                //alert(data);
                orderData = data.data;
            }
            else {
                orderData = null;
                alert('����ʧ��.');
            }
        }
    });
    return orderData;
}

//��������
function GetOrderDetail(baseid) {
    var orderDetail = null;
    var url = GetApiUrlPrefix();
    var userKey = getCookie('userkey');
    $.ajax({
        url: url + 'Order/GetOrderDetail',
        type: "Get",
        headers: { 'Authorization': userKey },
        data: { baseID: baseid },
        dataType: 'JSON',
        async: false,
        success: function (data) {
            if (data.requestIfSuccess) {
                orderDetail = data.data;
            }
            else {
                alert('��ȡ�������ݳ���.');
            }
        }
    });
    return orderDetail;
}

///�����ύ
function SubmitOrder(orderids) {
    var orderID = '';
    var ifsuccess;
    var url = GetApiUrlPrefix();
    var userKey = getCookie('userkey');
    $.ajax({
        url: url + 'Order/SubmitOrderByCart',
        type: "Put",
        headers: { 'Authorization': userKey },
        data: { IDs: orderids, ifSumbitAll: false },
        dataType: 'JSON',
        async: false,
        success: function (data) {
            if (data.requestIfSuccess) {
                ifsuccess = data.data.ifSuccess;
                orderID = data.data.baseOrderID;
                if (ifsuccess) {
                    alert("�ύ�ɹ�");
                }
                else {
                    alert(data.data.remindMsg);
                }

            }
            else {
                alert('����ʧ��.');
                alert(data.errMeassage);
                ifsuccess = false;
            }
        }
    });
    return orderID;
}

//OrderLisr title
function GetOrderStatusTitle(orderData) {
    var htmlTitle = "<a href='javascript:void(0);' class='aui-well-item aui-well-item-clear'> " +
        "  <div class='aui-well-item-bd'> " +
        "  <h3> " + orderData.orderDate + "</h3> " +
        " </div>" +
        "<span class='aui-well-item-fr'> " + orderData.orderStatus +
        "</span >  </a >";
    return htmlTitle;
}

//OrderLisr button
function GetButton(orderdata) {
    var number = orderdata.goodsNumber;
    var cost = orderdata.sum;
    var value = '\
    <a href="javascript:;" class="aui-mail-payment">\
        <p>\
            ��<em>count</em>\
            ����Ʒ ʵ����: ��<i>allCost</i>\
        </p>\
    </a>\
        <div class="aui-mail-button">\
            <a href="javascript:;">�ٴι���</a>\
        </div>';
    value = value.replace('count', number).replace('allCost', cost);
    return value;
}

//OrderLisr goods
function GetGoodsHtml(goodsList) {
    var goodsHtml = '';
    for (var i = 0; i < goodsList.length; i++) {
        var goodsname = goodsList[i].goodsName;
        var pic = goodsList[i].goodsPic;
        var valueCell = " <div class='aui-mail-product'>" +
            " <a href = 'javascript:;' class='aui-mail-product-item' >" +
            "<div class='aui-mail-product-item-hd'>" +
            "<img src = " + pic + " alt=''> " +
            "  </div>" +
            "<div class='aui - mail - product - item - bd'> " +
            "   <p>goodsname</p>" +
            "</div>" +
            "</a> </div >";
        valueCell = valueCell.replace(/goodsname/, goodsname);
        goodsHtml += valueCell;
    }
    return goodsHtml;
}

//��ҳ��Ʒ��Ԫ
function GetOrderCellForOrderList(orderCell) {
    //title
    var titleHtml = GetOrderStatusTitle(orderCell);
    //�ײ�
    var button = GetButton(orderCell);
    var goods = GetGoodsHtml(orderCell.orderDetails);
    var all = "  <div class='divHeight'></div> <div class= 'tab-item'>" +
        titleHtml + goods + button +
        " </div >";
    return all;
}

//OrderList
function OrderListInit(orderList, type) {
    var html = '';
    for (var i = 0; i < orderList.length; i++) {
        var orderCell = orderList[i];
        html += GetOrderCellForOrderList(orderCell);
    }
    var id = '';
    switch (type) {
        case 0:
            id = '0';
            break;
        case 1:
            id = '1';
            break;
        case 2:
            id = '2';
            break;
        case 3:
            id = '3';
            break;
        case 4:
            id = '4';
            break;
        default:
    }
    $('#' + id).html(html);
    //alert(html);
}

function getEvent() {
    if (document.all) {
        return window.event; //�����ie
    }
    func = getEvent.caller;
    while (func != null) {
        var arg0 = func.arguments[0];
        if (arg0) {
            if ((arg0.constructor == Event || arg0.constructor == MouseEvent) || (typeof (arg0) == "object" && arg0.preventDefault && arg0.stopPropagation)) {
                return arg0;
            }
        }
        func = func.caller;
    }
    return null;
}

function stopevt() {
    var ev = getEvent();
    if (ev.stopPropagation) {
        ev.stopPropagation();
    } else if (window.ev) {
        window.ev.cancelBubble = true;
    }
}