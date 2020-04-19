// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
    $("time").each(function (i, e) {
        const dateTimeValue = $(e).attr("datetime");
        if (!dateTimeValue) {
            return;
        }

        const time = moment.utc(dateTimeValue).local();
        $(e).html(time.format("llll"));
        $(e).attr("title", $(e).attr("datetime"));
    });
});



   var connection = new signalR.HubConnectionBuilder()
        .withUrl("/sorthub")
        .build();

    connection.on("ReceiveSortUpdate", (beats) => {
        $("#feed-list").empty();
        for (var i = 0; i < beats.length; i++) {
            const beat = beats[i];
            const htmlString = `
                    <li class="wow fadeInUp">
                        <a href="../Beats/Byname/${beat.beatId}">
                            <figure>
                                <div class="blog-grid-image">
                                    <img class="photo" src="${beat.imageUrl}" alt="${beat.name}" />
                                </div>
                                <figcaption>
                                    <h3 class="beatName">${beat.name}</h3>
                                    <p class="producerName">produced by ${beat.producer}<p>
                                    <p class="price">${beat.standartPrice}<sup>.00</sup></p>
                                    <p class="description">
                                        ${beat.description ? beat.description : ``}
                                    </p>
                                </figcaption>
                            </figure>
                        </a>
                    </li>`
            $("#feed-list").append(htmlString);
        }
    });

    connection.start()
        .catch(err => console.error(err.toString()));

$("#dropdownCollection li").click(function () {
    var text = $(this).text();
    connection.invoke("SortBeats", text);
})

//var listItems = document.querySelectorAll("ul li");

//listItems.forEach(function (item) {
//    item.onclick = function (e) {
//        var sortCondition = this.innerText.val();
//        connection.invoke("SortBeats", sortCondition)
//    }
//});