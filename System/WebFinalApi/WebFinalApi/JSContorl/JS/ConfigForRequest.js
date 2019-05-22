// JavaScript source code
function Config() {
    var url1 = 'http://localhost:8012/api/';
    Config.prototype.GetUrlPrefix = function () {
    return url1;
    };
}

function GetUrlPrefix()
{
    var url1 = 'http://shopapitest.wicp.vip/api/';
    //var url1 ='http://localhost:8012/api/';
    return url1;
}