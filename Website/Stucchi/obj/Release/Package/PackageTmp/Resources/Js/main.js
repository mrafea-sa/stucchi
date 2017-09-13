
$(document).ready(function () {

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
    $(".problems-widget").last().addClass("no-border");
    $(".custom-solutions").last().addClass("no-border");
    $(".problems-videos").last().addClass("no-border");

    $(".button-search").click(function () {
        $(".search-box-hide").toggleClass("open");

    });
    $(".close-btn").click(function () {
        $(".search-box-hide").removeClass("open");
    });

    $('.button-menu').on('click', function (e) {
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

    $('.menu li').on('touchstart', function () {
        // $('.menu li').removeClass('open');
        // $(this).addClass('open');

        $(".menu li").not($(this)).removeClass("open");
        $(this).toggleClass("open");
    });

    $('.box-product-search').on('click', function () {
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


    initSearchTrigger();
    initStucchiPlayers();
    initConvertor();
});

function initConvertor() {
    $(".convertor .cm").attr("prev", "");
    $(".convertor .in").attr("prev", "");

    $(".convertor .in").keyup(function (e) {
        if (getMeasureValue($(".convertor .in")) == null) return false;
        else $(".convertor .cm").val($(".convertor .in").val() == "" ? "" : roundit(parseFloat($(".convertor .in").val()) * 2.54));
    });

    $(".convertor .cm").keyup(function (e) {
        if (getMeasureValue($(".convertor .cm")) == null) return false;
        else $(".convertor .in").val( $(".convertor .cm").val() == "" ? "" : roundit(parseFloat($(".convertor .cm").val()) / 2.54));
    });
}

function getMeasureValue(obj) {
    var isValid = true;

    if ($(obj).val().match(/./)) isValid = ($(obj).val() + "0").match(/^[+-]?\d+(\.\d+)?$/);
    else isValid = $(obj).val().match(/^[+-]?\d+(\.\d+)?$/);

    if (!isValid && $(obj).val() != "") {
        $(obj).val($(obj).attr("prev"));
        return null;
    }
    $(obj).attr("prev", $(obj).val());

    return $(obj).val();
}

function roundit(which) {
    return Math.round(which * 100) / 100
}
function initSearchTrigger() {
    applySearchTrigger("search-top-panel");
    applySearchTrigger("search-inner-panel");
    applySearchTrigger("search-top-desktop-panel");
}
function applySearchTrigger(panelIdentifier) {

    searchPanel = $("." + panelIdentifier);
    if (getParameterByName("searchQuery", location.href) != null) {
        $(searchPanel).find("input[type=text]").val(getParameterByName("searchQuery", location.href));
    }

    $(searchPanel).find("input[type=text]").keypress(function (e) {
        if (e.which == 13) {
            location.href = "/search-results/?indexCatalogue=main&searchQuery=" + $(this).val() + "&wordsMode=AllWords";
            return false;
        }
    });

    $(searchPanel).find(".search-action").on("click", function (e) {
        location.href = "/search-results/?indexCatalogue=main&searchQuery=" + $("." + panelIdentifier).find("input[type=text]").val() + "&wordsMode=AllWords";
    });

}
function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}
function initStucchiPlayers() {
    $(".stucchi-player").on("click", function (e) {
        var vimeoID = $(this).attr("data-vimeoID");
        $(this).html('<iframe src="https://player.vimeo.com/video/' + vimeoID + '?autoplay=1" width="640" height="360" frameborder="0"></iframe>');
        $(this).addClass('active');
    });
}