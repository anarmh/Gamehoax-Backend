"use strict";


let toggleSearch =document.querySelector(".searchToggle")

toggleSearch.addEventListener("click", function(){

    toggleSearch.classList.toggle("active")
})


var swiperAbout = new Swiper(".about", {
    effect:"fold",
    spaceBetween: 30,
    pagination: {
      el: ".swiper-pagination",
      clickable: true,
    },
  });


  
var swiperTeamMember= new Swiper(".team-member", {
    slidesPerView: 4,
    spaceBetween: 30,
   
    navigation: {
      nextEl: ".swiper-button-next",
      prevEl: ".swiper-button-prev",
    },
    autoplay: {
      delay: 2500,
      disableOnInteraction: false,
    },
  });



  var swiperBrand= new Swiper(".brand", {
    slidesPerView: 5,
    spaceBetween: 30,
   
    navigation: {
      nextEl: ".swiper-button-next",
      prevEl: ".swiper-button-prev",
    },
    autoplay: {
      delay: 2500,
      disableOnInteraction: false,
    },
  });


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