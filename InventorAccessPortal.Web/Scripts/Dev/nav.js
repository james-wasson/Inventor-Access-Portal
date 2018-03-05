// will collapse the nav is screen is too small
$().ready(function () {
    var $sidebar = $("#sidebar");
    var $toggle = $sidebar.find(".sidebar-toggle");
    var $toggleIcon = $toggle.find(".icon");
    var isActive = $sidebar.hasClass("active");
    
    function windowIsSmall() {
        return $(window).width() < 768;
    }
    // sets the active class on sidebar is b is true, remove if b i false
    // if override is set will always be active
    function setActive(override) {
        if (!$sidebar.hasClass("active") || override) {
            $sidebar.addClass("active");
            isActive = true;
        } else {
            $sidebar.removeClass("active");
            isActive = false;
        }
    }
    function toggleIcon() {
        function toggleI(left) {
            if (left) $toggleIcon.removeClass("fa-angle-double-right").addClass("fa-angle-double-left");
            else $toggleIcon.removeClass("fa-angle-double-left").addClass("fa-angle-double-right");
        }
        if (windowIsSmall()) {
            toggleI(isActive)
        } else {
            toggleI(!isActive)
        }
    }

    function toggle() {
        setActive();
        toggleIcon();
        cookie.set("SidebarIsActive", isActive, 1);
    }

    // init
    (function () {
        var c = cookie.get("SidebarIsActive");
        if (c != null) isActive = (c == "true");
        $sidebar.css("transition", "none");// removes bouncing issue
        setActive(isActive);
        toggleIcon();
        $sidebar.prop('style').removeProperty("transition"); // adds transition back
    }());

    // run on window resize
    $(window).resize(toggleIcon);
    // on collapse click
    $toggle.on("click", toggle);
});

