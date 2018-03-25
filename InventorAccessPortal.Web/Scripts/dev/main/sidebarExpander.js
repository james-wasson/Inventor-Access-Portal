// will collapse the nav is screen is too small
$().ready(function () {
    'use strict';

    var $sidebar = $("#sidebar");
    var $toggle = $sidebar.find(".sidebar-toggle");
    var $toggleIcon = $toggle.find(".icon");
    var isActive = $sidebar.hasClass("active");

    var transitionElements = [$sidebar, $toggle];
    
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
        for (var e in transitionElements) // removes transition, bouncing issue
            transitionElements[e].css("transition", "none"); 
        setActive(isActive);
        setTimeout(function () { // adds transition back
            for (var e in transitionElements) 
                transitionElements[e].prop('style').removeProperty("transition");
        }, 50);
    }());

    // on collapse click
    $toggle.on("click", toggle);
});

