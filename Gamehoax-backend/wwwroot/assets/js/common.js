﻿$(document).ready(function () {


  


    //let cart_count = $(".cart-count").text();
    //console.log(cart_count);

    //add  cart from shop

    $(document).on("click", "#add-to-cart", function (e) {
      
        let id = $(this).attr("data-id");
        console.log(id)
        let data = { id: id };
        $.ajax({
            type: "POST",
            url: "/Cart/AddToCart",
            data: data,
            success: function (res) {
                $('.cart-count').text('(' + res +')');
            }
        })
        return false;
    })


    function grandTotal() {
        let tbody = $(".tbody-basket").children()
        let sum = 0;
        for (var prod of tbody) {
            let price = parseFloat($(prod).children().eq(5).children().eq(1).text())

            sum += price
        }
        $(".grand-total").text(sum + ".00");
    }

    //delete product from basket
    $(document).on("click", ".delete-product", function () {
       
        let id = $(this).parent().parent().attr("data-id");
        let prod = $(this).parent().parent();
        
        let tbody = $(".tbody-basket").children();
        let data = { id: id };

        $.ajax({
            type: "POST",
            url: `Cart/DeleteDataFromBasket`,
            data: data,
            success: function (res) {
                if ($(tbody).length == 1) {
                    $(".product-table").addClass("d-none");
                    $(".alert-warning").removeClass("d-none");
                    $(".footer-total").addClass("d-none");
                }
                prod.remove();
                res--;
                let mega = count_all();
                $(".cart-count").text('(' + mega+')');
                grandTotal();
            }
        })
        return false;
    })

    function subTotal(res, priceValue, total_el) {
        
        //counterss.val(res);

        
        let mega = count_all();
        //console.log(mega);
        $(".cart-count").text(`(${mega})`);
    }


    //increment product count
    $(document).on("click", ".inc", function (e) {
        e.preventDefault();
        let id = $(this).parent().parent().parent().attr("data-id");
       
        let priceText = ($(this).closest(".product-quantity").prev().find(".item-price")).text();
        let priceValue = parseFloat(priceText);

        let total = parseFloat($(this).parent().parent().next().find(".total-price").text());
        let total_el = $(this).parent().parent().next().find(".total-price");
        let counterss = $(this).parent().prev();
        //console.log(count);
        //console.log(counterss);

       
        $.ajax({
            type: "POST",
            url: `Cart/IncrementProductCount?id=${id}`,
            success: function (res) {
                res++;
                subTotal(res, priceValue, total, total_el)
                grandTotal();
                counterss.val(res);
                let subtotal = parseFloat(priceValue * res);
                $(total_el).text(subtotal);
            }
        })
    })




    //decrement product count
    $(document).on("click", ".dec", function (e) {
        e.preventDefault();
        let id = $(this).parent().parent().parent().attr("data-id");

        let priceText = ($(this).closest(".product-quantity").prev().find(".item-price")).text();
        let priceValue = parseFloat(priceText);

        let total = parseFloat($(this).parent().parent().next().find(".total-price").text());
        let total_el = $(this).parent().parent().next().find(".total-price");
        let counterss = $(this).parent().prev();


        $.ajax({
            type: "POST",
            url: `Cart/DecrementProductCount?id=${id}`,
            success: function (res) {
                if ($(counterss).val() == 1) {
                    return;
                }
                res--;
                subTotal(res, priceValue, total, total_el)
                grandTotal();
                counterss.val(res);
                let subtotal = parseFloat(priceValue * res);
                $(total_el).text(subtotal);
            }
        })
    })

    //add to cart from home
    $(document).on("click", "#add-to-cart-rating", function (e) {

        let id = $(this).attr("data-id");
        console.log(id)
        let data = { id: id };
        $.ajax({
            type: "POST",
            url: "/Cart/AddToCart",
            data: data,
            success: function (res) {
                $('.cart-count').text('(' + res + ')');
            }
        })
        return false;
    })


    $(document).on("click", "#add-to-cart-date", function (e) {

        let id = $(this).attr("data-id");
        console.log(id)
        let data = { id: id };
        $.ajax({
            type: "POST",
            url: "/Cart/AddToCart",
            data: data,
            success: function (res) {
                $('.cart-count').text('(' + res + ')');
            }
        })
        return false;
    })

    $(document).on("click", "#add-to-cart-discount", function (e) {

        let id = $(this).attr("data-id");
        console.log(id)
        let data = { id: id };
        $.ajax({
            type: "POST",
            url: "/Cart/AddToCart",
            data: data,
            success: function (res) {
                $('.cart-count').text('(' + res + ')');
            }
        })
        return false;
    })

    $(document).on("click", "#add-to-cart-sale", function (e) {

        let id = $(this).attr("data-id");
        console.log(id)
        let data = { id: id };
        $.ajax({
            type: "POST",
            url: "/Cart/AddToCart",
            data: data,
            success: function (res) {
                $('.cart-count').text('(' + res + ')');
            }
        })
        return false;
    })

   
    //add to wish
    $(".add-to-wish").unbind().click(function (e) {
        e.stopPropagation();
        let el = $(this);
        if (el.hasClass('checked')) {
            let id = el.attr("data-id");
           
            let data = { id: id };
            let countWishlist = $(".wish-count");
            $.ajax({
                type: "Post",
                url: `Wishlist/DeleteDataFromWishlist`,
                data: data,
                success: function (res) {
                    el.removeClass('checked'); 
                    $(countWishlist).text(`(${res})`);
                    res--;
                    $(countWishlist).text(`(${res})`);
                }
            })
            el.removeClass('checked');
        } else {
            let id = $(this).attr("data-id");
            console.log('ID' + id);
            let data = { id: id };
            let countWishlist = (".wish-count");
            $.ajax({
                type: "POST",
                url: "/Wishlist/AddToWishlist",
                data: data,
                success: function (res) {
                    el.addClass('checked');
                    $(countWishlist).text(`(${res})`);
                }
            })
        }
        return false;
    })

    //delete product from wishlist
    $(document).on("click", ".delete-wishlist", function () {

        let id = $(this).parent().parent().attr("data-id");
        let prod = $(this).parent().parent();
        let tbody = $(".tbody-wishlist").children();
        let data = { id: id };
      

        $.ajax({
            type: "Post",
            url: `Wishlist/DeleteDataFromWishlist`,
            data: data,
            success: function (res) {
                if ($(tbody).length == 1) {
                    $(".product-table-wishlist").addClass("d-none");
                    $(".alert-warning").removeClass("d-none");
                  

                }
                $(prod).remove();
                res--;
                $(".wish-count").text(`(${res})`)
            }
        })
        return false;
    })





    //Product-detail-AddToCart

    $(document).on("click", ".single-add-to-cart", function (e) {
       
        let id = $(this).attr("data-id");
      
        let data = { id: id, count: $('.qty').val() };
        let countBasket = (".cart-count");

        $.ajax({
            type: "POST",
            url: "/Cart/AddToCart",
            data: data,
            success: function (res) {
                $('.qty').val(1);
                $(countBasket).text(`(${res})`);
            }
        })
        return false;
    })

    //Increment product count-Detail
    $(document).on("click", ".incrementDetail", function () {
        let id = $(this).attr("data-id");
        let input = $(this).parent().prev();
        
        let inputValue = $(this).parent().prev().val();
       
        inputValue++;
        $(input).val(inputValue);
       
        /*$.ajax({
            type: "POST",
            url: `/Cart/IncrementProductCount?id=${id}`,
            success: function (res) {
                console.log(res);
            }
        })*/
    })
    //Decrement product count-Detail
    $(document).on("click", ".decrementDetail", function () {
        let id = $(this).attr("data-id");
        let input = $(this).parent().prev();
        let inputValue = $(this).parent().prev().val();
        if (inputValue != 1) {
            inputValue--;
           /* $.ajax({
                type: "POST",
                url: `/Cart/DecrementProductCount?id=${id}`,
                success: function (res) {
                    console.log(res)
                    if ($(inputValue) == 1) {
                        return;
                    }
                }
            })*/
            $(input).val(inputValue);
        }
    })

})


function count_all() {
    let cnts = (document.querySelectorAll('.counter'));
    //console.log(".counter")
    let total_sum = 0;
    cnts.forEach(cnt => {
        total_sum += parseFloat(cnt.value)
    })
    return (total_sum);
}