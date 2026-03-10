$(document).ready(function () {
    // Show modal when clicking the "Buy Ticket" button
    $(".buy-ticket-btn").on("click", function () {
        const cinemaId = $(this).data("cinema-id");
        const movieId = $(this).data("movie-id");

        // Populate modal fields
        $("#cinemaId").val(cinemaId);
        $("#movieId").val(movieId);

        // Show modal
        $("#buyTicketModal").modal("show");
    });

    // Handle the "Buy Ticket" button click in the modal
    $("#buyTicketButton").on("click", function () {
        const requestData = {
            cinemaId: $("#cinemaId").val(),
            movieId: $("#movieId").val(),
            quantity: $("#quantity").val()
        };

        $.ajax({
            url: "https://localhost:7196/api/TicketApi/BuyTicket", // /api/TicketApi/BuyTicket
            method: "POST",
            contentType: "application/json",
            data: JSON.stringify(requestData),

            xhrFields: {
                withCredentials: true
            },

            success: function (response) {
                alert(response); // Show success message
                $("#buyTicketModal").modal("hide");
            },
            error: function (xhr) {
                const errorMessage = xhr.responseText || "An error occurred.";
                $("#errorMessage").text(errorMessage).removeClass("d-none");
            }
        });
    });
});