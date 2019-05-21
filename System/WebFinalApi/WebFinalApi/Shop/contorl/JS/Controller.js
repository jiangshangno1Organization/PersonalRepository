// JavaScript source code

///a : current loaction   b: target loaction
function ShowController(a, b) {

    var currentID;
    var targetID;
    //A
    var indexBody = '#indexbody';
    //B
    var goodsList = '#goodslist';
    //C
    var userCenter = '#usercenter';
    currentID = GetID(a);
    targetID = GetID(b);
    //ÇÐ»»
    function SwitchView() {
        $(this.currentID).hide();
        $(this.targetID).show();
    }
}

function GetID(a) {
    switch (a) {
        case 'A':
            a = indexBody;
            break;
        default:
    }
}
