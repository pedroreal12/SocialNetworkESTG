$(document).ready(function(){
    imageUrl = "https://www.themoviedb.org/t/p/w220_and_h330_face"
    $.ajax({
        url: '/Discussion/GetLastDiscussions',
        type: 'GET',
        success: function(data) {
            var content = JSON.parse(data)
            if (content.length > 0) {
                content.forEach(function(element){
                    $.ajax({
                        url: "/Movies/GetMovieId/" + element.MovieId,
                        type: "GET",
                        success: function(data){
                            var content = JSON.parse(data.content)
                            if (content.adult != undefined){
                                $(".displayDiscussions").append("<a href=\"Discussion/Details/" + element.Id+ "\"><p>" + element.Text + " - Posted At " + formatDate(element.DatePosted) + " By " + element.StrUserName + " </p></a>")
                                $(".displayDiscussions").append("<img src=\"" + imageUrl + content.backdrop_path + "\"></img>")
                            }
                        },
                        error: function(error){
                            alert("Error: " + error)
                        }
                    })
                })
            } else {
                alert("No discussions were found. Try refreshing this page later");
            }
        },
        error: function(error) {
            alert("Error: " + error);
        }
    })
})

function formatDate(dateObj){
    var formattedDate = new Date(dateObj);
    var d = formattedDate.getDate();
    var m =  formattedDate.getMonth();
    m += 1;  // JavaScript months are 0-11
    var y = formattedDate.getFullYear();
    var formatedDate = d + "/" + m  + "/" + y

    return formatedDate
}
