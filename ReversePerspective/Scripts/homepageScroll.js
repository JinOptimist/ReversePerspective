'use strict';

$(document).ready(function () {
    var blockAnimation = false;
    var countOfBlocks = $(".content").length;

    function init() {
        var linkPattern = $("<a href='#'></a>");
        for (var i = 0; i < countOfBlocks; i++) {
            var link = linkPattern.clone();
            $(".paging").append(link);

            var content = $(".content");

            for (var j = 0; j < content.length; j++) {
                $(content[j]).attr("id", "page" + (j+1));
            }
        }
        showDiv(1);
    };
    init();

    $('body').bind('mousewheel', function (e) {
        if (blockAnimation)
            return;

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

    function showDiv(pageId) {
        blockAnimation = true;
        $("#pageId").val(pageId);

        $(".content").hide(1000);
        $(".paging .active").removeClass("active");
        $(".paging a:eq(" + (pageId - 1) + ")").addClass("active");
        $("#page" + pageId).show(1000);

        blockAnimation = false;
    }

    function nextPage() {
        if (blockAnimation)
            return;
        var pageId = $("#pageId").val();
        if (pageId < countOfBlocks)
            pageId++;
        else
            return;
        showDiv(pageId);
    }

    function previousPage() {
        if (blockAnimation)
            return;
        var pageId = $("#pageId").val();
        if (pageId > 1)
            pageId--;
        else
            return;
        showDiv(pageId);
    }
});