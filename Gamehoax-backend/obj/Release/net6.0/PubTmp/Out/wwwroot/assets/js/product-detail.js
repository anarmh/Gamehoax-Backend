"use strict"



let searchToggle=document.querySelector(".searchToggle")

searchToggle.addEventListener("click", function(){

  searchToggle.classList.toggle("active")
})



var swiperRelated = new Swiper(".related", {
    slidesPerView: 4,
    spaceBetween: 30,
   
    navigation: {
      nextEl: ".swiper-button-next",
      prevEl: ".swiper-button-prev",
    },
  });



$(document).ready(function(){
    
   

    $(".xzoom, .xzoom-gallery").xzoom({
        zoomWidth:"800",
        tint:"#333",
        zoomHeight: "auto",
        position: "right",
        smoothScale: 20,
        defaultScale:1,
        scroll: false,
        adaptive: true,
        Xoffset: 10,
    })

    
  


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


       let menues = $("#tabs li .tab");

     menues.on("click", function(e) {
    e.preventDefault();
    $(".active-menu").removeClass("active-menu");
    $(this).addClass("active-menu");
  
    let id = $(this).data("id");
    $(".item").addClass("d-none");
    $(`.item[data-id=${id}]`).removeClass("d-none");
  });

  
    $('#stars li').on('mouseover', function () {
        var onStar = parseInt($(this).data('value'), 10); 

       
        $(this).parent().children('li.star').each(function (e) {
            if (e < onStar) {
                $(this).addClass('hover');
            }
            else {
                $(this).removeClass('hover');
            }
        });

    }).on('mouseout', function () {
        $(this).parent().children('li.star').each(function (e) {
            $(this).removeClass('hover');
        });
    });


  
    $('#stars li').on('click', function () {
        var onStar = parseInt($(this).data('value'), 10); 
        var stars = $(this).parent().children('li.star');

        for (let i = 0; i < stars.length; i++) {
            $(stars[i]).removeClass('selected');
        }

        for (let i = 0; i < onStar; i++) {
            $(stars[i]).addClass('selected');
        }

      
        var ratingValue = parseInt($('#stars li.selected').last().data('value'), 10);
        var msg = "";
        if (ratingValue > 1) {
            msg = "Thanks! You rated this " + ratingValue + " stars.";
        }
        else {
            msg = "We will improve ourselves. You rated this " + ratingValue + " stars.";
        }


    });

    $(document).on("click", "#add-rating-value", function () {

        let rateValue = $(this).attr("data-x");
        
       let input= $(".rating-value");
        $(input).val(rateValue);
        console.log($(input).val(rateValue));
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