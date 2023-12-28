$(document).ready(function() {
    $("#btnSearch").click(function() {
        var movieName = $("#movieSearch").val()
        $.ajax({
            url: '/Movies/GetMovieByTitle?title=' + encodeURIComponent(movieName),
            type: 'GET',
            success: function(data) {
                var content = JSON.parse(data.content)
                if (content.results.length > 0) {
                    $("#idMovie").find("option").remove().end()
                    content.results.forEach(function(element){
                        $("#idMovie").append("<option value=\"" + element.id + "\">" + element.original_title + "</option>")
                    })
                } else {
                    alert("Could not find a movie with the name " + movieName);
                }
            },
            error: function(error) {
                alert("Error: " + error);
            }
        })
    })
})
