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

                console.log(res)
                $(parent).html(res);

            }

        })
    })
})