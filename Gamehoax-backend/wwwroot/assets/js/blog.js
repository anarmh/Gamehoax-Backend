"use strict"



let toggleSearch =document.querySelector(".searchToggle")

toggleSearch.addEventListener("click", function(){

    toggleSearch.classList.toggle("active")
})



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



    //search
    $(document).on("submit", "#blog-search", function (e) {
        e.preventDefault();

      
        let searchText = $(".search-field-input").val();
        let parent = $(".blogs-area");

      
        $.ajax({
            url: `/Blog/Search?searchText=${searchText}`,
            type: "GET",
            success: function (res) {
                $(parent).html(res);
                $(".search-field-input").val("");
            }

        })
    })

    $(document).on("submit", "#blog-search-mobile", function (e) {
        e.preventDefault();

        
        let searchText = $(".search-field-input-mobile").val();
        let parent = $(".blogs-area-mobile");

        console.log(parent)
        $.ajax({
            url: `/Blog/Search?searchText=${searchText}`,
            type: "GET",
            success: function (res) {
                $(parent).html(res);
                $(".search-field-input-mobile").val("");
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