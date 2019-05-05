
$("#home_exit").click(function () {
    $.get("/login/UserLoginOut", function (data) {
        if (data.success) {
            window.location.href = "/Login";
        }
    }, "json")
});

$.post("/Home/loadMenu", function (data) {
    console.data(data);
})

