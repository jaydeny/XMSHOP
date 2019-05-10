// bootstrap 模态框
function bouncedLogin(obj) {
    $(obj.dialog).css({ "width": obj.width });
    $(obj.content).css({ "height": obj.height });
    $(obj.modal).modal('show');
    $.get(obj.url, function (data, status, xhr) {
        $(obj.body).html(data);
    },"html")
}