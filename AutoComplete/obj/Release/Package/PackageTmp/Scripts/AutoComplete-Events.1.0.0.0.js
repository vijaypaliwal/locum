$(document).ready(function () {
    function onOpen() {
        if ("kendoConsole" in window) {
            kendoConsole.log("event :: open");
        }
    };

    function onClose() {
        if ("kendoConsole" in window) {
            kendoConsole.log("event :: close");
        }
    };

    function onChange() {
        if ("kendoConsole" in window) {
            kendoConsole.log("event :: change");
        }
    };

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

    $("#products").kendoAutoComplete({
        dataSource: data,
        change: onChange,
        close: onClose,
        open: onOpen
    });

});