"use strict";

let toggleSearch =document.querySelector(".searchToggle")

toggleSearch.addEventListener("click", function(){

    toggleSearch.classList.toggle("active")
})


// let navlinks=document.querySelectorAll("#site-header-main .header-center ul li a")
// let windowPathname=window.location.pathname;

// navlinks.forEach(navlink=>{
//   const navLinkpathname=new URL(navlink.href).pathname;
//     if((windowPathname===navLinkpathname) || (windowPathname=== `/home.html` && navLinkpathname===`/`)){
//       navlink.classList.add(`active`)

//     }
    
  
// });

var swiper = new Swiper(".mySwiper", {
  effect: "cube",
  grabCursor: true,
  cubeEffect: {
    shadow: true,
    slideShadows: true,
    
    shadowOffset: 20,
    shadowScale: 0.94,
  },
  loop: true,
  // pagination: {
  //   el: ".swiper-pagination",
  // },
  autoplay: {
    delay: 2500,
    disableOnInteraction: false,
  },
});



var swiperCarousel = new Swiper(".swiper-carousel", {
  slidesPerView: 4,
  spaceBetween: 30,
 
  navigation: {
    nextEl: ".swiper-button-next",
    prevEl: ".swiper-button-prev",
  },
});


   
var swiperDealOfTheDay = new Swiper(".dealOfTheDay", {
  slidesPerView: 4,
  spaceBetween: 30,
 
  navigation: {
    nextEl: ".swiper-button-next",
    prevEl: ".swiper-button-prev",
  },
});

var swiperTestmonial = new Swiper(".testmonial", {
  pagination: {
    el: ".swiper-pagination",
    clickable: true,
  },
});


var swiperBlog = new Swiper(".blog", {
  slidesPerView: 3,
  spaceBetween: 30,
 
  navigation: {
    nextEl: ".swiper-button-next",
    prevEl: ".swiper-button-prev",
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






$(document).ready(function() {

  

  



 
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
    


      
  let menues = $("#our-products .tab-item .tab-title ");

  menues.on("click", function(e) {
    e.preventDefault();
    $(".active-menu").removeClass("active-menu");
    $(this).addClass("active-menu");
  
    let id = $(this).data("id");
    $(".item").addClass("d-none");
    $(`.item[data-id=${id}]`).removeClass("d-none");
  });
 

});



var countdownElement = document.querySelector('.countbox.hastime'); 
var targetDate = new Date(countdownElement.getAttribute('data-time')); 


function updateCountdown() {
  var currentDate = new Date(); 
  var remainingTime = targetDate - currentDate; 

  if (remainingTime > 0) {
    var days = Math.floor(remainingTime / (1000 * 60 * 60 * 24)); 
    var hours = Math.floor((remainingTime % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60)); 
    var minutes = Math.floor((remainingTime % (1000 * 60 * 60)) / (1000 * 60)); 
    var seconds = Math.floor((remainingTime % (1000 * 60)) / 1000); 

    
    countdownElement.innerHTML = '<span class="timebox day"><span class="timebox-inner"><strong>' + days + '</strong>days</span></span><span class="timebox hour"><span class="timebox-inner"><strong>' + hours + '</strong>hours</span></span><span class="timebox minute"><span class="timebox-inner"><strong>' + minutes + '</strong>mins</span></span><span class="timebox second"><span class="timebox-inner"><strong>' + seconds + '</strong>secs</span></span>';
  } else {
    countdownElement.innerHTML = 'Expired'; 
  }
}

setInterval(updateCountdown, 1000);


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