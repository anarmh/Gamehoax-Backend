$(document).ready(function () {



    //SORT
    $(document).on("change", "#sort", function (e) {
        e.preventDefault();

        let sortValue = $(this).val();
        let data = { sortValue: sortValue };
        let parent = $(".productss-area");

        $.ajax({
            url: `/Shop/sort`,
            type: "GET",
            data: data,
            success: function (res) {

               
                $(parent).html(res);

            }

        })
    })

    
      //search
    $(document).on("submit", "#search-area", function (e) {
        e.preventDefault();
        
        let value = $(".search-text").val();
      
        let data = { searchText: value };
       
        let parent = $(".productss-area");
       

        $.ajax({
            url: "/Shop/Search",
            type: "GET",
            data: data,
            success: function (res) {
                
                $(parent).html(res);
                $(".search-text").val("");
            }

        })
    })

    //FIlter
    function getProductsById(clickedElem, url) {
        $(document).on("click", clickedElem, function (e) {
            e.preventDefault();

            let id = $(this).attr("data-id");
            let data = { id: id };
            let parent = $(".productss-area")
            $.ajax({
                url: url,
                type: "GET",
                data: data,
                success: function (res) {
                    
                    $(parent).html(res);
                }
            })
        })

    }
    getProductsById(".category", "/Shop/GetProductsByCategory");
    getProductsById(".tag", "/Shop/GetProductsByTag");



    //FILTER
    $(document).on("submit", "#filter-price", function (e) {
        e.preventDefault();
        let value1 = $(".min-price").val();
        let value2 = $(".max-price").val();
        let data = { value1: value1, value2: value2 }
        let parent = $(".productss-area");

        $.ajax({
            url: "/Shop/GetRangeProducts",
            type: "Get",
            data: data,
            success: function (res) {
                console.log(res)
                $(parent).html(res);

                //if (value1 == "10" && value2 == "500") {
                //    $(".shop-navigation").addClass("d-none")
                //}

            }

        })
    })

})