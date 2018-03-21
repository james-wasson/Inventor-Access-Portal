$().ready(function () {
    var $footer = $("#footer");
    var $space = $("#spaceForFooter");
    var $content = $("#content-body");
    function resizeContent() {
        $space.css("min-height", $footer.outerHeight() + "px");
    } resizeContent();
    addResizeListener($footer[0], resizeContent);
});
