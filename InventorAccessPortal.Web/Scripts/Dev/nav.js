// will collapse the nav is screen is too small
(function ($, viewport) {
    function smallScreen() {
        if ($(window).width() < 768) {
            $("#sidebar").addClass("active");
        }
        else {
            $("#sidebar").removeClass("active");
        }
    } smallScreen(); // run at start
    $(window).resize(smallScreen);
})(jQuery);
