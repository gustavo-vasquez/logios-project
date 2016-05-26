function GetCookie(name) {
    var arg = name + "=";
    var alen = arg.length;
    var clen = document.cookie.length;
    var i = 0;

    while (i < clen) {
        var j = i + alen;
        if (document.cookie.substring(i, j) == arg)
            return "here";
        i = document.cookie.indexOf(" ", i) + 1;
        if (i == 0)
            break;
    }

    return null;
}

$(function () {
    var visit = GetCookie("COOKIE1");    

    if (visit == null) {
        $('#CookieDiv').removeClass('hide');
    }

    var expire = new Date();
    expire = new Date(expire.getTime() + 7776000000);
    document.cookie = "COOKIE1=here; expires=" + expire;
});