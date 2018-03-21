$().ready(function () {
    var $footer = $("#footer");
    var $space = $("#spaceForFooter");
    function resizeContent() {
        $space.css("height", $footer.outerHeight() + "px");
    } resizeContent();
    addResizeListener($footer[0], resizeContent);
});
