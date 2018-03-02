// will collapse the nav is screen is too small
$().ready(function () {
    var $sidebar = $("#sidebar");
    var override = true;
    var isActive = $sidebar.hasClass("active");
    // sets the active class on sidebar is b is true, remove if b i false
    // if override is set will always be active
    function setActive(set) {
        if ((set === true || override) && !isActive) {
            if (!$sidebar.hasClass("active")) $sidebar.addClass("active");
            isActive = true;
        } else if(set === false && !override && isActive){
            $sidebar.removeClass("active");
            isActive = false;
        }
        // if set by window width hide toggle
        if (set === true) $(".sidebar-toggle").fadeOut(500);
        else $(".sidebar-toggle").fadeIn(500); // show toggle
    }
    function smallScreen() {
        if ($(window).width() < 768) {
            setActive(true);
        }
        else {
            setActive(false);
        }
    }
    // run at start
    $(window).ready(smallScreen);
    // run on window resize
    $(window).resize(smallScreen);
    // on collapse click
    $sidebar.find(".sidebar-toggle").on("click", function () {
        override = !override;
        smallScreen();
    });
});

