$(function () {
    $(".shop-cart-add").click(function () {
        var multi = 0;
        var vall = $(this).prev().val();
        vall++;
        $(this).prev().val(vall);
        TotalPrice();
    });
    $(".shop-cart-subtract").click(function () {
        var multi = 0;
        var vall = $(this).next().val();
        vall--;
        if (vall <= 0) {
            vall = 1;
        }
        $(this).next().val(vall);
        TotalPrice();
    });

    $(".btn1").click(function () {
        var $btn2 = $(this).parent(".shop-cart-box2").siblings(".index-goods").children(".shop-cart-check2").children(".btn2");
        if ($(this).is(':checked')) {
            $btn2.prop("checked", this.checked);
            TotalPrice();
        } else {
            $btn2.removeAttr("checked");
            TotalPrice();
        }
    });

    $(".btn2").click(function () {
        var goods = $(this).closest(".shop-cart-listbox1").find(".btn2");
        var goodsC = $(this).closest(".shop-cart-listbox1").find(".btn2:checked");
        var Shops = $(this).closest(".shop-cart-listbox1").find(".btn1");
        if (goods.length == goodsC.length) {
            Shops.prop('checked', true);
            TotalPrice();
        } else {
            Shops.prop('checked', false);
            TotalPrice();
        }
    });

    $("#ckAll").click(function () {
        $("input[name='sub2']").prop("checked", this.checked);
        TotalPrice();
    });
    $("input[name='sub2']").click(function () {
        var $subs = $("input[name='sub2']");
        $("#ckAll").prop("checked", $subs.length == $subs.filter(":checked").length ? true : false);
        TotalPrice();
    });

    $(".shop-cart-htext1").click(function () {
        $(".scart-total-text2").toggleClass("hide");
        $(".scart-total-text3").toggleClass("hide");
        $(".scart-total-text4").toggleClass("hide");
        $(".delete").toggleClass("hide");
        TotalPrice();
    });

    $(".delete").click(function () {
        if ($("#ckAll").is(':checked')) {
            $(".shop-cart-listbox1").remove();
            $("#ckAll").prop('checked', false);
            TotalPrice();
        }
        if ($(".btn1").is(':checked')) {
            $(".btn1:checked").closest(".shop-cart-listbox1").remove();
            TotalPrice();
        }
        if ($(".btn2").is(':checked')) {
            $(".btn2:checked").parent(".shop-cart-check2").parent(".index-goods").remove();
            TotalPrice();
        }
    });

    function TotalPrice() {
        var allprice = 0;
        $(".shop-cart-listbox1").each(function () {
            var oprice = 0;
            $(this).find(".btn2").each(function () {
                if ($(this).is(":checked")) {
                    var num = $(this).parents(".index-goods").find(".shop-cart-numer").val();
                    var price = parseFloat($(this).parents(".index-goods").find(".priceJs").text());
                    var total = price * num;
                    oprice += total;
                }
                $(this).closest(".shop-cart-listbox1").find(".ShopTotal").text(oprice.toFixed(2));
            });
            var oneprice = parseFloat($(this).find(".ShopTotal").text());
            allprice += oneprice;
        });
        $("#AllTotal").text(allprice.toFixed(2));
    }
});