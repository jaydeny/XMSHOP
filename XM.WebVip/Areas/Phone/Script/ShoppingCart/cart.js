$(function () {
    $(".shop-cart-add").click(function () {
        console.log("aa")
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



    $(".btn2").click(function () {
        var goods = $(this).closest(".shop-cart-listbox1").find(".btn2");
        var goodsC = $(this).closest(".shop-cart-listbox1").find(".btn2:checked");
       
        if (goods.length == goodsC.length) {
           
            TotalPrice();
        } else {
          
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
        let ints = []

        if ($("#ckAll").is(':checked')) {
           
            $(".btn2:checked").each(function () {
                ints.push($(this)[0].id);
            })
           
            $(".shop-cart-listbox1").remove();
            $("#ckAll").prop('checked', false);
            TotalPrice();
        }
        if ($(".btn1").is(':checked')) {
            $(".btn1:checked").closest(".shop-cart-listbox1").remove();
            TotalPrice();
        }
        
        if ($(".btn2").is(':checked')) {
            
            $(".btn2:checked").each(function () {
                
                ints.push($(this)[0].id);
            })
            
            $(".btn2:checked").parent(".shop-cart-check2").parent(".index-goods").remove();
            TotalPrice();
        }
        deleCart(ints);
    });

    function deleCart(param) {
        $.ajax({
            url: "/Phone/PhoneShoppCart/deleCarts",
            method:'post',
            contentType: 'application/json',
            data: JSON.stringify(param)
        })
    };

    function TotalPrice() {
        var allprice = 0;
        $(".shop-cart-listbox1").each(function () {
            var oprice = 0;
            $(this).find(".btn2").each(function () {
                if ($(this).is(":checked")) {
                    var num = $(this).parents(".index-goods").find(".shop-cart-numer").val();
                   
                    var price = parseFloat($(this).parents(".index-goods").find(".priceJs").text().replace(",", ""));
                   
                    var total = price * num;
                    
                    oprice += total;
                    console.log(oprice)
                }
                $(this).closest(".shop-cart-listbox1").find(".ShopTotal").text(oprice.toFixed(2));
            });
            var oneprice = parseFloat($(this).find(".ShopTotal").text());
            allprice += oneprice;
        });
        
        $("#AllTotal").text(tranfoMoneyber(allprice.toFixed(2),2));
    };

    function tranfoMoneyber(number, n) {
        if (n != 0) {
            n = (n > 0 && n <= 20) ? n : 2;
        }
        number = parseFloat((number + "").replace(/[^\d\.-]/g, "")).toFixed(n) + "";
        var sub_val = number.split(".")[0].split("").reverse();
        var sub_xs = number.split(".")[1];

        var show_html = "";
        for (i = 0; i < sub_val.length; i++) {
            show_html += sub_val[i] + ((i + 1) % 3 == 0 && (i + 1) != sub_val.length ? "," : "");
        }

        if (n == 0) {
            return show_html.split("").reverse().join("");
        } else {
            return show_html.split("").reverse().join("") + "." + sub_xs;
        }
    }
});