$(document).ready(function() {
    var url = window.location.href;
    url = url.split("/");
    Id = url[5];
    imageUrl = "https://www.themoviedb.org/t/p/w220_and_h330_face"

    $.ajax({
        type: "GET",
        url: "/Details/" + Id,
        success: function(data) {
            var content = JSON.parse(data)
            if (content.Id != undefined) {
                $("#displayList").append()
            }
        },
        error: function(error){
            alert("Error: " + error)
        }
    })
})
