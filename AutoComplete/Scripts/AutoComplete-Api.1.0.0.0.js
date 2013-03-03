$(document).ready(function () {
    var data = ["12 Angry Men",
                    "Il buono, il brutto, il cattivo.",
                    "Inception",
                    "One Flew Over the Cuckoo's Nest",
                    "Pulp Fiction",
                    "Schindler's List",
                    "The Dark Knight",
                    "The Godfather",
                    "The Godfather: Part II",
                    "The Shawshank Redemption"
                   ];

    $("#products").kendoAutoComplete(data);
    // Cannot combine previous line with this line
    var autocomplete = $("#products").data("kendoAutoComplete");

    var setValue = function (e) {
        // If button clicked or pressed Enter, copy value to autocomplete
        if (e.type != "keypress" || kendo.keys.ENTER == e.keyCode)
            autocomplete.value($("#value").val());
    };
    $("#set").click(setValue);
    $("#value").keypress(setValue);

    var setSearch = function (e) {
        // If button clicked or pressed enter, search for this word
        if (e.type != "keypress" || kendo.keys.ENTER == e.keyCode)
            autocomplete.search($("#word").val());
    };
    $("#search").click(setSearch);
    $("#word").keypress(setSearch);

    $("#get").click(function () {
        alert(autocomplete.value());
    });

});