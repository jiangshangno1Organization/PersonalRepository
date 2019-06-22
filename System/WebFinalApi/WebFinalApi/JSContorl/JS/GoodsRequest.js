//首页商品数据加载 展示
function IndexGoodsInitAndShow() {
    var goodsData = GetAllGoods();
    //展示
    if (!isEmpty(goodsData)) {
        ShowGoodsForIndex(goodsData);
    }
}

//获取所有商品
function GetAllGoods() {
    var goodsData;
    var url = GetApiUrlPrefix();
    var userKey = getCookie('userkey');
    $.ajax({
        url: url + 'Goods/GetAllGoods',
        type: "Get",
        headers: { 'Authorization': userKey },
        dataType: 'JSON',
        async: false,
        success: function (data) {
            if (data.requestIfSuccess) {
                goodsData = data.data;
            }
            else {
                alert('请求失败.');
                goodsData = null;
            }
        }
    });
    return goodsData;
}

///获取goods Data
function GetGoodsDataByGoodsId(id) {
    var goodsData;
    var url = GetApiUrlPrefix();
    var userKey = getCookie('userkey');
    $.ajax({
        url: url + 'Goods/GetGoodsDetail',
        type: "Get",
        headers: { 'Authorization': userKey },
        data: { ID: id },
        dataType: 'JSON',
        async: false,
        success: function (data) {
            if (data.requestIfSuccess) {
                goodsData = data.data;
            }
            else {
                alert('请求失败.');
                goodsData = null;
            }
        }
    });
    return goodsData;
}

///通过分类CD 获取商品
function GetGoodsByCategoryCD(cd) {

    var url = GetApiUrlPrefix();
    var userKey = getCookie('userkey');
    $.ajax({
        url: url + 'Goods/ GetGoodsByCategoryCD',
        type: "Get",
        headers: { 'Authorization': userKey },
        data: { categoryCD: cd },
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

///首页所有商品添加
function ShowGoodsForIndex(goodsData) {
    if (!isEmpty(goodsData.goods) && goodsData.goods.length > 0) {
        for (var i = 0; i < goodsData.goods.length; i++) {
            GoodsCellForIndex(goodsData.goods[i]);
        }
    }
}

//购物车所有商品显示
function ShowGoodsForUserCart(cartData) {
    $('#cartbody').empty();
    for (var i = 0; i < cartData.length; i++) {
        GoodsCellForCartInit(cartData[i]);
    }
    AfterCartReview();
}

//购物车单元
function GoodsCellForCartInit(cartCell) {
    var cartBodyID = 'cartbody';
    var demo = 'cartDemo';
    var cartId = cartCell.carID;
    var goodsId = cartCell.goodsCell.goodsID;
    var goodsName = cartCell.goodsCell.goodsName;
    var goodspicture = cartCell.goodsCell.goodsPictrures[0].file;
    //goodspicture = 'assets/img/product/furits/22.jpg';
    var cPrc = cartCell.goodsCell.aPrice;
    var oPrc = cartCell.goodsCell.price;
    var dd = document.getElementById(cartBodyID);
    var sourceNode = document.getElementById(demo);
    // 克隆节点 
    //var clonedNode = sourceNode.cloneNode(true);
    var df = $('#cartDemo').clone(true, true);
    var clonedNode = df[0];
    // 修改一下id 值，避免id 重复 
    /*clonedNode.setAttribute("id", "div-"); */
    clonedNode.removeAttribute("style");

    //
    var choice = clonedNode.getElementsByClassName('product-em')[0];
    choice.removeAttribute("id");

    //cartid
    var cartid = clonedNode.getElementsByClassName("cartid")[0];
    cartid.innerText = cartId;
    //goodsid
    var goodsid = clonedNode.getElementsByClassName("goodsid")[0];
    goodsid.innerText = goodsId;
    //名称
    var name = clonedNode.getElementsByClassName("name")[0];
    name.innerText = goodsName;
    //图片
    var pic = clonedNode.getElementsByClassName("pic")[0];
    //  'assets/img/product/furits/22.jpg';
    pic.src = goodspicture;
    //价格
    var cprc = clonedNode.getElementsByClassName("cprc")[0];
    cprc.innerText = cPrc;
    //数量product-num
    var count = clonedNode.getElementsByClassName("product-num")[0];
    count.setAttribute("value", cartCell.goodsCount);
    //var oprc = clonedNode.getElementsByClassName("oprc")[0];
    //oprc.innerText = '￥' + oPrc;
    // 在父节点插入克隆
    dd.appendChild(clonedNode);
}

//首页商品单元
function GoodsCellForIndex(goodsCell) {
    var goodscontainer = 'goodscontainer';
    var goodsId = goodsCell.goodsID;
    var goodsName = goodsCell.goodsName;
    var goodspicture = goodsCell.goodsPictrures[0].file;
    var cPrc = goodsCell.price;
    var oPrc = goodsCell.aPrice;
    var dd = document.getElementById(goodscontainer);
    var df = $('#goodsmodel').clone(true, true);
    var clonedNode = df[0];
    //var sourceNode = document.getElementById("goodsmodel");
    // 克隆节点 
    //var clonedNode = sourceNode.cloneNode(true);
    // 修改一下id 值，避免id 重复 
    /*clonedNode.setAttribute("id", "div-"); */
    clonedNode.removeAttribute("style");
    var goodsid = clonedNode.getElementsByClassName("goodsid")[0];
    goodsid.innerText = goodsId;
    //名称
    var name = clonedNode.getElementsByClassName("name")[0];
    name.innerText = goodsName;
    //图片
    var pic = clonedNode.getElementsByClassName("pic")[0];
    //'assets/img/product/furits/22.jpg';
    pic.src = goodspicture;
    //价格
    var cprc = clonedNode.getElementsByClassName("cprc")[0];
    cprc.innerText = '￥' + cPrc;

    var oprc = clonedNode.getElementsByClassName("oprc")[0];
    oprc.innerText = '￥' + oPrc;
    dd.appendChild(clonedNode); // 在父节点插入克隆
}

///商品详情数据展示
function GoodsDetailDataInit(goodsid) {

    $('#goodsid').text(goodsid);
    if (isEmpty(goodsid)) {
        alert("商品不存在");
        window.location(-1);
    }
    //获取商品详情
    var goodsData = GetGoodsDataByGoodsId(goodsid);

    //展示
    var goodsName = goodsData.goodsCell.goodsName;
    var goodsPrice = goodsData.goodsCell.price;
    var goodsPicture = goodsData.goodsCell.goodsPictrures[0].file;

    $('#goodsName').text(goodsName);
    $('#price').text('$' + goodsPrice);
    $('#picture').attr('src', goodsPicture);

    $('#picture').click();
}