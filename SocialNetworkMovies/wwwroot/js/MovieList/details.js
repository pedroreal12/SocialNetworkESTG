$(document).ready(function() {
    var url = window.location.href;
    url = url.split("/");
    Id = url[5];
    imageUrl = "https://www.themoviedb.org/t/p/w220_and_h330_face"

    $.ajax({
        type: "GET",
        url: "/MovieList/GetMoviesByListId/?IdUserList=" + Id,
        success: function(data) {
            var html = "";
            if ((data.movieList !== undefined && data.data !== undefined) && (data.movieList.length > 0 || data.data.length > 0)) {
                $("#strNameList").append(data.movieList[0].strListName)
                $("#dateCreated").append(formatDate(data.movieList[0].dateCreated))
                data.data.forEach(function(element, key) {
                    element = JSON.parse(element)
                    html += "<dd class=\"col-sm-10\"><a href=\"" + (element.homepage != "" ? element.homepage : "#") + "\">" + element.original_title + "</a><button onclick=\"removeFromList(this.id, " + element.id + ")\" id=\"removeMovie_" + data.movieList[key].idMovieList + "\" class=\"btn btn-danger\">Remove from list</button></dd>"
                })
                $("#movieDisplayer").append(html)
            } else {
                var content = JSON.parse(data);
                alert(content.message)
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

function removeFromList(idMovieList, idMovie) {
    idMovieList = idMovieList.split("_")[1]
    $.ajax({
        url: "/MovieList/RemoveMovieFromList/?IdMovieList=" + idMovieList + "&IdMovie=" + idMovie,
        type: "GET",
        success: function(data) {
            var content = JSON.parse(data);
            if (content.success) {
                alert(content.message)
                location.reload()
            } else {
                alert(content.message)
                location.reload()
            }
        },
        error: function(error) {
            alert("Error: " + error)
        }
    })
}
