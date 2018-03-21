$(document).ready(function () {
    $content = $("#content");
    $footer = $("#footer");
    function resizeContent() {
        $content.css('max-height', ($(window).height() - $footer.height()) + "px");
    } resizeContent();
    addResizeListener($("#content")[0], resizeContent);
    $(window).resize(resizeContent);
});
