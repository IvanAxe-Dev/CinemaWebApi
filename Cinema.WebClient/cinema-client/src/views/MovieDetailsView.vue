<script setup>
import MovieInfo from '../components/MovieInfo.vue';

const movie = {
  id: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  title: "The Dark Knight",
  rentalStartDate: "2024-05-15T07:09:52.280Z",
  rentalEndDate: "2024-05-25T07:09:52.280Z",
  description: "string",
  imageUrl: "https://www.movieposters.com/cdn/shop/files/darkknight.building.24x36_20e90057-f673-4cc3-9ce7-7b0d3eeb7d83_480x.progressive.jpg?v=1707491191",
  releaseDate: "2024-05-15T07:09:52.280Z",
  director: "Christopher Nolan",
  duration: "string",
  ageRestriction: 12,
  trailerUrl: "string",
  actors: "string",
  ratings: [
    6,
    7,
    10,
    9,
    8
  ],
  sessions: [
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "date": "string",
      "startTime": "10:00",
      "price": 100,
      "availableSeats": 1,
      "movieId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "cinemaHallId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
    },
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "date": "string",
      "startTime": "12:00",
      "price": 140,
      "availableSeats": 1,
      "movieId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "cinemaHallId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
    },
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "date": "string",
      "startTime": "14:30",
      "price": 160,
      "availableSeats": 1,
      "movieId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "cinemaHallId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
    },
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "date": "string",
      "startTime": "16:45",
      "price": 100,
      "availableSeats": 1,
      "movieId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "cinemaHallId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
    },
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "date": "string",
      "startTime": "19:00",
      "price": 80,
      "availableSeats": 1,
      "movieId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "cinemaHallId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
    }
  ],
  categories: [
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "name": "action"
    },
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "name": "drama"
    },
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "name": "fiction"
    },
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "name": "action"
    },
  ]
}

</script>

<template>
  <div class="movie-details">
    <div class="poster-container">
      <img :src="movie.imageUrl">
      <button>Watch trailer</button>
    </div>
    <movie-info :movie="movie"></movie-info>
  </div>
</template>

<style scoped>
.movie-details {
  display: flex;
  align-items: flex-start;
  max-width: 1600px;
  padding: 20px;
  margin: 0 auto;
}

.poster-container {
  position: fixed;
  left: 40px;
  width: 300px;
  height: 600px;
  border-radius: 15px;

  img {
    width: 100%;
    height: auto;
    border-radius: 15px;
  }

  button {
    padding: 10px 20px;
    border-width: 2px;
    border-radius: 15px;
    color: white;
    background-color: #20232a;
  }
}

.movie-info {
  flex: 1;
  margin-left: 360px;
  padding: 20px;
}

@media (max-width: 768px) {
  .movie-details {
    flex-direction: column;
  }
  .poster-container {
    position: static;
    margin-bottom: 20px;
    width: 100%;

    img {
      height: 100%;
      width: auto;
    }
  }
  .movie-info {
    margin-left: 0;
  }
}
</style>