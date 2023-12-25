$(document).ready(function(){
    imageUrl = "https://www.themoviedb.org/t/p/w220_and_h330_face"
    $.ajax({
        url: '/Movies/GetMoviesPopular',
        type: 'GET',
        success: function(data) {
            var content = JSON.parse(JSON.parse(data).Content);
            if (content.results.length > 0) {
                results = content.results
                results.forEach(function(element) {
                    $(".displayMovies").append("<a href=\"/Movies/MovieDetails/" + element.id +"\"><img src=\"" + imageUrl + element.backdrop_path + "\"></img></a>")
                })
            } else {
                alert("No movies were found. Try refreshing this page later");
            }
        },
        error: function(error) {
            alert("Error: " + error);
        }
    })
});
