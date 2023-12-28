$(document).ready(function() {
    $("#btnSearch").click(function() {
        event.preventDefault()
        $.ajax({
            url: "/Movies/GetMovieByTitle/?title=" + $("#inputSearch").val(),
            type: "GET",
            success: function(data) {
                var content = JSON.parse(data.content)
                if (content.results.length > 0) {
                    var html = "<h5>Search Results</h5>"
                    content.results.forEach(function(element){
                        html += "<a href=\"/Movies/MovieDetails/" + element.id + "\">" + element.original_title + " - " + element.release_date + "</a><br>"
                    })
                    html += "<button class=\"btn btn-secondary\" onClick=\"clearSearches()\">Clear Searches</button>"
                    $(".searchResults").append(html)
                } else {
                    alert("No movies were found. Try later or with a different title")
                }
            },
            error: function(error) {
                alert("Error: " + error)
            }
        })
    })
})

function clearSearches(){
    $(".searchResults").children().remove()
    $("#inputSearch").val("")
}
