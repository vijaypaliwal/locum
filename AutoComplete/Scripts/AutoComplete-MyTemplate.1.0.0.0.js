// Author: Brother Bill, brotherbill@mail.com, May freely make changes and copies.
$(document).ready(function () {
    $("#MovieTitle").kendoAutoComplete({
        minLength: 2,
        dataTextField: "MovieTitle",  // Json property to use
        filter: "startswith",         // startswith, endswith, contains (lower case)
        suggest: true,                // Works with arrow keys only, includes Title: 
        template: '<dl>' +
                       '<dt>Title:</dt><dd>#=MovieTitle#</dd>' +
                       '<dt>Year:</dt><dd>#=ReleaseYear#</dd>' +
                  '</dl>',
        dataSource: {
            //type: "odata",          // Data protocol
            serverFiltering: true,  // Yes, handle filtering on the server
            serverPaging: true,     // Yes, handle paging on the server (only one page will be served)
            pageSize: 20,           // Show only 20 Movie Titles
            transport: {            // Read, without extra parameters
                read: {
                    url: "/Home/GetMovies/",
                    dataType: "json", // json is the default
                    type: "POST"
                }
            }
        }
    });

    // set width of the drop-down list
    $("#MovieTitle").data("kendoAutoComplete").list.width(400);
});