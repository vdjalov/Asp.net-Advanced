using CinemaWebAppOriginal.Data.Models;
using CinemaWebAppOriginal.Infrastructure.Repositories.Contracts;
using CinemaWebAppOriginal.Services.Data;
using CinemaWebAppOriginal.Services.Data.Interfaces;
using Moq;

namespace CInemaWebAppOriginal.Services.Tests
{

    [TestFixture]
    public class Tests
    {
        private Mock<IRepository<Movie, int>> movieRepository;
        private Mock<IRepository<Cinema, int>> cinemaRepository;
        private Mock<IRepository<CinemaMovie, object>> cinemaMovieRepository;

        [SetUp]
        public void Setup()
        {
            this.movieRepository = new Mock<IRepository<Movie, int>>();
            this.cinemaMovieRepository = new Mock<IRepository<CinemaMovie, object>>();
            this.cinemaRepository = new Mock<IRepository<Cinema, int>>();  
        }

        [Test]
        public async Task GetAllMoviesNoFilterPositive()
        {
             this.movieRepository
                    .Setup(Setup => Setup.GetAllAttached())
                    .Returns(new List<Movie>
                    {
                        new Movie { Id = 1, Title = "Movie 1", Genre = "Action", ReleaseDate = DateTime.Now, Director="Mitch Ray", Duration = 125, Description = "alabalanica turska panica", ImageUrl = ""  },
                        new Movie { Id = 2, Title = "Movie 2", Genre = "Comedy", ReleaseDate = DateTime.Now, Director="Mitch Ray", Duration = 125, Description = "alabalanica turska panica", ImageUrl = ""  },
                        new Movie { Id = 3, Title = "Movie 3", Genre = "Action", ReleaseDate = DateTime.Now, Director="Mitch Ray", Duration = 125, Description = "alabalanica turska panica", ImageUrl = ""  }
                    }.AsQueryable());

            this.cinemaRepository
                    .Setup(Setup => Setup.GetAllAttached())
                    .Returns(new List<Cinema>
                    {
                        new Cinema { Id = 1, Name = "Cinema 1" },
                        new Cinema { Id = 2, Name = "Cinema 2" }
                    }.AsQueryable());

            this.cinemaMovieRepository
                    .Setup(Setup => Setup.GetAllAttached())
                    .Returns(new List<CinemaMovie>
                    {
                        new CinemaMovie { CinemaId = 1, MovieId = 1 },
                        new CinemaMovie { CinemaId = 1, MovieId = 2 },
                        new CinemaMovie { CinemaId = 2, MovieId = 3 }
                    }.AsQueryable());

            IMovieService movieService = new MovieService(this.movieRepository.Object,  this.cinemaRepository.Object, this.cinemaMovieRepository.Object);

            

            Assert.Equals(3, movieService.GetOnlyAllMoviesAsync().Result.Count());


        }
    }
}
