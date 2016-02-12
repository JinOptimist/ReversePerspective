'use strict';

$(document).ready(function () {
    var blockAnimation = false;
    $('body').bind('mousewheel', function (e) {
        if (e.originalEvent.wheelDelta / 120 > 0) {
            previousPage();
            //$(this).text('scrolling up !');
        }
        else {
            nextPage();
            //$(this).text('scrolling down !');
        }
        return false;
    });

    function scrollToDiv(block) {
        blockAnimation = true;
        $('html, body').animate({
            scrollTop: block.offset().top
        }, 500, "swing", function () { blockAnimation = false });
    }

    function nextPage() {
        if (blockAnimation)
            return;
        var pageId = $("#pageId").val();
        if (pageId < 3)
            pageId++;
        else
            return;
        $("#pageId").val(pageId);

        var page = $("#page" + pageId);
        scrollToDiv(page);
    }

    function previousPage() {
        if (blockAnimation)
            return;
        var pageId = $("#pageId").val();
        if (pageId > 0)
            pageId--;
        else
            return;
        $("#pageId").val(pageId);

        var page = $("#page" + pageId);
        scrollToDiv(page);
    }
});