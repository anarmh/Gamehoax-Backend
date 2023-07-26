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