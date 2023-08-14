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
        console.log("#blog-search");
       /* $(".search-submit").trigger("click");*/

        let searchText = $(".search-field").val();
        console.log($(".search-field").val())
        let data = { searchText: searchText };
        console.log(data)
        let parent = $(".blogs-area");

        console.log(parent)
        $.ajax({
            url: "/Blog/Search",
            type: "GET",
            data: data,
            success: function (res) {

                $(parent).html(res);
                $(".search-field").val = "";
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