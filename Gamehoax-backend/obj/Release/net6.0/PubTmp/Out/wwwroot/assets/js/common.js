﻿$(document).ready(function () {


    $(document).on("click", ".click-search", function (e) {
        e.preventDefault();
        if ($(".search-text").val() != "") {
            $("#search-area").submit();
        }
      
    });


    //let cart_count = $(".cart-count").text();
    //console.log(cart_count);

    //add  cart from shop

    $(document).on("click", "#add-to-cart", function (e) {
      
        let id = $(this).attr("data-id");
        
        let data = { id: id };
        $.ajax({
            type: "POST",
            url: "/Cart/AddToCart",
            data: data,
            success: function (res) {
             
                $('.cart-count').text('(' + res + ')');
                toastr["success"]("Product added!")

                toastr.options = {
                    "closeButton": false,
                    "debug": false,
                    "newestOnTop": true,
                    "progressBar": false,
                    "positionClass": "toast-top-right",
                    "preventDuplicates": false,
                    "onclick": null,
                    "showDuration": "100",
                    "hideDuration": "200",
                    "timeOut": "500",
                    "extendedTimeOut": "300",
                    "showEasing": "swing",
                    "hideEasing": "linear",
                    "showMethod": "fadeIn",
                    "hideMethod": "fadeOut"
                }
            }
        })
        return false;
    })


    function grandTotal() {
        let tbody = $(".tbody-basket").children();
       
        let sum = 0;
        for (var prod of tbody) {
            let price = parseFloat($(prod).children().eq(5).children().eq(1).text());
            console.log('PRICE: ' + price);
            
            sum += price
        }
        console.log('SUM: ' + sum);
        $(".grand-total").text(sum.toFixed(2));
      
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
                if (mega == 0) {
                    $(".cart-count").text('');
                }
                else {
                    $(".cart-count").text('(' + mega+')');

                }
                grandTotal();
            }
        })
        return false;
    })

    function subTotal(res, priceValue, total_el) {
        //console.log('REs: ' + res, 'PRiceVal: ' + priceValue, 'Total_el: ' + total_el);
        let mega = count_all();
        //console.log('MEGA: ' + mega);
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
        //let total_el = $(this).parent().parent().next()[0].innerText.slice(1);
        //console.log('UUU: ' + total.el, $(this).parent().parent().next()[0].innerText.slice(1));
        let counterss = $(this).parent().prev();
        //console.log(count);
        //console.log(counterss);

       
        $.ajax({
            type: "POST",
            url: `Cart/IncrementProductCount?id=${id}`,
            success: function (res) {
                ++res;
                //console.log(res, priceValue, total, total_el);
                
                
                $(counterss).val(res);
                let subtotal = parseFloat(priceValue * res);
                $(total_el).text(subtotal);
                grandTotal();
                subTotal(res, priceValue, total, total_el)
            }
        })
    })




    //decrement product count
    $(document).on("click", ".dec", function (e) {
        let counterss = $(this).parent().prev();

        if (($(counterss).val() - 1) <= 0) {
            e.preventDefault();
            return;
        }
        e.preventDefault();
        let id = $(this).parent().parent().parent().attr("data-id");

        let priceText = ($(this).closest(".product-quantity").prev().find(".item-price")).text();
        let priceValue = parseFloat(priceText);

        let total = parseFloat($(this).parent().parent().next().find(".total-price").text());
        let total_el = $(this).parent().parent().next().find(".total-price");


        $.ajax({
            type: "POST",
            url: `/Cart/DecrementProductCount?id=${id}`,
            success: function (res) {
                if ($(counterss).val() < 1) {
                    return;
                }
                res--;
               
                
                $(counterss).val(res);
                let subtotal = parseFloat(priceValue * res);
                $(total_el).text(subtotal);
                grandTotal();
                subTotal(res, priceValue, total, total_el)
            }
        })
    })

    //add to cart from home
    $(document).on("click", "#add-to-cart-rating", function (e) {

        let id = $(this).attr("data-id");
       
        let data = { id: id };
        $.ajax({
            type: "POST",
            url: "/Cart/AddToCart",
            data: data,
            success: function (res) {
                $('.cart-count').text('(' + res + ')');
                toastr["success"]("Product added!");

                toastr.options = {
                    "closeButton": false,
                    "debug": false,
                    "newestOnTop": true,
                    "progressBar": false,
                    "positionClass": "toast-top-right",
                    "preventDuplicates": false,
                    "onclick": null,
                    "showDuration": "100",
                    "hideDuration": "200",
                    "timeOut": "500",
                    "extendedTimeOut": "300",
                    "showEasing": "swing",
                    "hideEasing": "linear",
                    "showMethod": "fadeIn",
                    "hideMethod": "fadeOut"
                }
            }
        })
        return false;
    })


    $(document).on("click", "#add-to-cart-date", function (e) {

        let id = $(this).attr("data-id");
       
        let data = { id: id };
        $.ajax({
            type: "POST",
            url: "/Cart/AddToCart",
            data: data,
            success: function (res) {
                $('.cart-count').text('(' + res + ')');
                toastr["success"]("Product added!");
                toastr.options = {
                    "closeButton": false,
                    "debug": false,
                    "newestOnTop": true,
                    "progressBar": false,
                    "positionClass": "toast-top-right",
                    "preventDuplicates": false,
                    "onclick": null,
                    "showDuration": "100",
                    "hideDuration": "200",
                    "timeOut": "500",
                    "extendedTimeOut": "300",
                    "showEasing": "swing",
                    "hideEasing": "linear",
                    "showMethod": "fadeIn",
                    "hideMethod": "fadeOut"
                }
            }
        })
        return false;
    })

    $(document).on("click", "#add-to-cart-discount", function (e) {

        let id = $(this).attr("data-id");
       
        let data = { id: id };
        $.ajax({
            type: "POST",
            url: "/Cart/AddToCart",
            data: data,
            success: function (res) {
                $('.cart-count').text('(' + res + ')');
                toastr["success"]("Product added!");
                toastr.options = {
                    "closeButton": false,
                    "debug": false,
                    "newestOnTop": true,
                    "progressBar": false,
                    "positionClass": "toast-top-right",
                    "preventDuplicates": false,
                    "onclick": null,
                    "showDuration": "100",
                    "hideDuration": "200",
                    "timeOut": "500",
                    "extendedTimeOut": "300",
                    "showEasing": "swing",
                    "hideEasing": "linear",
                    "showMethod": "fadeIn",
                    "hideMethod": "fadeOut"
                }
            }
        })
        return false;
    })

    $(document).on("click", "#add-to-cart-sale", function (e) {

        let id = $(this).attr("data-id");
        
        let data = { id: id };
        $.ajax({
            type: "POST",
            url: "/Cart/AddToCart",
            data: data,
            success: function (res) {
                $('.cart-count').text('(' + res + ')');
                toastr["success"]("Product added!");
                toastr.options = {
                    "closeButton": false,
                    "debug": false,
                    "newestOnTop": true,
                    "progressBar": false,
                    "positionClass": "toast-top-right",
                    "preventDuplicates": false,
                    "onclick": null,
                    "showDuration": "100",
                    "hideDuration": "200",
                    "timeOut": "500",
                    "extendedTimeOut": "300",
                    "showEasing": "swing",
                    "hideEasing": "linear",
                    "showMethod": "fadeIn",
                    "hideMethod": "fadeOut"
                }
            }
        })
        return false;
    })

   
    //add to wish
    $(document).on("click", ".add-to-wish", function (e) {
    //$(".add-to-wish").unbind().click(function (e) {
        e.stopPropagation();
        let el = $(this);
        if (el.hasClass('checked')) {
            let id = el.attr("data-id");
            
            let data = { id: id };
            let countWishlist = $(".wish-count");
            $.ajax({
                type: "Post",
                url: `/Wishlist/DeleteDataFromWishlist?id=${id}`,
                data: data,
                success: function (res) {
                    el.removeClass('checked'); 
                    $(countWishlist).text(`(${res})`);
                    res--;
                    if (res == 0) {
                        $(countWishlist).text(``);
                    }
                    else {
                        $(countWishlist).text(`(${res})`);
                    }
                   
                }
            })
            el.removeClass('checked');
        } else {
            let id = $(this).attr("data-id");
           
            let data = { id: id };
            let countWishlist = $(".wish-count");
            $.ajax({
                type: "POST",
                url: `/Wishlist/AddToWishlist?id=${id}`,
                data: data,
                success: function (res) {
                    el.addClass('checked');
                    if (res == 0) {
                        $(countWishlist).text('');
                    } else {
                        $(countWishlist).text(`(${res})`);
                    }
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
            url: `/Wishlist/DeleteDataFromWishlist?id=${id}`,
            data: data,
            success: function (res) {
              
                $(prod).remove();
                if ($('.product-table-wishlist .web tr').length == 0) {
                    $(".product-table-wishlist").addClass("d-none");
                    $(".alert-warning").removeClass("d-none");
                }
                res--;
                if (res == 0) {
                    $(".wish-count").text("");
                } else {
                    $(".wish-count").text(`(${res})`)
                }
            }
        })
        return false;
    })





    //Product-detail-AddToCart

    $(document).on("click", ".single-add-to-cart", function (e) {
       console.log()
        let id = $(this).attr("data-id");
        let inputCount = $(".qty-input").val();
       
        let data = { id: id, count: inputCount };
      
        let countBasket = (".cart-count");

        $.ajax({
            type: "POST",
            url: "/Cart/AddToCart",
            data: data,
            success: function (res) {
                $('.qty-input').val(1);
                $(countBasket).text(`(${res})`);
                toastr["success"]("Product added!");
                toastr.options = {
                    "closeButton": false,
                    "debug": false,
                    "newestOnTop": true,
                    "progressBar": false,
                    "positionClass": "toast-top-right",
                    "preventDuplicates": false,
                    "onclick": null,
                    "showDuration": "100",
                    "hideDuration": "200",
                    "timeOut": "500",
                    "extendedTimeOut": "300",
                    "showEasing": "swing",
                    "hideEasing": "linear",
                    "showMethod": "fadeIn",
                    "hideMethod": "fadeOut"
                }
            }
        })
        return false;
    })


   

    $(document).on("click", '.incrementDetail', function (e) {
        let count = $('.qty-input').val();
        $('.qty-input').val(parseInt(count)+1);
    });

    $(document).on("click", '.decrementDetail', function (e) {
        let count = $('.qty-input').val();
        if ($('.qty-input').val() == 1) {
            return;
        }
        $('.qty-input').val(parseInt(count) - 1);
       
    });
    
    $(document).on("click", "#add-to-cart-productDetail", function (e) {

        let id = $(this).attr("data-id");
       
        let data = { id: id };
        $.ajax({
            type: "POST",
            url: "/Cart/AddToCart",
            data: data,
            success: function (res) {
                $('.cart-count').text('(' + res + ')');

                toastr["success"]("Product added!");
                toastr.options = {
                    "closeButton": false,
                    "debug": false,
                    "newestOnTop": true,
                    "progressBar": false,
                    "positionClass": "toast-top-right",
                    "preventDuplicates": false,
                    "onclick": null,
                    "showDuration": "100",
                    "hideDuration": "200",
                    "timeOut": "500",
                    "extendedTimeOut": "300",
                    "showEasing": "swing",
                    "hideEasing": "linear",
                    "showMethod": "fadeIn",
                    "hideMethod": "fadeOut"
                }
            }
        })
        return false;
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


let navlinks = document.querySelectorAll("#site-header-main .header-center ul li a")
let windowPathname = window.location.pathname;

navlinks.forEach(navlink => {
    const navLinkpathname = new URL(navlink.href).pathname;
    
    if ((windowPathname === navLinkpathname) || (windowPathname === `/home.html` && navLinkpathname === `/`)) {
        navlink.classList.add(`active`)

    }


});