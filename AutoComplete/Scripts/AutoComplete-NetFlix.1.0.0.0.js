$(document).ready(function () {
    $("#MovieTitle").kendoAutoComplete({
        minLength: 3,
        dataTextField: "Name",      // Json property to use
        filter: "startswith",       // startswith, endswith, contains (lower case)
        dataSource: {
            type: "odata",          // Data protocol
            serverFiltering: true,  // Yes, handle filtering on the server
            serverPaging: true,     // Yes, handle paging on the server (only one page will be served)
            pageSize: 20,           // Show only 20 Movie Titles
            transport: {            // Read, without extra parameters
                read: "http://odata.netflix.com/Catalog/Titles"
            }
        }
    });

    // set width of the drop-down list
    $("#MovieTitle").data("kendoAutoComplete").list.width(200);
});