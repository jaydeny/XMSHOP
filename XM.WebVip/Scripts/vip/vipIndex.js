
/*调用模态框*/
// 登录
window.onload = function () {
    var obj = {
        "modal": "#myModal", "dialog": "#dialog", "content": "#content", "body": "#body"
    };
    $("#vipLogin").click(function () {
        obj.width = "400px";
        obj.height = "400px";
        obj.url = "/vip/Login";
        bouncedLogin(obj);
    });
    // 注册
    $("#vipRegistered").click(function () {
        obj.width = "400px";
        obj.height = "550px";
        obj.url = "/vip/Signin";
        bouncedLogin(obj);
    });
}
