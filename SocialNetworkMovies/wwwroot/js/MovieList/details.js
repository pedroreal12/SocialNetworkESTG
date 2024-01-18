$(document).ready(function() {
    var url = window.location.href;
    url = url.split("/");
    Id = url[5];
    imageUrl = "https://www.themoviedb.org/t/p/w220_and_h330_face"

    $.ajax({
        type: "GET",
        url: "/MovieList/GetMoviesByListId/" + Id,
        success: function(data) {
            var content = JSON.parse(data)
            if (content.length > 0) {
                content.forEach(function(element) {
                    console.log(element)
                })
                /*<dd class = "col-sm-10">
                </dd>*/
                $("#movieDisplayer").append()
            }
        },
        error: function(error) {
            alert("Error: " + error)
        }
    })
})
