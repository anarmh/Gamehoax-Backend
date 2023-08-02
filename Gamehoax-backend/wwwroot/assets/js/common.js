$(document).ready(function () {



    //add cart

    $(document).on("click", "#add-to-cart", function (e) {
        console.log("#add-to-cart")
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


    //delete product from basket
    $(document).on("click", ".delete-product", function () {
        console.log(this)
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
                $(".cart-count").text('(' + res+')');
               
            }
        })
        return false;
    })
})