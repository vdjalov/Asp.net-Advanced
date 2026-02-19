function openMangeTicketsModal(cinemaId)
{
    fetch(`api/TicketApi/GetMoviesByCinema/${cinemaId}`)
        .then(response => response.json())
        .then(movies => {
            renderMoviesInModal(movies);
            $('#manageTicketsModal').modal('show');
        })
        .catch(error => {
            console.error('Error fetching movies:', error);
            alert('Failed to load movies. Please try again later.');
        });
       

}