$().ready(function () {
    var $sidebar = $("#sidebar");
    var $space = $("#spaceForSidebar");
    function resizeContent() {
        $space.css("min-width", $sidebar.outerWidth() + "px");
    } resizeContent();
    addResizeListener($sidebar[0], resizeContent);
});
