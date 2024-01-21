$(document).ready(function(){
    imageUrl = "https://www.themoviedb.org/t/p/w220_and_h330_face"
    $.ajax({
        url: '/Discussion/GetLastDiscussions',
        type: 'GET',
        success: function(data) {
            if (data.discussions !== undefined) {
                var discussions = data.discussions
                if (discussions.length > 0) {
                    discussions.forEach(function(element){
                        $.ajax({
                            url: "/Movies/GetMovieId/" + element.movieId,
                            type: "GET",
                            success: function(data){
                                var content = JSON.parse(data.content)
                                if (content.adult != undefined){
                                    $(".displayDiscussions").append("<a href=\"Discussion/Details/" + element.id+ "\"><p>" + element.text + " - Posted At " + formatDate(element.datePosted) + " By <a href=\"/User/Details/" + element.idUser + "\">" + element.strUserName + "</a></p>")
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
