$(document).ready(function() {
    $("#createUserList").click(function() {
        event.preventDefault();
        data = {
            StrListName: $("#strListName").val()
        }
        $.ajax({
            url: "/UserList/Create",
            type: "POST",
            data: data,
            success: function(data){
                var content = JSON.parse(data);
                if (content.success){
                    alert("List created successfully!");
                    window.location = "/UserList/Index";
                } else {
                    alert("Error on creating the list");
                }
            },
            error: function(error) {
                alert("Error: " + error)
            }
        })
    })
})
