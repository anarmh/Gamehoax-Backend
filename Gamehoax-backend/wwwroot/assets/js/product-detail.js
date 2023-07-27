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
    $(`.item[data-id=${id}]`).removeClass("d-none").slick("setPosition");
  });

    /* 1. Visualizing things on Hover - See next part for action on click */
    $('#stars li').on('mouseover', function () {
        var onStar = parseInt($(this).data('value'), 10); // The star currently mouse on

        // Now highlight all the stars that's not after the current hovered star
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


    /* 2. Action to perform on click */
    $('#stars li').on('click', function () {
        var onStar = parseInt($(this).data('value'), 10); // The star currently selected
        var stars = $(this).parent().children('li.star');

        for (let i = 0; i < stars.length; i++) {
            $(stars[i]).removeClass('selected');
        }

        for (let i = 0; i < onStar; i++) {
            $(stars[i]).addClass('selected');
        }

        // JUST RESPONSE (Not needed)
        var ratingValue = parseInt($('#stars li.selected').last().data('value'), 10);
        var msg = "";
        if (ratingValue > 1) {
            msg = "Thanks! You rated this " + ratingValue + " stars.";
        }
        else {
            msg = "We will improve ourselves. You rated this " + ratingValue + " stars.";
        }


    });
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