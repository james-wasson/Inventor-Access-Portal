$().ready(function () {
    $sidebar = $("#sidebar");
    $space = $("#spaceForSidebar");
    function resizeContent() {
        $space.css("min-width", $sidebar.outerWidth() + "px");
    } resizeContent();
    addResizeListener($sidebar[0], resizeContent);
});
