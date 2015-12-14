'use strict';

$(document).ready(function () {


    $(".hero").live("click", function () {
        var hero = $(this);
        alert(hero.val());
    });

});