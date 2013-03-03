// Author: Brother Bill, brotherbill@mail.com, May freely make changes and copies.
$(document).ready(function () {
    $("#Name").kendoAutoComplete({
        minLength: 1,
        dataTextField: "Name",      // Json property to use
        suggest: true,              // Shows suggestion in textbox, when selecting with arrow keys or mouse cursor.
        dataSource: {
            serverFiltering: true,  // Yes, handle filtering on the server.  No, all entries will be sent once, cache locally.
            transport: {            // Read, without extra parameters
                read: {
                    url: "/Home/GetFruits/",
                    dataType: "json", // json is the default
                    type: "POST"
                }
            }
        }
    });
});