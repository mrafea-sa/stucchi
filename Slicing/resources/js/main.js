
$(document).ready(function() {

    // handle links with @href started with '#' only
    // $(document).on('click', 'a[href^="#"]', function(e) {
    //     var id = $(this).attr('href');
    //     var $id = $(id);
    //     if ($id.length === 0) {
    //         return;
    //     }
    //     e.preventDefault();
    //     var pos = $id.offset().top;
    //     $('body, html').animate({ scrollTop: pos }, 2000);
    // });

   // $('.menu li').on('click ', function(e) {
   //      $(this).toggleClass('open');
   //  });
  $(".button-search").click(function() {
        $(".search-box-hide").toggleClass("open");
    });
    $(".close-btn").click(function() {
        $(".search-box-hide").removeClass("open");
    });

 $('.button-menu').on('click', function(e) {
        e.preventDefault();
        e.stopPropagation();
        $('.menu').toggleClass('open');
        $(document).one('click', function closeMenu(e) {
            if ($('.menu').has(e.target).length === 0) {
                $('.menu').removeClass('open');
            } else {
                $(document).one('click', closeMenu);
            }
        });
    });

 $('.menu li').on('touchstart', function(){
    // $('.menu li').removeClass('open');
    // $(this).addClass('open');

    $( ".menu li" ).not($( this )).removeClass( "open" );
        $( this ).toggleClass( "open" );
});

 $('.box-product-search').on('click', function(){
    $('.box-product-search.active').removeClass('active');
    $(this).addClass('active');
});

    // search top header
    // $(".js-celebros-form").submit(function() {
    //     var searchBox = $(this).find(".input-search");
    //     if (searchBox.val() == 'Search...') {
    //         searchBox.val('');
    //     }
    //     return true;
    // });

    // $(".js-icon-magnifier").click(function(event) {
    //     console.log('aaaaa');
    //     var searchInput = $(this).parents('.js-celebros-form').find('input');
    //     if (searchInput.val() == "") {
    //         searchInput.focus();
    //         return false;
    //     }
    // });
    // button menu mobile
//     $('.button-open-menu').on('click', function(e) {
//         e.preventDefault();
//         e.stopPropagation();
//         $('.menu').addClass('open');
//         $('header').addClass('menu-fixed');
//         $('body').addClass('hidden-y');

//         $(document).one('click', function closeMenu(e) {
//             if ($('.menu').has(e.target).length === 0) {
//                 $('.menu').removeClass('open');
//                 $('header').removeClass('menu-fixed');
//                 $('body').removeClass('hidden-y');

//             } else {
//                 $(document).one('click', closeMenu);
//             }
//         });
//     });
//     $('.button-close-menu').on('click', function(e) {
//         e.preventDefault();
//         e.stopPropagation();
//         $('.menu').removeClass('open');
//         $('header').removeClass('menu-fixed');
//         $('body').removeClass('hidden-y');

//         $(document).one('click', function closeMenu2(e) {
//             if ($('.menu').has(e.target).length === 0) {
//                 $('.menu').removeClass('open');
//                 $('header').removeClass('menu-fixed');
//                 $('body').removeClass('hidden-y');

//             } else {
//                 $(document).one('click', closeMenu2);
//             }
//         });
//     });
//     $('.owl-carousel').owlCarousel({
//         items: 1,
//         loop: true,
//         margin: 10,
//         dots: true,
//         nav: true,
//         smartSpeed: 4000,
//         autoplay: true,
//         autoplayTimeout: 8000,
//         autoplayHoverPause: false
//     });

//     $('.nav-scroll li').click(function() {
//         $(this).siblings().removeClass('active');
//         $(this).addClass('active');
//     });
//     $(document).on('click', 'a.page-scroll', function(event) {
//         var $anchor = $(this);
//         $('html, body').stop().animate({
//             scrollTop: $($anchor.attr('href')).offset().top
//         }, 1500, 'easeInOutExpo');
//         event.preventDefault();
//     });
//     $('.cotnact-us').click(function() {
//         $('.contact-us-overlay').addClass('active');
//     });
//     $('.close-contact-us').click(function() {
//         $('.contact-us-overlay').removeClass('active');
//     });
//     $('.contact-us-overlay .column').click(function() {
//         $(this).siblings().removeClass('focus');
//         $(this).addClass('focus');
//     $(document).one('click', function closeMenu4(e) {
//             if ($('.contact-us-overlay .column').has(e.target).length === 0) {
//                 $('.contact-us-overlay .column').removeClass('focus');
//             } else {
//                 $(document).one('click', closeMenu4);
//             }
//         });
//     });
// });

// $(window).scroll(function() {
//     var scroll = $(window).scrollTop();
//     if (scroll >= 100) {
//         $("header").addClass("scroll");
//     } else {
//         $("header").removeClass("scroll");
//     }

    //  if (scroll >= 100) {
    //     $(this).addClass("nav-fix");
    // } else {
    //     $(this).removeClass("nav-fix");
    // }

});
