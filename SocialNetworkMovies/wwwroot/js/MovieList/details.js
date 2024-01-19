$(document).ready(function() {
    var url = window.location.href;
    url = url.split("/");
    Id = url[5];
    imageUrl = "https://www.themoviedb.org/t/p/w220_and_h330_face"

    $.ajax({
        type: "GET",
        url: "/MovieList/GetMoviesByListId/" + Id,
        success: function(data) {
            var html = "";
            if (data.movieList != undefined || data.data != undefined) {
                $("#strNameList").append(data.movieList[0].strListName)
                $("#dateCreated").append(formatDate(data.movieList[0].dateCreated))
                data.data.forEach(function(element) {
                element = JSON.parse(element)
                    html += "<dd class=\"col-sm-10\"><a href=\"" + element.homepage + "\">" + element.original_title + "</a></dd>"
                })
                $("#movieDisplayer").append(html)
            }
        },
        error: function(error) {
            alert("Error: " + error)
        }
    })
})

function formatDate(dateObj) {
    var formattedDate = new Date(dateObj);
    var d = formattedDate.getDate();
    var m = formattedDate.getMonth();
    m += 1;  // JavaScript months are 0-11
    var y = formattedDate.getFullYear();
    var formatedDate = d + "/" + m + "/" + y

    return formatedDate
}
