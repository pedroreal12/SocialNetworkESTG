$(document).ready(function() {
    var url = window.location.href;
    url = url.split("/");
    Id = url[5];

    $("#editUserList").click(function() {
        event.preventDefault();
        if ($("#strListName") != "") {
            data = {
                StrListName: $("#strListName").val()
            }
            $.ajax({
                url: "/UserList/Edit/" + Id,
                type: "POST",
                data: data,
                success: function(data) {
                    var content = JSON.parse(data);
                    if (content.success) {
                        alert("List edited successfully!");
                        window.location = "/UserList/Index";
                    } else {
                        alert("Error on editing the list");
                    }
                },
                error: function(error) {
                    alert("Error: " + error)
                }
            })
        } else {
            alert("Please, provide a name for your list");
        }
    })
})
