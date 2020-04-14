// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
let connection = null;

setupConnection = () => {
    connection = new SignalR.HubConnectionBuilder()
        .withUrl("/likehub")
        .build();

    connection.on("ReceiveLikeUpdate", function (likes, beatId, userId) {
        var s = "#beat-" + beatId;
        if ($(s).hasClass("liked")) {
            $(s).removeClass("liked");
        } else {
            $(s).addClass("liked");
        }
        $("likesCount").text(likes);    
    });
    connection.on("Finished", function () {
        //connection.stop();
    });

    connection.start()
        .catch(err => console.error(err.toString()));
};

setupConnection();

$(".likeButton").bind("click", function (event) {
    var userId = $(this).attr("data-userid");
    var postId = $(this).attr("data-postid");

    $connection.invoke("SetLike", userId, postId).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
