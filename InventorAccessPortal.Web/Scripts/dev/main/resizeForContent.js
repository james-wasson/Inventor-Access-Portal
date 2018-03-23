$().ready(function () {
    var $footer = $("#footer");
    var $space = $("#spaceForFooter");
    var $body = $("#content-body");
    var $content = $("#content");
    function resizeContent() {
        var spaceleft = $content.innerHeight();
        $space.css("min-height", $footer.outerHeight() + "px");
        spaceleft -= $space.outerHeight();
        $body.css("min-height", spaceleft + "px");
    } resizeContent();
    addResizeListener($footer[0], resizeContent);
});
