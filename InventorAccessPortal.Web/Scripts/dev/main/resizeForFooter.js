$().ready(function () {
    var $footer = $("#footer");
    var $space = $("#spaceForFooter");
    function resizeContent() {
        $space.css("min-height", $footer.outerHeight() + "px");
    } resizeContent();
    if ($footer.length > 0)
        addResizeListener($footer[0], resizeContent);
});
