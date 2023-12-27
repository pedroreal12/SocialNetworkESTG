$(document).ready(function() {
    var url = window.location.href;
    url = url.split("/");
    Id = url[5];

    imageUrl = "https://www.themoviedb.org/t/p/w220_and_h330_face"

    $.ajax({
        url: '/Movies/GetMovieId/' + Id,
        type: 'GET',
        success: function(data) {
            var content = JSON.parse(JSON.parse(data).Content);
            if (content.adult != undefined) {
                console.log(content)
                $(".movieDetails").append("<a href=\"" + content.homepage + "\"><img src=\"" + imageUrl + content.backdrop_path + "\"></img></a>")
                $("#title").html("<a href=\"" + content.homepage + "\"<h4 id=\"title\">" + content.original_title + "</h4></a>")
                $(".movieDetails").append("<p>Overview " + content.overview + "</p>")
                $(".movieDetails").append("<p>Release Date " + content.release_date + "</p>")
                if (content.adult){
                    $(".movieDetails").append("<p>Rated R Movie</p>")
                } else {
                    $(".movieDetails").append("<p>Movie for the whole family</p>")
                }
                content.genres.forEach(function(element){
                    $(".movieDetails").append("<p>Genres " + element.name + "</p>")
                })
                if (content.belongs_to_collection != null) {
                    $(".movieDetails").append("<p>Collections Name " + content.belongs_to_collection.name + "</p>")
                }
                content.production_companies.forEach(function(element){
                    $(".movieDetails").append("<p>Production Companie Name " + element.name  + "</p>")
                })
                content.production_countries.forEach(function(element){
                    $(".movieDetails").append("<p>Production Country " + element.name  + "</p>")
                })
                $(".movieDetails").append("<p>Budget " + content.budget + " US$</p>")
                $(".movieDetails").append("<p>Revenue " + content.revenue + " US$</p>")
                $(".movieDetails").append("<p>Vote Average " + content.vote_average + "</p>")
                $(".movieDetails").append("<p>Vote Count " + content.vote_count + "</p>")
            } else {
                alert("No movies were found. Try refreshing this page later");
            }
        },
        error: function(error) {
            alert("Error: " + error);
        }
    })
});
