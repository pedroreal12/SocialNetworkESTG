$(document).ready(function() {
    var url = window.location.href;
    url = url.split("/");
    Id = url[5];

    imageUrl = "https://www.themoviedb.org/t/p/w220_and_h330_face"

    $.ajax({
        url: '/Movies/GetMovieId/' + Id,
        type: 'GET',
        success: function(data) {
            var movie = JSON.parse(data);
            if (true) {
            } else {
                alert("No movies were found. Try refreshing this page later");
            }
        },
        error: function(error) {
            alert("Error: " + error);
        }
    })
});
