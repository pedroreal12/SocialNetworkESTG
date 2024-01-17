$(document).ready(function() {
    $.ajax({
        type: "GET",
        url: "UserList/GetUserLists",
        success: function(data){
            var content = JSON.parse(data)
            if (content.length > 0) {
                var html = "<tr>"
                content.forEach(function(element){
                    html += "<td><a href=\"/MovieList/Index\"/" + element.IdMovieList + ">" + element.StrName + "</td>"
                    html += "<td>" + element.DateCreated + "</td>"
                })
                html += "</tr>"
            } else {
                alert("No lists were created.")
            }
        },
        error: function(error){
            alert("Error: " + error)
        }
    })
})
