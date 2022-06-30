// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$('select').selectpicker();

$(document).ready(function () {
    // ANIMATION CONTROLLER
    $('#header div.dropdown-menu').addClass('animated fadeInUp');
    $('#header #shopping-cart .dropdown-menu').addClass('animated fadeInUp');
    $('#shopping-cart .dropdown-menu li a i').addClass('animated fadeIn');
    $('#left-menu').sidr({
        name: 'sidr-menu-left',
        side: 'left'
    });
    $('#right-menu').sidr({
        name: 'sidr-menu-right',
        side: 'right'
    });
    $('.zoom').zoom();
});