// 验证确定密码
function vailConfPwd(id, pwd, confirm_pwd) {
    if (confirm_pwd == "") {
        $(id).text("请输入您的密码。");
        return false;
    } else if (confirm_pwd != pwd) {
        $(id).text("两次密码不一致。");
        return false;
    }
    else {
        $(id).text("");
        return true;
    }
}
// 验证密码
function vailPwd(id, pwd) {
    if (pwd == "") {
        $(id).text("请输入您的密码");
        return false;
    }
    else if (pwd.length > 16 || pwd.length < 6) {
        $(id).text("密码长度不在6-16位之间!");
        return false;
    }
    else {
        $(id).text("");
        return true;
    }
}

// 验证账号
function vailAN(id, an) {
    if (an == "") {
        $(id).text("请输入您的账号");
        return false;
    }
    else if (an.length > 12 || an.length < 4) {
        $(id).text("账号长度不在4-12位之间!");
        return false;
    }
    else {
        $(id).text("");
        return true;
    }
}

// 验证邮箱
function vailEmail(id, email) {
    if (email == '') {
        $(id).text("请输入您的邮箱");
        return false;
    } else {
        var reg = /^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/;
        if (!reg.test(email)) {
            $(id).text("邮箱格式不正确，请重新输入！");
            return false;
        } else {
            $(id).text("");
            return true;
        }
    }
}

// 验证手机号
function vailPhone(id, phone) {
    var myreg = /^(((13[0-9]{1})|(14[0-9]{1})|(17[0]{1})|(15[0-3]{1})|(15[5-9]{1})|(18[0-9]{1}))+\d{8})$/;
    if (phone == '') {
        $(id).text("手机号码不能为空！");
        return false;
    } else if (phone.length != 11) {
        $(id).text("请输入有效的手机号码！");
        return false;
    } else if (!myreg.test(phone)) {
        $(id).text("请输入有效的手机号码！");
        return false;
    } else {
        $(id).text("");
        return true;
    }
}