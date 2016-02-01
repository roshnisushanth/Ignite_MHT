/* ==========================================================================
main.js
========================================================================== */
//var $j = jQuery.noConflict();

$(function () {

    var dials = $(".dials ol li");
    var index;
    var number = $(".number");
    var total;

    dials.click(function (e) {
        e.stopPropagation();
        index = dials.index(this);

        if (index == 9) {

            number.append("*");

        } else if (index == 10) {

            number.append("0");

        } else if (index == 11) {

            number.append("#");

        } else if (index == 12) {

            //            number.empty();

        } else if (index == 14) {

            total = number.text();
            total = total.slice(0, -1);
            number.empty().append(total);

        } else if (index == 13) {

            //add any call action here

        } else {
            number.append(index + 1);
        }
    });


    $(document).keydown(function (e) {
       
        e.stopPropagation();
        if ($("#divdialpad").is(":visible")) {
            switch (e.which) {

                case 96:
                case 48:
                    number.append("0");
                    break;

                case 97:
                case 49:
                    number.append("1");
                    break;

                case 98:
                case 50:
                    number.append("2");
                    break;

                case 99:
                case 51:
                    number.append("3");
                    break;

                case 100:
                case 52:
                    number.append("4");
                    break;

                case 101:
                case 53:
                    number.append("5");
                    break;

                case 102:
                case 54:
                    number.append("6");
                    break;

                case 103:
                case 55:
                    number.append("7");
                    break;

                case 104:
                case 56:
                    number.append("8");
                    break;

                case 105:
                case 57:
                    number.append("9");
                    break;

                case 8:

                    total = number.text();
                    total = total.slice(0, -1);
                    number.empty().append(total);
                    break;

                case 27:

                    number.empty();
                    break;

                case 106:

                    number.append("*");
                    break;

                case 107:

                    number.append("+");
                    break;
                case 35:

                    number.append("#");
                    break;

                case 13:

                    $('.pad-action').click();
                    break;

                default: return;
            }

            e.preventDefault();
        }
    });
});


