$(document).ready(function () {


    $(document).on("click", ".status-slider", function () {

        let sliderId = $(this).attr("data-id");

        let changedElem = $(this);

        let data = { id: sliderId };

        $.ajax({
            url: "slider/changestatus",
            type: "Post",
            data: data,
            success: function (res) {
                if (res) {
                    $(changedElem).removeClass("status-false");
                    $(changedElem).addClass("status-true");
                } else {
                    $(changedElem).removeClass("status-true");
                    $(changedElem).addClass("status-false");
                }
            }

        })

    })
})