//��ҳ��Ʒ���ݼ��� չʾ
function IndexGoodsInitAndShow() {
    var goodsData = GetAllGoods();
    //չʾ
    if (!isEmpty(goodsData)) {
        ShowGoodsForIndex(goodsData);
    }
}

//��ȡ������Ʒ
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
                alert('����ʧ��.');
                goodsData = null;
            }
        }
    });
    return goodsData;
}

///��ȡgoods Data
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
                alert('����ʧ��.');
                goodsData = null;
            }
        }
    });
    return goodsData;
}

///ͨ������CD ��ȡ��Ʒ
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
                alert('����ʧ��.');
            }
        }
    });
}

///��ҳ������Ʒ���
function ShowGoodsForIndex(goodsData) {
    if (!isEmpty(goodsData.goods) && goodsData.goods.length > 0) {
        for (var i = 0; i < goodsData.goods.length; i++) {
            GoodsCellForIndex(goodsData.goods[i]);
        }
    }
}

//���ﳵ������Ʒ��ʾ
function ShowGoodsForUserCart(cartData) {
    $('#cartbody').empty();
    for (var i = 0; i < cartData.length; i++) {
        GoodsCellForCartInit(cartData[i]);
    }
    AfterCartReview();
}

//���ﳵ��Ԫ
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
    // ��¡�ڵ� 
    //var clonedNode = sourceNode.cloneNode(true);
    var df = $('#cartDemo').clone(true, true);
    var clonedNode = df[0];
    // �޸�һ��id ֵ������id �ظ� 
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
    //����
    var name = clonedNode.getElementsByClassName("name")[0];
    name.innerText = goodsName;
    //ͼƬ
    var pic = clonedNode.getElementsByClassName("pic")[0];
    //  'assets/img/product/furits/22.jpg';
    pic.src = goodspicture;
    //�۸�
    var cprc = clonedNode.getElementsByClassName("cprc")[0];
    cprc.innerText = cPrc;
    //����product-num
    var count = clonedNode.getElementsByClassName("product-num")[0];
    count.setAttribute("value", cartCell.goodsCount);
    //var oprc = clonedNode.getElementsByClassName("oprc")[0];
    //oprc.innerText = '��' + oPrc;
    // �ڸ��ڵ�����¡
    dd.appendChild(clonedNode);
}

//��ҳ��Ʒ��Ԫ
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
    // ��¡�ڵ� 
    //var clonedNode = sourceNode.cloneNode(true);
    // �޸�һ��id ֵ������id �ظ� 
    /*clonedNode.setAttribute("id", "div-"); */
    clonedNode.removeAttribute("style");
    var goodsid = clonedNode.getElementsByClassName("goodsid")[0];
    goodsid.innerText = goodsId;
    //����
    var name = clonedNode.getElementsByClassName("name")[0];
    name.innerText = goodsName;
    //ͼƬ
    var pic = clonedNode.getElementsByClassName("pic")[0];
    //'assets/img/product/furits/22.jpg';
    pic.src = goodspicture;
    //�۸�
    var cprc = clonedNode.getElementsByClassName("cprc")[0];
    cprc.innerText = '��' + cPrc;

    var oprc = clonedNode.getElementsByClassName("oprc")[0];
    oprc.innerText = '��' + oPrc;
    dd.appendChild(clonedNode); // �ڸ��ڵ�����¡
}

///��Ʒ��������չʾ
function GoodsDetailDataInit(goodsid) {

    $('#goodsid').text(goodsid);
    if (isEmpty(goodsid)) {
        alert("��Ʒ������");
        window.location(-1);
    }
    //��ȡ��Ʒ����
    var goodsData = GetGoodsDataByGoodsId(goodsid);

    //չʾ
    var goodsName = goodsData.goodsCell.goodsName;
    var goodsPrice = goodsData.goodsCell.price;
    var goodsPicture = goodsData.goodsCell.goodsPictrures[0].file;

    $('#goodsName').text(goodsName);
    $('#price').text('$' + goodsPrice);
    $('#picture').attr('src', goodsPicture);

    $('#picture').click();
}