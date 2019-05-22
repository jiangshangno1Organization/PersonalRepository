//获取所有商品
function GetAllGoods() {
    var url = GetUrlPrefix();
    var userKey = getCookie('userkey');
    $.ajax({
        url: url + 'Goods/GetAllGoods',
        type: "Get",
        headers: { 'Authorization': userKey },
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

//通过分类CD 获取商品
function GetGoodsByCategoryCD(cd) {
   
    var url = GetUrlPrefix();
    var userKey = getCookie('userkey');
    $.ajax({
        url: url + 'Goods/ GetGoodsByCategoryCD',
        type: "Get",
        headers: { 'Authorization': userKey },
        data: { categoryCD: cd},
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