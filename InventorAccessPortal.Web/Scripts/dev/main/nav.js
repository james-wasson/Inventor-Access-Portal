// will collapse the nav is screen is too small
$().ready(function () {
    'use strict';

    var $sidebar = $("#sidebar");
    var $toggle = $sidebar.find(".sidebar-toggle");
    var $toggleIcon = $toggle.find(".icon");
    var isActive = $sidebar.hasClass("active");
    
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

    function toggle() {
        setActive();
        cookie.set("SidebarIsActive", isActive, 1);
    }

    // init
    (function () {
        var c = cookie.get("SidebarIsActive");
        if (c != null) isActive = (c == "true");
        $sidebar.css("transition", "none");// removes bouncing issue
        setActive(isActive);
        $sidebar.prop('style').removeProperty("transition"); // adds transition back
    }());

    // on collapse click
    $toggle.on("click", toggle);
});

