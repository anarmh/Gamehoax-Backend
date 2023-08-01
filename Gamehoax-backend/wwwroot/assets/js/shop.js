"use strict";



let toggleSearch=document.querySelector(".searchToggle")

toggleSearch.addEventListener("click", function(){

    toggleSearch.classList.toggle("active")
})

//Filter Price
let minValue = document.getElementById("min-value");
let maxValue = document.getElementById("max-value");

function validateRange(minPrice, maxPrice) {
    if (minPrice > maxPrice) {
        
        let tempValue = maxPrice;
        maxPrice = minPrice;
        minPrice = tempValue;
    }

    minValue.innerHTML = "&euro;" + minPrice;
    maxValue.innerHTML = "&euro;" + maxPrice;
}

const inputElements = document.querySelectorAll(".range-slider input");
inputElements.forEach((element) => {
    element.addEventListener("change", (e) => {
        let minPrice = parseInt(inputElements[0].value);
        let maxPrice = parseInt(inputElements[1].value);

        validateRange(minPrice, maxPrice);
    });
});

validateRange(inputElements[0].value, inputElements[1].value);



$(document).ready(function(){

      $(window).scroll(function(){
        var scrollTop = 200;
        
        if($(window).scrollTop() >= scrollTop){
            $('#site-header-main').addClass("scroll-ivent" );
        }
        else{
            $('#site-header-main').removeClass("scroll-ivent");  
        }
         })


         $('.header-contact').click(function() {
        $(this).find('.contact-content').toggle();
         });



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
   


   

});



var toTopButton = document.getElementById("to-top");

  window.addEventListener("scroll", function() {
    if (window.pageYOffset > 500) {
      toTopButton.style.display = "block";
    } else {
      toTopButton.style.display = "none";
    }
  });

  toTopButton.addEventListener("click", function() {
    window.scrollTo({
      top: 0,
      behavior: "smooth"
    });
  });

