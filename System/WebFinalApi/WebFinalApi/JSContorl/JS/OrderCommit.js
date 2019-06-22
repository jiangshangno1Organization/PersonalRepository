function PageInit(baseID) {
    var orderData = GetOrderDetail(baseID);
    if (isEmpty(orderData)) {
        alert("订单数据获取出错，请重新下单");
        return;
    }
    // 显示 goodscount
    var goodscount = orderData.orderDetails.length;
    $('#goodsCount').text('商品清单' + goodscount + '件');
    // goods div
    GoodsDataInit(orderData.orderDetails);
    // goodsNeedPayCount
    var goodsNeedPayCount = orderData.sum;
    $('#goodsNeedPayCount').text('￥' + goodsNeedPayCount);
    // allNeedPayCount
    $('#allNeedPayCount').text('￥' + goodsNeedPayCount);

}

//商品html
function GoodsDataInit(orderDetails)
{
    var html = '';
    for (var i = 0; i < orderDetails.length; i++) {
        var goods = orderDetails[i];
        html += GoodsCellHtmlInit(goods);
    }
    $('#goods').html(html);
}

//商品html Cell
function GoodsCellHtmlInit(goods) {

    var htmlCell = '<div class="aui-flex aui-flex-default aui-mar15" > \
            <div class="aui-flex-goods"> \
                <img src="goodspicture" alt="">\
                        </div>\
                <div class="aui-flex-box">\
                    <h2>goodsname</h2>\
                    <div class="aui-flex aui-flex-clear">\
                        <div class="aui-flex-box">￥goodsprice</div>\
                        <div>xgoodscount</div>\
                    </div>\
                </div>\
            </div> ';
    var goodsName = goods.goodsName;
    var goodsPic = goods.goodsPic;
    var unitPrice = goods.unitPrice;
    var goodsCount = goods.goodsCount;
    htmlCell = htmlCell.replace(/goodspicture/, goodsPic)
        .replace(/goodsname/, goodsName).replace(/goodsprice/, unitPrice)
        .replace(/goodscount/, goodsCount);
    return htmlCell;
}

//点击选择收货地址
function ToChooseAddress() {
    $('#orderbody').hide();
    $('#addresschoose').show();
    UserAddressListShow();
    //绑定 选择事件
    $('.b-line').click(function () {

        var addressde = this.children[0].innerText;
        var address = this.children[1].innerText;
        var name = this.children[2].innerText;
        BackOrder();
        ReciveAddressShow(addressde, address, name);
    });
}

function ReciveAddressShow(addressde, address, name)
{
    var html = ' <h2>detail</h2>\
             <h3>address</h3>\
            <p>name</p>';
    html = html.replace(/detail/, addressde).replace(/address/, address).replace(/name/, name);
    $('#reciveaddress').html(html);
}

//选择地址返回下单
function BackOrder() {
    $('#orderbody').show();
    $('#addresschoose').hide();
}

//地址新增后
function ReViewAddressChoose() {
    var action = GetAction();
    if (action == 'ToChooseReciveAddress') {
        ToChooseAddress();
    }
}

function ComeBackIndex() {
    var baseurl = GetUrlPrefix();
    window.location.href = baseurl + 'Index.html';
}